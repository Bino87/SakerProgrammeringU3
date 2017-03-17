﻿using System;
using DatabaseInterface;
using DatabaseInterface.Classes;

namespace IntranetApplication
{
    public partial class SickLeaveView : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e) {
            if (IsCallback)
                return;

            var user = (User)Session["User"];

            Excel.AddLogMessage(Request.UserHostAddress, $"redirected to {Request.Url} from {Request.UrlReferrer}",
                user.UserId);
        }

        protected void OnClick(object sender, EventArgs e) {
            var user = Excel.GetUser(int.Parse(searchbyIDTB.Text));
            if (user == null) {
                ErrorLbl.Text = "User not found!";
                return;
            }

            ErrorLbl.Text = string.Empty;
            var sickLeaveData = Excel.GetSickleaveList(user);


            silckLeaveGV.DataSource = SickLeaveWraper.TripSickLeaveData(sickLeaveData);
            silckLeaveGV.DataBind();

            if (sickLeaveData.Count == 0) {
                ErrorLbl.Text = "No sick leaves found!";
            }

            var admin = (User)Session["User"];
            Excel.AddLogMessage(Request.UserHostAddress,
                $"{admin.UserName} checked for sick leave of: {Excel.GetUser(int.Parse(searchbyIDTB.Text)).UserName}",
                admin.UserId);
        }

    }
}