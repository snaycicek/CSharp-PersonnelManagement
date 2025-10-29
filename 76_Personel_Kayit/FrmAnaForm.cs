using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;    //SQL kodları için gerekli kütüphane

namespace _76_Personel_Kayit
{
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }

        //global alanda database ile bağlantı sağlıyoruz

        SqlConnection baglanti = new SqlConnection("Data Source=PCEF;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        void temizle()
        {
            Txtid.Text = null;
            TxtAd.Text = null;
            TxtSoyad.Text = null;
            TxtMeslek.Text = null;
            MskMaas.Text = null;
            CmbSehir.Text = null;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            TxtAd.Focus();//imleci ada odakladık
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'personelVeriTabaniDataSet.Tbl_Personel' tablosuna veri yükler.
            // Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
           this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);


        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open(); //bağlantıyı açtık


            //sql kodlarını buraya yazacağız, parametrelerle ilişkilendireceğiz
            SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Personel (PerAd, PerSoyad, PerSehir, PerMaas, PerMeslek, PerDurum) VALUES (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);

            komut.Parameters.AddWithValue("@p1", TxtAd.Text);   //txtad dan gelen veriyi p1 parametresine ata
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", CmbSehir.Text);
            komut.Parameters.AddWithValue("@p4", MskMaas.Text);
            komut.Parameters.AddWithValue("@p5", TxtMeslek.Text);
            komut.Parameters.AddWithValue("@p6", LblCinsiyet.Text);
            komut.ExecuteNonQuery(); //sorguyu çalıştır

            baglanti.Close(); //bağlantıyı kapattık
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                LblCinsiyet.Text = "True";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
                LblCinsiyet.Text = "False";
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //data griti secilen hücreleri icerisinde sıfırıncı hucrenin satır indeksini secilen degiskenine ata
            //hücreye çitf tıklandığında secilen degiskenine atadık

            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            //secilen hucrenin sıfırıncı sutunun satır indexi, o satırın indexindeki veriyi index txt ye atadık

            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbSehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskMaas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            LblCinsiyet.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtMeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void LblCinsiyet_TextChanged(object sender, EventArgs e)
        {
            if (LblCinsiyet.Text == "True")
                radioButton1.Checked = true;
            if (LblCinsiyet.Text == "False")
                radioButton2.Checked = true;
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            //her sql cümleciğinde bağlantı acılır ve kapatılır
            baglanti.Open();

            SqlCommand komutsil = new SqlCommand("Delete From Tbl_Personel Where Perid=@k1", baglanti);
            komutsil.Parameters.AddWithValue("@k1", Txtid.Text);
            komutsil.ExecuteNonQuery(); //sql komutlarını çalıstır

            baglanti.Close();
            MessageBox.Show("Kayıt silindi");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutguncelle = new SqlCommand("UPDATE Tbl_Personel Set PerAd= @a1, PerSoyad= @a2, PerSehir= @a3, PerMaas= @a4, PerMeslek= @a5, PerDurum= @a6 WHERE Perid=@a7", baglanti);
            komutguncelle.Parameters.AddWithValue("@a1", TxtAd.Text);
            komutguncelle.Parameters.AddWithValue("@a2", TxtSoyad.Text);
            komutguncelle.Parameters.AddWithValue("@a3", CmbSehir.Text);
            komutguncelle.Parameters.AddWithValue("@a4", MskMaas.Text);
            komutguncelle.Parameters.AddWithValue("@a5", TxtMeslek.Text);
            komutguncelle.Parameters.AddWithValue("@a6", LblCinsiyet.Text);
            komutguncelle.Parameters.AddWithValue("@a7", Txtid.Text);
            komutguncelle.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Personel Bilgisi Güncellendi");
        }

        private void BtnIstatistik_Click(object sender, EventArgs e)
        {
            Frmistatistik frmistatistik = new Frmistatistik();
            frmistatistik.Show();
        }

     
        private void BtnGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler frmg = new FrmGrafikler();

            frmg.Show();
        }

        private void LblCinsiyet_Click(object sender, EventArgs e)
        {

        }
    }
}
