using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using GradePredictor.Models;

namespace GradePredictor.Config
{

    /// <author> Shamal Perera </author>
    /// <datecreated>18-05-2014</datecreated>
    /// <summary>Connection to the database</summary>
    public static class DBConnection
    {

        private static SQLiteConnection conn;

        /// <summary>
        /// Creates a new database
        /// </summary>
        private static void Create()
        {
            SQLiteConnection.CreateFile("GradePredictor.sqlite");
        }


        /// <summary>
        /// Creates a new connection
        /// </summary>
        public static void Connect()
        {
            if (conn == null)
            {
                conn = new SQLiteConnection("Data Source=GradePredictor.sqlite;Version=3;");
                conn.Open();
            }

            CreateTables();
        }

        /// <summary>
        /// Creates Student, Module and Assessment tables in the database
        /// </summary>
        private static void CreateTables()
        {

            #region Create Student Table
            string sql_student = "CREATE TABLE IF NOT EXISTS " + Student.TABLE + "("
                            + Student.STUDENT_ID + " INT(8) PRIMARY KEY,"
                            + Student.STUDENT_NAME + " VARCHAR(30),"
                            + Student.COURSE_NAME + " VARCHAR(30));";

            SQLiteCommand command1 = new SQLiteCommand(sql_student, conn);

            command1.ExecuteNonQuery();

            #endregion

            #region Create Module Table
            string sql_module = "CREATE TABLE IF NOT EXISTS " + Module.TABLE + "("
                            + Student.STUDENT_ID + " INT(8),"
                            + Module.CODE + " VARCHAR(10),"
                            + Module.NAME + " VARCHAR(30),"
                            + Module.CREDITS + " INT(2),"
                            + Module.LEVEL + " INT(1) CHECK(" + Module.LEVEL + " IN(4,5,6)),"
                            + "PRIMARY KEY(" + Student.STUDENT_ID + "," + Module.CODE + "),"
                            + "FOREIGN KEY(" + Student.STUDENT_ID + ") REFERENCES "
                            + Student.TABLE + "(" + Student.STUDENT_ID + "));";

            SQLiteCommand command2 = new SQLiteCommand(sql_module, conn);

            command2.ExecuteNonQuery();

            #endregion

            #region Create Assessment Table
            string sql_assessment = "CREATE TABLE IF NOT EXISTS " + Assessment.TABLE + "("
                            + Assessment.ID + " INT,"
                            + Student.STUDENT_ID + " INT(8),"
                            + Module.CODE + " VARCHAR(10),"
                            + Assessment.TYPE + " VARCHAR(10),"
                            + Assessment.WEIGHT + " INT(2),"
                            + Assessment.MARK + " INT(3),"
                            + "PRIMARY KEY(" + Assessment.ID + "," + Student.STUDENT_ID + "," + Module.CODE + "),"
                            + "FOREIGN KEY(" + Student.STUDENT_ID + ") REFERENCES "
                            + Module.TABLE + "(" + Student.STUDENT_ID + "),"
                            + "FOREIGN KEY(" + Module.CODE + ") REFERENCES "
                            + Module.TABLE + "(" + Module.CODE + "));";

            SQLiteCommand command3 = new SQLiteCommand(sql_assessment, conn);

            command3.ExecuteNonQuery();

            #endregion
        }

        /// <summary>
        /// Drop all the tables
        /// </summary>
        private static void DropTables()
        {

            #region Delete Assessment Table
            string sql_assessment = "DROP TABLE assessment";

            SQLiteCommand command1 = new SQLiteCommand(sql_assessment, conn);

            command1.ExecuteNonQuery();

            #endregion

            #region Delete Module Table
            string sql_module = "DROP TABLE module";

            SQLiteCommand command2 = new SQLiteCommand(sql_module, conn);

            command2.ExecuteNonQuery();

            #endregion

            #region Delete Student Table
            string sql_student = "DROP TABLE student";

            SQLiteCommand command3 = new SQLiteCommand(sql_student, conn);

            command3.ExecuteNonQuery();

            #endregion
        }


        /// <summary>
        /// Close the current connection
        /// </summary>
        public static void Disconnect()
        {
            conn.Close();
        }


        /// <summary>
        /// SQL query (insert, update, delete)
        /// </summary>
        /// <param name="sql">"eg :- INSERT INTO TABLE_NAME VALUES('0')"</param>
        /// <returns></returns>
        public static bool Set(string sql)
        {
            if (conn == null)
            {
                throw new NullReferenceException();
            }

            SQLiteCommand command = new SQLiteCommand(sql, conn);

            //Once the query is executed, get number of rows affected
            int count = command.ExecuteNonQuery();

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// SQL query (select)
        /// </summary>
        /// <param name="sql">"eg:- SELECT * FROM TABLE_NAME"</param>
        /// <returns></returns>
        public static SQLiteDataReader Get(string sql)
        {
            if (conn == null)
            {
                throw new NullReferenceException();
            }

            SQLiteCommand command = new SQLiteCommand(sql, conn);

            return command.ExecuteReader();
        }

    }

}
//__________________________________END__________________________________\\