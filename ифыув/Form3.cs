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
    public partial class Form3 : Form
    {
        
        public Form3()
        {
            InitializeComponent();   
        }
        int inedx = 0;

        public Form3(int ind)
        {
            InitializeComponent();
            inedx = ind;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Value.ToString().Substring(0, 10);
            textBox7.Text = date;
        }
        DataTable dt1 = new DataTable();
        DataTable dt12 = new DataTable();
        DataTable dt123 = new DataTable();
        DataTable dt1234 = new DataTable();
        private void Form3_Load(object sender, EventArgs e)
        {
            OleDbConnection connection1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=db.accdb");
            
            OleDbDataAdapter adapter1 = new OleDbDataAdapter("Select ID_Изделия, Наименование from Изделия", connection1);
            adapter1.Fill(dt1);
            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "Наименование";
            comboBox1.ValueMember = "ID_Изделия";
            OleDbDataAdapter adapter12 = new OleDbDataAdapter("Select ID_Цикл_работ, Наименование from [Цикл работ]", connection1);
            
            adapter12.Fill(dt12);
            comboBox2.DataSource = dt12;
            comboBox2.DisplayMember = "Наименование";
            comboBox2.ValueMember = "ID_Цикл_работ";
            OleDbDataAdapter adapter123 = new OleDbDataAdapter("Select ID_Бригады, Название from Бригады", connection1);
            
            adapter123.Fill(dt123);
            comboBox3.DataSource = dt123;
            comboBox3.DisplayMember = "Название";
            comboBox3.ValueMember = "ID_Бригады";
            OleDbDataAdapter adapter1234 = new OleDbDataAdapter("Select ID_Участка, Название from Участки", connection1);
            
            adapter1234.Fill(dt1234);
            comboBox4.DataSource = dt1234;
            comboBox4.DisplayMember = "Название";
            comboBox4.ValueMember = "ID_Участка";
            connection1.Close();

            if (inedx == 0)
            {
                textBox1.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
            }
            else 
            {
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=db.accdb");
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT ID_Сборка, [Дата сборки], " +
                        "Изделие, " +
                        "[Цикл работ], " +
                        "Бригада, Участок " +
                        "FROM [Сборка изделия]" +
                        $" WHERE ID_Сборка = {inedx}", con);
                DataTable dt_ = new DataTable();
                adapter.Fill(dt_);
                con.Close();
                textBox7.Text = dt_.Rows[0][1].ToString().Substring(0, 10);
                textBox1.Text = dt_.Rows[0][2].ToString();
                textBox8.Text = dt_.Rows[0][3].ToString();
                textBox9.Text = dt_.Rows[0][4].ToString();
                textBox10.Text = dt_.Rows[0][5].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (inedx == 0)
            {
                try
                {

                    OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=db.accdb");
                    OleDbDataAdapter adapter = new OleDbDataAdapter("INSERT INTO [Сборка изделия] ([Дата сборки], Изделие, [Цикл работ], Бригада, Участок) " +
                        "VALUES " +
                        $"('{textBox7.Text}','{textBox1.Text}','{textBox8.Text}','{textBox9.Text}','{textBox10.Text}');", connection);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);
                    connection.Close();
                    Close();

                }
                catch { MessageBox.Show("Введите все значения"); }
            }
            else 
            {
                try
                {

                    OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=db.accdb");
                    OleDbDataAdapter adapter = new OleDbDataAdapter("UPDATE [Сборка изделия] SET " +
                        $"[Дата сборки]='{textBox7.Text}', Изделие ='{textBox1.Text}'," +
                        $"[Цикл работ] = '{textBox8.Text}', Бригада = '{textBox9.Text}'," +
                        $"Участок ='{textBox10.Text}' " +
                        $"WHERE ID_Сборка = {inedx}",connection);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);
                    connection.Close();
                    Close();

                }
                catch { MessageBox.Show("Введите все значения"); }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = comboBox1.SelectedValue.ToString();
            }
            catch { }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox8.Text = comboBox2.SelectedValue.ToString();
            }
            catch { }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox9.Text = comboBox3.SelectedValue.ToString();
            }
            catch { }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox10.Text = comboBox4.SelectedValue.ToString();
            }
            catch { }
        }


    }
}
