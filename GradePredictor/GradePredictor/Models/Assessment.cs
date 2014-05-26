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

    public class Assessment
    {
        #region Entity CONSTANTS

        public static string TABLE = "assessment";
        public static string TYPE = "type";
        public static string WEIGHT = "weight";
        public static string MARK = "mark";
        public static string ID = "aid";

        #endregion

        public int AID { get; set; }
        public AssessmentType Type { get; set; }
        public int Weight { get; set; }
        public int Mark { get; set; }


        /// <summary>
        /// Add a new assessment
        /// </summary>
        /// <param name="assessment">Assessment instance</param>
        /// <param name="moduleID">Module ID (NOT moduleCode)</param>
        public static void Set(Assessment assessment, string moduleCode, int studentID)
        {
            string sql = "INSERT INTO " + Assessment.TABLE + " VALUES (" + assessment.AID + "," + studentID + ",'" + moduleCode + "','" + assessment.Type + "'," + assessment.Weight + "," + assessment.Mark + ")";

            DBConnection.Set(sql);
        }

        /// <summary>
        /// Delete all assessments
        /// </summary>
        /// <param name="studentID">Student ID</param>
        /// <param name="moduleCode">Module Code</param>
        public static void Delete(int studentID, string moduleCode)
        {
            string sql = "DELETE FROM " + Assessment.TABLE + " WHERE " + Student.STUDENT_ID + "=" + studentID + " AND "
                        + Module.CODE + "='" + moduleCode + "'";

            DBConnection.Set(sql);

        }

        /// <summary>
        /// Get a list of assessments
        /// </summary>
        /// <param name="moduleID">Module ID (NOT moduleCode)</param>
        /// <returns></returns>
        public static List<Assessment> Get(int studentID, string moduleCode)
        {
            List<Assessment> assessments = new List<Assessment>();

            string sql = "SELECT * FROM " + Assessment.TABLE + " WHERE " + Student.STUDENT_ID + "=" + studentID + " AND "
                        + Module.CODE + "='" + moduleCode + "'";

            SQLiteDataReader reader = DBConnection.Get(sql);

            #region Creates a List<Assessment>
            while (reader.Read())
            {
                Assessment assessment = new Assessment();
                assessment.AID = int.Parse(reader[0].ToString());
                assessment.Type = (AssessmentType)reader[3];
                assessment.Weight = int.Parse(reader[4].ToString());
                assessment.Mark = int.Parse(reader[5].ToString());

                assessments.Add(assessment);
            }
            #endregion

            return assessments;
        }

        public bool Equals(object obj)
        {
            Assessment assessment = obj as Assessment;
            if (assessment.Type == this.Type)
            {
                if (assessment.Weight == this.Weight)
                {
                    if (assessment.Mark == this.Mark)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public enum AssessmentType
    {
        ICT, Coursework
    }
}
//__________________________________END__________________________________\\