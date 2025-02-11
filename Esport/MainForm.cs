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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            tampildata();

           
        }

        private void tampildata()
        {
            using (var conn = Properti.conn())
            {
                SqlCommand cmd = new SqlCommand("select \r\nschedule.id, concat(HomeTeam.team_name, ' vs ' , AwayTeam.team_name) as Match , schedule.time\r\nfrom [schedule] \r\ninner join [team] as AwayTeam on schedule.away_team_id = AwayTeam.id\r\ninner join [team] as HomeTeam on schedule.home_team_id = HomeTeam.id;\r\n", conn);
                conn.Open();
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);

                DataGridViewButtonColumn b = new DataGridViewButtonColumn();
                b.Text = "Book";
                b.Name = "Book";
                b.UseColumnTextForButtonValue = true;
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["id"].Visible = false;
                b.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                b.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7A2EA8");
                dataGridView1.Columns.Add(b);
                conn.Close();



            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                string id = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                BookForm bookForm = new BookForm(id);
                bookForm.ShowDialog();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyTicket ticket = new MyTicket();
            ticket.ShowDialog();

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                string id = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                BookForm bookForm = new BookForm(id);
                bookForm.ShowDialog();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            var mess = MessageBox.Show("Apakah anda yakin ingin logout?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (mess == DialogResult.Yes)
            {
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();
               
            }
        }
    }
}
