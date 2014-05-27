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
using GradePredictor.Models;

namespace GradePredictor.Views
{
    public partial class ModuleForm : Form
    {

        private Student student;
        private LevelType lvl;
        public ModuleForm(Student st, LevelType l)
        {
            InitializeComponent();
            this.student = st;
            this.lvl = l;
        }

        private void ModuleForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Module module = new Module();
            if (textBoxMCode.Text.Equals("") || textBoxMName.Text.Equals("") || textBoxCredits.Text.Equals(""))
            {
                MessageBox.Show("One or more fields are empty");
            }
            else
            {
                module.Code = textBoxMCode.Text;
                module.Name = textBoxMName.Text;
                module.Credits = Int32.Parse(textBoxCredits.Text);
                this.dataGridView1.Rows.Add(1);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            student.Levels[0].Name = lvl;
            student.Levels[0].Credits = Int32.Parse(textBoxCredits.Text);
        }
    }
}
