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
            Application.Run(new MainForm());

            /*
            Console.WriteLine("Hello");

            DBConnection.Connect();

            DBConnection.Set("INSERT INTO " + Student.TABLE + " VALUES (2012017,'Shamal','Software')");
            SQLiteDataReader reader = DBConnection.Get("SELECT * FROM "+Student.TABLE);

            while(reader.Read())
            {
                Console.WriteLine(reader[Student.STUDENT_ID]+" "+reader[Student.STUDENT_NAME]);
            }*/
            
        }
    }
}
