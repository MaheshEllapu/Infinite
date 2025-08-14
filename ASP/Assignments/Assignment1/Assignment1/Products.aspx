<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Assignment1.Products" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
<style>

        body {
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    background: linear-gradient(to right, #e0eafc, #cfdef3);
    margin: 0;
    padding: 0;
}

.container {
    max-width: 600px;
    margin: 60px auto;
    background-color: #ffffff;
    padding: 40px 30px;
    border-radius: 12px;
    box-shadow: 0 12px 24px rgba(0, 0, 0, 0.15);
    text-align: center;
    transition: transform 0.3s ease;
}

.container:hover {
    transform: translateY(-5px);
}

h2 {
    color: #2c3e50;
    font-size: 28px;
    margin-bottom: 25px;
}

.dropdown {
    width: 100%;
    padding: 10px;
    font-size: 16px;
    border-radius: 6px;
    border: 1px solid #ccc;
    margin-bottom: 20px;
}

.button {
    padding: 12px 25px;
    background-color: #28a745;
    color: white;
    font-size: 16px;
    border: none;
    border-radius: 6px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.button:hover {
    background-color: #218838;
}

.price-label {
    font-size: 20px;
    font-weight: bold;
    color: #e74c3c;
    margin-top: 25px;
    display: block;
}

img {
    margin-top: 25px;
    border-radius: 12px;
    box-shadow: 0 6px 18px rgba(0, 0, 0, 0.1);
    transition: transform 0.3s ease;
}

img:hover {
    transform: scale(1.02);
}

</style>
 
</head>
<body>
<form id="form1" runat="server">
<div class="container">
<h2>Select a Product</h2>
<asp:DropDownList ID="ddlProducts" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged" CssClass="dropdown"></asp:DropDownList>
<br />
<asp:Image ID="imgProduct" runat="server" Width="400px" Height="400px" />
<br />
<asp:Button ID="btnGetPrice" runat="server" Text="Get Price" OnClick="btnGetPrice_Click"  CssClass="button"/>
<br />
<asp:Label ID="lblPrice" runat="server"  CssClass="price-label"></asp:Label>
</div>
</form>
</body>
</html>
 
