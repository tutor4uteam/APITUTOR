<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zaloga.aspx.cs" Inherits="seminarskamm.zaloga" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        vnesi novi izdelek<p>
            id_izdelka:<asp:TextBox ID="vnosIdIzdelek" runat="server"></asp:TextBox>
        </p>
        <p>
            naziv:<asp:TextBox ID="vnosNazivIzdelek" runat="server"></asp:TextBox>
        </p>
        <p>
            opis:<asp:TextBox ID="vnosOpisIzdelek" runat="server" Height="64px" Width="189px"></asp:TextBox>
        </p>
        kategorija<asp:DropDownList ID="kategorijaIzdelek" runat="server" DataSourceID="SqlDataSource1" DataTextField="NAZIV" DataValueField="ID_KATEGORIJA">
        </asp:DropDownList>
        <p>
            <asp:Button ID="buttonDodajIzdelek" runat="server" OnClick="buttonDodajIzdelek_Click" Text="Dodaj izdelek" />
        </p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:islozardbConnectionString %>" SelectCommand="SELECT * FROM [KATEGORIJA]"></asp:SqlDataSource>
        <p>
            vnesi zalogo:</p>
        <p>
            naziv_izdelka:<asp:DropDownList ID="vnosZalogeIzdelek" runat="server" DataSourceID="SqlDataSourceVnosZaloge" DataTextField="NAZIV" DataValueField="ID_IZDELEK" Height="16px">
            </asp:DropDownList>
        </p>
        število kosovo<asp:TextBox ID="vnosZaloga" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="buttonVnosZaloge" runat="server" OnClick="buttonVnosZaloge_Click" Text="Vnesi zalogo" />
        <asp:SqlDataSource ID="SqlDataSourceVnosZaloge" runat="server" ConnectionString="<%$ ConnectionStrings:islozardbConnectionString %>" SelectCommand="SELECT [ID_IZDELEK], [NAZIV] FROM [IZDELEK]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:islozardbConnectionString %>" SelectCommand="SELECT * FROM [IZDELEK]"></asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_IZDELEK" DataSourceID="SqlDataSource2">
            <Columns>
                <asp:BoundField DataField="ID_IZDELEK" HeaderText="ID_IZDELEK" ReadOnly="True" SortExpression="ID_IZDELEK" />
                <asp:BoundField DataField="ID_KATEGORIJA" HeaderText="ID_KATEGORIJA" SortExpression="ID_KATEGORIJA" />
                <asp:BoundField DataField="NAZIV" HeaderText="NAZIV" SortExpression="NAZIV" />
                <asp:BoundField DataField="OPIS" HeaderText="OPIS" SortExpression="OPIS" />
                <asp:BoundField DataField="ZALOGA" HeaderText="ZALOGA" SortExpression="ZALOGA" />
            </Columns>
        </asp:GridView>
        <br />
        Izbriši izdelek iz sestema:<br />
        <asp:DropDownList ID="izbiraIzbrisIzdelek" runat="server" DataSourceID="SqlDataSourceVnosZaloge" DataTextField="NAZIV" DataValueField="ID_IZDELEK">
        </asp:DropDownList>
        <asp:Button ID="buttonIzbisIzdelek" runat="server" Height="23px" OnClick="buttonIzbisIzdelek_Click" Text="Izbrisi izdelek" />
    </form>
</body>
</html>
