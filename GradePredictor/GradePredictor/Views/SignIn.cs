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
    /// <datecreated>27-05-2014</datecreated>
    /// <summary>Signin Form</summary>
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            //Close the application
            this.Close();
        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            //Presence check
            if(textBoxStudentID.Text.Length>0 & textBoxStudentName.Text.Length>0)
            {
                //Creates a new student
                Student student = new Student();
                student.StudentID = int.Parse(textBoxStudentID.Text);
                student.StudentName = textBoxStudentName.Text;
                //Pass the student
                new MainForm(student).Show();

                this.Hide();
            }
        }

        private void textBoxStudentID_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Allow only digits
           if(!char.IsDigit(e.KeyChar) && e.KeyChar!=8)
           {
               e.Handled = true;
           }
        }

    }
}
//__________________________________END__________________________________\\