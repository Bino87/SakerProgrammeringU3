﻿
using System.Data.OleDb;
using Utilities;
using DatabaseInterface.Classes;
using System;

namespace DatabaseInterface
{
    public static class Excel
    {
        private static int NextSickleaveID = 0;
        private static int NextLogId = 0;
        public static User CheckForUser(string userName, string password)
        {
            userName = StringManipulation.Neutralize(userName);
            password = HashPassword(password).Replace("'", "");
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
                using (OleDbCommand cmd = new OleDbCommand("INSERT INTO [Sickleave$] VALUES("+ GetNextSickleaveID() + ", "+user.UserId+ ", "+(int)sickleaveType + ", #"+ fromDate.ToString("yyyy-MM-dd") + "#, #"+ toDate.ToString("yyyy-MM-dd") + "#, "+ childSocNo+ ")", connection))
                {
                    //cmd.CommandText = "CREATE TABLE [Sickleave] ([SickleaveId] Numeric, [SickleaveTypeId] Numeric, [UserId] Numeric, [FromDate] Date, [ToDate] Date, [ChildSocNo] Numeric)";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void AddLogMessage(User user, string ipAddress, string action)
        {
            ipAddress = StringManipulation.Neutralize(ipAddress);
            action = StringManipulation.Neutralize(action);
            string connectionString = GetExcelDatabaseConnectionString();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand("INSERT INTO [LogActivity$] VALUES(" + GetNextLogID() + ", " + user.UserId + ", #" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "#, '" + ipAddress + "', '" + action + "')", connection))
                {
                    //cmd.CommandText = "CREATE TABLE [LogActivity] ([LogId] Numeric, [UserId] Numeric, [DateTime] Date, [Ip] memo, [Action] memo)";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static int GetNextSickleaveID()
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
        public static int GetNextLogID()
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

        private static string GetExcelDatabaseConnectionString()
        {

            return "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + System.Web.HttpRuntime.BinDirectory + "Database.xls';Mode=ReadWrite;Extended Properties=\"Excel 8.0;HDR=YES;IMEX=0;MAXSCANROWS=0\" ";
        }
    }
}
