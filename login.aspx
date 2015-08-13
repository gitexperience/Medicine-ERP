<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login | SignUp</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="loginDiv">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Enter Your Username:"></asp:Label>
            <asp:TextBox ID="uName" runat="server" placeholder="example@domain.com"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="Label2" runat="server" Text="Enter Your Password:"></asp:Label>
            <asp:TextBox ID="pass" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="loginButton" runat="server" Text="Login" OnClick="loginButton_Click" />
        </div>
        <hr />
    </div>
        <div id="signUp">
             <div>
            <asp:Label ID="Label3" runat="server" Text="Enter Your Name:"></asp:Label>
            <asp:TextBox ID="name" runat="server" placeholder="John Doe"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="Label4" runat="server" Text="Enter Your email:"></asp:Label>
            <asp:TextBox ID="email" runat="server"></asp:TextBox>
        </div>
            <div>
            <asp:Label ID="Label5" runat="server" Text="Enter Your Address:"></asp:Label>
            <asp:TextBox ID="address" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
            <div>
            <asp:Label ID="Label6" runat="server" Text="Enter Your City:"></asp:Label>
            <asp:TextBox ID="city" runat="server"></asp:TextBox>
        </div>
            <div>
            <asp:Label ID="Label7" runat="server" Text="Enter Your country:"></asp:Label>
            <asp:TextBox ID="country" runat="server"></asp:TextBox>
        </div>
            <div>
            <asp:Label ID="Label8" runat="server" Text="Enter Your PinCode:"></asp:Label>
            <asp:TextBox ID="pin" runat="server"></asp:TextBox>
        </div>
            <div>
            <asp:Label ID="Label9" runat="server" Text="Enter Your Role:"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="supplier" Selected="True">Supplier</asp:ListItem>
                    <asp:ListItem Value="retailer">Retailer</asp:ListItem>
                    <asp:ListItem Value="customer">Customer</asp:ListItem>
                </asp:DropDownList>
        </div>
            <div>
            <asp:Label ID="Label11" runat="server" Text="Enter Your Plan:"></asp:Label>
                <asp:DropDownList ID="DropDownList2" runat="server">
                   
                </asp:DropDownList>
        </div>
            <div>
            <asp:Label ID="Label10" runat="server" Text="Enter Your password:"></asp:Label>
            <asp:TextBox ID="password" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="Button1" runat="server" Text="SignUp" OnClick="Button1_Click" />
        </div>
        </div>
    </form>
</body>
</html>
