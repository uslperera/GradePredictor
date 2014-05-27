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
    public partial class RegisterCourse : Form
    {
        private Student student;
        private Label labelCName;
        public RegisterCourse(Student student,Label cname)
        {
            InitializeComponent();

            this.student = student;
            this.labelCName = cname;
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            //Set the course
            this.student.CourseName = textBoxCName.Text;
            //Set the text in Label
            labelCName.Text = textBoxCName.Text;
            this.Hide();
        }

    }
}
//__________________________________END__________________________________\\