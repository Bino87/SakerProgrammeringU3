<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SickLeaveView.aspx.cs" Inherits="IntranetApplication.SickLeaveView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="position: relative; left: 15%">
            <asp:RequiredFieldValidator runat="server" ControlToValidate="searchbyIDTB" ErrorMessage="This field is required."
                ForeColor="red" Display="Dynamic" /><br />
            <asp:RegularExpressionValidator runat="server" ControlToValidate="searchbyIDTB" ErrorMessage="Only Numbers are allowed."
                ForeColor="red" Display="Dynamic" ValidationExpression="\d+" />
            <br />
            <asp:Label runat="server" Text="Please enter employeeID."/><br/>
            <asp:TextBox runat="server" ID="searchbyIDTB" />

            <asp:Button runat="server" Text="SearchById" OnClick="OnClick" />
            <br />
        </div>
        <asp:GridView runat="server" ID="silckLeaveGV" AutoGenerateColumns="False" GridLines="None"
            CellSpacing="4">
            <HeaderStyle BackColor="gray" />
            <AlternatingRowStyle BackColor="white" />
            <Columns>

                <asp:BoundField HeaderText="User ID:" DataField="UserId" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="From:" DataField="FromDate" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="To:" DataField="ToDate" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Sick Leave Type:" DataField="SickLeaveType" ItemStyle-Width="20%"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Child Soc Nr:" DataField="ChildSocNo" ItemStyle-Width="10%"
                    ItemStyle-HorizontalAlign="Center" />

            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
