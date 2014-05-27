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
            for (int i = 4; i < 7; i++)
            {
                int index = dataGridView4.Rows.Add(1);
                dataGridView4.Rows[index].Cells[0].Value = "Level " + i;
            }
            int graderow = dataGridView4.Rows.Add(1);
            dataGridView4.Rows[graderow].Cells[0].Value = "Final Grade";
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            new RegisterCourse(student, labelCName).ShowDialog();

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            /*//dataGridView1.Rows.Add(1);
            Module mod = new Module();
            mod.Code = "ECSC";
            mod.Name = "OOD";
            mod.Credits = 15;
            Assessment a1 = new Assessment();
            a1.Type = "Coursework";
            a1.Weight = 30;

            Assessment a2 = new Assessment();
            a2.Type = "ICT";
            a2.Weight = 30;

            Assessment a3 = new Assessment();
            a3.Type = "Coursework";
            a3.Weight = 40;

            mod.Assessments = new List<Assessment>();

            mod.Assessments.Add(a1);
            mod.Assessments.Add(a2);
            mod.Assessments.Add(a3);

            AddModule(mod,LevelType.Level4);*/

            ModuleForm moduleForm = new ModuleForm(student,LevelType.Level4,this);
            moduleForm.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            try
            {
                ModuleForm moduleForm = new ModuleForm(student, LevelType.Level5, this);
                moduleForm.ShowDialog();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Select a row to edit details");
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            try
            {
                ModuleForm moduleForm = new ModuleForm(student, LevelType.Level6, this);
                moduleForm.ShowDialog();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Select a row to edit details");
            }
        }

        private string CalculateAward()
        {
            var avgs = CalcAverage();
            int level6Avg = avgs.Item1;
            int level5Avg = avgs.Item2;

            if (level6Avg >= 70 & level5Avg >= 60)
                return "First";
            else if (level6Avg >= 60 & level5Avg >= 50)
                return "Upper Second";
            else if (level6Avg >= 50 & level5Avg >= 40)
                return "Lower Second";
            else if ((level6Avg + level5Avg) / 2 >= 40)
                return "Third";
            else
                return "Failed";
        }


        private Tuple<int, int> CalcAverage()
        {
            List<Module> level5 = student.Levels[1].Modules.GetRange(0, student.Levels[1].Modules.Count);
            List<Module> level6 = student.Levels[2].Modules.GetRange(0, student.Levels[2].Modules.Count); ;

            level5.Sort();
            level6.Sort();

            int totalCreditsL5 = 0;
            int moduleCountL5 = 0;
            int totalMarksL5 = 0;

            int totalCreditsL6 = 0;
            int moduleCountL6 = 0;
            int totalMarksL6 = 0;

            foreach (Module module in level6)
            {
                if (module.Credits == 30 & totalCreditsL6 + 30 <= 105)
                {
                    totalCreditsL6 += 30;
                    totalMarksL6 += module.Total;
                    moduleCountL6++;
                }
            }

            foreach (Module module in level6)
            {
                if (module.Credits == 15 & totalCreditsL6 + 15 <= 105)
                {
                    totalCreditsL6 += 15;
                    totalMarksL6 += module.Total;
                    moduleCountL6++;
                }
                else
                {
                    foreach (Module mod in level5)
                    {
                        if (mod.Credits == 30 & totalCreditsL5 + 30 <= 105)
                        {
                            totalCreditsL5 += 30;
                            totalMarksL5 += mod.Total;
                            moduleCountL5++;
                        }
                    }
                    bool markL6Added = false;
                    foreach (Module mod in level5)
                    {
                        if (mod.Credits == 15 & totalCreditsL5 + 15 <= 105)
                        {
                            if (!markL6Added)
                            {
                                if (module.Total >= mod.Total)
                                {
                                    totalCreditsL5 += 15;
                                    totalMarksL5 += module.Total;
                                    moduleCountL5++;
                                }
                            }
                            totalCreditsL5 += 15;
                            totalMarksL5 += mod.Total;
                            moduleCountL5++;
                        }
                    }
                }
            }

            return new Tuple<int, int>((totalMarksL6 / moduleCountL6) * 100, (totalMarksL5 / moduleCountL5) * 100);
        }

        private void AddToDataGrid(DataGridView grid,List<Module> modules)
        {
            grid.RowCount = 0 ;
            foreach(Module module in modules)
            {
                int index = grid.Rows.Add(1);
                grid.Rows[index].Cells[0].Value = module.Code + " - " + module.Name + " " + module.Credits+" Credits";

                int i = 1;
                foreach (Assessment asm in module.Assessments)
                {
                    grid.Rows[index].Cells[i].Value = asm.Type + " Weight: " + asm.Weight+"%";
                    grid.Rows[index].Cells[i + 1].ReadOnly = false;
                    grid.Rows[index].Cells[i + 1].Value = asm.Mark+"";
                    i += 2;

                }
                grid.Rows[index].Cells[9].Value = module.Total;
            }
            
        }

        public void LoadModules(LevelType level)
        {
            if (level == LevelType.Level4)
            {
                AddToDataGrid(dataGridView1,student.Levels[0].Modules);
            }
            else if (level == LevelType.Level5)
            {
                AddToDataGrid(dataGridView2, student.Levels[1].Modules);
            }
            else if (level == LevelType.Level6)
            {
                AddToDataGrid(dataGridView3, student.Levels[2].Modules);
            }
            
        }

        public void AddModule(Module module,LevelType level)
        {
            if(level==LevelType.Level4)
            {
                int index = dataGridView1.Rows.Add(1);
                dataGridView1.Rows[index].Cells[0].Value = module.Code + " " + module.Name + " " + module.Credits;

                int i = 1;
                foreach (Assessment asm in module.Assessments)
                {
                    dataGridView1.Rows[index].Cells[i].Value = asm.Type + " Weight " + asm.Weight;
                    dataGridView1.Rows[index].Cells[i + 1].ReadOnly = false;
                    i += 2;

                }

            }
            else if (level == LevelType.Level5)
            {
                int index = dataGridView2.Rows.Add(1);
                dataGridView2.Rows[index].Cells[0].Value = module.Code + " " + module.Name + " " + module.Credits;

                int i = 1;
                foreach (Assessment asm in module.Assessments)
                {
                    dataGridView2.Rows[index].Cells[i].Value = asm.Type + " Weight " + asm.Weight;
                    dataGridView2.Rows[index].Cells[i + 1].ReadOnly = false;
                    i += 2;

                }
            }
            else if (level == LevelType.Level6)
            {
                int index = dataGridView3.Rows.Add(1);
                dataGridView3.Rows[index].Cells[0].Value = module.Code + " " + module.Name + " " + module.Credits;

                int i = 1;
                foreach (Assessment asm in module.Assessments)
                {
                    dataGridView3.Rows[index].Cells[i].Value = asm.Type + " Weight " + asm.Weight;
                    dataGridView3.Rows[index].Cells[i + 1].ReadOnly = false;
                    i += 2;

                }
            }
            
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                int row = dataGridView1.SelectedRows[0].Index;
                ModuleForm moduleForm = new ModuleForm(student.Levels[0].Modules.ElementAt(row), LevelType.Level4, this);
                moduleForm.ShowDialog();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Select a row to edit details");
            }
            
        }

        
        //calculate and update the total
        private void UpdateTotal(int rowIndex, int colIndex, DataGridView dgv)
        {
            int level = 0;
            string[] modRow = GetRowInfo(rowIndex, dgv);
            double total = 0;

            //level selection
            if (dgv == dataGridView1)
            {
                level = 0;
            }

            if (dgv == dataGridView2)
            {
                level = 1;
            }

            if (dgv == dataGridView3)
            {
                level = 2;
            }

            //module selection
            Module cuMod = student.Levels[level].Modules[rowIndex];

            //assesment count
            int assCount = cuMod.Assessments.Count;

            //total calculation
            for (int i = 0; i < assCount; i++)
            {
                double weight = cuMod.Assessments.ElementAt(i).Weight;
                double mark = double.Parse(modRow[colIndex]);

                //get mark from ass 1
                if (i == 0)
                {
                    mark = double.Parse(modRow[2]);
                }

                //get mark from ass 2
                if (i == 1)
                {
                    mark = double.Parse(modRow[4]);
                }

                //get mark from ass 3
                if (i == 2)
                {
                    mark = double.Parse(modRow[6]);
                }


                total += (mark * (weight * 0.01));

                //modRow[9] = ""+total;
            }

            //update table row
            dgv[9, rowIndex].Value = total;

            //update edited column according to column index
            switch(colIndex){
                case 2:
                    cuMod.Assessments.ElementAt(0).Mark = int.Parse(modRow[colIndex]);
                    break;

                case 4:
                    cuMod.Assessments.ElementAt(1).Mark = int.Parse(modRow[colIndex]);
                    break;

                case 6:
                    cuMod.Assessments.ElementAt(2).Mark = int.Parse(modRow[colIndex]);
                    break;
            }

            //update modules total
            cuMod.Total = int.Parse(total.ToString());

            //update the module with new updated marks and total
            student.Levels[level].Modules[rowIndex] = cuMod;

        }

        //get all row info
        private string[] GetRowInfo(int rowIndex, DataGridView dgv)
        {
            string[] modRow = new string[10];
            for (int i = 0; i < 10; i++)
            {
                string s = "0" + dgv.Rows[rowIndex].Cells[i].Value;
                modRow[i] = s;

            }

            return modRow;
        }
       

        //on cell edit event
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateTotal(e.RowIndex, e.ColumnIndex, dataGridView1);
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateTotal(e.RowIndex, e.ColumnIndex, dataGridView2);
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateTotal(e.RowIndex, e.ColumnIndex, dataGridView3);
        }

        

    }

}
