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
            mlogretmendetay.Visible = false;
            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();
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
            mlvogrenci.Items.Clear();
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


            mdpogradisoyadi.Items.Clear();
            Veritabani vt2 = new Veritabani();
            SqlCommand cmd2 = new SqlCommand("Select * From tbl_ogrenci", vt2.conAc());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                mdpogradisoyadi.DisplayMember = "Text";
                mdpogradisoyadi.ValueMember = "Value";
                mdpogradisoyadi.Items.Add(new { Text = dr2["Ad"].ToString() + " " + dr2["Soyad"].ToString(), Value = dr2["Id"].ToString() });
            }
            vt2.conKapa();

            mlvnot.Items.Clear();
            Veritabani vt22 = new Veritabani();
            SqlCommand cmd22 = new SqlCommand("select tbl_notlar.Id,tbl_ogrenci.Ad ograd,tbl_ogrenci.Soyad ogrsoyad,tbl_ders.Ad dersad,tbl_notlar.[Not] from tbl_notlar inner join tbl_ogrenci on tbl_notlar.Ogrenci_id=tbl_ogrenci.Id inner join tbl_ders on tbl_notlar.Ders_id=tbl_ders.Id", vt22.conAc());
            SqlDataReader dr22 = cmd22.ExecuteReader();
            while (dr22.Read())
            {
                ListViewItem item = new ListViewItem(dr22["Id"].ToString());
                item.SubItems.Add(dr22["ograd"].ToString());
                item.SubItems.Add(dr22["ogrsoyad"].ToString());
                item.SubItems.Add(dr22["dersad"].ToString());
                item.SubItems.Add(dr22["Not"].ToString());
                mlvnot.Items.Add(item);
            }
            vt22.conKapa();


        }
        public void dersMLV()
        {
            mlders.Items.Clear();
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
        public void verilenDerslerMLV()
        {
            mlvVerilenDersler.Items.Clear();
            Veritabani vt = new Veritabani();
            SqlCommand cmd = new SqlCommand("select tbl_verilenDersler.Id,tbl_ogretmen.Id ogrid,tbl_ogretmen.Ad,tbl_ogretmen.Soyad,tbl_ders.Id dersid,tbl_ders.Ad dersad from tbl_verilenDersler inner join tbl_ders on tbl_verilenDersler.Ders_id=tbl_ders.Id inner join tbl_ogretmen on tbl_verilenDersler.Ogretmen_id=tbl_ogretmen.Id", vt.conAc());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem item = new ListViewItem(dr["Id"].ToString());
                item.SubItems.Add(dr["ogrid"].ToString());
                item.SubItems.Add(dr["Ad"].ToString());
                item.SubItems.Add(dr["Soyad"].ToString());
                item.SubItems.Add(dr["dersid"].ToString());
                item.SubItems.Add(dr["dersad"].ToString());
                mlvVerilenDersler.Items.Add(item);
            }
            vt.conKapa();

            mldpogrvd.Items.Clear();
            Veritabani vt2 = new Veritabani();
            SqlCommand cmd2 = new SqlCommand("Select * From tbl_ogretmen", vt2.conAc());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                mldpogrvd.DisplayMember = "Text";
                mldpogrvd.ValueMember = "Value";
                mldpogrvd.Items.Add(new { Text = dr2["Ad"].ToString() + " " + dr2["Soyad"].ToString(), Value = dr2["Id"].ToString() });
            }
            vt2.conKapa();


            mldpdersvd.Items.Clear();
            Veritabani vt3 = new Veritabani();
            SqlCommand cmd3 = new SqlCommand("Select * From tbl_ders", vt3.conAc());
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                mldpdersvd.DisplayMember = "Text";
                mldpdersvd.ValueMember = "Value";
                mldpdersvd.Items.Add(new { Text = dr3["Ad"].ToString(), Value = dr3["Id"].ToString() });
            }
            vt2.conKapa();


        }

        public void alinanDerslerMLV()
        {
            mlvAlinanDersler.Items.Clear();
            Veritabani vt = new Veritabani();
            SqlCommand cmd = new SqlCommand("select tbl_alinanDersler.Id,tbl_ogrenci.Id ogrid,tbl_ogrenci.Ad ograd,tbl_ogrenci.Soyad ogrsoyad,tbl_ders.Id dersid,tbl_ders.Ad dersadi from tbl_alinanDersler inner join tbl_ders on tbl_alinanDersler.Ders_id=tbl_ders.Id inner join tbl_ogrenci on tbl_alinanDersler.Ogrenci_id=tbl_ogrenci.Id", vt.conAc());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem item = new ListViewItem(dr["Id"].ToString());
                item.SubItems.Add(dr["ogrid"].ToString());
                item.SubItems.Add(dr["ograd"].ToString());
                item.SubItems.Add(dr["ogrsoyad"].ToString());
                item.SubItems.Add(dr["dersid"].ToString());
                item.SubItems.Add(dr["dersadi"].ToString());
                mlvAlinanDersler.Items.Add(item);
            }
            vt.conKapa();

            mldpograd.Items.Clear();
            Veritabani vt2 = new Veritabani();
            SqlCommand cmd2 = new SqlCommand("Select * From tbl_ogrenci", vt2.conAc());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                mldpograd.DisplayMember = "Text";
                mldpograd.ValueMember = "Value";
                mldpograd.Items.Add(new { Text = dr2["Ad"].ToString() + " " + dr2["Soyad"].ToString(), Value = dr2["Id"].ToString() });
            }
            vt2.conKapa();

            mldpdersad.Items.Clear();
            Veritabani vt3 = new Veritabani();
            SqlCommand cmd3 = new SqlCommand("Select * From tbl_ders", vt3.conAc());
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                mldpdersad.DisplayMember = "Text";
                mldpdersad.ValueMember = "Value";
                mldpdersad.Items.Add(new { Text = dr3["Ad"].ToString(), Value = dr3["Id"].ToString() });
            }
            vt2.conKapa();


        }
        public void ogretmenMLV()
        {
            mlOgretmen.Items.Clear();
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
                vt.conKapa();
            }
            else
            {
                mlblmessage.Text = "Ad Ve Soyadı Doldurunuz";
            }
            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();

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
                vt.conKapa();
            }
            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();
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
                vt.conKapa();
            }
            else
            {
                mlblmessage.Text = "Tüm Alanları Doldurunuz";
            }
            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();
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
                vt.conKapa();
            }
            else
            {
                mlblalertders.Text = "Ders Adı Giriniz";
            }
            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();
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
                vt.conKapa();
            }
            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();
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
                vt.conKapa();
            }
            else
            {
                mlblalertders.Text = "Tüm Alanları Doldurunuz";
            }
            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();
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

        private void mbtnogretmentemizle_Click(object sender, EventArgs e)
        {
            mtxtogretmenid.Text = "";
            mtxtogretmenadi.Text = "";
            mtxtogretmensoyadi.Text = "";
        }

        private void mbtnekleogretmen_Click(object sender, EventArgs e)
        {
            if (mtxtogretmenadi.Text != "" && mtxtogretmensoyadi.Text != "")
            {
                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("insert into tbl_ogretmen (ad,soyad) values(@ad,@soyad)", vt.conAc());
                cmd.Parameters.AddWithValue("ad", mtxtogretmenadi.Text);
                cmd.Parameters.AddWithValue("soyad", mtxtogretmensoyadi.Text);
                int durum = cmd.ExecuteNonQuery();
                if (durum == 1)
                {
                    mlblogretmen.Text = "Başarılı";
                }
                else
                {
                    mlblogretmen.Text = "Hata";
                }
                mlOgretmen.Items.Clear();
                vt.conKapa();
            }
            else
            {
                mlblogretmen.Text = "Ad Ve Soyadı Doldurunuz";
            }
            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();
        }

        private void mbtnsilogretmen_Click(object sender, EventArgs e)
        {
            if (mtxtogretmenid.Text != "")
            {
                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("delete from tbl_ogretmen where Id=@id", vt.conAc());
                cmd.Parameters.AddWithValue("id", mtxtogretmenid.Text);
                int durum = cmd.ExecuteNonQuery();
                if (durum == 1)
                {
                    mlblogretmen.Text = "Başarılı";
                }
                else
                {
                    mlblogretmen.Text = "Hata";
                }
                mlOgretmen.Items.Clear();
                vt.conKapa();
            }
            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();
        }

        private void mbtnogretmenguncelle_Click(object sender, EventArgs e)
        {
            if (mtxtogretmenadi.Text != "" && mtxtogretmensoyadi.Text != "")
            {
                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("update tbl_ogretmen set ad=@ad,soyad=@soyad where Id=@id", vt.conAc());
                cmd.Parameters.AddWithValue("ad", mtxtogretmenadi.Text);
                cmd.Parameters.AddWithValue("soyad", mtxtogretmensoyadi.Text);
                cmd.Parameters.AddWithValue("id", mtxtogretmenid.Text);
                int durum = cmd.ExecuteNonQuery();
                if (durum == 1)
                {
                    mlblogretmen.Text = "Başarılı";
                }
                else
                {
                    mlblogretmen.Text = "Hata";
                }
                mlOgretmen.Items.Clear();
                vt.conKapa();
            }
            else
            {
                mlblogretmen.Text = "Tüm Alanları Doldurunuz";
            }
            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();
        }

        private void mbtnogretmendetay_Click(object sender, EventArgs e)
        {
            if (mbtnogretmendetay.Text == "Detay" && mtxtogretmenid.Text != "")
            {
                mlogretmendetay.Visible = true;
                mlOgretmen.Visible = false;

                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("select tbl_ders.Ad DersAdi,tbl_ogrenci.Ad,tbl_ogrenci.Soyad from tbl_ogrenci inner join tbl_alinanDersler on tbl_alinanDersler.Ogrenci_id=tbl_ogrenci.Id inner join tbl_ders on tbl_ders.Id=tbl_alinanDersler.Ders_id inner join tbl_verilenDersler on tbl_ders.Id=tbl_verilenDersler.Ders_id inner join tbl_ogretmen on tbl_verilenDersler.Ogretmen_id=tbl_ogretmen.Id where tbl_ogretmen.Id=@id", vt.conAc());
                cmd.Parameters.AddWithValue("id", mtxtogretmenid.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item2 = new ListViewItem(dr["DersAdi"].ToString());
                    item2.SubItems.Add(dr["Ad"].ToString());
                    item2.SubItems.Add(dr["Soyad"].ToString());
                    mlogretmendetay.Items.Add(item2);
                }
                vt.conKapa();
                mbtnogretmendetay.Text = "X";
            }
            else if (mbtnogretmendetay.Text == "X")
            {
                mlogretmendetay.Items.Clear();
                mlogretmendetay.Visible = false;
                mlOgretmen.Visible = true;
                mlOgretmen.Items.Clear();
                ogretmenMLV();
                mbtnogretmendetay.Text = "Detay";
            }
        }

        private void mbtneklevd_Click(object sender, EventArgs e)
        {
            dynamic item = mldpogrvd.Items[mldpogrvd.SelectedIndex];
            var itemValue = item.Value;
            var itemText = item.Text;

            dynamic item2 = mldpdersvd.Items[mldpdersvd.SelectedIndex];
            var itemValue2 = item2.Value;
            var itemText2 = item2.Text;

            Veritabani vt = new Veritabani();
            SqlCommand cmd = new SqlCommand("insert into tbl_verilenDersler (Ogretmen_id,Ders_id) values(@Ogretmen_id,@Ders_id)", vt.conAc());
            cmd.Parameters.AddWithValue("Ogretmen_id", Convert.ToInt32(itemValue));
            cmd.Parameters.AddWithValue("Ders_id", Convert.ToInt32(itemValue2));
            int durum = cmd.ExecuteNonQuery();
            if (durum == 1)
            {
                lblderssecim.Text = "Başarılı";
            }
            else
            {
                lblderssecim.Text = "Hata";
            }
            mlvVerilenDersler.Items.Clear();
            vt.conKapa();

            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();

        }

        private void mlvVerilenDersler_MouseClick(object sender, MouseEventArgs e)
        {
            mtxtidverilenders.Text = mlvVerilenDersler.SelectedItems[0].SubItems[0].Text;
        }

        private void mbtnsilvd_Click(object sender, EventArgs e)
        {
            if (mtxtidverilenders.Text != "")
            {
                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("delete from tbl_verilenDersler where Id=@id", vt.conAc());
                cmd.Parameters.AddWithValue("id", mtxtidverilenders.Text);
                int durum = cmd.ExecuteNonQuery();
                if (durum == 1)
                {
                    lblderssecim.Text = "Başarılı";
                }
                else
                {
                    lblderssecim.Text = "Hata";
                }
                mlvVerilenDersler.Items.Clear();
                vt.conKapa();
            }
            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();
        }

        private void mlvAlinanDersler_MouseClick(object sender, MouseEventArgs e)
        {
            mtxtidad.Text = mlvAlinanDersler.SelectedItems[0].SubItems[0].Text;
        }

        private void mbtneklead_Click(object sender, EventArgs e)
        {
            dynamic item = mldpograd.Items[mldpograd.SelectedIndex];
            var itemValue = item.Value;
            var itemText = item.Text;

            dynamic item2 = mldpdersad.Items[mldpdersad.SelectedIndex];
            var itemValue2 = item2.Value;
            var itemText2 = item2.Text;

            Veritabani vt = new Veritabani();
            SqlCommand cmd = new SqlCommand("insert into tbl_alinanDersler (Ogrenci_id,Ders_id) values(@Ogrenci_id,@Ders_id)", vt.conAc());
            cmd.Parameters.AddWithValue("Ogrenci_id", Convert.ToInt32(itemValue));
            cmd.Parameters.AddWithValue("Ders_id", Convert.ToInt32(itemValue2));
            int durum = cmd.ExecuteNonQuery();
            if (durum == 1)
            {
                lblderssecimad.Text = "Başarılı";
            }
            else
            {
                lblderssecimad.Text = "Hata";
            }
            mlvAlinanDersler.Items.Clear();
            vt.conKapa();

            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();

        }

        private void mbtnsilad_Click(object sender, EventArgs e)
        {
            if (mtxtidad.Text != "")
            {
                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("delete from tbl_alinanDersler where Id=@id", vt.conAc());
                cmd.Parameters.AddWithValue("id", mtxtidad.Text);
                int durum = cmd.ExecuteNonQuery();
                if (durum == 1)
                {
                    lblderssecimad.Text = "Başarılı";
                }
                else
                {
                    lblderssecimad.Text = "Hata";
                }
                mlvAlinanDersler.Items.Clear();
                vt.conKapa();
            }
            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();
        }

        private void mlvnot_MouseClick(object sender, MouseEventArgs e)
        {
            mtxtidnot.Text = mlvnot.SelectedItems[0].SubItems[0].Text;
            mtxtnotogre.Text = mlvnot.SelectedItems[0].SubItems[4].Text;
        }

        private void mbtnsilogrnot_Click(object sender, EventArgs e)
        {

            if (mtxtidnot.Text != "")
            {
                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("delete from tbl_notlar where Id=@id", vt.conAc());
                cmd.Parameters.AddWithValue("id", mtxtidnot.Text);
                int durum = cmd.ExecuteNonQuery();
                if (durum == 1)
                {
                    mlblogrnot.Text = "Başarılı";
                }
                else
                {
                    mlblogrnot.Text = "Hata";
                }
                mlvnot.Items.Clear();
                vt.conKapa();
            }
            ogrenciMLV();
            dersMLV();
            ogretmenMLV();
            verilenDerslerMLV();
            alinanDerslerMLV();
        }

        private void mbtnekleogrnot_Click(object sender, EventArgs e)
        {
            try
            {
                dynamic item = mdpdersogr.Items[mdpdersogr.SelectedIndex];
                var itemValue = item.Value;
                var itemText = item.Text;

                dynamic item2 = mdpogradisoyadi.Items[mdpogradisoyadi.SelectedIndex];
                var itemValue2 = item2.Value;
                var itemText2 = item2.Text;


                Veritabani vt = new Veritabani();
                SqlCommand cmd = new SqlCommand("insert into tbl_notlar (Ogrenci_id,Ders_id,[Not]) values(@Ogrenci_id,@Ders_id,@Not)", vt.conAc());
                cmd.Parameters.AddWithValue("Ogrenci_id", Convert.ToInt32(itemValue2));
                cmd.Parameters.AddWithValue("Ders_id", Convert.ToInt32(itemValue));
                cmd.Parameters.AddWithValue("Not", mtxtnotogre.Text);
                int durum = cmd.ExecuteNonQuery();
                if (durum == 1)
                {
                    mlblogrnot.Text = "Başarılı";
                }
                else
                {
                    mlblogrnot.Text = "Hata";
                }
                mlvnot.Items.Clear();
                vt.conKapa();

                ogrenciMLV();
                dersMLV();
                ogretmenMLV();
                verilenDerslerMLV();
                alinanDerslerMLV();
            }
            catch (Exception)
            {

                mlblogrnot.Text = "Hata";
            }

        }

        private void mdpogradisoyadi_SelectedIndexChanged(object sender, EventArgs e)
        {

            dynamic item2 = mdpogradisoyadi.Items[mdpogradisoyadi.SelectedIndex];
            var itemValue2 = item2.Value;
            var itemText2 = item2.Text;

            if (true)
            {
                mdpdersogr.Items.Clear();
                Veritabani vt3 = new Veritabani();
                SqlCommand cmd3 = new SqlCommand("select tbl_ders.Id dersid,tbl_ders.Ad dersadi from tbl_alinanDersler inner join tbl_ders on tbl_alinanDersler.Ders_id=tbl_ders.Id inner join tbl_ogrenci on tbl_alinanDersler.Ogrenci_id=tbl_ogrenci.Id where tbl_ogrenci.Id=@id", vt3.conAc());
                cmd3.Parameters.AddWithValue("id", itemValue2);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    mdpdersogr.DisplayMember = "Text";
                    mdpdersogr.ValueMember = "Value";
                    mdpdersogr.Items.Add(new { Text = dr3["dersadi"].ToString(), Value = dr3["dersid"].ToString() });
                }
                vt3.conKapa();
            }

        }
    }
}
