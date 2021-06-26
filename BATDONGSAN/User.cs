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
    public partial class User : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=BatDongSan;Integrated Security=True");
        private string _message;
        private string _idnv;

        public User()
        {
            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {
            load();
            ten.Text = _message;
            idnv.Text = _idnv;
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public string Idnv
        {
            get { return _idnv; }
            set { _idnv = value; }
        }




        void load()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from TypeHome", con);
            DataTable tb = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            tb.Clear();
            adt.Fill(tb);
            dataGridView1.DataSource = tb;
            con.Close();
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            id.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            name.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            des.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into  TypeHome values(@name,@des)", con);
            cmd.Parameters.AddWithValue("name", name.Text);
            cmd.Parameters.AddWithValue("des", des.Text);
            cmd.ExecuteNonQuery();

            con.Close();
            load();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            id.Text = "";
            id.Enabled = false;
            name.Text = "";
            des.Text = "";
            load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("DELETE?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from  TypeHome where idtype=@id", con);
                cmd.Parameters.AddWithValue("id", id.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                load();
                id.Text = "";
                name.Text = "";
                des.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("EDIT?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update  TypeHome set name=@name , description=@des where idtype=@id", con);
                cmd.Parameters.AddWithValue("id", id.Text);
                cmd.Parameters.AddWithValue("name", name.Text);
                cmd.Parameters.AddWithValue("des", des.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                load();
            }





        }

        private void button4_Click(object sender, EventArgs e)
        {
            id.Enabled = true;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from TypeHome where idtype=@id ", con);
            cmd.Parameters.AddWithValue("id", id.Text);
            cmd.ExecuteNonQuery();
            DataTable tb = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            tb.Clear();
            adt.Fill(tb);
            dataGridView1.DataSource = tb;
            con.Close();
            if (dataGridView1.Rows.Count == 1)
            {
                MessageBox.Show("Not found Information");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Owner O = new Owner();
            O.FormClosed += new FormClosedEventHandler(cc);
            O.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Est E = new Est();
            E.FormClosed += new FormClosedEventHandler(cc);
            E.Message = ten.Text;
            E.Idnv = idnv.Text;
            E.Show();
            this.Hide();
        }
        private void cc(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Sell E = new Sell();
            E.FormClosed += new FormClosedEventHandler(cc);
            E.Idnv = idnv.Text;
            E.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Clients E = new Clients();
            E.FormClosed += new FormClosedEventHandler(cc);
            E.Show();
            this.Hide();

        }
    }
}
