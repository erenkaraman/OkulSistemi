using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OkulSistemiFinal
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtemail.Text != "" && txtsifre.Text != "")
            {

                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("select * from tbl_ogretmen where email=@email and sifre=@sifre", vt.conAc());
                cmd.Parameters.AddWithValue("email", txtemail.Text);
                cmd.Parameters.AddWithValue("sifre", txtsifre.Text);
                SqlDataReader oku = cmd.ExecuteReader();
                if (oku.Read())
                {
                    this.Hide();
                    OkulSistemi os = new OkulSistemi();
                    os.ShowDialog();
                }
                else
                {
                    label1.Text = "Hatalı Email Veya Şifre";
                    label1.ForeColor = Color.Red;
                }
                vt.conKapa();
            }
            else
            {
                label1.Text = "Lütfen Email Ve Şifre Giriniz";
                label1.ForeColor = Color.Red;
            }



        }
    }
}
