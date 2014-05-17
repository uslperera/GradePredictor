using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradePredictor.Models
{
    /// <author> Shamal Perera </author>
    /// <datecreated>17-05-2014</datecreated>
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

        public AssessmentType Type { get; set; }
        public int Weight { get; set; }
        public int Mark { get; set; }

    }

    public enum AssessmentType
    {
        ICT, Coursework
    }
}
