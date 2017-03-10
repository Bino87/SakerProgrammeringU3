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

        public User(OleDbDataReader reader)
        {
            reader.Read();
            userId = Convert.ToInt32((double) reader["UserId"]);
            firstName = (string)reader["Firstname"];
            lastName = (string)reader["LastName"];
            userName = (string)reader["UserName"];
            reader.Close();
        }
        public User(int userId_)
        {
            userId = userId_;
        }

        public int UserId { get => userId; }
        public string FirstName { get => firstName; }
        public string UserName { get => userName; }
        public string LastName { get => lastName;  }
    }
}
