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
using GradePredictor.Config;
using System.Threading;


namespace GradePredictor.Views
{
    /// <datecreated>27-05-2014</datecreated>
    /// <summary>Main Form</summary>
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

            labelCName.Text = student.CourseName+"";
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            new RegisterCourse(student, labelCName).ShowDialog();

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ModuleForm moduleForm = new ModuleForm(student, LevelType.Level4, this);
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
            try
            {
                //Create a copy of the two lists
                List<Module> level5 = student.Levels[1].Modules.GetRange(0, student.Levels[1].Modules.Count);
                List<Module> level6 = student.Levels[2].Modules.GetRange(0, student.Levels[2].Modules.Count);

                //Sort the two modules lists
                level5.Sort();
                level6.Sort();

                int totalCreditsL5 = 0;
                int moduleCountL5 = 0;
                int totalMarksL5 = 0;

                int totalCreditsL6 = 0;
                int moduleCountL6 = 0;
                int totalMarksL6 = 0;

                //First find the 30 credit modules in level 6
                foreach (Module module in level6)
                {
                    if (module.Credits == 30 & totalCreditsL6 + 30 <= 105)
                    {
                        totalCreditsL6 += 30;
                        totalMarksL6 += module.Total;
                        moduleCountL6++;
                    }
                }
                //Next add up the best 15 credit modules in level 6
                foreach (Module module in level6)
                {
                    if (module.Credits == 15 & totalCreditsL6 + 15 <= 105)
                    {
                        totalCreditsL6 += 15;
                        totalMarksL6 += module.Total;
                        moduleCountL6++;
                    }

                }

                //First find the 30 credit modules in level 5
                foreach (Module mod in level5)
                {
                    if (mod.Credits == 30 & totalCreditsL5 + 30 <= 105)
                    {
                        totalCreditsL5 += 30;
                        totalMarksL5 += mod.Total;
                        moduleCountL5++;
                    }
                }

                //Add the next best 15 credit modules in level 5 and the level 6
                bool markL6Added = false;
                Module modul = level6.ElementAt(level6.Capacity - 1);
                foreach (Module mod in level5)
                {
                    if (mod.Credits == 15 & totalCreditsL5 + 15 <= 105)
                    {
                        if (!markL6Added && level6.Capacity > 7)
                        {
                            if (modul.Total >= mod.Total)
                            {
                                totalCreditsL5 += 15;
                                totalMarksL5 += modul.Total;
                                moduleCountL5++;
                            }
                        }
                        totalCreditsL5 += 15;
                        totalMarksL5 += mod.Total;
                        moduleCountL5++;
                    }
                }
                // Do the above process until best 105 credits are met

                //return average for level 6 and level 5
                return new Tuple<int, int>((totalMarksL6 / moduleCountL6), (totalMarksL5 / moduleCountL5));
            }
            catch (Exception e)
            {
                
            }
            return new Tuple<int, int>(0,0);
           
        }

        /// <summary>
        /// Add modules to the datagridview
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="modules"></param>
        private void AddToDataGrid(DataGridView grid, List<Module> modules)
        {
            //First set row count to 0
            grid.RowCount = 0;
            foreach (Module module in modules)
            {
                //Add module details to the datagridview
                int index = grid.Rows.Add(1);
                grid.Rows[index].Cells[0].Value = module.Code + " - " + module.Name + " " + module.Credits + " Credits";

                double total = 0;
                int i = 1;
                //Add assessment details to the datagridview
                foreach (Assessment asm in module.Assessments)
                {
                    grid.Rows[index].Cells[i].Value = asm.Type + " Weight: " + asm.Weight + "%";
                    grid.Rows[index].Cells[i + 1].ReadOnly = false;
                    grid.Rows[index].Cells[i + 1].Value = asm.Mark + "";
                    i += 2;
                    total += (asm.Mark * (asm.Weight * 0.01));
                    module.Total = (int)total;
                }
                grid.Rows[index].Cells[9].Value = module.Total;
            }

        }
        /// <summary>
        /// Load modules
        /// </summary>
        /// <param name="level"></param>
        public void LoadModules(LevelType level)
        {
            if (level == LevelType.Level4)
            {
                AddToDataGrid(dataGridView1, student.Levels[0].Modules);
                double avg = calculateAvg(0);
                label2.Text = "Average: " + avg;
            }
            else if (level == LevelType.Level5)
            {
                AddToDataGrid(dataGridView2, student.Levels[1].Modules);
                double avg = calculateAvg(1);
                label3.Text = "Average: " + avg;
            }
            else if (level == LevelType.Level6)
            {
                AddToDataGrid(dataGridView3, student.Levels[2].Modules);
                double avg = calculateAvg(2);
                label4.Text = "Average: " + avg;
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
            try
            {
                //update edited column according to column index
                switch (colIndex)
                {
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
            }
            catch (Exception ex)
            {

            }



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
            double avg = calculateAvg(0);
            label2.Text = "Average: " + avg;
            Console.WriteLine(avg);
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateTotal(e.RowIndex, e.ColumnIndex, dataGridView2);
            double avg = calculateAvg(1);
            label3.Text = "Average: " + avg;
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateTotal(e.RowIndex, e.ColumnIndex, dataGridView3);
            double avg = calculateAvg(2);
            label4.Text = "Average: " + avg;
        }

        private void tabControl_MouseClick(object sender, MouseEventArgs e)
        {
            int selectedIndex = tabControl.SelectedIndex;
            if (selectedIndex == 4)
            {
                updateSummeryGrid(dataGridView4);
                Console.WriteLine(CalculateAward());
            }
        }

        //update summery info
        private void updateSummeryGrid(DataGridView dgv)
        {
            //calculate level 4 credits
            int level4 = getCalculatedCredits(0);

            //calculate level 5 credits
            int level5 = getCalculatedCredits(1);

            //calculate level 6 credits
            int level6 = getCalculatedCredits(2);

            //add to the grid view

            dgv.Rows[0].SetValues("Level 4", "" + level4);


            dgv.Rows[1].SetValues("Level 5", "" + level5);


            dgv.Rows[2].SetValues("Level 6", "" + level6);

            //Fianl Award calculation
            string award = CalculateAward();

            dgv.Rows[3].SetValues("Final Grade", "" + award);
        }

        //get module credits
        private int getCalculatedCredits(int level)
        {
            int modCredits = 0;
            //get all module list
            List<Module> listMod = student.Levels[level].Modules;



            //iterate over modules
            for (int i = 0; i < listMod.Count; i++)
            {
                Module mod = listMod.ElementAt(i);

                //get current module credits
                int credits = mod.Credits;

                //get all assinment list
                List<Assessment> listAss = listMod.ElementAt(i).Assessments;

                //iterate over assinments
                int counter = 0;
                for (int j = 0; j < listAss.Count; j++)
                {
                    //current assinment
                    Assessment ass = listAss.ElementAt(j);

                    //check marks for assinemnt credits
                    if (ass.Mark == 0)
                    {
                        continue;
                    }
                    counter++;
                }

                //if any assenment is not fails
                if (counter == listAss.Count)
                {
                    if (mod.Total > 30)
                    {
                        modCredits += credits;
                    }
                    if (mod.Total <= 30)
                    {
                        modCredits += 0;
                    }
                }

            }

            return modCredits;
        }


        //calculate average
        private double calculateAvg(int level)
        {
            //get all module list
            List<Module> listMod = student.Levels[level].Modules;

            //total module marks and avarage
            int totalModMarks = 0;
            double avg = 0;

            //calculate total
            for (int i = 0; i < listMod.Count; i++)
            {
                totalModMarks += listMod.ElementAt(i).Total;

            }

            //calculate average
            avg = double.Parse("" + totalModMarks) / listMod.Count;

            return avg;

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                int row = dataGridView1.SelectedRows[0].Index;
                student.Levels[0].Modules.RemoveAt(row);
                MessageBox.Show("Module deleted");
                LoadModules(LevelType.Level4);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Select a row to edit details");
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

            try
            {
                int row = dataGridView2.SelectedRows[0].Index;
                student.Levels[1].Modules.RemoveAt(row);
                MessageBox.Show("Module deleted");
                LoadModules(LevelType.Level5);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Select a row to edit details");
            }
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            try
            {
                int row = dataGridView3.SelectedRows[0].Index;
                student.Levels[2].Modules.RemoveAt(row);
                MessageBox.Show("Module deleted");
                LoadModules(LevelType.Level6);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Select a row to edit details");
            }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            try
            {
                int row = dataGridView3.SelectedRows[0].Index;
                ModuleForm moduleForm = new ModuleForm(student.Levels[2].Modules.ElementAt(row), LevelType.Level6, this);
                moduleForm.ShowDialog();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Select a row to edit details");
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            try
            {
                int row = dataGridView2.SelectedRows[0].Index;
                ModuleForm moduleForm = new ModuleForm(student.Levels[1].Modules.ElementAt(row), LevelType.Level5, this);
                moduleForm.ShowDialog();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Select a row to edit details");
            }
        }

        private void SaveStudent()
        {
            Student.Set(this.student);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save your changes?", "Grade Predictor", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                Thread thread = new Thread(new ThreadStart(SaveStudent));
                thread.Start();

            }
            else if (result == DialogResult.No)
            {
                e.Cancel = false;
                DBConnection.Disconnect();
                this.Dispose();
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

    }

}
//__________________________________END__________________________________\\