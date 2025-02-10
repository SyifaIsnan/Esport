using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esport
{
    public partial class CreateAcoount : Form
    {
        public CreateAcoount()
        {
            InitializeComponent();
            
        }

        private bool validasi()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Silahkan isi username!");
                return false;
            } 

            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Silahkan pilih gender anda!");
                return false;
            }
            
            if (textBox2.Text != textBox2.Text)
            {
                MessageBox.Show("Mohon masukkan password yang sama!");
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("Silahkan isi password anda!");
            }
            
            if (textBox3.Text == "")
            {
                MessageBox.Show("Silahkan isi password anda!");
            }

            if (textBox2.Text.Length < 6)
            {
                MessageBox.Show("Password harus lebih dari 6 karakter!");            
            }
            return true;
        }

        private void CreateAcoount_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            LoginForm l = new LoginForm();
            l.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var conn = Properti.conn())
            {
                if (validasi())
                {
                    try
                    {
                        SqlCommand cekuser = new SqlCommand("select count(*) from [User] where username = @username", conn);
                        cekuser.CommandType = CommandType.Text;
                        conn.Open();
                        cekuser.Parameters.AddWithValue("@username", textBox1.Text);
                        int username = (int)cekuser.ExecuteScalar();
                        if (username > 0)
                        {
                            MessageBox.Show("Akun sudah terdaftar! Silahkan gunakan username lain");
                            return;
                        }
                        conn.Close();

                        SqlCommand bikin = new SqlCommand("insert into [User] (username , password, birthdate, gender, role, created_at) values (@username , @password, @birthdate, @gender, @role, @created_at)", conn);
                        bikin.CommandType = CommandType.Text;
                        conn.Open();
                        bikin.Parameters.AddWithValue("@username", textBox1.Text);
                        bikin.Parameters.AddWithValue("@password", textBox2.Text);
                        bikin.Parameters.AddWithValue("@birthdate", dateTimePicker1.Value);

                        if (radioButton1.Checked)
                        {
                            bikin.Parameters.AddWithValue("@gender", 1);
                        }else if (radioButton2.Checked)
                        {
                            bikin.Parameters.AddWithValue("@gender", 0);
                        }

                        bikin.Parameters.AddWithValue("@role", 1);
                        bikin.Parameters.AddWithValue("created_at", DateTime.Now);

                        bikin.ExecuteNonQuery();
                        MessageBox.Show("Akun berhasil dibuat! Silahkan login menggunakan akun yang sudah dibuat", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();

                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
