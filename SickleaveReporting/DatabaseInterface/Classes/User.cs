using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface.Classes
{
    public class User
    {
        private int userId;
        private string firstName;
        private string lastName;
        private string userName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public User(OleDbDataReader reader)
        {
            reader.Read();
            userId = Convert.ToInt32((double) reader["UserId"]);
            firstName = (string)reader["FirstName"];
            lastName = (string)reader["LastName"];
            userName = (string)reader["UserName"];
            reader.Close();
        }
        public User(int userId_)
        {
            userId = userId_;
        }

        public int UserId        => userId; 
        public string FirstName  => firstName; 
        public string LastName   => lastName; 
        public string UserName   => userName; 
    }
}
