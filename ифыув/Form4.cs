using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ифыув
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt64(textBox5.Text);
                try
                {
                    OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=db.accdb");
                    OleDbDataAdapter adapter4 = new OleDbDataAdapter("INSERT INTO Сотрудники (ФИО, [Номер телефона], Должность) " +
                        "VALUES " +
                        $"('{textBox4.Text}','{textBox5.Text}','{textBox6.Text}')", connection);
                    DataTable dt = new DataTable();
                    adapter4.Fill(dt);
                    connection.Close();
                    Close();
                }
                catch { MessageBox.Show("Введите все значения"); }
            }
            catch { MessageBox.Show("Неправильно набран номер"); }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            OleDbConnection connection1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=db.accdb");
            OleDbDataAdapter adapter1 = new OleDbDataAdapter("Select ID_Должности, Должность from Должности", connection1);
            DataTable dt1 = new DataTable();
            adapter1.Fill(dt1);
            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "Должность";
            comboBox1.ValueMember = "ID_Должности";
            connection1.Close();
            textBox6.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox6.Text = comboBox1.SelectedValue.ToString();
        }
    }
}
