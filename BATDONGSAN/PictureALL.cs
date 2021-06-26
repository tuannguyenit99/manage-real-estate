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
    public partial class PictureALL : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=BatDongSan;Integrated Security=True");
        private string _ides;
        public PictureALL()
        {
            InitializeComponent();
        }
        public string Ides
        {
            get { return _ides; }
            set { _ides = value; }
        }

        private void PictureALL_Load(object sender, EventArgs e)
        {

           

                con.Open();

                DataTable dt = new DataTable();

                string chuoi = "";

                SqlCommand cmd4 = new SqlCommand("select PictureHome from PictureHome where IDEs=@us", con);
                cmd4.Parameters.AddWithValue("us", Ides.ToString());
                dt.Load(cmd4.ExecuteReader());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    chuoi = dt.Rows[i]["PictureHome"].ToString();
                }

            if (chuoi.ToString() == "")
            {
                MessageBox.Show("Do Not Picture ");
            }
            if (chuoi.ToString() != "")
            {
                pictureBox1.Image = Image.FromFile("D:\\BATDONGSAN\\BATDONGSAN\\Picture\\" + chuoi.ToString());
            }

            con.Close();
            
            








        }

        private void button2_Click(object sender, EventArgs e)
        {

          

                con.Open();

                DataTable dt = new DataTable();

                string chuoi = "";



                SqlCommand cmd4 = new SqlCommand("select PictureHome from PictureHome where IDEs=@us", con);
                cmd4.Parameters.AddWithValue("us", Ides.ToString());
                dt.Load(cmd4.ExecuteReader());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    chuoi = dt.Rows[i]["PictureHome"].ToString();
                }
                if(chuoi.ToString()!="")
                 {
                pictureBox1.Image = Image.FromFile("D:\\BATDONGSAN\\BATDONGSAN\\Picture\\" + chuoi.ToString());
                  }
              if (chuoi.ToString() == "")
                {
                MessageBox.Show("Do Not Picture");
                }
           
            

                con.Close();
            





        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                con.Open();

                DataTable dt = new DataTable();

                string chuoi2 = "";
                SqlCommand cmd = new SqlCommand("select PictureBed from PictureHome where IDEs=@us", con);
                cmd.Parameters.AddWithValue("us", Ides.ToString());
                dt.Load(cmd.ExecuteReader());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    chuoi2 = dt.Rows[i]["PictureBed"].ToString();
                }
                
            if (chuoi2.ToString() != "")
            {
                pictureBox1.Image = Image.FromFile("D:\\BATDONGSAN\\BATDONGSAN\\Picture\\" + chuoi2.ToString());
            }

            if (chuoi2.ToString() == "")
            {
                MessageBox.Show("Do Not Picture ");
            }
            con.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

           
                con.Open();

                DataTable dt = new DataTable();

                string chuoi3 = "";
                SqlCommand cmd3 = new SqlCommand("select PictureBath from PictureHome where IDEs=@us", con);
                cmd3.Parameters.AddWithValue("us", Ides.ToString());
                dt.Load(cmd3.ExecuteReader());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    chuoi3 = dt.Rows[i]["PictureBath"].ToString();
                }
               
            if (chuoi3.ToString() != "")
            {
                pictureBox1.Image = Image.FromFile("D:\\BATDONGSAN\\BATDONGSAN\\Picture\\" + chuoi3.ToString());
            }


            if (chuoi3.ToString() == "")
            {
                MessageBox.Show("Do Not Picture ");
            }
            con.Close();
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
                con.Open();

                DataTable dt = new DataTable();

                string chuoi4 = "";
                SqlCommand cmd5 = new SqlCommand("select PictureAll from PictureHome where IDEs=@us", con);
                cmd5.Parameters.AddWithValue("us", Ides.ToString());
                dt.Load(cmd5.ExecuteReader());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    chuoi4 = dt.Rows[i]["PictureAll"].ToString();
                }
            if (chuoi4.ToString() == "")
            {
                MessageBox.Show("Do Not Picture ");
            }
            if (chuoi4.ToString() != "")
            {
                pictureBox1.Image = Image.FromFile("D:\\BATDONGSAN\\BATDONGSAN\\Picture\\" + chuoi4.ToString());
            }
            con.Close();

        }
    }
}
