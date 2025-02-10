using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esport
{
    public static class ext
    {
        public static void setup(this DataGridView dataGridView)
        {
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ReadOnly = true;
            dataGridView.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#7A2EA8");
        }
        
    }
}
