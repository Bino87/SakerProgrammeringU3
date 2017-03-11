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
        protected void Page_Load(object sender, EventArgs e)
        {
			if(IsCallback)
				 return;
            var  user = Excel.CheckForUser("davi006", "uberPasswordDeluxe123%");
            user = Excel.CheckForUser("tofu23", "v3g3TaR1an!#");
            user = Excel.CheckForUser("kabi007", "#J4m3sB1konD%");


            Excel.AddSickleave(user, Sickleave.SickleaveType.Sick, DateTime.Parse("2017-02-25"), DateTime.Parse("2017-03-12"), 0);
            Excel.AddLogMessage(user, Request.UserHostAddress, "Opened testpage: "+Request.Url);
            Excel.AddLogMessage(user, Request.UserHostAddress, "Opened testpage line 2: " + Request.Url);
            Excel.AddLogMessage(user, Request.UserHostAddress, "Opened testpage line 3: " + Request.Url);
        }

	    protected void loginBtn_OnClick(object sender, EventArgs e) {
		    errorLbl.Text = "";
		    var user = Excel.CheckForUser(loginTB.Text, pwTB.Text);
		    if (user != null) {
                var cookie =  new HttpCookie("user");
		        cookie["id"] = user.UserId.ToString();
		        cookie["userN"] = user.UserName;
		        cookie["fName"] = user.FirstName;
		        cookie["lName"] = user.LastName;
                Response.Cookies.Add(cookie);
				Response.Redirect("~/User/LoggedUser.aspx");
                Excel.AddLogMessage(user,Request.UserHostAddress,$"redirecting to: {Response.RedirectLocation}");
                
		    } else {
			    errorLbl.Text = "Login failed!";
		    }
	    }
    }
}