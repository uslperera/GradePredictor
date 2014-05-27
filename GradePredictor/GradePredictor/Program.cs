using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GradePredictor.Config;
using GradePredictor.Models;
using System.Data.SQLite;
using GradePredictor.Views;

namespace GradePredictor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SignIn());

            #region Drop Tables
            /*DBConnection.Connect();
            DBConnection.DropTables();*/
            #endregion  

        }
    }
}
