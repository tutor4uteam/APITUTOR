<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zaposleni.aspx.cs" Inherits="seminarskamm.zaposleni" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:islozardbConnectionString %>" SelectCommand="SELECT [DAVCNA_ST], [PRIIMEK], [IME], [ID_ZAPOSLEN] FROM [ZAPOSLEN]"></asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_ZAPOSLEN" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="DAVCNA_ST" HeaderText="DAVCNA_ST" SortExpression="DAVCNA_ST" />
                <asp:BoundField DataField="PRIIMEK" HeaderText="PRIIMEK" SortExpression="PRIIMEK" />
                <asp:BoundField DataField="IME" HeaderText="IME" SortExpression="IME" />
                <asp:BoundField DataField="ID_ZAPOSLEN" HeaderText="ID_ZAPOSLEN" InsertVisible="False" ReadOnly="True" SortExpression="ID_ZAPOSLEN" />
            </Columns>
        </asp:GridView>
        <p>
            Novi zaposleni:</p>
        <p>
            Ime in priimek:<asp:TextBox ID="vnosImeZaposlen" runat="server"></asp:TextBox>
            <asp:TextBox ID="vnosPriimekZaposlen" runat="server"></asp:TextBox>
        </p>
        <p>
            Davcna stevilka zaposlenega:
            <asp:TextBox ID="vnosDavcnaStevilka" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="buttonVnosZaposleni" runat="server" OnClick="buttonVnosZaposleni_Click" Text="Vnesi zaposlnega" />
    </form>
</body>
</html>

