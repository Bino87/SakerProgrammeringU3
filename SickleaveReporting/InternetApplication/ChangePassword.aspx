<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs"
    Inherits="InternetApplication.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Label runat="server" Text="Login:" Width="150px" />
            <asp:TextBox ID="loginTB" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="loginTB" ErrorMessage="This field is required!"
                ForeColor="red" Display="Dynamic" EnableClientScript="True" SetFocusOnError="True"/>
            
            <br />

            <asp:Label runat="server" Text="Old Password:" Width="150px" />
            <asp:TextBox ID="oPassTB" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="oPassTB" ErrorMessage="This field is required!"
                ForeColor="red" Display="Dynamic" EnableClientScript="True" SetFocusOnError="True"/>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="oPassTB"
                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{12,}"
                ErrorMessage="Old assword must contain: Minimum 12 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character ($@$!%*?&)"
                ForeColor="Red" Display="Dynamic" EnableClientScript="True" SetFocusOnError="True"/>
            <asp:CompareValidator runat="server" ControlToValidate="oPassTB" ControlToCompare="nPassTB" Type="String" Operator="NotEqual" ErrorMessage="New password and the old passwords are the same " ForeColor="red"/>
            <br />

            <asp:Label runat="server" Text="New password:" Width="150px" />
            <asp:TextBox ID="nPassTB" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="nPassTB" ErrorMessage="This field is required!"
                ForeColor="red" Display="Dynamic" EnableClientScript="True" SetFocusOnError="True"/>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="nPassTB"
                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{12,}"
                ErrorMessage="New password must contain: Minimum 12 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character ($@$!%*?&)"
                ForeColor="Red" Display="Dynamic" EnableClientScript="True" SetFocusOnError="True"/>
            <br />

            <asp:Label runat="server" Text="Repeat new passowrd:" Width="150px" />
            <asp:TextBox ID="rnPassTB" runat="server" TextMode="Password"  />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="rnPassTB" ErrorMessage="This field is required!"
                ForeColor="red" Display="Dynamic" EnableClientScript="True" SetFocusOnError="True"/>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="rnPassTB"
                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{12,}"
                ErrorMessage="New password must contain: Minimum 12 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character ($@$!%*?&)"
                ForeColor="Red" Display="Dynamic" EnableClientScript="True" SetFocusOnError="True"/>

            <asp:CompareValidator runat="server" ControlToCompare="nPassTB" ControlToValidate="rnPassTB" Type="String" Operator="Equal" ErrorMessage="New passwords does not match" ForeColor="red"/>
            <br />
            
            <asp:Button runat="server" Text="Submit" OnClick="OnClick"/>
            
             <%--<asp:ValidationSummary runat="server" ShowMessageBox="False" HeaderText="Following input errors:"
                    ShowSummary="True" DisplayMode="BulletList" ForeColor="red" Width="600px" />--%>

        </div>
    </form>
</body>
</html>
