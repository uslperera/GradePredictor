using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GradePredictor.Models;
namespace GradePredictor.Views
{
    public partial class MainForm : Form
    {
        private Student student;
        public MainForm(Student student)
        {
            InitializeComponent();

            this.student = student;
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            new RegisterCourse(student,labelCName).ShowDialog();

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(1);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            //dataGridView2.Rows.Add(1);
        }

       
    }
}
