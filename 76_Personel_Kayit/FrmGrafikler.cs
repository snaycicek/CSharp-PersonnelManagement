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
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }


        SqlConnection baglanti = new SqlConnection("Data Source=PCEF;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            //SEHİR SAYISI GRAFIGI
            baglanti.Open();
            SqlCommand komutg1 = new SqlCommand("SELECT PerSehir, Count(*) FROM Tbl_Personel Group By PerSehir",baglanti);
            //bu sql cümleciği sonucunda geriye dönenler;
            //1. sutun - index[0] =>   PerSehir
            //2. sutun - index[1] =>   PerSehir değerlerinin adet sayısı
            SqlDataReader dr1 = komutg1.ExecuteReader();

            while (dr1.Read())
            {
                chart1.Series["Sehirler"].Points.AddXY(dr1[0], dr1[1]);
                //adı "Sehirler" olan grafiğin, //xy kordinatına ekle, datareaderin okuduğu index 0 ve 1 i
                //x => dr1[0]   y =>dr[1]
            }
            baglanti.Close();


            //MESLEK - MAAS GRAFIGI
            //her bir sehirdekilerin mesleğe bağlı ortalama maaşları
            baglanti.Open();
            SqlCommand komutg2 = new SqlCommand("SELECT PerMeslek, Avg(PerMaas) From Tbl_Personel Group By PerMeslek", baglanti);
            SqlDataReader dr2 = komutg2.ExecuteReader();

            while (dr2.Read())
            {
                chart2.Series["Meslek-Maas"].Points.AddXY(dr2[0], dr2[1]);
            }
            baglanti.Close();

        }
    }
}
