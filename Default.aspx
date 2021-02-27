<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="domoarigato.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="LEER digital" />
    
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="LEER analógico" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Estado: Sepa la bola"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Prender 13" />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Apagar 13" />
        <br />
    
    </div>
    </form>
</body>
</html>
