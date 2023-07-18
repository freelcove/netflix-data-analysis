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
    public partial class Data_Edit : Form
    {
        static string motion;
        private static List<TextBox> txtList = new List<TextBox>();
        private static List<DateTimePicker> dateList = new List<DateTimePicker>();
        private static List<ComboBox> cmbList = new List<ComboBox>();
        private static List<Button> btnList = new List<Button>();
        public Data_Edit(string control)
        {
            InitializeComponent();
            List<string> tables = DataManager.Tables;
            motion = control;
            this.Text = "Data Edit (" + control + ")";
            /*
            foreach (string table in tables)
            {
                comboBox1.Items.Add(table);
            }
            comboBox1.SelectedItem = tables[1];
            */
            Form1 form = new Form1();
            comboBox1.Items.Clear();
            form.comboBoxAddItems(ref comboBox1);
            comboBox1.Text = "Devices";
            form.dataGridViewSelectDT(comboBox1.Text, ref dataGridView1, ref groupBox2);
            dataGridView1.CellClick += (e,a)=> { dataGridView1_CellContentClick(e,a); };
            string table = (comboBox1.SelectedItem as dynamic).Display;
            int columnCount = (comboBox1.SelectedItem as dynamic).Value;
            addTextBox(table, columnCount);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string table = (comboBox1.SelectedItem as dynamic).Display;
            int columnCount = (comboBox1.SelectedItem as dynamic).Value;
            Form1 form = new Form1();
            form.dataGridViewSelectDT(table, ref dataGridView1,ref groupBox2);
            addTextBox(table, columnCount);
        }

        private void userCombo(ref ComboBox comboBox, string column)
        {
            comboBox.Items.Clear();
            comboBox.DisplayMember = "Display";
            comboBox.ValueMember = "Value";

            switch (column)
            {
                case "subscription_type":
                    foreach (var item in DataManager.Subscription_Types)
                    {
                        comboBox.Items.Add(new { Display = item.name, Value = item.id });
                    }
                    break;
                case "country":
                    foreach (var item in DataManager.Countries)
                    {
                        comboBox.Items.Add(new { Display = item.name, Value = item.id });
                    }
                    break;
                case "gender":
                    foreach (var item in DataManager.Genders)
                    {
                        comboBox.Items.Add(new { Display = item.name, Value = item.id });
                    }
                    break;
                case "device":
                    foreach (var item in DataManager.Devices)
                    {
                        comboBox.Items.Add(new { Display = item.name, Value = item.id });
                    }
                    break;
                case "preferred_genre":
                    foreach (var item in DataManager.Genres)
                    {
                        comboBox.Items.Add(new { Display = item.name, Value = item.id });
                    }
                    break;
                default:
                    Console.WriteLine(column);
                    break;
            }

        }

        private void addUserTextBox(string table)
        {
            groupBox1.Controls.Clear();
            List<string> columns = columnName(table);
            int row = 0;
            int col = 0;
            foreach (string item in columns)
            {
                Point labellocation = new Point(7 + 250 * col, 21 + 30 * row);
                Point textboxlocation = new Point(140 + 250 * col, 18 + 30 * row);
                Label label = new Label();
                label.Name = "lbl" + item;
                label.Text = item;
                label.Location = labellocation;
                label.AutoSize = true;
                groupBox1.Controls.Add(label);
                if (item.Equals("id") && motion.Equals("insert"))
                {
                    label.Text += " (아이디 자동 삽입)";
                }
                if (item.Equals("id") || item.Equals("average_watch_time"))
                {
                    
                    TextBox textBox = new TextBox();
                    if (motion.Equals("insert"))
                    {
                        textBox.Enabled = false;
                    }
                    textBox.Name = "tb" + item;
                    textBox.Location = textboxlocation;
                    textBox.Size = new Size(110, 21);
                    groupBox1.Controls.Add(textBox);
                    txtList.Add(textBox);
                }
                else if (item.Equals("join_date") || item.Equals("last_payment_date") || item.Equals("birth_date"))
                {
                    DateTimePicker dateTimePicker = new DateTimePicker();
                    dateTimePicker.Name = "dtp" + item;
                    dateTimePicker.Location = textboxlocation;
                    dateTimePicker.Size = new Size(110, 21);
                    groupBox1.Controls.Add(dateTimePicker);
                    dateList.Add(dateTimePicker);
                }
                else
                {
                    ComboBox comboBox = new ComboBox();
                    userCombo(ref comboBox, item); ;
                    comboBox.Size = new Size(110, 21);
                    comboBox.Location = textboxlocation;
                    groupBox1.Controls.Add(comboBox);
                    cmbList.Add(comboBox);
                }
                row++;
                if (row >= 4)
                {
                    row = 0;
                    col++;
                }
            }
        }

        private void addTextBox(string table, int columnCount)
        {
            groupBox1.Controls.Clear();
            List<string> columns = columnName(table);
            int row = 0;
            int col = 0;
            txtList.Clear();
            cmbList.Clear();
            dateList.Clear();
            btnList.Clear();
            if (table.Equals("Users"))
            {
                addUserTextBox(table);
            }
            else
            {
                foreach (string item in columns)
                {
                    Point labellocation = new Point(7 + 250 * col, 21 + 30 * row);
                    Point textboxlocation = new Point(140 + 240 * col, 18 + 30 * row);
                    Label label = new Label();
                    label.Name = "lbl" + item;
                    label.Text = item;
                    if (motion.Equals("update")&&item.Equals("id"))
                    {
                        label.Text += " (아이디 수정불가)";
                    }
                    label.Location = labellocation;
                    label.AutoSize = true;

                    TextBox textBox = new TextBox();
                    textBox.Name = "tb" + item;
                    textBox.Location = textboxlocation;
                    if (motion.Equals("update")&&item.Equals("id"))
                    {
                        textBox.Enabled = false;
                    }
                    textBox.Size = new Size(110, 21);
                    row++;
                    if (row >= 4)
                    {
                        row = 0;
                        col++;
                    }
                    groupBox1.Controls.Add(label);
                    groupBox1.Controls.Add(textBox);
                    txtList.Add(textBox);
                }
            }
            Button button = new Button();
            string butt = "";
            if (motion.Equals("insert"))
            {
                butt = "추가";
            }
            else if (motion.Equals("update"))
            {
                butt = "수정";
            }
            else
            {
                butt = "삭제";
            }
            int buttPos = columnCount / 4;
            int x = 0;
            int y = 0;
            if (buttPos != 2)
            {
                col++;
                row = 0;
                x = 7 + 250 * col;
                y = 18 + 30 * row;
            }
            else
            {
                x = 681;
                y = 113;
            }
            button.Text = butt;
            button.Location = new Point(x,y);
            button.Size = new Size(88, 23);
            button.Click += (e,a)=> { buttonAction(table); };
            groupBox1.Controls.Add(button);
            btnList.Add(button);

        }

        private List<string> columnName(string table)
        {
            switch (table)
            {
                case "Countries":
                    return Country.columns;
                case "Devices":
                    return Device.columns;
                case "Genders":
                    return Gender.columns;
                case "Genres":
                    return Genre.columns;
                case "Subscription_Types":
                    return Subscription_Type.columns;
                case "Users":
                    return User.columns;
                default:
                    Console.WriteLine(table);
                    return null;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string table = (comboBox1.SelectedItem as dynamic).Display;
            DataTable dt = new DataTable();
            switch (table)
            {
                case "Countries":
                    DataRow country = (dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row;
                    txtList[0].Text = country["id"].ToString();
                    txtList[1].Text = country["name"].ToString();
                    txtList[2].Text = country["region"].ToString();
                    txtList[3].Text = country["population"].ToString();
                    txtList[4].Text = country["gdp"].ToString();
                    txtList[5].Text = country["gdp_per_capita"].ToString();
                    break;
                case "Devices":
                    DataRow device = (dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row;
                    txtList[0].Text = device["id"].ToString();
                    txtList[1].Text = device["name"].ToString();
                    break;
                case "Genders":
                    DataRow gender = (dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row;
                    txtList[0].Text = gender["id"].ToString();
                    txtList[1].Text = gender["name"].ToString();
                    break;
                case "Genres":
                    DataRow genre = (dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row;
                    txtList[0].Text = genre["id"].ToString();
                    txtList[1].Text = genre["name"].ToString();
                    break;
                case "Subscription_Types":
                    DataRow subscription = (dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row;
                    txtList[0].Text = subscription["id"].ToString();
                    txtList[1].Text = subscription["name"].ToString();
                    break;
                case "Users":
                    DataRow user = (dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row;
                    txtList[0].Text = user["id"].ToString();
                    txtList[1].Text = user["average_watch_time"].ToString();
                    cmbList[0].Text = DataManager.Subscription_TypesDIC[int.Parse(user["subscription_type"].ToString())].name;
                    cmbList[1].Text = DataManager.CountriesDIC[int.Parse(user["country"].ToString())].name;
                    cmbList[2].Text = DataManager.GendersDIC[int.Parse(user["gender"].ToString())].name;
                    cmbList[3].Text = DataManager.DevicesDIC[int.Parse(user["device"].ToString())].name;
                    cmbList[4].Text = DataManager.GenresDIC[int.Parse(user["preferred_genre"].ToString())].name;
                    dateList[0].Value = DateTime.Parse(user["join_date"].ToString());
                    dateList[1].Value = DateTime.Parse(user["last_payment_date"].ToString());
                    dateList[2].Value = DateTime.Parse(user["birth_date"].ToString());
                    break;
                default:
                    Console.WriteLine(table);
                    break;
            }
        }

        private void buttonAction(string table)
        {
            Form1 form = new Form1();
            Dictionary<string, object> data = new Dictionary<string, object>();
            string contents = "";
            int.TryParse(txtList[0].Text, out int id);
            if (table.Equals("Countries"))
            {
                data.Add("id", txtList[0].Text);
                data.Add("name", txtList[1].Text);
                data.Add("region", txtList[2].Text);
                data.Add("population", txtList[3].Text);
                data.Add("gdp", txtList[4].Text);
                data.Add("gdp_per_capita", txtList[5].Text);
            }
            else if (table.Equals("Users"))
            {
                data.Add("id", txtList[0].Text);
                data.Add("average_watch_time", txtList[1].Text);
                data.Add("subscription_type", (cmbList[0].SelectedItem as dynamic).Value);
                data.Add("country", (cmbList[1].SelectedItem as dynamic).Value);
                data.Add("gender", (cmbList[2].SelectedItem as dynamic).Value);
                data.Add("device", (cmbList[3].SelectedItem as dynamic).Value);
                data.Add("preferred_genre", cmbList[4].SelectedValue);
                data.Add("join_date", dateList[0].Value);
                data.Add("last_payment_date", dateList[1].Value);
                data.Add("birth_date", dateList[2].Value);
            }
            else
            {
                data.Add ("id", txtList[0].Text);
                data.Add ("name", txtList[1].Text);
            }
            if (motion.Equals("insert"))
            {
                DataManager.insertDB(table, data, out contents);
            }
            else if (motion.Equals("update"))
            {
                DataManager.updateDB(table, data, out contents);
            }
            else if (motion.Equals("delete"))
            {
                DataManager.deleteDB(table, id, out contents);
            }
            else
            {
                DataManager.printLog("Data_Edit.Motion = "+ motion + " Error");
            }
            MessageBox.Show(contents);
            DataManager.LoadDT(table);
            form.dataGridViewSelectDT(table, ref dataGridView1, ref groupBox2);

        }
    }
}
