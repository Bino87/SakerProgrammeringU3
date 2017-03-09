<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SäkerProgrameringU3.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body runat="server" id="body">
    <form id="form1" runat="server">
        <div style="position: fixed; left: 35%; top: 25%; width: 20%;">
        
            
            <asp:Label ID="loginLbl" runat="server" Text="Login:" Width="75"></asp:Label>
            <asp:TextBox ID="loginTB" runat="server"></asp:TextBox><br />
            
            <asp:Label ID="pwLbl" runat="server" Text="Password:" Width="75"></asp:Label>
            <asp:TextBox ID="pwTB" runat="server" TextMode="Password"></asp:TextBox><br />

            <div style="position: relative; left: 78px; top: 5px" >

                <asp:Button ID="loginBtn" runat="server" Text="Log in" OnClick="LoginBtn_Click" /><br />
            </div>
        </div>
    </form>
</body>
</html>
