using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _76_Personel_Kayit
{
    public partial class Frmistatistik : Form
    {
        public Frmistatistik()
        {
            InitializeComponent();
        }



        SqlConnection baglanti = new SqlConnection("Data Source=PCEF;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        private void Frmistatistik_Load(object sender, EventArgs e)
        {
            //TOPLAM PERSONEL SAYISI
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("SELECT Count(*) FROM Tbl_Personel", baglanti);  //tablodaki tüm satırı sayar
            SqlDataReader dr1 = komut1.ExecuteReader(); //komut1 deki verileri çalıştır ve oku sonra dr1(veriokuyuxcu) e ata //select için sorguyu çalıştırır

            while (dr1.Read())  //dr1 okunduğu sürece //bu döngü kayıt sonuna kadar devam edecek
            {
                LblToplamPersonel.Text = dr1[0].ToString();
                //sütundan başlarsak index 0,1,2 şeklinde devam eder
                //veri tabanındaki 1. sütun indexi 0 dır
            }
            baglanti.Close();


            //EVLİ PERSONEL SAYISI
            baglanti.Open();
            SqlCommand komut2= new SqlCommand("SELECT COUNT(*) FROM Tbl_Personel WHERE Perdurum=1", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader(); //okuyup sql data okuyucusuna ata

            while (dr2.Read())
            {
                LblEvliPersonel.Text = dr2[0].ToString();
            }
            baglanti.Close();



            //BEKAR PERSONEL SAYISI
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("SELECT Count(*) FROM Tbl_Personel WHERE Perdurum=0", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();

            while (dr3.Read())
            {
                LblBekarPersonel.Text = dr3[0].ToString();
            }
            baglanti.Close();


            //SEHİR SAYISI
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("SELECT COUNT(DISTINCT(PerSehir)) FROM Tbl_Personel", baglanti);
            SqlDataReader dr4 = komut4.ExecuteReader();

            while (dr4.Read())
            {
                LblSehirSayisi.Text = dr4[0].ToString();
            }
            baglanti.Close();



            //TOPLAM MAAS
            //sum() toplama yapar
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("SELECT Sum(PerMaas) FROM Tbl_Personel", baglanti);
            SqlDataReader dr5 = komut5.ExecuteReader();

            while (dr5.Read())
            {
                LblToplamMaas.Text = dr5[0].ToString();
            }
            baglanti.Close();


            //ORTALAMA MAAS
            //Avg() ortlalamayı alır
            baglanti.Open();
            SqlCommand komut6 = new SqlCommand("SELECT Avg(PerMaas) FROM Tbl_Personel", baglanti);
            SqlDataReader dr6 = komut6.ExecuteReader();

            while (dr6.Read())
            {
                LblOrtalamaMaas.Text = dr6[0].ToString();
            }
            baglanti.Close();
        }
    }
}
