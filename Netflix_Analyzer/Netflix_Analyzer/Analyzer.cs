using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Netflix_Analyzer
{
    public partial class Analyzer : Form
    {
        private BindingList<object> cmbList = new BindingList<object>();

        public Analyzer()
        {
            InitializeComponent();
            addListComboBox();
            chart1.Series.Clear();

            button1.Click += (e,a)=> { createChart(); };
        }

        private void addListComboBox()
        {
            addCmbList();

            comboBox1.Items.Clear();
            comboBox1.DataSource = cmbList;
            comboBox1.DisplayMember = "Display";
            comboBox1.ValueMember = "Value";

        }

        private void addCmbList()
        {
            cmbList.Clear();
            cmbList.Add(new { Display = "국가별 구독등급 수 TOP 10", Value = @"
WITH subscription_counts AS(
        SELECT
            country,
            subscription_type,
            COUNT(*) AS count
        FROM usersView
        GROUP BY country, subscription_type
    )
    SELECT *
    FROM subscription_counts
    WHERE country IN(
        SELECT TOP 10 country
        FROM subscription_counts
        GROUP BY country
        ORDER BY SUM(count) DESC
    )
    ORDER BY count DESC
    " });
            }

        private void createChart()
        {
            string query = (comboBox1.SelectedItem as dynamic).Value;
            Console.WriteLine(query);
            DataManager.LoadAnalyzerDT(query);
            
            chart1.Series.Clear();

            foreach (DataRow row in DBHelper.dt.Rows)
            {
                string country = row["country"].ToString();
                Console.WriteLine(country);
                string subscription_type = row["subscription_type"].ToString();
                Console.WriteLine(subscription_type);
                int count = Convert.ToInt32(row["count"]);
                Console.WriteLine(count);

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
