using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            for (int i = 0; i < DataManager.Tables.Count; i++)
            {
                cmbList.Add(new { Display = "나라별 구독등급별 가입자 수 (TOP 10 Country)", Value = @"
WITH subscription_counts AS (
        SELECT
            c.name AS country,
            s.name AS subscription_type,
            COUNT(*) AS count
        FROM users u
        JOIN countries c ON u.country = c.id
        JOIN subscription_types s ON u.subscription_type = s.id
        WHERE c.name NOT IN ('China', 'Russia')
        GROUP BY c.name, s.name
    )
    SELECT *
    FROM subscription_counts
    WHERE country IN (
        SELECT TOP 10 country
        FROM subscription_counts
        GROUP BY country
        ORDER BY SUM(count) DESC
    )
    ORDER BY count DESC
    " });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = (comboBox1.SelectedItem as dynamic).Value;
            DataManager.LoadAnalyzerDT(query);

            chart1.Series.Clear();
            foreach (DataRow row in DBHelper.dt.Rows)
            {
                string country = row["country"].ToString();
                string subscription_type = row["subscription_type"].ToString();
                int count = Convert.ToInt32(row["count"]);

                // create a new series for each subscription type
                var series = chart1.Series.FirstOrDefault(s => s.Name == subscription_type);

                if (series == null)
                {
                    series = new Series(subscription_type);
                    series.ChartType = SeriesChartType.Column;
                    chart1.Series.Add(series);
                }

                series.Points.AddXY(country, count);
            }
        }
    }
}
