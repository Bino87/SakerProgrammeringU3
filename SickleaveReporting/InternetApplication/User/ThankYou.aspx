<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThankYou.aspx.cs" Inherits="InternetApplication.User.ThankYou" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="position: relative; left: 15%; width: 70%; top: 100px; background-color: gainsboro">

        <asp:Label runat="server" Text="" ID="thanksLbl"/>
        <br/>
        <asp:Button runat="server" Text="Back" OnClick="BackOnClick"/>
    
    </div>
    </form>
</body>
</html>
