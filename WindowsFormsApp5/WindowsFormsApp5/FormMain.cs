using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp5
{
    public partial class FormMain : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;

        private DataSet ds = new DataSet();
        private string alamat, query;
        public FormMain()
        {
            alamat = "server=localhost; database=vispro; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text != "")
                {
                    query = string.Format("select * from pengguna where username = '{0}'", txtUsername.Text);
                    ds.Clear();
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    perintah.ExecuteNonQuery();
                    adapter.Fill(ds);
                    koneksi.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow kolom in ds.Tables[0].Rows)
                        {
                            txtUserID.Text = kolom["id_pengguna"].ToString();
                            txtPassword.Text = kolom["password"].ToString();
                            txtName.Text = kolom["nama_pengguna"].ToString();
                            CBLevel.Text = kolom["level"].ToString();

                        }
                        txtUsername.Enabled = false;
                        dataGridView.DataSource = ds.Tables[0];
                        btnSave.Enabled = false;
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                        btnSearch.Enabled = false;
                        btnClear.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No Data");
                        FormMain_Load(null, null);
                    }

                }
                else
                {
                    MessageBox.Show("There's No Data to Show");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text != "" && txtPassword.Text != "" && txtName.Text != "")
                {

                    query = string.Format("insert into pengguna  values ('{0}','{1}','{2}','{3}','{4}');", txtUserID.Text, txtUsername.Text, txtPassword.Text, txtName.Text, CBLevel.Text);


                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    int res = perintah.ExecuteNonQuery();
                    koneksi.Close();
                    if (res == 1)
                    {
                        MessageBox.Show("Data Inserted");
                        FormMain_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Failed to Insert Data");
                    }
                }
                else
                {
                    MessageBox.Show("Incomplete Data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != "" && txtName.Text != "" && txtUsername.Text != "" && txtUserID.Text != "")
                {

                    query = string.Format("update pengguna set password = '{0}', nama_pengguna = '{1}', level = '{2}' where id_pengguna = '{3}'", txtPassword.Text, txtName.Text, CBLevel.Text, txtUserID.Text);


                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    int res = perintah.ExecuteNonQuery();
                    koneksi.Close();
                    if (res == 1)
                    {
                        MessageBox.Show("Update Data Succeed");
                        FormMain_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Failed");
                    }
                }
                else
                {
                    MessageBox.Show("Incomplete Data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserID.Text != "")
                {
                    if (MessageBox.Show("Are You Sure Delete This Data??", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        query = string.Format("Delete from pengguna where id_pengguna = '{0}'", txtUserID.Text);
                        ds.Clear();
                        koneksi.Open();
                        perintah = new MySqlCommand(query, koneksi);
                        adapter = new MySqlDataAdapter(perintah);
                        int res = perintah.ExecuteNonQuery();
                        koneksi.Close();
                        if (res == 1)
                        {
                            MessageBox.Show("Deleted Data Succeed");
                        }
                        else
                        {
                            MessageBox.Show("Failed Delete data");
                        }
                    }
                    FormMain_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Data Yang Anda Pilih Tidak Ada !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                FormMain_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = string.Format("select * from pengguna");
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                ds.Clear();
                adapter.Fill(ds);
                koneksi.Close();
                dataGridView.DataSource = ds.Tables[0];
                dataGridView.Columns[0].Width = 100;
                dataGridView.Columns[0].HeaderText = "ID Pengguna";
                dataGridView.Columns[1].Width = 150;
                dataGridView.Columns[1].HeaderText = "Username";
                dataGridView.Columns[2].Width = 120;
                dataGridView.Columns[2].HeaderText = "Password";
                dataGridView.Columns[3].Width = 120;
                dataGridView.Columns[3].HeaderText = "Nama Pengguna";
                dataGridView.Columns[4].Width = 120;
                dataGridView.Columns[4].HeaderText = "Level";

                txtUserID.Clear();
                txtName.Clear();
                txtPassword.Clear();
                txtUsername.Clear();
                txtUserID.Focus();
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnClear.Enabled = false;
                btnSave.Enabled = true;
                btnSearch.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
