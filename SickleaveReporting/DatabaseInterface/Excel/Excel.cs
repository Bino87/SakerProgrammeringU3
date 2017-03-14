
using System.Data.OleDb;
using Utilities;
using DatabaseInterface.Classes;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DatabaseInterface
{
    /// <summary>
    /// Vinberg: Excel document used as application database
    /// </summary>
    public static class Excel
    {
        private static int NextSickleaveID = 0;
        private static int NextLogId = 0;
        /// <summary>
        /// Vinberg: Test username and password against database
        /// </summary>
        /// <param name="userName">The username</param>
        /// <param name="password">Userpassword in its raw form</param>
        /// <returns>If username and password is correct this function returns the complete user object</returns>
        public static User CheckForUser(string userName, string password)
        {
            userName = StringManipulation.Neutralize(userName);
            password = HashPassword(password).Replace("'", "");
            string connectionString = GetExcelDatabaseConnectionString();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Users$] WHERE Username = '" + userName + "' AND Password='" + password + "' AND FailedAttempts < 10 AND PasswordExpires > #"+DateTime.Now.ToString("yyyy-MM-dd")+"#", connection))
                {
                    var userOleDbReader = cmd.ExecuteReader();
                    if (userOleDbReader.HasRows)
                    {
                        userOleDbReader.Read();
                        if (((DateTime)userOleDbReader["TemporaryDisabled"]) > DateTime.Now.AddDays(-1))
                            return null;
                        User userLoggedIn = new User(userOleDbReader);
                        ResetFailedAttempt(userLoggedIn);
                        return userLoggedIn;
                    }
                    IncrementFailedAttempt(userName);
                    return null;
                }
            }
        }

        /// <summary>
        /// Vinberg: Get user by userId, used to search for a specific employee by id.
        /// </summary>
        /// <param name="userId">Employee id</param>
        /// <returns>If found the function returns the complete user object</returns>
        public static User GetUser(int userId)
        {
            string connectionString = GetExcelDatabaseConnectionString();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Users$] WHERE UserId = " + userId + " ", connection))
                {
                    var userOleDbReader = cmd.ExecuteReader();
                    if (userOleDbReader.Read())
                    {
                        return new User(userOleDbReader);
                    }
                    return null;
                }
            }
        }
        /// <summary>
        /// Vinberg: Unlock an user account and give it a new password
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <returns>False if unsuccessfull</returns>
        public static bool UnlockUserAccount(string userName, string password)
        {
            if (CheckForPasswordReuse(userName, password))
                return false;
            if (!TestPasswordMinimumRequirements(password))
                return false;
            userName = StringManipulation.Neutralize(userName);
            string hashedPassword = HashPassword(password).Replace("'", "");
            string connectionString = GetExcelDatabaseConnectionString();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand("UPDATE [Users$] SET [Password] = '"+ hashedPassword + "', [FailedAttempts]=0, [TemporaryDisabled]='1900-01-01', [PasswordExpires] = '"+DateTime.Now.AddYears(2).ToString("yyyy-MM-dd")+"' WHERE [Username] = '" + userName + "' ", connection))
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }
        /// <summary>
        /// Changes the user password
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public static bool ChangePassword(string userName, string password)
        {
            return UnlockUserAccount(userName, password);
        }
        /// <summary>
        /// Check that minimum password strength is fulfilled
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static bool TestPasswordMinimumRequirements(string password)
        {
            if (password.Length < 12 || !Regex.Match(password, @"\d+").Success || !Regex.Match(password, @"[a-z]").Success || !Regex.Match(password, @"[A-Z]").Success || !Regex.Match(password, @".[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]").Success)
                return false;
            return true;

        }

        /// <summary>
        /// Check if password is same as last password
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <returns>True if password matches old password</returns>
        private static bool CheckForPasswordReuse(string userName, string password)
        {
            userName = StringManipulation.Neutralize(userName);
            string hashedPassword = HashPassword(password).Replace("'", "");
            string connectionString = GetExcelDatabaseConnectionString();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Users$] WHERE Password = '" + hashedPassword + "' AND Username = '" + userName + "' ", connection))
                {
                    var userOleDbReader = cmd.ExecuteReader();
                    if(userOleDbReader.HasRows)
                        return true;
                    return false;
                }
            }
        }


        /// <summary>
        /// Vinberg: Get all sickleave database entries
        /// </summary>
        /// <param name="user">User of whom you are getting sickleave entries for</param>
        /// <returns>List of Sickleave entries</returns>
        public static List<Sickleave> GetSickleaveList(User user)
        {
            List<Sickleave> userSickleave = new List<Sickleave>();
            string connectionString = GetExcelDatabaseConnectionString();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sickleave$] WHERE UserId = '" + user.UserId + "' ", connection))
                {
                    var userOleDbReader = cmd.ExecuteReader();
                    while (userOleDbReader.Read())
                    {
                        userSickleave.Add(new Sickleave(userOleDbReader));
                    }

                }
            }
            return userSickleave;
        }
        /// <summary>
        /// Vinberg: Increment failed login attempt if user exists, when incremented to 10 user will be locked out permanently
        /// </summary>
        /// <param name="userName">Username specified in failed login</param>
        private static void IncrementFailedAttempt(string userName)
        {
            userName = StringManipulation.Neutralize(userName);
            string connectionString = GetExcelDatabaseConnectionString();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand("UPDATE [Users$] SET FailedAttempts = FailedAttempts+1 WHERE Username = '" + userName + "' ", connection))
                {
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT FailedAttempts FROM [Users$] WHERE Username = '"+userName+"'";
                    
                    object result = cmd.ExecuteScalar();
                    if (result != System.DBNull.Value && result != null)
                    {
                        if (((double)result) == 5)
                            TemporaryLockAccount(userName);
                    }

                }
            }
        }
        /// <summary>
        /// Vinberg: Reset failed login attempts after successfull login
        /// </summary>
        /// <param name="user">User for whom to reset login attepts</param>
        private static void ResetFailedAttempt(User user)
        { 
            string connectionString = GetExcelDatabaseConnectionString();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand("UPDATE [Users$] SET FailedAttempts = 0 WHERE UserId = " + user.UserId + " ", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Vinberg: Temporary lock account for 1 day after five failed login attempts
        /// </summary>
        /// <param name="userName">Username of account to be locked out</param>
        public static void TemporaryLockAccount(string userName)
        {
            userName = StringManipulation.Neutralize(userName);
            string connectionString = GetExcelDatabaseConnectionString();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand("UPDATE [Users$] SET TemporaryDisabled = '"+DateTime.Now.ToString("yyyy-MM-dd")+"' WHERE Username = '" + userName + "' ", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Vinberg: Internal function for hashing userpasswords
        /// </summary>
        /// <param name="password">Raw password</param>
        /// <returns>Hashed password</returns>
        private static string HashPassword(string password)
        {
            return Utilities.Hashing.SHA256Hashing.Hash(Utilities.Hashing.SHA256Hashing.Hash(password, "#¤!3supersecretSalt12123$1ASXZ3#%¤"), "Sal5s123!#¤a123SA");
        }
        public static void AddSickleave(User user, Sickleave.SickleaveType sickleaveType, DateTime fromDate, DateTime toDate, long childSocNo)
        {
            string connectionString = GetExcelDatabaseConnectionString();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand("INSERT INTO [Sickleave$] VALUES("+ GetNextSickleaveID() + ", " + (int)sickleaveType + ", " +user.UserId + ", #" + fromDate.ToString("yyyy-MM-dd") + "#, #"+ toDate.ToString("yyyy-MM-dd") + "#, "+ childSocNo+ ")", connection))
                {
                    //cmd.CommandText = "CREATE TABLE [Sickleave] ([SickleaveId] Numeric, [SickleaveTypeId] Numeric, [UserId] Numeric, [FromDate] Date, [ToDate] Date, [ChildSocNo] Numeric)";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Vinberg: Add log message
        /// </summary>
        /// <param name="ipAddress">Source ip</param>
        /// <param name="action">Log message</param>
        /// <param name="userId">UserId of logged in user. Skip if not available, not compulsory parameter</param>
        public static void AddLogMessage(string ipAddress, string action, int userId = -1)
        {
            ipAddress = StringManipulation.Neutralize(ipAddress);
            action = StringManipulation.Neutralize(action);
            string connectionString = GetExcelDatabaseConnectionString();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand("INSERT INTO [LogActivity$] VALUES(" + GetNextLogID() + ", " + userId + ", #" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "#, '" + ipAddress + "', '" + action + "')", connection))
                {
                    //cmd.CommandText = "CREATE TABLE [LogActivity] ([LogId] Numeric, [UserId] Numeric, [DateTime] Date, [Ip] memo, [Action] memo)";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Vinberg: Database table id management, due to lacking functionality in excel
        /// </summary>
        /// <returns>Next available id for sickleave table</returns>
        private static int GetNextSickleaveID()
        {
            if (NextSickleaveID == 0)
            { 
                string connectionString = GetExcelDatabaseConnectionString();
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand cmd = new OleDbCommand("SELECT MAX(SickleaveId) AS SickleaveId FROM [Sickleave$]", connection))
                    {
                        object sickleaveId = cmd.ExecuteScalar();
                        if (sickleaveId != System.DBNull.Value)
                            NextSickleaveID = int.Parse((string) sickleaveId);
                    }
                }
            }
            NextSickleaveID++;
            return NextSickleaveID;
        }
        /// <summary>
        /// Vinberg: Database table id management, due to lacking functionality in excel
        /// </summary>
        /// <returns>Next available id for log table</returns>
        private static int GetNextLogID()
        {
            if (NextLogId == 0)
            {
                string connectionString = GetExcelDatabaseConnectionString();
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand cmd = new OleDbCommand("SELECT MAX(LogId) AS LogId FROM [LogActivity$]", connection))
                    {
                        object logId = cmd.ExecuteScalar();
                        if (logId != System.DBNull.Value)
                            NextLogId = int.Parse((string) logId);
                    }
                }
            }
            NextLogId++;
            return NextLogId;
        }
        /// <summary>
        /// Get the connectionstring to the database
        /// </summary>
        /// <returns>Complete connection string</returns>
        private static string GetExcelDatabaseConnectionString()
        {
            return "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + System.Web.HttpRuntime.BinDirectory + "Database.xls';Mode=ReadWrite;Extended Properties=\"Excel 8.0;HDR=YES;IMEX=0;MAXSCANROWS=0\" ";
        }
    }
}
