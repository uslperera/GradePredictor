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
        private Module module;
        private LevelType level;
        private bool updateModule;
        private MainForm mainform;
        public ModuleForm(Student student, LevelType level, MainForm mainform)
        {
            InitializeComponent();

            this.student = student;
            this.level = level;
            this.mainform = mainform;
        }

        public ModuleForm(Module module, LevelType level, MainForm mainform)
        {
            InitializeComponent();

            this.module = module;
            this.mainform = mainform;

            textBoxMCode.Text = module.Code;
            textBoxMName.Text = module.Name;
            comboBoxCredit.SelectedValue = module.Credits;

            foreach (Assessment asm in module.Assessments)
            {
                int index = dataGridView1.Rows.Add(1);
                dataGridView1.Rows[index].Cells[0].Value = asm.Type;
                dataGridView1.Rows[index].Cells[1].Value = asm.Weight;
            }

            updateModule = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(1);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows.RemoveAt(selectedRow);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Select a row to delete");
            }

        }

        private void ModuleForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxMCode.Text.Length > 0 & textBoxMName.Text.Length > 0 & comboBoxCredit.SelectedItem.ToString().Length > 0)
            {
                if (updateModule == false)
                {
                    module = new Module();
                }

                module.Code = textBoxMCode.Text;
                module.Name = textBoxMName.Text;
                module.Credits = int.Parse(comboBoxCredit.SelectedItem.ToString());
                module.Assessments = new List<Assessment>();

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    Assessment asm = new Assessment();
                    asm.Type = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    asm.Weight = int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());

                    module.Assessments.Add(asm);
                }

                if (updateModule == false)
                {
                    Console.WriteLine(((int)level));
                    student.Levels[((int)level) - 4].Modules.Add(module);
                }
                mainform.LoadModules(level);
                this.Hide();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
