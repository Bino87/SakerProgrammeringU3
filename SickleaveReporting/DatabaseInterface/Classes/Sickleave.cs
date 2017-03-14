using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterface.Classes
{
    /// <summary>
    /// Sickleave object representation
    /// </summary>
    public class Sickleave
    {
        /// <summary>
        /// Different types of sickleave entries
        /// </summary>
        public enum SickleaveType
        {
            SickWithOutRef = 0,
            SickWithRef = 1,
            VAB = 2
        }

        private SickleaveType sickLeaveType;
        private int sickleaveId;
        private long userId;
        private DateTime fromDate;
        private DateTime toDate;
        private string childSocNo;
        /// <summary>
        /// Class only instantiatable by database reader
        /// </summary>
        /// <param name="reader"></param>
        public Sickleave(OleDbDataReader reader)
        {
            sickleaveId = int.Parse((string) reader["SickleaveId"]);
            sickLeaveType = (SickleaveType) int.Parse((string) reader["SickleaveTypeId"]);
            userId = long.Parse((string) reader["UserId"]);
            fromDate = DateTime.Parse((string) reader["FromDate"]);
            toDate = DateTime.Parse((string) reader["ToDate"]);
            childSocNo = (string) reader["ChildSocNo"];

        }

        public SickleaveType SickLeaveType { get => sickLeaveType; }
        public long SickleaveId { get => sickleaveId; }
        public long UserId { get => userId;  }
        public DateTime FromDate { get => fromDate;}
        public DateTime ToDate { get => toDate; }
        public string ChildSocNo { get => childSocNo; }

       
    }
}
