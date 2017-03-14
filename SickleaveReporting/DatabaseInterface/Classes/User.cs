using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface.Classes
{
    /// <summary>
    /// Object representation of a user
    /// </summary>
    public class User
    {
        private int userId;
        private string firstName;
        private string lastName;
        private string userName;
        private DateTime passwordExpires;


        /// <summary>
        /// Only instantiatable by database reader
        /// </summary>
        /// <param name="reader"></param>
        public User(OleDbDataReader reader)
        {
            userId = Convert.ToInt32((double) reader["UserId"]);
            firstName = (string)reader["FirstName"];
            lastName = (string)reader["LastName"];
            userName = (string)reader["UserName"];
            passwordExpires = (DateTime)reader["PasswordExpires"];
        }

        public int UserId        => userId; 
        public string FirstName  => firstName; 
        public string LastName   => lastName; 
        public string UserName   => userName;

        public DateTime PasswordExpires { get => passwordExpires; }
    }
}
