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
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
            dataGridView1.DataSource = DataManager.Genres;
            dataGridView2.DataSource = DataManager.Devices;
            dataGridView3.DataSource = DataManager.Genders;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
