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
    public partial class Clients : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=BatDongSan;Integrated Security=True");

        public Clients()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;
            i = dataGridView1.CurrentRow.Index;
            if (i + 1 >= dataGridView1.Rows.Count)
            {
                clean();
            }
            else
            {
                idclient.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                name.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                ad.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                phone.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                em.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
               
            }

        }
        void clean()
        {
            idclient.Enabled = false;
            idclient.Text = "";
            name.Text = "";
            ad.Text = "";
            phone.Text = "";
            em.Text = "";
            load();
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            load();
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
            load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("EDIT?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update  client set fullname=@name, address=@ad,phone=@phone,email=@em   where idclient=@id", con);
                cmd.Parameters.AddWithValue("id", idclient.Text);
                cmd.Parameters.AddWithValue("name", name.Text);
                cmd.Parameters.AddWithValue("ad", ad.Text);
                cmd.Parameters.AddWithValue("phone", phone.Text);
                cmd.Parameters.AddWithValue("em", em.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                load();
            }
        }
        void load()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from client", con);
            DataTable tb = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            tb.Clear();
            adt.Fill(tb);
            dataGridView1.DataSource = tb;
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clean();
            load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            idclient.Enabled = true;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from client where idclient=@id ", con);
            cmd.Parameters.AddWithValue("id", idclient.Text);
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
            TransactedOfClient E = new TransactedOfClient();
            E.FormClosed += new FormClosedEventHandler(cc);
            E.Idnv = idclient.Text;
            E.Show();
            this.Hide();

        }
        private void cc(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
    }
}
