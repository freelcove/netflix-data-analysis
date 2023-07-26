using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Netflix_Analyzer
{
    public partial class Form1 : Form
    {
        static string tempTable = "Devices";
        static int id = -1;
        static int count = 0;
        static int pageNum = 1;
        static List<int> ids = new List<int>() { -1 };

        private BindingList<object> cmbList = new BindingList<object>();

        public Form1()
        {
            InitializeComponent();
            dataGridViewSelectDT(tempTable, ref dataGridView1, ref groupBox1);
            comboBox1.Items.Clear();
            comboBoxAddItems(ref comboBox1);
            comboBox1.Text = tempTable;

            insertToolStripMenuItem.Click += (e,a) =>
            {
                Data_Edit data_Edit = new Data_Edit("insert");
                data_Edit.ShowDialog();
            };
            updateToolStripMenuItem.Click += (e, a) =>
            {
                Data_Edit data_Edit = new Data_Edit("update");
                data_Edit.ShowDialog();
            };
            deleteToolStripMenuItem.Click += (e, a) =>
            {
                Data_Edit data_Edit = new Data_Edit("delete");
                data_Edit.ShowDialog();
            };

        }

        //ComboBox 데이터 입력
        public void comboBoxAddItems(ref ComboBox comboBox)
        {
            comboBox.Items.Clear();
            cmbList.Clear();
            for (int i = 0; i < DataManager.Tables.Count; i++)
            {
                cmbList.Add(new { Display = DataManager.Tables[i], Value = DataManager.ColumnCount[i] });
            }
            comboBox.DataSource = cmbList;
            comboBox.DisplayMember = "Display";
            comboBox.ValueMember = "Value";
        }

        //DataGridView 출력(From DT)
        public void dataGridViewSelectDT(string table, ref DataGridView dataGridView, ref GroupBox groupBox)
        {
            dataGridView.DataSource = "null";
            groupBox.Text = table;
            switch (table)
            {
                case "Countries":
                    dataGridView.DataSource = DataManager.CountriesDT;
                    break;
                case "Devices":
                    dataGridView.DataSource = DataManager.DevicesDT;
                    break;
                case "Genders":
                    dataGridView.DataSource = DataManager.GendersDT;
                    break;
                case "Genres":
                    dataGridView.DataSource = DataManager.GenresDT;
                    break;
                case "Subscription_Types":
                    dataGridView.DataSource = DataManager.Subscription_TypesDT;
                    break;
                case "Users":
                    dataGridView.DataSource = DataManager.UsersDT;
                    break;
            }
        }

        //DataGridView 출력(From List)
        public void dataGridViewSelect(string table, ref DataGridView dataGridView, ref GroupBox groupBox)
        {
            dataGridView.DataSource = "null";
            groupBox.Text = table;
            switch (table)
            {
                case "Countries":
                    dataGridView.DataSource = DataManager.Countries;
                    count = DataManager.Countries.Count;
                    if (count >= 100)
                    {
                        id = DataManager.Countries[count - 1].id;
                    }
                    else
                    {
                        count = 0;
                        id = -1;
                    }
                    break;
                case "Devices":
                    dataGridView.DataSource = DataManager.Devices;
                    count = DataManager.Devices.Count;
                    if (count >= 100)
                    {
                        id = DataManager.Devices[count - 1].id;
                    }
                    else
                    {
                        count = 0;
                        id = -1;
                    }
                    break;
                case "Genders":
                    dataGridView.DataSource = DataManager.Genders;
                    count = DataManager.Genders.Count;
                    if (count >= 100)
                    {
                        id = DataManager.Genders[count - 1].id;
                    }
                    else
                    {
                        count = 0;
                        id = -1;
                    }
                    break;
                case "Genres":
                    dataGridView.DataSource = DataManager.Genres;
                    count = DataManager.Genres.Count;
                    if (count >= 100)
                    {
                        id = DataManager.Genres[count - 1].id;
                    }
                    else
                    {
                        count = 0;
                        id = -1;
                    }
                    break;
                case "Subscription_Types":
                    dataGridView.DataSource = DataManager.Subscription_Types;
                    count = DataManager.Subscription_Types.Count;
                    if (count >= 100)
                    {
                        id = DataManager.Subscription_Types[count - 1].id;
                    }
                    else
                    {
                        count = 0;
                        id = -1;
                    }
                    break;
                case "Users":
                    dataGridView.DataSource = DataManager.Users;
                    count = DataManager.Users.Count;
                    if (count >= 100)
                    {
                        id = DataManager.Users[count - 1].id;
                    }
                    else
                    {
                        count = 0;
                        id = -1;
                    }
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string table = (comboBox1.SelectedItem as dynamic).Display;
            if (tempTable.Equals(table))
            {

            }
            else
            {
                id = -1;
                pageNum = 1;
                count = 0;
                ids.Clear();
                ids.Add(-1);
                tempTable = table;
                DataManager.LoadDT(tempTable);
            }
            dataGridViewSelectDT(table, ref dataGridView1, ref groupBox1);

            groupBox2.Controls.Clear();
            createButton();
        }

        private void createButton()
        {
            if (pageNum > 1)
            {
                Button button = new Button();
                button.Name = "back";
                button.Text = "이전 페이지";
                button.Click += (s, a) =>
                {
                    DataManager.Load(tempTable, ids[--pageNum - 1], ">");
                    ids.RemoveAt(pageNum);
                    dataGridViewSelectDT(tempTable, ref dataGridView1, ref groupBox1);
                    button1_Click(s, a);

                };
                button.Location = new Point(6, 20);
                button.Size = new Size(95, 23);
                groupBox2.Controls.Add(button);
            }
            if (count >= 100)
            {
                Button button1 = new Button();
                button1.Name = "forward";
                button1.Text = "다음 페이지";
                button1.Click += (s, a) =>
                {
                    ids.Add(id);
                    pageNum++;
                    DataManager.Load(tempTable, id, ">");
                    dataGridViewSelectDT(tempTable, ref dataGridView1, ref groupBox1);
                    button1_Click(s, a);
                };
                button1.Location = new Point(147, 20);
                button1.Size = new Size(95, 23);
                groupBox2.Controls.Add(button1);
            }
        }

        private void analyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analyzer analyzer = new Analyzer();
            analyzer.ShowDialog();
        }
    }
}
