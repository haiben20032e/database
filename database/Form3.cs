using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace database
{
    public partial class frm3 : Form
    {
        public frm3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm2 frm2 = new frm2();
            frm2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hanghoa_frm4 frm = new Hanghoa_frm4();
            _ = frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           frm5 frm = new frm5();
            _ = frm.ShowDialog();
        }

        private void frm5_Load(object sender, EventArgs e)
        {

        }

        private void hàngHóaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Hanghoa_frm4 frm = new Hanghoa_frm4();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frm6 frm = new frm6();
            _= frm.ShowDialog();
        }
    }
}
