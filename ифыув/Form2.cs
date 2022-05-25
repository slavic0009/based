using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace ифыув
{
    public partial class Form2 : Form
    {
        string tablename;
        static DataTable dt = new DataTable();
        static OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.accdb");
        public Form2(string tablename_)
        {
            InitializeComponent();
            load(tablename_);
        }
        public void load(string tablename_)
        {
            tablename = tablename_;
            switch (tablename)
            {
                case "Сборка изделия":
                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT ID_Сборка, [Дата сборки], " +
                        "Изделия.Наименование as Наименование, " +
                        "[Цикл работ].Наименование as [Цикл работ], " +
                        "Бригады.Название as Бригада, Участки.Название as Участок" +
                        " FROM [Сборка изделия], Изделия, [Цикл работ], Бригады, Участки " +
                        "WHERE [Сборка Изделия].Изделие = Изделия.ID_Изделия " +
                        "AND [Сборка Изделия].[Цикл работ] = [Цикл работ].ID_Цикл_Работ" +
                        " AND [Сборка Изделия].Бригада = Бригады.ID_Бригады" +
                        " AND [Сборка Изделия].Участок = Участки.ID_Участка", con);
                    DataTable dt_ = new DataTable();
                    adapter.Fill(dt_);
                    dt = dt_;
                    dataGridView1.DataSource = dt;
                    break;
                case "Испытание изделия":
                    OleDbDataAdapter adapte = new OleDbDataAdapter("SELECT ID_Испытания, [Дата испытания], " +
                        "ID_Сборка, Полигоны.Название as Полигон, " +
                        "Сотрудники.ФИО as Испытатель, " +
                        "Оборудование.Название as Оборудованиея, " +
                        "[Тип испытания].Наименование as [Тип испытания], " +
                        "Статус " +
                        "FROM [Испытания изделия], Полигоны, Сотрудники, Оборудование, [Тип испытания] " +
                        "WHERE [Испытания изделия].Полигон = Полигоны.ID_Полигона " +
                        "AND [Испытания изделия].[Испытатель (ID_Сотрудники)] = Сотрудники.ID_Сотрудники " +
                        "AND [Испытания изделия].Оборудование = Оборудование.ID_Оборудования " +
                        "AND [Испытания изделия].[Тип испытания] = [Тип испытания].[ID_Тип_испытания]", con);
                    DataTable dt__ = new DataTable();
                    adapte.Fill(dt__);
                    dt = dt__;
                    dataGridView1.DataSource = dt;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    break;
                case "Сотрудники":
                    OleDbDataAdapter adapte1 = new OleDbDataAdapter("SELECT ID_Сотрудники, " +
                        "ФИО, [Номер телефона], Должности.Должность as Должность " +
                        "FROM Сотрудники, Должности " +
                        "WHERE Сотрудники.Должность = Должности.ID_Должности", con);
                    DataTable dt23 = new DataTable();
                    adapte1.Fill(dt23);
                    dt = dt23;
                    dataGridView1.DataSource = dt;
                    //button1.Enabled = false;
                    button2.Enabled = false;
                    break;
                case "Цеха":
                    OleDbDataAdapter adapte13 = new OleDbDataAdapter("SELECT ID_Цеха, Название, Сотрудники.ФИО " +
                        "FROM Цеха, Сотрудники" +
                        " WHERE Цеха.[Начальник (ID_Сотрудники)]=Сотрудники.ID_Сотрудники", con);
                    DataTable dt5 = new DataTable();
                    adapte13.Fill(dt5);
                    dt = dt5;
                    dataGridView1.DataSource = dt;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    break;

            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                label1.Text = dataGridView1[0, e.RowIndex].Value.ToString();
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {
                OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=db.accdb");
                switch (tablename)
                {
                    case "Сборка изделия":
                        OleDbDataAdapter adapter = new OleDbDataAdapter("Delete * from [Сборка изделия] " +
                            "where ID_Сборка=" + label1.Text, connection);
                        DataTable dt1 = new DataTable();
                        adapter.Fill(dt1);
                        connection.Close();
                        load(tablename);
                        break;
                    case "Испытание изделия":
                        OleDbDataAdapter adapter1 = new OleDbDataAdapter("Delete * from [Испытания изделия] " +
                            "where ID_Испытания=" + label1.Text, connection);
                        DataTable dt12 = new DataTable();
                        adapter1.Fill(dt12);
                        connection.Close();
                        load(tablename);
                        break;
                    case "Сотрудники":
                        OleDbDataAdapter adapter12 = new OleDbDataAdapter("Delete * from Сотрудники " +
                            "where ID_Сотрудники=" + label1.Text, connection);
                        DataTable dt123 = new DataTable();
                        adapter12.Fill(dt123);
                        connection.Close();
                        load(tablename);
                        break;
                    case "Цеха":
                        OleDbDataAdapter adapter129 = new OleDbDataAdapter("Delete * from Цеха " +
                            "where ID_Цеха=" + label1.Text, connection);
                        DataTable dt19 = new DataTable();
                        adapter129.Fill(dt19);
                        connection.Close();
                        load(tablename);
                        break;

                }
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (tablename)
            {
                case "Сборка изделия":
                    Form3 form3 = new Form3();
                    form3.ShowDialog();
                    load(tablename);
                    break;
                case "Сотрудники":

                    Form4 form4 = new Form4();
                    form4.ShowDialog();
                    load(tablename);
                    break;
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {
                Form3 form3 = new Form3(Convert.ToInt32(label1.Text));
                form3.ShowDialog();
                load(tablename);
            }
        }
    }
}


