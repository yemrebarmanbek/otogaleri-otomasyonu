using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Otogaleri_Otomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.OleDb.12.0;Data Source=" + Application.StartupPath + "\\otogaleri.accdb;Persist Security Info=False");

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Green;
            kayitlari_listele();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // marka comboboxı
        {
            comboBox2.Items.Clear(); // her yeni marka ifadesine göre model combocoxını temizliyoruz
            string marka = comboBox1.SelectedItem.ToString();

            if (marka == "Toyota")
            {
                string[] model = { "Auris", "Yaris", "Corolla" };
                comboBox2.Items.AddRange(model);
            }
            if (marka == "Honda")
            {
                string[] model = { "Cıvıc", "Accord" };
                comboBox2.Items.AddRange(model);
            }
            if (marka == "Opel")
            {
                string[] model = { "Astra", "Vectra", "Corsa" };
                comboBox2.Items.AddRange(model);
            }
        }

        private void kayitlari_listele()  // her durumda acces verisini data gride yansıtmak için metot kullanırız
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter liste = new OleDbDataAdapter("select *from araclar", baglantim);
                DataSet hafiza = new DataSet();
                liste.Fill(hafiza);  // access verilerini formdaki veri tablosuna attık fill doldur anlamına geliyor
                dataGridView1.DataSource = hafiza.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamesaj)
            {
                MessageBox.Show(hatamesaj.Message);
                baglantim.Close();


            }

        }

        private void button2_Click(object sender, EventArgs e) //kaydet butonu
        {
            // bu buton ile form da girilen bilgileri acces veritabanına kaydedeceğiz
            try
            {
                baglantim.Open();
                OleDbDataAdapter ekle_komutu = new OleDbDataAdapter("insert into araclar (ruhsatno,marka,model,yakit_tipi,kasa_tipi,kilometre,fiyat) values('" + textBox1.Text + "','" + comboBox1.SelectedItem.ToString() + "','" + comboBox2.SelectedItem.ToString() + "','" + comboBox3.SelectedItem.ToString() + "','" + comboBox4.SelectedItem.ToString() + "','" + textBox2.Text + "','" + textBox3.Text + "')", baglantim);
                DataSet hafiza = new DataSet();
                ekle_komutu.Fill(hafiza);
                baglantim.Close();
                MessageBox.Show("araç veri tabanına eklendi");
                textBox1.Clear(); textBox2.Clear(); textBox3.Clear();
                kayitlari_listele();
            }
            catch (Exception hatamesaj)
            {
                MessageBox.Show(hatamesaj.Message);
                baglantim.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e) // sil komutu
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter sil_komutu = new OleDbDataAdapter("delete from araclar where ruhsatno='" + textBox1.Text + "'", baglantim);
                DataSet hafiza = new DataSet();
                sil_komutu.Fill(hafiza);
                baglantim.Close();
                MessageBox.Show("araç veri tabanından silindi");
                kayitlari_listele();

            }
            catch (Exception hatamesaj)
            {
                MessageBox.Show(hatamesaj.Message);
                baglantim.Close();
                
            }
        }

        private void button4_Click(object sender, EventArgs e) // fiyat guncelle butonu
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter guncelle = new OleDbDataAdapter("update araclar set fiyat='" + textBox3.Text + "' where ruhsatno='" + textBox1.Text + "'", baglantim);
                DataSet hafiza = new DataSet();
                guncelle.Fill(hafiza);
                baglantim.Close();
                MessageBox.Show("araç fiyatları güncellendi");
                kayitlari_listele();


            }
            catch (Exception mesaj)
            {
                MessageBox.Show(mesaj.Message);
                baglantim.Close();
                
            }
        }

        private void button1_Click(object sender, EventArgs e) // ara butonu
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter ara = new OleDbDataAdapter("select *from araclar where ruhsatno='" + textBox1.Text + "'", baglantim);
                DataSet hafiza = new DataSet();
                ara.Fill(hafiza);
                dataGridView1.DataSource = hafiza.Tables[0];
                baglantim.Close();

            }
            catch (Exception mesaj)
            {

                MessageBox.Show(mesaj.Message);
                baglantim.Close();

            }
        }
    }
}

          