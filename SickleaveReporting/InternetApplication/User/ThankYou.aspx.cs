using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseInterface;

namespace InternetApplication.User
{
    public partial class ThankYou : System.Web.UI.Page
    {
        /// <summary>
        /// Binko: sets up the display message after succesfully submiting sick leave.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e) {
            var user = (DatabaseInterface.Classes.User)Session["User"];

            var from = DateTime.Parse(Session["from"].ToString());
            var to = DateTime.Parse(Session["to"].ToString());


            thanksLbl.Text =
                $"{user.FirstName} {user.LastName} din frånvaro från {from.ToString("yyyy-MM-dd")} till {to.ToString("yyyy-MM-dd")} hade blivit registrerad!";
        }

        /// <summary>
        /// Binko: Returns the user to testpage which is the default page. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BackOnClick(object sender, EventArgs e) {
            var user = (DatabaseInterface.Classes.User)Session["User"];
            Excel.AddLogMessage(Request.UserHostAddress, $"Redirected to: {Request.Url} from: {Request.UrlReferrer}",
                user.UserId);
            Session.Remove("User");
            Session.Remove("from");
            Session.Remove("to");

            Response.Redirect("~/TestPage.aspx");
        }

    }
}