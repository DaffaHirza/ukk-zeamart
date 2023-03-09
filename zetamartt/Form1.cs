using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zetamartt
{
    public partial class Form1 : Form
    {
        Form2 form;

        public Form1()
        {
            InitializeComponent();
            form = new Form2(this);

            dataGridView.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);
        }
        public void Display()
        {
            Dbzeamart.DisplayandSearchZeamart("SELECT ID, nama_barang, kode_barang, stok, harga, expired, image FROM zeamart_tbl", dataGridView);
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            form.Clear();
            form.Addsave();
            form.ShowDialog();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Display();  
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Dbzeamart.DisplayandSearchZeamart("SELECT ID, nama_barang, kode_barang, stok, harga, expired FROM zeamart_tbl WHERE nama_barang LIKE '%"+ txtSearch.Text +"%'", dataGridView);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0)
            {
                form.Clear();
                form.id = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                form.nama = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                form.kode = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                form.stok = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                form.harga = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                form.expired = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                form.UpdateInfo();
                form.ShowDialog();
                return;
            } 
            if(e.ColumnIndex == 1)
            {
                if (MessageBox.Show("Apakah kamu yakin menghapus?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Dbzeamart.DeleteZeamart(dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Display(); 
                }
                return;
            }
        }
        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Kosongkan pesan kesalahan DataGridView untuk menghindari pesan kesalahan default
            e.ThrowException = false;

            // Tampilkan pesan kesalahan
            MessageBox.Show("Kesalahan: " + e.Exception.Message);
        }
    }
}
