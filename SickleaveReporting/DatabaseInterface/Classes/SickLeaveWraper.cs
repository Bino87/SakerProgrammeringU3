using System.Collections.Generic;
using System.Linq;

namespace DatabaseInterface.Classes
{
    public class SickLeaveWraper
    {

        public string UserId{ get; private set; }
        public string SickLeaveType{ get; private set; }
        public string FromDate{ get; private set; }
        public string ToDate{ get; private set; }
        public string ChildSocNo{ get; private set; }

        SickLeaveWraper(Sickleave sl) {
            UserId = sl.UserId.ToString();
            FromDate = sl.FromDate.ToString("yyyy MM dd");
            ToDate = sl.ToDate.ToString("yyyy MM dd");
            ChildSocNo = sl.ChildSocNo == "0" ? "N/A" : sl.ChildSocNo;

            switch (sl.SickLeaveType) {
                case Sickleave.SickleaveType.SickWithOutRef:
                    SickLeaveType = "Sjukskrivning utan läkarintyg";
                    break;
                case Sickleave.SickleaveType.SickWithRef:
                    SickLeaveType = "Sjukskrivning med läkarintyg";
                    break;
                case Sickleave.SickleaveType.VAB:
                    SickLeaveType = "Vård av barn";
                    break;
                default:
                    SickLeaveType = "N/A";
                    break;
            }
        }

        public static List<SickLeaveWraper> TripSickLeaveData(List<Sickleave> list) {
            var wrapedList = new List<SickLeaveWraper>(list.Count);
            wrapedList.AddRange(list.Select(sickleave => new SickLeaveWraper(sickleave)));

            return wrapedList;
        }

    }
}