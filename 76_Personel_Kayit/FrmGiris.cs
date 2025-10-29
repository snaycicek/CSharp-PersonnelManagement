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
using System.Data.OleDb;

namespace _76_Personel_Kayit
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }
        //Data Source=PCEF;Initial Catalog=PersonelVeriTabani;Integrated Security=True
        //+ Application.StartupPath +

        // 

        SqlConnection baglanti = new SqlConnection("Data Source=PCEF;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM Tbl_Yonetici Where KullaniciAd = @p1 AND Sifre = @p2", baglanti);

            komut.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);

            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmAnaForm frmana= new FrmAnaForm();
                frmana.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatali kullanici adi ya da sifre");
            }
            baglanti.Close();
        }

      
    }
}
