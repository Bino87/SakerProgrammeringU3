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
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			if(IsCallback)
				 return;
            User user = DatabaseInterface.Excel.CheckForUser("davi006", "uberPasswordDeluxe123%");
            user = DatabaseInterface.Excel.CheckForUser("tofu23", "v3g3TaR1an!#");
            user = DatabaseInterface.Excel.CheckForUser("kabi007", "#J4m3sB1konD%");


            Excel.AddSickleave(user, Sickleave.SickleaveType.Sick, DateTime.Parse("2017-02-25"), DateTime.Parse("2017-03-12"), 0);
            Excel.AddLogMessage(user, Request.UserHostAddress, "Opened testpage: "+Request.Url);
            Excel.AddLogMessage(user, Request.UserHostAddress, "Opened testpage line 2: " + Request.Url);
            Excel.AddLogMessage(user, Request.UserHostAddress, "Opened testpage line 3: " + Request.Url);
        }

	    protected void loginBtn_OnClick(object sender, EventArgs e) {

		    if (Excel.CheckForUser(loginTB.Text, pwTB.Text) != null) {
			    //check if acc is closed. 
				//if closed then give warnign.
				//else redirect and add log message.
		    } else {
			    //login failed 
				//increment user failed attempt
				//if failed attempt > maxfailed attempt 
				//close account
		    }


	    }

    }
}