<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validator.aspx.cs" Inherits="Assignment1.Validator" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Validators</title>
    <style>
        .error {
            color: red;
            font-family: Arial;
        }
        .yellowBackground {
            background-color: #FFFFCC;
        }
        .asterisk {
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Insert your details :</h3>
 
            Name:
            <asp:TextBox ID="txtName" runat="server" CssClass="yellowBackground"></asp:TextBox>
            <span class="asterisk">*</span>
            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                ErrorMessage="Name is required" CssClass="error" Display="Dynamic"></asp:RequiredFieldValidator>
            <br /><br />
 
            Family Name:
            <asp:TextBox ID="txtFamily" runat="server" CssClass="yellowBackground"></asp:TextBox>
            <span class="asterisk">*</span> differs from name
            <asp:RequiredFieldValidator ID="rfvFamily" runat="server" ControlToValidate="txtFamily"
                ErrorMessage="Family name is required" CssClass="error" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cvFamilyDiff" runat="server" ControlToValidate="txtFamily"
                OnServerValidate="cvFamilyDiff_ServerValidate" ErrorMessage="Family name must differ from name"
                CssClass="error" Display="Dynamic"></asp:CustomValidator>
            <br /><br />
 
            Address:
            <asp:TextBox ID="txtAddress" runat="server" CssClass="yellowBackground"></asp:TextBox>
            <span class="asterisk">*</span> at least 2 chars
            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                ErrorMessage="Address is required" CssClass="error" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revAddress" runat="server" ControlToValidate="txtAddress"
                ValidationExpression=".{2,}" ErrorMessage="Address must be at least 2 characters"
                CssClass="error" Display="Dynamic"></asp:RegularExpressionValidator>
            <br /><br />
 
            City:
            <asp:TextBox ID="txtCity" runat="server" CssClass="yellowBackground"></asp:TextBox>
            <span class="asterisk">*</span> at least 2 chars
            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity"
                ErrorMessage="City is required" CssClass="error" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revCity" runat="server" ControlToValidate="txtCity"
                ValidationExpression=".{2,}" ErrorMessage="City must be at least 2 characters"
                CssClass="error" Display="Dynamic"></asp:RegularExpressionValidator>
            <br /><br />
 
            Zip Code:
            <asp:TextBox ID="txtZip" runat="server" CssClass="yellowBackground"></asp:TextBox>
            <span class="asterisk">*</span> (xxxxx)
            <asp:RequiredFieldValidator ID="rfvZip" runat="server" ControlToValidate="txtZip"
                ErrorMessage="Zip code is required" CssClass="error" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revZip" runat="server" ControlToValidate="txtZip"
                ValidationExpression="^\d{5}$" ErrorMessage="Zip code must be exactly 5 digits"
                CssClass="error" Display="Dynamic"></asp:RegularExpressionValidator>
            <br /><br />
 
            Phone:
            <asp:TextBox ID="txtPhone" runat="server" CssClass="yellowBackground"></asp:TextBox>
            <span class="asterisk">*</span> (xx-xxxxxxxx / xxx-xxxxxxx)
            <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone"
                ErrorMessage="Phone is required" CssClass="error" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone"
                ValidationExpression="^\d{2}-\d{8}$|^\d{3}-\d{7}$"
                ErrorMessage="Phone must be in format XX-XXXXXXXX or XXX-XXXXXXX"
                CssClass="error" Display="Dynamic"></asp:RegularExpressionValidator>
            <br /><br />
 
            E-Mail:
            <asp:TextBox ID="txtEmail" runat="server" CssClass="yellowBackground"></asp:TextBox>
            <span class="asterisk">*</span>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Email is required" CssClass="error" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                ValidationExpression="\w+@\w+\.\w+" ErrorMessage="Invalid email format"
                CssClass="error" Display="Dynamic"></asp:RegularExpressionValidator>
            <br /><br />
 
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error"
                HeaderText="ValidationSum" DisplayMode="BulletList" ShowSummary="True" ShowMessageBox="True" />
 
            <br />
            <asp:Button ID="btnCheck" runat="server" Text="Check" OnClick="btnCheck_Click" />
        </div>
    </form>
</body>
</html>
 