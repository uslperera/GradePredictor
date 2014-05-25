using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GradePredictor.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            tableLayoutPanel1.RowCount = 8;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tabPageL4_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Label my = new Label();
            my.Text = "AD";
            tableLayoutPanel1.Controls.Add(my,0,0);

            TextBox te = new TextBox();
            
            tableLayoutPanel1.Controls.Add(te, 1, 0);
        }

    }
}
