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
    public partial class TransactedOfClient : Form
    {
        private string _idnv;
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=BatDongSan;Integrated Security=True");

        public TransactedOfClient()
        {
            InitializeComponent();
        }

        private void TransactedOfClient_Load(object sender, EventArgs e)
        {
            load();
        }

        public string Idnv
        {
            get { return _idnv; }
            set { _idnv = value; }
        }
        void load()
        {
            SqlCommand cmd2 = new SqlCommand("select CLient.IDClient, Client.FullName,CLient.Address,CLient.Phone, Estate.NameEs,Estate.Adress,Estate.Acreage,Estate.Price,Estate.cmt,Estate.age,AddEstate.DateSold,Estate.bedrooms,Estate.bathrooms from CLient inner join AddEstate on CLient.IDClient = AddEstate.IDCilent inner join Estate on Estate.IDEs = AddEstate.IDEs where AddEstate.IDCilent=@idc", con);
            cmd2.Parameters.AddWithValue("idc", Idnv.ToString());
            DataTable tb = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd2);
            tb.Clear();
            adt.Fill(tb);
            dataGridView1.DataSource = tb;
            if(dataGridView1.Rows.Count==1)
            {
                MessageBox.Show("Customer has not transacted");
            }    
        }
    }
}
