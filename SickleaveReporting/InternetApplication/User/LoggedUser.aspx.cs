using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;
using DatabaseInterface;
using DatabaseInterface.Classes;
using static DatabaseInterface.Classes.Sickleave;

namespace InternetApplication.User
{
    public partial class LoggedUser : System.Web.UI.Page
    {

        /// <summary>
        /// Binko: Sets up the logger user page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e) {
            if (IsCallback)
                return;
            if (IsPostBack)
                return;

            var user = (DatabaseInterface.Classes.User)Session["User"];
            Session["User"] = user;
            var fName = user.FirstName;
            var lName = user.LastName;

            nameLbl.Text = $"Hello {fName} {lName}!";

            PopulateDropDownListWithValues();

            Excel.AddLogMessage(Request.UserHostAddress, $"Redirected to: {Request.Url} from: {Request.UrlReferrer}",
                user.UserId);
            
            var temp = Excel.GetSickleaveList(user);

            sickLeaveBL.DataSource = SickLeaveWraper.TripSickLeaveData(new List<Sickleave>());

            sickLeaveBL.DataBind();
        }

        /// <summary>
        /// Binko: Asigns values to sick leave drop down list.
        /// </summary>
        private void PopulateDropDownListWithValues() {
            leaveTypeDdl.Items.Add(new ListItem("Sjukskrivning utan läkarintyg",
                ( (int)SickleaveType.SickWithOutRef ).ToString()));
            leaveTypeDdl.Items.Add(new ListItem("Sjukskrivning med läkarintyg", ( (int)SickleaveType.SickWithRef ).ToString()));
            leaveTypeDdl.Items.Add(new ListItem("Vård av barn", ( (int)SickleaveType.VAB ).ToString()));
        }

        /// <summary>
        /// Binko: Sibmits sick leave. field checking is handled by validators.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e) {
            var user = (DatabaseInterface.Classes.User)Session["User"];
            var sickLeavetype = (SickleaveType)int.Parse(leaveTypeDdl.SelectedValue);
            long prNr;
            var startDate = DateTime.Parse(Request.Form[txtDateTB.UniqueID]);
            var endDate = DateTime.Parse(Request.Form[txtDateEndTB.UniqueID]);
            Excel.AddSickleave(user, sickLeavetype, startDate, endDate,
                long.TryParse(childPersonNrTB.Text, out prNr) ? prNr : 0);

            Session["from"] = startDate;
            Session["to"] = endDate;
            Response.Redirect("~/User/ThankYou.aspx");
        }

        /// <summary>
        /// Binko: Turns on/off personnummer textbox and its validator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void leaveTypeDdl_OnSelectedIndexChanged(object sender, EventArgs e) {
            if (leaveTypeDdl.SelectedIndex == leaveTypeDdl.Items.Count - 1) {
                childPersonNrTB.ReadOnly = false;
                childPersonNrRFV.Enabled = true;
            } else {
                childPersonNrTB.ReadOnly = true;
                childPersonNrTB.Text = string.Empty;
                childPersonNrRFV.Enabled = false;
            }
        }

    }
}