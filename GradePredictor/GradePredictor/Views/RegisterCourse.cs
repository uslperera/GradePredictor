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
            this.student.CourseName = textBoxCName.Text;
            labelCName.Text = textBoxCName.Text;
            this.Hide();
        }

        private void RegisterCourse_Load(object sender, EventArgs e)
        {

        }

    }
}
