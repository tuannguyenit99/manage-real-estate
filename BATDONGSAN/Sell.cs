using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BATDONGSAN
{
    public partial class Sell : Form
    {
        private string _idnv;
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=BatDongSan;Integrated Security=True");
        public Sell()
        {
            InitializeComponent();
        }
        public string Idnv
        {
            get { return _idnv; }
            set { _idnv = value; }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into  Client values(@name,@ad,@phone,@em)", con);
            cmd.Parameters.AddWithValue("name", name.Text);
            cmd.Parameters.AddWithValue("ad", ad.Text);
            cmd.Parameters.AddWithValue("phone", phone.Text);
            cmd.Parameters.AddWithValue("em", em.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("ADD Client COMPLETED");
            con.Close();
            loadEstate();
            loadclient();

        }
        
        void clean()
        {
           name.Text="";
           ad.Text="";
           phone.Text = "";
           em.Text = "";
           dataGridView1.Refresh();
        }
        void loadclient()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from Client order by idclient desc", con);
            DataTable tb = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            tb.Clear();
            adt.Fill(tb);
            cli.DataSource = tb;
            cli.DisplayMember = "FullName";
            cli.ValueMember = "IDclient";
            con.Close();
        }
        void loadEstate()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from Estate where Status=0", con);
            DataTable tb = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            tb.Clear();
            adt.Fill(tb);
            est.DataSource = tb;
            est.DisplayMember = "Namees";
            est.ValueMember = "IDEs";
            con.Close();
           
        }

        private void Sell_Load(object sender, EventArgs e)
        {
            loadEstate();
            loadclient();
            date.Text = DateTime.Now.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clean();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into  Addestate values(@idc,@ides,@date,@IDus,@comment)", con);
            cmd.Parameters.AddWithValue("idc", cli.SelectedValue);
            cmd.Parameters.AddWithValue("ides", est.SelectedValue);
            cmd.Parameters.AddWithValue("date", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            cmd.Parameters.AddWithValue("IDus", Idnv.ToString());
            cmd.Parameters.AddWithValue("comment", cmt.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("SELL ESTATE COMPLETED");
            SqlCommand cmd1 = new SqlCommand("update Estate set status=1 where ides=@ides ", con);
            cmd1.Parameters.AddWithValue("ides", est.SelectedValue);
            cmd1.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("select Client.FullName,CLient.Address,CLient.Phone, Estate.NameEs,Estate.Adress,Estate.Acreage,Estate.Price,Estate.cmt,AddEstate.DateSold from CLient inner join AddEstate on CLient.IDClient = AddEstate.IDCilent inner join Estate on Estate.IDEs = AddEstate.IDEs where AddEstate.IDCilent =@idc", con);
            cmd2.Parameters.AddWithValue("idc", cli.SelectedValue);
            DataTable tb = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd2);
            tb.Clear();
            adt.Fill(tb);
            dataGridView1.DataSource = tb;


            cmd2.ExecuteNonQuery();






            con.Close();
            loadEstate();
            loadclient();
        }

       
    }
}
