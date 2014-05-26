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
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            if(textBoxStudentID.Text.Length>0 & textBoxStudentName.Text.Length>0)
            {
                Student student = new Student();
                student.StudentID = int.Parse(textBoxStudentID.Text);
                student.StudentName = textBoxStudentName.Text;

                new MainForm(student).Show();

                this.Hide();
            }
        }

        private void textBoxStudentID_KeyPress(object sender, KeyPressEventArgs e)
        {
           if(!char.IsDigit(e.KeyChar) && e.KeyChar!=8)
           {
               e.Handled = true;
           }
        }

    }
}
