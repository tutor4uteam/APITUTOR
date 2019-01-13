<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="seminarskamm.main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="buttonOdjava" runat="server" OnClick="buttonOdjava_Click" Text="Odjava" />
        <asp:Button ID="buttonZaloga" runat="server" OnClick="buttonZaloga_Click" Text="Pregled zaloge" />
        <asp:Button ID="buttonZaposleni" runat="server" Text="Pregled zaposlenih" />
        <asp:Button ID="buttonKupci" runat="server" Text="Pregled kupcev" />
        <asp:Button ID="buttonNakupi" runat="server" Text="Pregled nakupov" />
    </form>
</body>
</html>
