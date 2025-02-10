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
    public partial class BookForm : Form
    {
        private string scheduleid;

        public BookForm(string scheduleid)
        {
            InitializeComponent();  // ----> Harus paling awal/pertama, karena harus inisialisasi komponen

            this.scheduleid = scheduleid;
            sisatiket();
            tampildata();
            nicknamehometeam();
            nicknameawayteam();
            dataGridView1.setup();
            dataGridView2.setup();
        }

        private void sisatiket()
        {
            using (var conn = Properti.conn())
            {
                //SqlCommand cmd = new SqlCommand("select 60 - sum(total_ticket) from [schedule_detail] where schedule_id = @schedule_id;", conn);
                //cmd.CommandType = CommandType.Text;
                //conn.Open();
                //cmd.Parameters.AddWithValue("@schedule_id", scheduleid);
                //DataTable dt = new DataTable();
                //SqlDataReader dr = cmd.ExecuteReader(); 
                //dt.Load(dr);
                //string sisatiket = dt.Rows[0][0].ToString();
                //label6.Text = sisatiket + "Tickets";
                //conn.Close();

                int totaltiket = 60;
                int booking = Convert.ToInt32(numericUpDown1.Value.ToString());
                int totalBooking = totaltiket - booking;

                label6.Text = totalBooking.ToString() + " Tikects";

            }
        }

        private void nicknameawayteam()
        {
            using (var conn = Properti.conn())
            {
                SqlCommand cmd = new SqlCommand("select player.nick_name from [schedule] \r\ninner join [team] on schedule.away_team_id = team.id \r\ninner join [team_detail] on team.id = team_detail.team_id\r\ninner join [player] on team_detail.player_id = player.id\r\nwhere schedule.id = '" + scheduleid + "'", conn);
                conn.Open();
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();
                dataGridView2.DataSource = dt;
            }
        }

        private void nicknamehometeam()
        {
            using(var conn = Properti.conn())
            {
                SqlCommand cmd = new SqlCommand("select player.nick_name from [schedule] \r\ninner join [team] on schedule.home_team_id = team.id \r\ninner join [team_detail] on team.id = team_detail.team_id\r\ninner join [player] on team_detail.player_id = player.id\r\nwhere schedule.id = '" + scheduleid + "'", conn);
                conn.Open();
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();
                dataGridView1.DataSource = dt;
            }
        }

        private void tampildata()
        {
            using (var conn = Properti.conn())
            {
                SqlCommand cmd = new SqlCommand("select team.team_name from [schedule] inner join [team] on schedule.home_team_id = team.id where schedule.id = '" + scheduleid +"'", conn);
                conn.Open();
                cmd.CommandType = CommandType.Text;        
                DataTable dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                string hometeamname = dt.Rows[0][0].ToString();
                label1.Text = hometeamname;

                SqlCommand cmd2 = new SqlCommand("select team.team_name from [schedule] inner join [team] on schedule.away_team_id = team.id where schedule.id = @scheduleid;", conn);
               // conn.Open();
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.AddWithValue("@scheduleid", scheduleid);
                DataTable dt1 = new DataTable();
                SqlDataReader dr1 = cmd2.ExecuteReader();
                dt1.Load(dr1);
                string awayteamname = dt1.Rows[0][0].ToString() ;
                label2.Text = awayteamname;


                SqlCommand cmd3 = new SqlCommand("select team.company_name from [schedule] inner join [team] on schedule.home_team_id = team.id where schedule.id = '" + scheduleid + "'", conn);               
                cmd3.CommandType = CommandType.Text;
                DataTable dt3 = new DataTable();
                SqlDataReader dr3 = cmd3.ExecuteReader();
                dt3.Load(dr3);
                string companyhome = dt3.Rows[0][0].ToString();
                label7.Text = companyhome;

                SqlCommand cmd4 = new SqlCommand("select team.company_name from [schedule] inner join [team] on schedule.away_team_id = team.id where schedule.id = '" + scheduleid + "'", conn);
                cmd4.CommandType = CommandType.Text;
                DataTable dt4 = new DataTable();
                SqlDataReader dr4 = cmd4.ExecuteReader();
                dt4.Load(dr4);
                string companyaway = dt4.Rows[0][0].ToString();
                label8.Text = companyaway;

            }
        }

        private void BookForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var conn = Properti.conn())
            {
                try
                {


                    SqlCommand cmd = new SqlCommand("insert into [Schedule_detail] (schedule_id, user_id, total_ticket, created_at) values (@schedule_id, @user_id, @total_ticket, @created_at)", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@schedule_id", scheduleid);
                    cmd.Parameters.AddWithValue("@user_id", 1);
                    cmd.Parameters.AddWithValue("@total_ticket", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@created_at", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tiket berhasil dipesan!");
                    sisatiket();
                    clear();
                    conn.Close();
                    
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void clear()
        {
            numericUpDown1.Value = 0;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mf = new MainForm();
            mf.ShowDialog();
        }
    }
}
