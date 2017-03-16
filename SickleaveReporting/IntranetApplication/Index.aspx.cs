using System;
using System.Web.UI;
using DatabaseInterface;
using DatabaseInterface.Classes;

namespace IntranetApplication
{
    public partial class Index : Page
    {

        protected void Page_Load(object sender, EventArgs e) {
            if (IsCallback)
                return;

            Excel.AddLogMessage(Request.UserHostAddress, "Redirected from main page to admin login");
        }


        protected void LogInAsAdminOnClick(object sender, EventArgs e) {
            var login = loginTB.Text;
            var pass = pwTB.Text;
            var user = Excel.CheckForUser(login, pass);

            if (user == null) {
                Excel.AddLogMessage(Request.UserHostAddress, $"Failed attempt to log in as : {login}");
                return;
            }

            Excel.AddLogMessage(Request.UserHostAddress, $"Logged in as admin UserName: {user.UserName}", user.UserId);
            Session["User"] = user;
            Response.Redirect("~/SickLeaveView.aspx");
        }

    }
}