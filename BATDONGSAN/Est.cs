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
    public partial class Est : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=BatDongSan;Integrated Security=True");
        private string _message;
        private string _idnv;
        public Est()
        {
            InitializeComponent();
        }


        private void Est_Load(object sender, EventArgs e)
        {
            use.Text = _message;
            load();
            loadOwner();
            loadhome();
        }
        public string Idnv
        {
            get { return _idnv; }
            set { _idnv = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }


        void load()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from Estate", con);
            DataTable tb = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            tb.Clear();
            adt.Fill(tb);
            dataGridView1.DataSource = tb;
            con.Close();
        }
        void loadOwner()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from Owner", con);
            DataTable tb = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            tb.Clear();
            adt.Fill(tb);
            owner.DataSource = tb;
            owner.DisplayMember = "FullName";
            owner.ValueMember = "IDowner";

            con.Close();
        }
        void loadhome()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from Typehome", con);
            DataTable tb = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            tb.Clear();
            adt.Fill(tb);
            hometype.DataSource = tb;
            hometype.DisplayMember = "Name";
            hometype.ValueMember = "IDtype";

            con.Close();
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;
            i = dataGridView1.CurrentRow.Index;
            if(i+1>=dataGridView1.Rows.Count)
            {
                clean();
            }  
           else
            {
                id.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                name.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                ad.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                m2.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                bed.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                bat.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
                age.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
                pr.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
                use.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
                owner.SelectedValue = dataGridView1.Rows[i].Cells[9].Value.ToString();
                hometype.SelectedValue = dataGridView1.Rows[i].Cells[10].Value.ToString();
                sta.Text = dataGridView1.Rows[i].Cells[11].Value.ToString();
                picture.Text = dataGridView1.Rows[i].Cells[12].Value.ToString();
                cmt.Text = dataGridView1.Rows[i].Cells[13].Value.ToString();


                pictureBox1.Image = Image.FromFile("D:\\BATDONGSAN\\BATDONGSAN\\Picture\\" + picture.Text);

            }    

          
                
                

            
            
               


        }

      

        private void button5_Click(object sender, EventArgs e)
        {
            clean();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into  Estate values(@name,@ad,@Ac,@bet,@bat,@age,@pr,@idus,@idowner,@idhometype,@sta,@picture,@cmt)", con);
            cmd.Parameters.AddWithValue("name", name.Text);
            cmd.Parameters.AddWithValue("ad", ad.Text);
            cmd.Parameters.AddWithValue("Ac", m2.Text);
            cmd.Parameters.AddWithValue("bet", bed.Text);
            cmd.Parameters.AddWithValue("bat", bat.Text);
            cmd.Parameters.AddWithValue("age", age.Text);
            cmd.Parameters.AddWithValue("pr", pr.Text);
            cmd.Parameters.AddWithValue("idus", Idnv.ToString());
            cmd.Parameters.AddWithValue("idowner", owner.SelectedValue);
            cmd.Parameters.AddWithValue("idhometype", hometype.SelectedValue);
            cmd.Parameters.AddWithValue("sta", sta.Text);
            cmd.Parameters.AddWithValue("picture", picture.Text);
            cmd.Parameters.AddWithValue("cmt", cmt.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("ADD COMPLE");

            con.Close();
            load();
        }
        void clean()
        {
            id.Enabled = false;
            id.Text = "";
            name.Text = "";
            ad.Text = "";
            m2.Text = "";
            bed.Text = "0";
            bat.Text = "0";
            age.Text = "0";
            pr.Text = "";
            use.Text = _message;
            owner.Text = "";
            hometype.Text = "";
            sta.Text = "0";
            picture.Text = "";
            cmt.Text="";
            load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("EDIT?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update  estate set NameEs=@name,Adress=@ad,Acreage=@Ac,bedrooms=@bet,bathrooms=@bat,age=@age,Price=@pr,IDOwner=@idowner,IDType=@idhometype,Status=@sta,Picture=@picture,cmt=@cmt  where ides=@id", con);
                cmd.Parameters.AddWithValue("id", id.Text);
                cmd.Parameters.AddWithValue("name", name.Text);
                cmd.Parameters.AddWithValue("ad", ad.Text);
                cmd.Parameters.AddWithValue("Ac", m2.Text);
                cmd.Parameters.AddWithValue("bet", bed.Text);
                cmd.Parameters.AddWithValue("bat", bat.Text);
                cmd.Parameters.AddWithValue("age", age.Text);
                cmd.Parameters.AddWithValue("pr", pr.Text);
                
                cmd.Parameters.AddWithValue("idowner", owner.SelectedValue);
                cmd.Parameters.AddWithValue("idhometype", hometype.SelectedValue);
                cmd.Parameters.AddWithValue("sta", sta.Text);
                cmd.Parameters.AddWithValue("picture", picture.Text);
                cmd.Parameters.AddWithValue("cmt", cmt.Text); 
                cmd.ExecuteNonQuery();
                con.Close();
                load();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("DELETE?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from  estate where ides=@id", con);
                cmd.Parameters.AddWithValue("id", id.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                load();
                clean();

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            id.Enabled = true;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from estate where ides=@id ", con);
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

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() {Filter="Image file(*.jpg;*.jpeg;*.png)|*.jpg|*.jpeg|*.png",Multiselect=false })
            {
                if(ofd.ShowDialog()==DialogResult.OK)
                {
                    picture.Text = System.IO.Path.GetFileName(ofd.FileName);
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    load();

                }    
            }    
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PictureALL E = new PictureALL();
            E.FormClosed += new FormClosedEventHandler(cc);
            E.Ides = id.Text;
            E.Show();
            this.Hide();
        }
        private void cc(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
    }
}
