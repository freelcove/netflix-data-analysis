using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace chart_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // populate the chart when the form loads
            this.Load += (s, e) => PopulateChart();

        }

        private void PopulateChart()
        {
            DBHelper.ConnectDB();

            // get the subscription type popularity by country
            DBHelper.selectQueryWithGdpPerCapita();

            // clear the chart
            chart1.Series.Clear();

            // populate the series with data
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

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

