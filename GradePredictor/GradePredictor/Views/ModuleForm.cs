using GradePredictor.Models;
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
    public partial class ModuleForm : Form
    {

        private Student student;
        public ModuleForm(Student st)
        {
            InitializeComponent();
            this.student = st;
        }

        private void ModuleForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Module module = new Module();
            module.Code = textBoxMCode.Text;
            module.Name = textBoxMName.Text;
            module.Credits = Int32.Parse(textBoxCredits.Text);

        }
    }
}
