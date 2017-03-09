
using System.Data.OleDb;
using Utilities;
namespace DatabaseInterface.Excel
{
    public static class Excel
    {
        public static bool CheckForUser(string userName, string password)
        {
            userName = StringManipulation.Neutralize(userName);
            password = password.Replace("'", "");
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='Database.xls';Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\" ";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Users$] WHERE Username = '" + userName + "' AND Password='" + password + "'", connection))
                {
                    var userFound = cmd.ExecuteReader();
                    if (userFound.Read())
                        return true;
                    return false;
                }
            }
        }
    }
}
