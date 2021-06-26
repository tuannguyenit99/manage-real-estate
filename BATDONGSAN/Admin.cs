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
    public partial class Admin : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=BatDongSan;Integrated Security=True");

        private string _message;
        private string _idnv;
        public Admin()
        {
            InitializeComponent();
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

        private void Admin_Load(object sender, EventArgs e)
        {
            ten.Text = _message;
            load();
            
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
                id.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                name.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                 date.Text= dataGridView1.Rows[i].Cells[2].Value.ToString();
                gender.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                ad.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                phone.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
                em.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
                fun.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
                user.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
                pass.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
                
            }

        }
        void clean()
        {
            id.Enabled = false;
            id.Text = "";
            name.Text ="";
            gender.Text = "";
            ad.Text = "";
            phone.Text = "";
            em.Text = "";
            fun.Text = "";
            user.Text = "";
            pass.Text = "";
           
        }



        void load()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from Users", con);
            DataTable tb = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            tb.Clear();
            adt.Fill(tb);
            dataGridView1.DataSource = tb;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into  users values(@name,@date,@gender,@ad,@phone,@em,@fun,@user,@pass)", con);
            cmd.Parameters.AddWithValue("name", name.Text);
            cmd.Parameters.AddWithValue("date", date.Value.ToString("MM/dd/yyyy"));
            cmd.Parameters.AddWithValue("gender", gender.Text);
            cmd.Parameters.AddWithValue("ad", ad.Text);
            cmd.Parameters.AddWithValue("phone", phone.Text);
            cmd.Parameters.AddWithValue("em", em.Text);
            cmd.Parameters.AddWithValue("fun", fun.Text);
            cmd.Parameters.AddWithValue("user", user.Text);
            cmd.Parameters.AddWithValue("pass", pass.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("ADD COMPLE");

            con.Close();
            load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("EDIT?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update  users set fullname=@name,dateb=@date,gender=@gender,address=@ad,phone=@phone,email=@em,funtions=@fun,users=@user,pass=@pass where idus=@id", con);
                cmd.Parameters.AddWithValue("id", id.Text);
                cmd.Parameters.AddWithValue("name", name.Text);
                cmd.Parameters.AddWithValue("date", date.Value.ToString("MM/dd/yyyy"));
                cmd.Parameters.AddWithValue("gender", gender.Text);
                cmd.Parameters.AddWithValue("ad", ad.Text);
                cmd.Parameters.AddWithValue("phone", phone.Text);
                cmd.Parameters.AddWithValue("em", em.Text);
                cmd.Parameters.AddWithValue("fun", fun.Text);
                cmd.Parameters.AddWithValue("user", user.Text);
                cmd.Parameters.AddWithValue("pass", pass.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                load();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("DELETE?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
            {

                if(id.Text==Idnv.ToString()||fun.Text=="Admin")
                {
                    MessageBox.Show("You can't erase yourself or other Admin");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from  users where idus=@id", con);
                    cmd.Parameters.AddWithValue("id", id.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    load();
                    clean();
                }    
                

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clean();
            load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            id.Enabled = true;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from users where idus=@id ", con);
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
    }
}
