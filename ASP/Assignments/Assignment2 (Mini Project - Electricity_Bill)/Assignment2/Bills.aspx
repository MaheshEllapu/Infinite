<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bills.aspx.cs" Inherits="Assignment2.Bills" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Electricity Bills</title>
</head>
<body>
    <form id="form1" runat="server">

        <!-- Welcome Message -->
        <h2 style="color:darkblue; font-family:Arial; text-align:center;">
            🌟 Welcome to eBill Services! 🌟
        </h2>
        <p style="text-align:center; font-size:16px; color:gray;">
            Manage your electricity bills easily – add new bills or view past bills with just one click.
        </p>
        <hr />
 
        <!-- Navigation Buttons -->
        <div style="text-align:center; margin-bottom:20px;">
            <asp:Button ID="btnShowAdd" runat="server" Text="➕ Add Bill"
                        OnClick="btnShowAdd_Click" BackColor="#4CAF50" ForeColor="White" Font-Bold="true" />
            &nbsp;&nbsp;
            <asp:Button ID="btnShowView" runat="server" Text="📄 View Bills"
                        OnClick="btnShowView_Click" BackColor="#2196F3" ForeColor="White" Font-Bold="true" />
        </div>
        <hr />
 
        <!-- Add Bill Panel -->
        <asp:Panel ID="pnlAddBill" runat="server" Visible="false">
            <h3>Add New Bill</h3>
            Number of bills to be added: <asp:TextBox ID="txtCount" runat="server" Text="1" /><br />
            Consumer Number: <asp:TextBox ID="txtCN" runat="server" /><br />
            Consumer Name: <asp:TextBox ID="txtName" runat="server" /><br />
            Units Consumed: <asp:TextBox ID="txtUnits" runat="server" /><br /><br />
            <asp:Button ID="btnAdd" runat="server" Text="Add Bill" OnClick="btnAdd_Click" BackColor="#FF9800" ForeColor="White" Font-Bold="true" />
            <br /><br />
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" /><br />
            <asp:Label ID="lblOut" runat="server" Font-Bold="true" />
        </asp:Panel>
 
        <!-- View Bill Panel -->
        <asp:Panel ID="pnlViewBill" runat="server" Visible="false">
            <h3>View Previous Bills</h3>
            Enter Last 'N' Number of Bills To Generate:
            <asp:TextBox ID="txtN" runat="server" Text="2" />
            <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" BackColor="#9C27B0" ForeColor="White" Font-Bold="true" />
            <br /><br />
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="true" BorderColor="Gray" BorderWidth="1" CellPadding="5" />
        </asp:Panel>
    </form>
</body>
</html>
