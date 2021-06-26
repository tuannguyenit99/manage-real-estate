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
    public partial class Owner : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=BatDongSan;Integrated Security=True");

        public Owner()
        {
            InitializeComponent();
        }
        void load()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from Owner", con);
            DataTable tb = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            tb.Clear();
            adt.Fill(tb);
            dataGridView1.DataSource = tb;
            con.Close();
        }
       

        private void Owner_Load(object sender, EventArgs e)
        {
            load();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            id.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            name.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            ad.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            phone.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            em.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into  Owner values(@name,@ad,@phone,@em)", con);
            cmd.Parameters.AddWithValue("name", name.Text);
            cmd.Parameters.AddWithValue("ad", ad.Text);
            cmd.Parameters.AddWithValue("phone", phone.Text);
            cmd.Parameters.AddWithValue("em", em.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("ADD COMPLETED");
            con.Close();
            load();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("DELETE?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from  Owner where Idowner=@id", con);
                cmd.Parameters.AddWithValue("id", id.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                load();
                clean();
               
            }
        }
        void clean()
        {
            id.Text = "";
            name.Text = "";
            ad.Text = "";
            phone.Text = "";
            em.Text = "";
            id.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("EDIT?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update  owner set fullname=@name , address=@ad, phone=@phone, Email=@em where idowner=@id", con);
                cmd.Parameters.AddWithValue("id", id.Text);
                cmd.Parameters.AddWithValue("name", name.Text);
                cmd.Parameters.AddWithValue("ad", ad.Text);
                cmd.Parameters.AddWithValue("phone", phone.Text);
                cmd.Parameters.AddWithValue("em", em.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                load();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            id.Enabled = true;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from owner where idowner=@id ", con);
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

        private void button5_Click(object sender, EventArgs e)
        {
            id.Enabled = false;
            clean();
            load();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Delivery E = new Delivery();
            E.FormClosed += new FormClosedEventHandler(cc);
            E.Idowner = id.Text;
            E.Show();
            this.Hide();
        }
        private void cc(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
    }
}
