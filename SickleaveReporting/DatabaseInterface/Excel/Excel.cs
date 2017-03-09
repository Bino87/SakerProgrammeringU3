
using System.Data.OleDb;
using Utilities;
using DatabaseInterface.Classes;
using System;

namespace DatabaseInterface
{
    public static class Excel
    {
        public static User CheckForUser(string userName, string password)
        {
            userName = StringManipulation.Neutralize(userName);
            password = password.Replace("'", "");
            string connectionString = GetExcelDatabaseConnectionString();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Users$] WHERE Username = '" + userName + "' AND Password='" + password + "'", connection))
                {
                    var userFound = cmd.ExecuteReader();
                    if (userFound.HasRows)
                        return new User(userFound);
                    return null;
                }
            }
        }
        public static bool AddSickleave(User user, Sickleave.SickleaveType sickleaveType, DateTime fromDate, DateTime toDate, long childSocNo)
        {
            string connectionString = GetExcelDatabaseConnectionString();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand("INSERT INTO [Sickleave$] VALUES(1, "+user.UserId+ ", "+(int)sickleaveType + ", #"+ fromDate.ToString("yyyy-MM-dd") + "#, #"+ toDate.ToString("yyyy-MM-dd") + "#, "+ childSocNo+ ")", connection))
                {
                    //cmd.CommandText = "CREATE TABLE [Sickleave] ([SickleaveId] Numeric, [SickleaveTypeId] Numeric, [UserId] Numeric, [FromDate] Date, [ToDate] Date, [ChildSocNo] Numeric)";

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        private static string GetExcelDatabaseConnectionString()
        {

            return "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + System.Web.HttpRuntime.BinDirectory + "Database.xls';Mode=ReadWrite;Extended Properties=\"Excel 8.0;HDR=YES;IMEX=0;\" ";
        }
    }
}
