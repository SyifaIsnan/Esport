using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esport
{
    internal class Properti
    {

        public static SqlConnection conn()
        {
            return new SqlConnection("Data Source=DESKTOP-18L8S2S;Initial Catalog=EsemkaEsport;Integrated Security=True");
        }

        public static bool validasi(Control.ControlCollection container, TextBox kosong = null)
        {
            foreach(Control c in container)
            {
                if(c is TextBoxBase textBox && string.IsNullOrWhiteSpace(textBox.Text) && textBox != kosong){
                    return true;
                }
            } return false;
        }
    }
}
