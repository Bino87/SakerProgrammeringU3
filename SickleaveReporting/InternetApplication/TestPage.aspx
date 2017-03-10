<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="InternetApplication.TestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" >
        <div style="position: absolute; top: 15%; left: 35%">


            <asp:Label runat="server" Text="Login:" Width="75px" />
            <asp:TextBox runat="server" ID="loginTB" />
            <br />
            <asp:Label runat="server" Text="Password" Width="75px" />
            <asp:TextBox runat="server" ID="pwTB" TextMode="Password"  />

            <div style="position: relative; left: 78px; top: 5px">
                <asp:Button runat="server" ID="loginBTn" OnClick="loginBtn_OnClick" Text="Log in" />
            </div>
            <div style="position: relative; top: 8px; left: 1%">

                <asp:ValidationSummary runat="server" ShowMessageBox="False" HeaderText="Following input errors:"
                    ShowSummary="True" DisplayMode="BulletList" ForeColor="red" Width="600px" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="loginTB" ErrorMessage="Login is required"
                    ForeColor="red" EnableClientScript="True" Display="None" SetFocusOnError="True" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="pwTB" ErrorMessage="Password is required"
                    ForeColor="red" EnableClientScript="True" Display="None" SetFocusOnError="True" />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="pwTB"
                    ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{12,}"
                    ErrorMessage="Password must contain: Minimum 12 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character ($@$!%*?&)"
                    ForeColor="Red" Display="None" /> 
            </div>
        </div>
    </form>
</body>
</html>
