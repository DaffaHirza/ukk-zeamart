using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zetamartt
{
    class Dbzeamart
    { 
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=zeamartmysql";
            MySqlConnection con = new MySqlConnection(sql);
            try
            {
                con.Open();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("MySql Connection! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return con;
        }

        public static void AddZeamart(Zeamart zmt)
        {
            string sql = "INSERT INTO zeamart_tbl VALUES (NULL, @Zeamartnama_barang, @Zeamartkode_barang, @Zeamartstok, @Zeamartharga, @Zeamartexpired, @Zeamartimage, NULL)";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Zeamartnama_barang", MySqlDbType.VarChar).Value = zmt.NamaBarang;
            cmd.Parameters.Add("@Zeamartkode_barang", MySqlDbType.VarChar).Value = zmt.KodeBarang;
            cmd.Parameters.Add("@Zeamartstok", MySqlDbType.VarChar).Value = zmt.Stok;
            cmd.Parameters.Add("@Zeamartharga", MySqlDbType.VarChar).Value = zmt.Harga;
            cmd.Parameters.Add("@Zeamartexpired", MySqlDbType.VarChar).Value = zmt.Expired;
            cmd.Parameters.Add("@Zeamartimage", MySqlDbType.VarChar).Value = zmt.Image;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil menambahkan.", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);   
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Zeamart tidak ditambahkan. \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        public static void UpdateZeamart(Zeamart zmt, string id)
        {
            string sql = "UPDATE zeamart_tbl SET nama_barang = @Zeamartnama_barang, kode_barang = @Zeamartkode_barang, stok = @Zeamartstok, harga = @Zeamartharga, expired = @Zeamartexpired, image = @Zeamartimage WHERE ID = @ZeamartID";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ZeamartID", MySqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@Zeamartnama_barang", MySqlDbType.VarChar).Value = zmt.NamaBarang;
            cmd.Parameters.Add("@Zeamartkode_barang", MySqlDbType.VarChar).Value = zmt.KodeBarang;
            cmd.Parameters.Add("@Zeamartstok", MySqlDbType.VarChar).Value = zmt.Stok;
            cmd.Parameters.Add("@Zeamartharga", MySqlDbType.VarChar).Value = zmt.Harga;
            cmd.Parameters.Add("@Zeamartexpired", MySqlDbType.VarChar).Value = zmt.Expired;
            cmd.Parameters.Add("@Zeamartimage", MySqlDbType.Blob).Value = zmt.Image;
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
        public static void DeleteZeamart(string id)
        {
            string sql = "DELETE FROM zeamart_tbl WHERE ID = @ZeamartID";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ZeamartID", MySqlDbType.VarChar).Value = id;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil menghapus.", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Zeamart tidak menghapus. \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        public static void DisplayandSearchZeamart(string query, DataGridView dgv)
        {
            string sql = query;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adp.Fill(tbl);
            dgv.DataSource = tbl;
            con.Close();    
        }
    }
}
