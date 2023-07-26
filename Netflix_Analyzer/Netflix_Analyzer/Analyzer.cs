using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Netflix_Analyzer
{
    public partial class Analyzer : Form
    {
        private BindingList<object> cmbList = new BindingList<object>();

        public Analyzer()
        {
            InitializeComponent();
            comboBoxAddItems();
            chart1.Series.Clear();

        }

        private void comboBoxAddItems()
        {
            comboBox1.Items.Clear();

            addCmbList();

            comboBox1.DataSource = cmbList;
            comboBox1.DisplayMember = "Display";
            comboBox1.ValueMember = "Value";
        }

        private void addCmbList()
        {
            cmbList.Clear();

            //x축 => List, Series => SubList, y축 => Value로 
            cmbList.Add(new { Display = "나라별 구독등급별 가입자 수 (TOP 10 Country)", Value = @"
WITH subscription_counts AS (
        SELECT
            country as List,
            subscription_type as SubList,
            COUNT(*) AS Value
        FROM usersView
        GROUP BY country, subscription_type
    )
    SELECT *
    FROM subscription_counts
    WHERE List IN (
        SELECT TOP 10 List
        FROM subscription_counts
        GROUP BY List
        ORDER BY SUM(Value) DESC
    )
    ORDER BY Value DESC
    " });

            cmbList.Add(new { Display = "성별 및 선호 장르별 사용자 수", Value = @"
SELECT
    g.name as List,
    gen.name as SubList,
    COUNT(*) AS Value
FROM users u
JOIN genders g ON u.gender = g.id
JOIN genres gen ON u.preferred_genre = gen.id
GROUP BY g.name, gen.name
ORDER BY Value DESC
" });
            cmbList.Add(new { Display = "장치별 사용자 수", Value = @"
SELECT
    d.name as List,
    COUNT(*) AS Value
FROM users u
JOIN devices d ON u.device = d.id
GROUP BY d.name
ORDER BY Value DESC
" });
            cmbList.Add(new { Display = "구독 유형별 사용자 수", Value = @"
SELECT
    s.name as List,
    COUNT(*) AS Value
FROM users u
JOIN subscription_types s ON u.subscription_type = s.id
GROUP BY s.name
ORDER BY Value DESC
" });
            cmbList.Add(new { Display = "장르별 사용자 수", Value = @"
SELECT
    g.name as List,
    COUNT(*) AS Value
FROM users u
JOIN genres g ON u.preferred_genre = g.id
GROUP BY g.name
ORDER BY Value DESC
" });
        
            cmbList.Add(new { Display = "장치별 선호 장르", Value = @"
SELECT
    d.name as List,
    g.name as SubList,
    COUNT(*) AS Value
FROM users u
JOIN devices d ON u.device = d.id
JOIN genres g ON u.preferred_genre = g.id
GROUP BY d.name, g.name
ORDER BY Value DESC
" });


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = (comboBox1.SelectedItem as dynamic).Value;
            DataManager.LoadAnalyzerDT(query);

            chart1.Series.Clear();
            foreach (DataRow row in DBHelper.dt.Rows)
            {
                string List = row["List"].ToString();
                int Value = Convert.ToInt32(row["Value"]);
                string SubList;

                // Check if SubList column exists in the row
                if (row.Table.Columns.Contains("SubList"))
                {
                    SubList = row["SubList"].ToString();
                }
                else
                {
                    SubList = "Value";
                }

                // create a new series for each subscription type
                var series = chart1.Series.FirstOrDefault(s => s.Name == SubList);

                if (series == null)
                {
                    series = new Series(SubList);
                    series.ChartType = SeriesChartType.Column;
                    chart1.Series.Add(series);
                }

                series.Points.AddXY(List, Value);
            }
        }

    }
}
