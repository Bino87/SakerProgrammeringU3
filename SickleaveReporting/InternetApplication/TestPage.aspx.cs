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
        protected void Page_Load(object sender, EventArgs e) {
            if (IsCallback)
                return;
            if (IsPostBack)
                return;
            return;
        }

        /// <summary>
        /// Binko: Login buton 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void loginBtn_OnClick(object sender, EventArgs e) {
            errorLbl.Text = "";
            var user = Excel.CheckForUser(loginTB.Text, pwTB.Text);


            if (user != null) {
                Session["User"] = user;
                Response.Redirect("~/User/LoggedUser.aspx");
            } else {
                errorLbl.Text = "Login failed!";
                Excel.AddLogMessage(Request.UserHostAddress, "Login failed for user " + loginTB.Text);
            }
        }

        /// <summary>
        /// Binko: Change password button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick(object sender, EventArgs e) {
            
            Excel.AddLogMessage(Request.UserHostAddress,$"Change password via IP: {Request.UserHostAddress}");
            Response.Redirect("~/ChangePassword.aspx");
        }

    }
}