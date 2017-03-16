using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseInterface;

namespace InternetApplication
{
    public partial class ChangePassword : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e) {
        }

        protected void OnClick(object sender, EventArgs e) {
            var login = loginTB.Text;
            var oPass = oPassTB.Text;

            var user = Excel.CheckForUser(login, oPass);

            var nPass = nPassTB.Text;
            if (user != null) {
                Excel.ChangePassword(login, nPass);
                Excel.AddLogMessage(Request.UserHostAddress, $"password change for user: {login} was succesfull!",user.UserId);
            } else
                Excel.AddLogMessage(Request.UserHostAddress, $"password change for user: {login} was unsuccesfull");
        }

    }
}