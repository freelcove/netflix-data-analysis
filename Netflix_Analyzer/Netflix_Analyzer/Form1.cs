using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Netflix_Analyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridViewSelect("Devices",ref dataGridView1, ref groupBox1);


        }
        public void dataGridViewSelect(string table, ref DataGridView dataGridView, ref GroupBox groupBox)
        {
            dataGridView.DataSource = "null";
            groupBox.Text = table;
            switch (table)
            {
                case "Countries":
                    dataGridView.DataSource = DataManager.Countries;
                    break;
                case "Devices":
                    dataGridView.DataSource = DataManager.Devices;
                    break;
                case "Genders":
                    dataGridView.DataSource = DataManager.Genders;
                    break;
                case "Genres":
                    dataGridView.DataSource = DataManager.Genres;
                    break;
                case "Subscriptions":
                    dataGridView.DataSource = DataManager.Subscription_Types;
                    break;
                case "Users":
                    dataGridView.DataSource = DataManager.Users;
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string table = comboBox1.Text;
            dataGridViewSelect(table, ref dataGridView1, ref groupBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
