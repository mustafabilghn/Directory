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

namespace Rehber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-JQ02U7VO;Initial Catalog=Rehber;Integrated Security=True;");
        string fotografyolu;

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from KISILER", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["FOTOGRAF"].Visible = false;
        }

        void temizle()
        {
            txtad.Text = "";
            txtid.Text = "";
            txtmail.Text = "";
            msktel.Text = "";
            txtsoyad.Text = "";
            pictureBox1.Refresh();
            txtad.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (txtad.Text == "" || txtsoyad.Text == "" || msktel.Text == "" || txtmail.Text == "")
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult cevap = MessageBox.Show("Kişi sisteme kaydedilsin mi?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("Insert into KISILER (AD,SOYAD,TEL,MAIL,FOTOGRAF) values (@p1,@p2,@p3,@p4,@p5)", conn);
                komut.Parameters.AddWithValue("@p1", txtad.Text);
                komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
                komut.Parameters.AddWithValue("@p3", msktel.Text);
                komut.Parameters.AddWithValue("@p4", txtmail.Text);
                komut.Parameters.AddWithValue("@p5", fotografyolu);
                komut.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Kişi sisteme kaydedildi.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Kişi sisteme kaydedilemedi.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtad.Text == "" || txtsoyad.Text == "" || msktel.Text == "" || txtmail.Text == "")
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult cevap = MessageBox.Show("Kişi sistemden silinsin mi?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("Delete from KISILER where ID = @p1", conn);
                komut.Parameters.AddWithValue("@p1", txtid.Text);
                komut.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Kişi sistemden silindi.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Kişi silinemedi.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.ShowDialog();
            fotografyolu = dosya.FileName;
            pictureBox1.ImageLocation = fotografyolu;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            msktel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtmail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand komut = new SqlCommand("Update KISILER set AD = @p1,SOYAD = @p2,TEL = @p3,MAIL = @p4,FOTOGRAF = @p5 where ID = @p6", conn);
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", msktel.Text);
            komut.Parameters.AddWithValue("@p4", txtmail.Text);
            komut.Parameters.AddWithValue("@p5", pictureBox1.ImageLocation);
            komut.Parameters.AddWithValue("@p6", txtid.Text);
            komut.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Bilgiler güncellendi.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}
