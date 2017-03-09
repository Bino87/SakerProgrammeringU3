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
            User user = new User(1);

            Excel.AddSickleave(user, Sickleave.SickleaveType.Sick, DateTime.Parse("2017-02-25"), DateTime.Parse("2017-03-12"), 0);
        }
    }
}