using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradePredictor.Models
{
    /// <author> Shamal Perera </author>
    /// <datecreated>18-05-2014</datecreated>
    /// <summary>Model</summary>
    public class Level
    {

        public LevelType Name { get; set; }
        public int Credits { get; set; }
        public List<Module> Modules { get; set; }
        public int Total { get; set; }

        public Level()
        {
            Modules = new List<Module>();
        }
    }

    public enum LevelType
    {
        Level4 = 4, Level5 = 5, Level6 = 6
    }

}
//__________________________________END__________________________________\\
