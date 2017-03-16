<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoggedUser.aspx.cs" Inherits="InternetApplication.User.LoggedUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=txtDateTB]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'https://www.aspsnippets.com/demos/img/calendar.png'
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id$=txtDateEndTB]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'https://www.aspsnippets.com/demos/img/calendar.png'
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="position: relative; left: 15%; width: 70%; top: 100px; background-color: gainsboro">
            <div style="position: relative; left: 15%">
                <br />
                <asp:Label runat="server" ForeColor="green" ID="nameLbl" Font-Size="16px" />
            </div>
            <br />
            <div style="position: relative; left: 5%; width: 90%;">
                <asp:Label runat="server" Width="122px" Text="Type of Sick leave:" />
                <asp:DropDownList runat="server" ID="leaveTypeDdl" OnSelectedIndexChanged="leaveTypeDdl_OnSelectedIndexChanged"
                    AutoPostBack="True" /><br />

                <asp:Label runat="server" Width="122px" Text="From: " />
                <asp:TextBox ID="txtDateTB" runat="server" ReadOnly="true" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDateTB" ErrorMessage="This field is required!"
                    Display="Dynamic" ForeColor="red" />
                <br />

                <asp:Label runat="server" Width="122px" Text="To:" />
                <asp:TextBox ID="txtDateEndTB" runat="server" ReadOnly="true" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDateEndTB" ErrorMessage="This field is required!"
                    Display="Dynamic" ForeColor="red" />

                <br />
                <asp:Label runat="server" Width="122px" Text="Child's peronnumber: " />
                <asp:TextBox runat="server" ID="childPersonNrTB" />
                <asp:RegularExpressionValidator runat="server" ID="childPersonNrREV" ControlToValidate="childPersonNrTB"
                    ValidationExpression="[0-9]{10,12}" ErrorMessage="This field is required. Minumum 10 maximum 12 numbers"
                    ForeColor="red" />
                <asp:RequiredFieldValidator runat="server" ID="childPersonNrRFV" ControlToValidate="childPersonNrTB"
                    Enabled="False" ForeColor="red" ErrorMessage="This field is required!" />
                <br />
                <div style="position: relative; left: 125px">

                    <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                        OnClick="btnSubmit_Click" />
                </div>

            </div>

            <asp:GridView runat="server" ID="sickLeaveBL" AutoGenerateColumns="False" GridLines="None"
                CellSpacing="4">
                <HeaderStyle BackColor="gray" />
                <AlternatingRowStyle BackColor="white"/>
                <Columns>

                    <asp:BoundField HeaderText="User ID:" DataField="UserId" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField HeaderText="From:" DataField="FromDate"  ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField HeaderText="To:" DataField="ToDate"  ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField HeaderText="Sick Leave Type:" DataField="SickLeaveType" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField HeaderText="Child Soc Nr:" DataField="ChildSocNo" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>

                </Columns>
            </asp:GridView>
        </div>

    </form>
</body>
</html>
