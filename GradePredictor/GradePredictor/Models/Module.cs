using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradePredictor.Config;
using System.Data.SQLite;

namespace GradePredictor.Models
{
    /// <author> Shamal Perera </author>
    /// <datecreated>18-05-2014</datecreated>
    /// <summary>Entity Class</summary>
    public class Module
    {
        #region Entity CONSTANTS

        public const string TABLE = "module";
        public const string CODE = "code";
        public const string NAME = "name";
        public const string CREDITS = "credits";
        public const string LEVEL = "level";

        #endregion

        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public List<Assessment> Assessments { get; set; }


        /// <summary>
        /// Add a new module
        /// </summary>
        /// <param name="module">Module instance</param>
        /// <param name="studentID">Student ID</param>
        /// <param name="level">Level</param>
        public static void Set(Module module, int studentID, LevelType level)
        {
            string sql = "INSERT INTO " + Module.TABLE + " VALUES (" + studentID + ",'" + module.Code + "','"
                            + module.Name + "'," + module.Credits + "," + level + ")";

            DBConnection.Set(sql);

            //Delete all the assessments of the module
            Assessment.Delete(studentID, module.Code);

            //Insert all the assessments of the module
            foreach (Assessment assessment in module.Assessments)
            {
                Assessment.Set(assessment, module.Code, studentID);
            }

        }

        /// <summary>
        /// Delete a module
        /// </summary>
        /// <param name="studentID">Student ID</param>
        /// <param name="moduleCode">Module Code</param>
        public static void Delete(int studentID, string moduleCode)
        {
            string sql = "DELETE FROM " + Module.TABLE + " WHERE " + Student.STUDENT_ID + "=" + studentID + " AND "
                        + Module.CODE + "='" + moduleCode + "'";

            DBConnection.Set(sql);

        }

        /// <summary>
        /// Delete all modules
        /// </summary>
        /// <param name="studentID">Student ID</param>
        public static void Delete(int studentID)
        {
            string sql = "DELETE FROM " + Module.TABLE + " WHERE " + Student.STUDENT_ID + "=" + studentID;

            DBConnection.Set(sql);

        }

        /// <summary>
        /// Get a list of modules
        /// </summary>
        /// <param name="studentID">Student ID</param>
        /// <param name="level">Level</param>
        public static List<Module> Get(int studentID, LevelType level)
        {
            List<Module> modules = new List<Module>();

            string sql = "SELECT * FROM " + Module.TABLE + " WHERE " + Student.STUDENT_ID + "=" + studentID + " AND "
                + Module.LEVEL + "=" + level;

            SQLiteDataReader reader = DBConnection.Get(sql);

            #region Creates a List<Module>
            while (reader.Read())
            {
                Module module = new Module();
                module.Code = reader[1].ToString();
                module.Name = reader[2].ToString();
                module.Credits = int.Parse(reader[3].ToString());
                module.Assessments = Assessment.Get(studentID, module.Code);

                modules.Add(module);
            }
            #endregion

            return modules;
        }

        /// <summary>
        /// Get a module
        /// </summary>
        /// <param name="studentID">Student ID</param>
        /// <param name="level">Level</param>
        public static Module Get(int studentID, string moduleCode)
        {
            Module module = null;
            string sql = "SELECT * FROM " + Module.TABLE + " WHERE " + Student.STUDENT_ID + "=" + studentID + " AND "
                        + Module.CODE + "='" + moduleCode + "'";

            SQLiteDataReader reader = DBConnection.Get(sql);

            #region Creates a Module
            if (reader.Read())
            {
                module = new Module();
                module.Code = reader[1].ToString();
                module.Name = reader[2].ToString();
                module.Credits = int.Parse(reader[3].ToString());
                module.Assessments = Assessment.Get(studentID, moduleCode);

            }
            #endregion

            return module;
        }

        public bool Equals(Object obj)
        {
            Module module = obj as Module;
            if (module.Code == this.Code)
            {
                if (module.Name == this.Name)
                {
                    if (module.Credits == this.Credits)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

}
//__________________________________END__________________________________\\