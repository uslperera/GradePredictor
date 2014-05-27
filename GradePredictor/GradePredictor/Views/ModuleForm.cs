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
    /// <summary>Module Form</summary>
    public partial class ModuleForm : Form
    {
        private Student student;
        private Module module;
        private LevelType level;
        private bool updateModule;
        private MainForm mainform;

        /// <summary>
        /// To create a new module
        /// </summary>
        /// <param name="student"></param>
        /// <param name="level"></param>
        /// <param name="mainform"></param>
        public ModuleForm(Student student, LevelType level, MainForm mainform)
        {
            InitializeComponent();

            this.student = student;
            this.level = level;
            this.mainform = mainform;
        }

        /// <summary>
        /// Edit existing details of a module
        /// </summary>
        /// <param name="module"></param>
        /// <param name="level"></param>
        /// <param name="mainform"></param>
        public ModuleForm(Module module, LevelType level, MainForm mainform)
        {
            InitializeComponent();

            this.module = module;
            this.level = level;
            this.mainform = mainform;

            //Set the values of the existing module
            textBoxMCode.Text = module.Code;
            textBoxMName.Text = module.Name;
            comboBoxCredit.SelectedValue = module.Credits;

            //Add assessment details
            foreach (Assessment asm in module.Assessments)
            {
                int index = dataGridView1.Rows.Add(1);
                dataGridView1.Rows[index].Cells[0].Value = asm.Type;
                dataGridView1.Rows[index].Cells[1].Value = asm.Weight;
            }
            //Flag variable to update or add a new module
            updateModule = true;
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Add a new row
            dataGridView1.Rows.Add(1);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //Get selected row and remove from the datagrid
                int selectedRow = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows.RemoveAt(selectedRow);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                //If no row is selected to delete
                MessageBox.Show("Select a row to delete");
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //Presence check
            if (textBoxMCode.Text.Length > 0 & textBoxMName.Text.Length > 0 & comboBoxCredit.SelectedItem != null)
            {
                //A new module
                if (updateModule == false)
                {
                    module = new Module();
                }
                //Set the details
                module.Code = textBoxMCode.Text;
                module.Name = textBoxMName.Text;
                module.Credits = int.Parse(comboBoxCredit.SelectedItem.ToString());
                //Create a new list of assessments(Old list is lost)
                module.Assessments = new List<Assessment>();

                //Add new assessments to the list
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    Assessment asm = new Assessment();
                    //Set details of the assessment
                    asm.Type = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    try
                    {
                        asm.Weight = int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    }
                    catch (FormatException ex)
                    {
                        //If weight is not a number
                        MessageBox.Show("Weight cannot contain non-numerical values");
                        return;
                    }

                    //Add assessment into the list
                    module.Assessments.Add(asm);
                }
                //If a new module
                if (updateModule == false)
                {
                    //Add new module to the list in level
                    student.Levels[((int)level) - 4].Modules.Add(module);
                }
                //Refresh all the module details
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
//__________________________________END__________________________________\\