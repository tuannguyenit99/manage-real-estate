using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BATDONGSAN
{
    public partial class LogIN : Form
    {

        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=BatDongSan;Integrated Security=True");
        public LogIN()
        {
            InitializeComponent();
            pass.Text = "";
            pass.PasswordChar = '*';


        }



        public int Login()
        {
                con.Open();
            DataTable dt = new DataTable();
            string sql = "select * from Users where users=@users and pass=@pass";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("users", use.Text);
            cmd.Parameters.AddWithValue("pass", pass.Text);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            con.Close();
            adt.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0][7].ToString() == "Admin")
                {
                    return 1;
                }
                else if (dt.Rows[0][7].ToString() == "User")
                {
                    return 2;
                }

            }
           
            return 0;
          


        }

        private void button1_Click(object sender, EventArgs e)
        {




            if (use.Text == "" || pass.Text == "")
            {
                tb.Text = "NOT NULL PASS OR USER";
            }
            else
            {
                int tk = Login();
                if (tk == 1)
                {

                    con.Open();

                    DataTable dt = new DataTable();

                    string chuoi = "";

                    SqlCommand cmd4 = new SqlCommand("select IDUS from Users where users=@us", con);
                    cmd4.Parameters.AddWithValue("us", use.Text);
                    dt.Load(cmd4.ExecuteReader());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        chuoi = dt.Rows[i]["IDUS"].ToString();
                    }

                    Admin l = new Admin();
                    l.FormClosed += new FormClosedEventHandler(cc);
                    l.Idnv = chuoi.ToString();
                    l.Message = use.Text;
                    l.Show();
                    this.Hide();
                    use.Text = "";
                    pass.Text = "";
                    tb.Text = "";
                    con.Close();

                }
                if (tk == 2)
                {
                    con.Open();

                    DataTable dt = new DataTable();


                    string chuoi = "";
                    

                    SqlCommand cmd4 = new SqlCommand("select IDUS from Users where users=@us", con);
                    cmd4.Parameters.AddWithValue("us", use.Text);
                    dt.Load(cmd4.ExecuteReader());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        chuoi = dt.Rows[i]["IDUS"].ToString();
                    }
 

                    User u = new User();
                    u.FormClosed += new FormClosedEventHandler(cc);
                    u.Idnv = chuoi.ToString();
                    u.Message = use.Text;
                    u.Show();
                    this.Hide();
                    use.Text = "";
                    pass.Text = "";
                    tb.Text = "";
                    con.Close();

                }


                else
                {
                    tb.Text = "USER OR PASS NOT CORRECT";
                }


            }


        }
        

        private void cc(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }


       

      

    }
}
