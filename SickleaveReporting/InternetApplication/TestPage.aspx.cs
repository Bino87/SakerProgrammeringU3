using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;
using DatabaseInterface;
using DatabaseInterface.Classes;

namespace InternetApplication
{
    public partial class Index : Page
    {
        /// <summary>
        /// Vinberg: Page to test out backend functionality
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
			if(IsCallback)
				 return;
            var user = Excel.CheckForUser("davi006", "uberPasswordDeluxe123%");
            user = Excel.CheckForUser("tofu23", "v3g3TaR1an!#");
            user = Excel.CheckForUser("kabi007", "#J4m3sB1konD%");


            Excel.AddSickleave(user, Sickleave.SickleaveType.SickWithRef, DateTime.Parse("2017-02-25"), DateTime.Parse("2017-03-12"), 0);
            Excel.AddSickleave(user, Sickleave.SickleaveType.SickWithOutRef, DateTime.Parse("2017-03-13"), DateTime.Parse("2017-03-28"), 0);
            Excel.AddLogMessage(Request.UserHostAddress, "Opened testpage: " + Request.Url, user.UserId);
            Excel.AddLogMessage(Request.UserHostAddress, "Opened testpage line 2: " + Request.Url, user.UserId);
            Excel.AddLogMessage(Request.UserHostAddress, "Opened testpage line 3: " + Request.Url);
            bool result = Excel.ChangePassword("davi006", "uberPasswordDeluxe123%");
            result = Excel.ChangePassword("davi006", "uberPasswordDeluxe1234%");
            user = Excel.GetUser(3);
            List<Sickleave> sickleaveList = Excel.GetSickleaveList(user);
            var user2 = Excel.GetUser(1);
        }

	    protected void loginBtn_OnClick(object sender, EventArgs e) {
		    errorLbl.Text = "";
		    var user = Excel.CheckForUser(loginTB.Text, pwTB.Text);
		    if (user != null) {
                //var cookie =  new HttpCookie("user");
                //cookie["id"] = user.UserId.ToString();
                //cookie["userN"] = user.UserName;
                //cookie["fName"] = user.FirstName;
                //cookie["lName"] = user.LastName;
                //Response.Cookies.Add(cookie);
                Session["User"] = user; 
                Excel.AddLogMessage(Request.UserHostAddress,$"redirecting to: {Response.RedirectLocation}", user.UserId);
                Server.Transfer("~/User/LoggedUser.aspx");

            } else {
			    errorLbl.Text = "Login failed!";
                Excel.AddLogMessage(Request.UserHostAddress, "Login failed for user "+ loginTB.Text);
            }
	    }
    }
}