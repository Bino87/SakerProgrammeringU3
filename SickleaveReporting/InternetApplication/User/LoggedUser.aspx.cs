using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseInterface;
using static DatabaseInterface.Classes.Sickleave;

namespace InternetApplication.User
{
    public partial class LoggedUser : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e) {
            if (IsCallback)
                return;
            if (IsPostBack)
                return;

            var cookie = Request.Cookies["user"];

            if (cookie == null)
                Response.Redirect("~/TestPage.aspx");

            if (cookie == null)
                return;
            var userID = int.Parse(cookie["id"]);

            var user = new DatabaseInterface.Classes.User(userID);
            var fName = cookie["fName"];
            var lName = cookie["lName"];

            nameLbl.Text = $"Hello {fName} {lName}!";

            leaveTypeDdl.Items.Add(new ListItem("Myself", ( (int)SickleaveType.Sick ).ToString()));
            leaveTypeDdl.Items.Add(new ListItem("Child", ( (int)SickleaveType.WAB ).ToString()));


            Excel.AddLogMessage(user, Request.UserHostAddress,
                $"Redirected to: {Request.Url} from: {Request.UrlReferrer}");
        }

        protected void btnSubmit_Click(object sender, EventArgs e) {
            var cookie = Request.Cookies["user"];
            var userID = int.Parse(cookie["id"]);
            var user = new DatabaseInterface.Classes.User(userID);
            var sickLeavetype = leaveTypeDdl.SelectedIndex == 0 ? SickleaveType.Sick : SickleaveType.WAB;
            long prNr;
            var startDate = DateTime.Parse(Request.Form[txtDateTB.UniqueID]);
            var endDate = DateTime.Parse(Request.Form[txtDateEndTB.UniqueID]);
            Excel.AddSickleave(user, sickLeavetype, startDate, endDate,
                long.TryParse(childPersonNrTB.Text, out prNr) ? prNr : 0);
        }

        protected void leaveTypeDdl_OnSelectedIndexChanged(object sender, EventArgs e) {
            if (leaveTypeDdl.SelectedIndex == 0) {
                childPersonNrTB.ReadOnly = true;
                childPersonNrTB.Text = string.Empty;
                childPersonNrRFV.Enabled = false;
            } else {
                childPersonNrTB.ReadOnly = false;
                childPersonNrRFV.Enabled = true;
            }
        }

    }
}