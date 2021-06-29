using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulSistemiFinal
{
    public partial class OkulSistemi : MaterialForm
    {
        readonly MaterialSkin.MaterialSkinManager materialSkinManager;
        public OkulSistemi()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Indigo500, MaterialSkin.Primary.Indigo700, MaterialSkin.Primary.Indigo100, MaterialSkin.Accent.Pink200, MaterialSkin.TextShade.WHITE);

            mbogrencibtn.Text = veriSayaci("tbl_ogrenci");
            mbogretmenbtn.Text = veriSayaci("tbl_ogretmen");
            mbdersbtn.Text = veriSayaci("tbl_ders");
            mldetay.Visible = false;
            mlvdersdetay.Visible = false;
            ogrenciMLV();
            dersMLV();
            ogretmenMLV();

        }

        public string veriSayaci(string tabloAdi)
        {
            string data = "";
            Veritabani vt = new Veritabani();
            SqlCommand cmd = new SqlCommand("select count(Id) sqlsayac from " + tabloAdi, vt.conAc());
            SqlDataReader oku = cmd.ExecuteReader();
            if (oku.Read())
            {
                data = oku["sqlsayac"].ToString();
            }
            else
            {
                data = "0";
            }
            vt.conKapa();
            return data;
        }

        private void mbogrencibtn_Click(object sender, EventArgs e)
        {
            materialTabControl1.SelectedIndex = 1;
        }

        private void mbdersbtn_Click(object sender, EventArgs e)
        {
            materialTabControl1.SelectedIndex = 2;
        }

        private void mbogretmenbtn_Click(object sender, EventArgs e)
        {
            materialTabControl1.SelectedIndex = 3;
        }

        public void ogrenciMLV()
        {
            Veritabani vt = new Veritabani();
            SqlCommand cmd = new SqlCommand("Select * From tbl_ogrenci", vt.conAc());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem item = new ListViewItem(dr["Id"].ToString());
                item.SubItems.Add(dr["Ad"].ToString());
                item.SubItems.Add(dr["Soyad"].ToString());
                mlvogrenci.Items.Add(item);
            }
            vt.conKapa();
        }
        public void dersMLV()
        {
            Veritabani vt = new Veritabani();
            SqlCommand cmd = new SqlCommand("Select * From tbl_ders", vt.conAc());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem item = new ListViewItem(dr["Id"].ToString());
                item.SubItems.Add(dr["Ad"].ToString());
                mlders.Items.Add(item);
            }
            vt.conKapa();
        }
        public void ogretmenMLV()
        {
            Veritabani vt = new Veritabani();
            SqlCommand cmd = new SqlCommand("Select * From tbl_ogretmen", vt.conAc());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem item = new ListViewItem(dr["Id"].ToString());
                item.SubItems.Add(dr["Ad"].ToString());
                item.SubItems.Add(dr["Soyad"].ToString());
                mlOgretmen.Items.Add(item);
            }
            vt.conKapa();
        }
        private void mlvogrenci_MouseClick(object sender, MouseEventArgs e)
        {
            mtxtid.Text = mlvogrenci.SelectedItems[0].SubItems[0].Text;
            mtxtad.Text = mlvogrenci.SelectedItems[0].SubItems[1].Text;
            mtxtsoyad.Text = mlvogrenci.SelectedItems[0].SubItems[2].Text;
        }


        private void mbtntemizle_Click(object sender, EventArgs e)
        {
            mtxtid.Text = "";
            mtxtad.Text = "";
            mtxtsoyad.Text = "";
        }

        private void mbtnekle_Click(object sender, EventArgs e)
        {
            if (mtxtad.Text != "" && mtxtsoyad.Text != "")
            {
                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("insert into tbl_ogrenci (ad,soyad) values(@ad,@soyad)", vt.conAc());
                cmd.Parameters.AddWithValue("ad", mtxtad.Text);
                cmd.Parameters.AddWithValue("soyad", mtxtsoyad.Text);
                int durum = cmd.ExecuteNonQuery();
                if (durum == 1)
                {
                    mlblmessage.Text = "Başarılı";
                }
                else
                {
                    mlblmessage.Text = "Hata";
                }
                mlvogrenci.Items.Clear();
                ogrenciMLV();
                vt.conKapa();
            }
            else
            {
                mlblmessage.Text = "Ad Ve Soyadı Doldurunuz";
            }

        }

        private void mbtnsil_Click(object sender, EventArgs e)
        {
            if (mtxtid.Text != "")
            {
                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("delete from tbl_ogrenci where Id=@id", vt.conAc());
                cmd.Parameters.AddWithValue("id", mtxtid.Text);
                int durum = cmd.ExecuteNonQuery();
                if (durum == 1)
                {
                    mlblmessage.Text = "Başarılı";
                }
                else
                {
                    mlblmessage.Text = "Hata";
                }
                mlvogrenci.Items.Clear();
                ogrenciMLV();
                vt.conKapa();
            }
        }

        private void mbtnguncelle_Click(object sender, EventArgs e)
        {
            if (mtxtad.Text != "" && mtxtsoyad.Text != "" && mtxtid.Text != "")
            {
                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("update tbl_Ogrenci set ad=@ad, soyad=@soyad where Id=@id", vt.conAc());
                cmd.Parameters.AddWithValue("ad", mtxtad.Text);
                cmd.Parameters.AddWithValue("soyad", mtxtsoyad.Text);
                cmd.Parameters.AddWithValue("id", mtxtid.Text);
                int durum = cmd.ExecuteNonQuery();
                if (durum == 1)
                {
                    mlblmessage.Text = "Başarılı";
                }
                else
                {
                    mlblmessage.Text = "Hata";
                }
                mlvogrenci.Items.Clear();
                ogrenciMLV();
                vt.conKapa();
            }
            else
            {
                mlblmessage.Text = "Tüm Alanları Doldurunuz";
            }
        }

        private void mbtndetay_Click(object sender, EventArgs e)
        {
            if (mbtndetay.Text == "Detay" && mtxtid.Text != "")
            {
                mldetay.Visible = true;
                mlvogrenci.Visible = false;

                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("select tbl_ders.Ad DersAdi,tbl_notlar.[Not] from tbl_ogrenci tblo inner join tbl_alinanDersler tbla on tblo.Id=tbla.Ogrenci_id inner join tbl_ders on tbla.Ders_id=tbl_ders.Id inner join tbl_notlar on tblo.Id=tbl_notlar.Ogrenci_id where tblo.Id=@id", vt.conAc());
                cmd.Parameters.AddWithValue("id", mtxtid.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item2 = new ListViewItem(dr["DersAdi"].ToString());
                    item2.SubItems.Add(dr["Not"].ToString());
                    mldetay.Items.Add(item2);
                }
                vt.conKapa();
                mbtndetay.Text = "X";
            }
            else if (mbtndetay.Text == "X")
            {
                mldetay.Items.Clear();
                mldetay.Visible = false;
                mlvogrenci.Visible = true;
                mlvogrenci.Items.Clear();
                ogrenciMLV();
                mbtndetay.Text = "Detay";
            }
        }

        private void mlders_MouseClick(object sender, MouseEventArgs e)
        {
            mtxtidders.Text = mlders.SelectedItems[0].SubItems[0].Text;
            mtxtdersadi.Text = mlders.SelectedItems[0].SubItems[1].Text;
        }

        private void mbtntemizleders_Click(object sender, EventArgs e)
        {
            mtxtidders.Text = "";
            mtxtdersadi.Text = "";
        }

        private void mbtnekleders_Click(object sender, EventArgs e)
        {
            if (mtxtdersadi.Text != "")
            {
                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("insert into tbl_ders (ad) values(@ad)", vt.conAc());
                cmd.Parameters.AddWithValue("ad", mtxtdersadi.Text);
                int durum = cmd.ExecuteNonQuery();
                if (durum == 1)
                {
                    mlblalertders.Text = "Başarılı";
                }
                else
                {
                    mlblalertders.Text = "Hata";
                }
                mlders.Items.Clear();
                dersMLV();
                vt.conKapa();
            }
            else
            {
                mlblalertders.Text = "Ders Adı Giriniz";
            }
        }

        private void mbtnsilder_Click(object sender, EventArgs e)
        {
            if (mtxtidders.Text != "")
            {
                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("delete from tbl_ders where Id=@id", vt.conAc());
                cmd.Parameters.AddWithValue("id", mtxtidders.Text);
                int durum = cmd.ExecuteNonQuery();
                if (durum == 1)
                {
                    mlblalertders.Text = "Başarılı";
                }
                else
                {
                    mlblalertders.Text = "Hata";
                }
                mlders.Items.Clear();
                dersMLV();
                vt.conKapa();
            }
        }

        private void mbtnguncelleders_Click(object sender, EventArgs e)
        {
            if (mtxtdersadi.Text != "" && mtxtidders.Text != "")
            {
                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("update tbl_ders set ad=@ad where Id=@id", vt.conAc());
                cmd.Parameters.AddWithValue("ad", mtxtdersadi.Text);
                cmd.Parameters.AddWithValue("id", mtxtidders.Text);
                int durum = cmd.ExecuteNonQuery();
                if (durum == 1)
                {
                    mlblalertders.Text = "Başarılı";
                }
                else
                {
                    mlblalertders.Text = "Hata";
                }
                mlders.Items.Clear();
                dersMLV();
                vt.conKapa();
            }
            else
            {
                mlblalertders.Text = "Tüm Alanları Doldurunuz";
            }
        }

        private void mbtndetayders_Click(object sender, EventArgs e)
        {
            if (mbtndetayders.Text == "Detay" && mtxtidders.Text != "")
            {
                mlvdersdetay.Visible = true;
                mlders.Visible = false;

                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("select tbl_ogrenci.Id,tbl_ogrenci.Ad,tbl_ogrenci.Soyad,tbl_ogretmen.Ad OgretmenAdi,tbl_ogretmen.Soyad OgretmenSoyadi from tbl_ogrenci inner join tbl_alinanDersler on tbl_alinanDersler.Ogrenci_id=tbl_ogrenci.Id inner join tbl_ders on tbl_ders.Id=tbl_alinanDersler.Ders_id inner join tbl_verilenDersler on tbl_ders.Id=tbl_verilenDersler.Ders_id inner join tbl_ogretmen on tbl_verilenDersler.Ogretmen_id=tbl_ogretmen.Id where tbl_ders.Id=@id", vt.conAc());
                cmd.Parameters.AddWithValue("id", mtxtidders.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item2 = new ListViewItem(dr["Ad"].ToString());
                    item2.SubItems.Add(dr["Soyad"].ToString());
                    item2.SubItems.Add(dr["OgretmenAdi"].ToString());
                    item2.SubItems.Add(dr["OgretmenSoyadi"].ToString());
                    mlvdersdetay.Items.Add(item2);
                }
                vt.conKapa();
                mbtndetayders.Text = "X";
            }
            else if (mbtndetayders.Text == "X")
            {
                mlvdersdetay.Items.Clear();
                mlvdersdetay.Visible = false;
                mlders.Visible = true;
                mlders.Items.Clear();
                dersMLV();
                mbtndetayders.Text = "Detay";
            }
        }

        private void mlOgretmen_MouseClick(object sender, MouseEventArgs e)
        {
            mtxtogretmenid.Text = mlOgretmen.SelectedItems[0].SubItems[0].Text;
            mtxtogretmenadi.Text = mlOgretmen.SelectedItems[0].SubItems[1].Text;
            mtxtogretmensoyadi.Text = mlOgretmen.SelectedItems[0].SubItems[2].Text;
        }
    }
}
