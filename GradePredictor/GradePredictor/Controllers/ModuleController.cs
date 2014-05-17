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
    public static class ModuleController
    {
        public static void Insert(Module module, int studentID, LevelType level)
        {
            string sql = "INSERT INTO " + Module.TABLE + " VALUES (" + studentID + ",'" + module.Code + "','" + module.Name + "'," + module.Credits + "," + level + ")";

            DBConnection.Set(sql);
        }
    }
}
