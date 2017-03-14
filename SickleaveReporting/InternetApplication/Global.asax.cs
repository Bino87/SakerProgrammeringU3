using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace InternetApplication
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition(
           "jquery",
           new ScriptResourceDefinition
           {
               Path = "/static/scripts/jquery-1.8.3.min.js",
               DebugPath = "/static/scripts/jquery-1.8.3.js",
               CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js",
               CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.js",
               CdnSupportsSecureConnection = true,
               LoadSuccessExpression = "jQuery"
           });
        }
    }
}