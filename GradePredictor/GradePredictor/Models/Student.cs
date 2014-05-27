using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using GradePredictor.Config;

namespace GradePredictor.Models
{
    /// <datecreated>18-05-2014</datecreated>
    /// <summary>Entity Class</summary>
    public class Student
    {
        #region Entity CONSTANTS

        public const string TABLE = "student";
        public const string STUDENT_NAME = "studentname";
        public const string STUDENT_ID = "studentid";
        public const string COURSE_NAME = "coursename";

        #endregion

        public string StudentName { get; set; }
        public int StudentID { get; set; }
        public string CourseName { get; set; }
        public Level[] Levels { get; set; }

        public Student()
        {
            #region Initializes the levels
            Levels = new Level[3];

            for (int i = 0; i < 3; i++)
            {
                Levels[i] = new Level();
            }

            Levels[0].Name = LevelType.Level4;
            Levels[1].Name = LevelType.Level5;
            Levels[2].Name = LevelType.Level6;
            #endregion
        }

        /// <summary>
        /// Add a new student
        /// </summary>
        /// <param name="student">Student instance</param>
        public static void Set(Student student)
        {
            if (Get(student.StudentID) == null)
            {
                string sql = "INSERT INTO " + Student.TABLE + " VALUES (" + student.StudentID + ",'" + student.StudentName + "','" + student.CourseName + "')";

                DBConnection.Set(sql);
            }

            //Delete all the modules of the student
            Module.Delete(student.StudentID);

            //Go through each level
            foreach (Level level in student.Levels)
            {
                //Insert all the modules in each level
                foreach (Module module in level.Modules)
                {
                    Module.Set(module, student.StudentID, level.Name);
                }
            }
        }

        /// <summary>
        /// Get a student
        /// </summary>
        /// <param name="studentID">Student ID</param>
        /// <returns></returns>
        public static Student Get(int studentID)
        {
            Student student = null;

            string sql = "SELECT * FROM " + Student.TABLE + " WHERE " + Student.STUDENT_ID + "=" + studentID;

            SQLiteDataReader reader = DBConnection.Get(sql);

            #region Creates the Student
            if (reader.Read())
            {
                student = new Student();
                student.StudentID = int.Parse(reader[0].ToString());
                student.StudentName = reader[1].ToString();
                student.CourseName = reader[2].ToString();

                //Assign modules recorded for each level
                foreach (Level level in student.Levels)
                {
                    level.Modules = Module.Get(student.StudentID, level.Name);
                }
            }
            #endregion

            return student;

        }
    }

}
//__________________________________END__________________________________\\