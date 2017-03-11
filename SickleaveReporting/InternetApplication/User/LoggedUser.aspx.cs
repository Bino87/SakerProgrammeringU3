using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseInterface;

namespace InternetApplication.User
{
	public partial class LoggedUser: System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e){

			if(IsCallback) return;


		    var user = new DatabaseInterface.Classes.User(int.Parse(Request.Cookies["user"]["id"]));

		    Excel.AddLogMessage(user, Request.UserHostAddress, $"Redirected to: {Request.Url} from: {Request.UrlReferrer}");
		}
	}
}