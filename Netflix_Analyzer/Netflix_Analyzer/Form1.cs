using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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


        public Form1()
        {
            InitializeComponent();
            dataGridViewSelect(tempTable, ref dataGridView1, ref groupBox1);
            comboBox1.Text = tempTable;


        }
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
            string table = comboBox1.Text;
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
                DataManager.Load(tempTable);
            }
            dataGridViewSelect(table, ref dataGridView1, ref groupBox1);

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
                    DataManager.Load(tempTable, ids[--pageNum-1], ">");
                    ids.RemoveAt(pageNum);
                    dataGridViewSelect(tempTable, ref dataGridView1, ref groupBox1);
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
                    dataGridViewSelect(tempTable, ref dataGridView1, ref groupBox1);
                    button1_Click(s, a);
                };
                button1.Location = new Point(147, 20);
                button1.Size = new Size(95, 23);
                groupBox2.Controls.Add(button1);
            }
        }





    }
}
