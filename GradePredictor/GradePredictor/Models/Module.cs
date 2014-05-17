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
    public class Module
    {
        #region Entity CONSTANTS

        public static const string TABLE = "module";
        public static const string CODE = "code";
        public static const string NAME = "name";
        public static const string CREDITS = "credits";
        public static const string LEVEL = "level";
        public static const string ID = "mid";

        #endregion

        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public Assessment[] Assessments { get; set; }
        
    }
}
