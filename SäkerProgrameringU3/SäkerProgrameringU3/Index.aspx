<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SäkerProgrameringU3.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="position: fixed; left: 50%; top: 25%; background-color: rgb(100,255,100); width: 326px;">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label> <br/>
        <asp:Label ID="loginLbl" runat="server" Text="Login:" Width="70"></asp:Label><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br/>
        <asp:Label ID="pwLbl" runat="server"    Text="Password:" Width="70"></asp:Label><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br/>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /><br/>
        

    </div>
    </form>
</body>
</html>
