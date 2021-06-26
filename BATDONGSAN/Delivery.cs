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
    public partial class Delivery : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=BatDongSan;Integrated Security=True");
        private string _idowner;

        public Delivery()
        {
            InitializeComponent();
        }

        private void Delivery_Load(object sender, EventArgs e)
        {
            load();
        }
        public string Idowner
        {
            get { return _idowner; }
            set { _idowner = value; }
        }
        void load()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from Estate where idowner=@idowner", con);
            cmd.Parameters.AddWithValue("idowner",Idowner.ToString());
            DataTable tb = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            tb.Clear();
            adt.Fill(tb);
            dataGridView1.DataSource = tb;
            if(dataGridView1.Rows.Count==1)
            {
                MessageBox.Show("The owner has not completed the information");
            }    
            con.Close();
        }

    }
}
