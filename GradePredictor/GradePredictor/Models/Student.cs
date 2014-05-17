using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradePredictor.Controllers;

namespace GradePredictor.Models
{
    /// <author> Shamal Perera </author>
    /// <datecreated>17-05-2014</datecreated>
    /// <summary>Entity Class</summary>
    public class Student
    {
        #region Entity CONSTANTS

        public static const string TABLE = "student";
        public static const string STUDENT_NAME = "studentname";
        public static const string STUDENT_ID = "studentid";
        public static const string COURSE_NAME = "coursename";

        #endregion

        public string StudentName { get; set; }
        public int StudentID { get; set; }
        public string CourseName { get; set; }
        public Level[] Levels { get; set; }

    }
}
