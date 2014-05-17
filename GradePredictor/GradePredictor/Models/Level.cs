using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradePredictor.Models
{
    /// <author> Shamal Perera </author>
    /// <datecreated>17-05-2014</datecreated>
    /// <summary>Model</summary>
    public class Level
    {

        public LevelType Name { get; set; }
        public int Credits { get; set; }
        public Module[] Modules { get; set; }

    }

    public enum LevelType
    {
        Level4 = 4, Level5 = 5, Level6 = 6
    }
}
