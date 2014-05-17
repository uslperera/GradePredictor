using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradePredictor.Models;

namespace GradePredictor.Controllers
{
    /// <author> Shamal Perera </author>
    /// <datecreated>17-05-2014</datecreated>
    public static class StudentController
    {
        public static void Insert(Student student)
        {
            string sql = "INSERT INTO " + Student.TABLE + " VALUES (" + student.StudentID + ",'" + student.StudentName + "','" + student.CourseName + "')";

            DBConnection.Set(sql);
        }
    }
}
