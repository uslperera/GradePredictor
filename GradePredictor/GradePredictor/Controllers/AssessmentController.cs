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
    public static class AssessmentController
    {
        public static void Insert(Assessment assessment, int moduleID)
        {
            string sql = "INSERT INTO " + Assessment.TABLE + " VALUES (" + moduleID + ",'" + assessment.Type + "'," + assessment.Weight + "," + assessment.Mark + ")";

            DBConnection.Set(sql);
        }
    }
}
