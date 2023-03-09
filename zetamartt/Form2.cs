using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zetamartt
{
    public partial class Form2 : Form
    {
        private readonly Form1 _parent;
        public string id, nama, kode, stok, harga, expired;

        public Form2(Form1 parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=zeamartmysql";
            MySqlConnection con = new MySqlConnection(sql);
            try
            {
                con.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySql Connection! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return con;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Choose Image | *.jpg;.png;.png;.gif;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateInfo()
        {
            lbltext.Text = "Update Barang";
            btnSave.Text = "Update";
            txtNamabarang.Text = nama;
            txtKodebarang.Text = kode;
            txtStok.Text = stok;
            txtHarga.Text = harga;
            txtExpaired.Text = expired;

        }
        public void Addsave()
        {
            lbltext.Text = "Add barang";
            btnSave.Text = "Save";
        }

        public void Clear()
        {
            txtNamabarang.Text = txtKodebarang.Text = txtStok.Text = txtHarga.Text = txtExpaired.Text = String.Empty;
            pictureBox1.Image = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtNamabarang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Masukan nama");
                return;
            }
            if (txtKodebarang.Text.Trim().Length == 0)
            {
                MessageBox.Show("masukan kode barang");
                return;
            }
            if (txtStok.Text.Trim().Length == 0)
            {
                MessageBox.Show("masukan stok");
                return;
            }
            if (txtHarga.Text.Trim().Length == 0)
            {
                MessageBox.Show("masukan harga");
                return;
            }
            if (txtExpaired.Text.Trim().Length == 0)
            {
                MessageBox.Show("masukan expired");
                return;
            }
            if(btnSave.Text == "Save")
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] img = ms.ToArray();

                string sql = "INSERT INTO zeamart_tbl VALUES (NULL, @Zeamartnama_barang, @Zeamartkode_barang, @Zeamartstok, @Zeamartharga, @Zeamartexpired, @Zeamartimage, NULL)";
                MySqlConnection con = GetConnection();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Zeamartnama_barang", MySqlDbType.VarChar).Value = txtNamabarang.Text;
                cmd.Parameters.Add("@Zeamartkode_barang", MySqlDbType.VarChar).Value = txtKodebarang.Text;
                cmd.Parameters.Add("@Zeamartstok", MySqlDbType.VarChar).Value = txtStok.Text;
                cmd.Parameters.Add("@Zeamartharga", MySqlDbType.VarChar).Value = txtHarga.Text;
                cmd.Parameters.Add("@Zeamartexpired", MySqlDbType.VarChar).Value = txtExpaired.Text;
                cmd.Parameters.Add("@Zeamartimage", MySqlDbType.VarChar).Value = img;
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil menambahkan.", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Zeamart tidak ditambahkan. \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            if(btnSave.Text == "Update")
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] img = ms.ToArray();

                string sql = "UPDATE zeamart_tbl SET nama_barang = @Zeamartnama_barang, kode_barang = @Zeamartkode_barang, stok = @Zeamartstok, harga = @Zeamartharga, expired = @Zeamartexpired, image = @Zeamartimage WHERE ID = @ZeamartID";
                MySqlConnection con = GetConnection();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd); 
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ZeamartID", MySqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@Zeamartnama_barang", MySqlDbType.VarChar).Value = txtNamabarang.Text;
                cmd.Parameters.Add("@Zeamartkode_barang", MySqlDbType.VarChar).Value = txtKodebarang.Text;
                cmd.Parameters.Add("@Zeamartstok", MySqlDbType.VarChar).Value = txtStok.Text;
                cmd.Parameters.Add("@Zeamartharga", MySqlDbType.VarChar).Value = txtHarga.Text;
                cmd.Parameters.Add("@Zeamartexpired", MySqlDbType.VarChar).Value = txtExpaired.Text;
                cmd.Parameters.Add("@Zeamartimage", MySqlDbType.Blob).Value = img;
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil mengubah.", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Zeamart tidak menubah. \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            _parent.Display();
        }
    }
}
