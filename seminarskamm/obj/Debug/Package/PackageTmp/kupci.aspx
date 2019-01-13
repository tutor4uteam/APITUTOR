<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kupci.aspx.cs" Inherits="seminarskamm.kupci" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID_KUPCA" DataSourceID="SqlDataSource1" AutoGenerateEditButton="true">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="ID_KUPCA" HeaderText="ID_KUPCA" InsertVisible="False" ReadOnly="True" SortExpression="ID_KUPCA" />
                    <asp:BoundField DataField="POSTNA_ST" HeaderText="POSTNA_ST" SortExpression="POSTNA_ST" />
                    <asp:BoundField DataField="IME" HeaderText="IME" SortExpression="IME" />
                    <asp:BoundField DataField="PRIIMEK" HeaderText="PRIIMEK" SortExpression="PRIIMEK" />
                    <asp:BoundField DataField="TELEFONSKA_STEVILKA" HeaderText="TELEFONSKA_STEVILKA" SortExpression="TELEFONSKA_STEVILKA" />
                    <asp:BoundField DataField="ULICA" HeaderText="ULICA" SortExpression="ULICA" />
                    <asp:BoundField DataField="HISNA_STEVILKA" HeaderText="HISNA_STEVILKA" SortExpression="HISNA_STEVILKA" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:islozardbConnectionString %>" SelectCommand="SELECT * FROM [KUPEC]" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [KUPEC] WHERE [ID_KUPCA] = @original_ID_KUPCA AND (([POSTNA_ST] = @original_POSTNA_ST) OR ([POSTNA_ST] IS NULL AND @original_POSTNA_ST IS NULL)) AND [IME] = @original_IME AND [PRIIMEK] = @original_PRIIMEK AND [TELEFONSKA_STEVILKA] = @original_TELEFONSKA_STEVILKA AND [ULICA] = @original_ULICA AND [HISNA_STEVILKA] = @original_HISNA_STEVILKA" InsertCommand="INSERT INTO [KUPEC] ([POSTNA_ST], [IME], [PRIIMEK], [TELEFONSKA_STEVILKA], [ULICA], [HISNA_STEVILKA]) VALUES (@POSTNA_ST, @IME, @PRIIMEK, @TELEFONSKA_STEVILKA, @ULICA, @HISNA_STEVILKA)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [KUPEC] SET [POSTNA_ST] = @POSTNA_ST, [IME] = @IME, [PRIIMEK] = @PRIIMEK, [TELEFONSKA_STEVILKA] = @TELEFONSKA_STEVILKA, [ULICA] = @ULICA, [HISNA_STEVILKA] = @HISNA_STEVILKA WHERE [ID_KUPCA] = @original_ID_KUPCA AND (([POSTNA_ST] = @original_POSTNA_ST) OR ([POSTNA_ST] IS NULL AND @original_POSTNA_ST IS NULL)) AND [IME] = @original_IME AND [PRIIMEK] = @original_PRIIMEK AND [TELEFONSKA_STEVILKA] = @original_TELEFONSKA_STEVILKA AND [ULICA] = @original_ULICA AND [HISNA_STEVILKA] = @original_HISNA_STEVILKA">
                <DeleteParameters>
                    <asp:Parameter Name="original_ID_KUPCA" Type="Int32" />
                    <asp:Parameter Name="original_POSTNA_ST" Type="Int32" />
                    <asp:Parameter Name="original_IME" Type="String" />
                    <asp:Parameter Name="original_PRIIMEK" Type="String" />
                    <asp:Parameter Name="original_TELEFONSKA_STEVILKA" Type="String" />
                    <asp:Parameter Name="original_ULICA" Type="String" />
                    <asp:Parameter Name="original_HISNA_STEVILKA" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="POSTNA_ST" Type="Int32" />
                    <asp:Parameter Name="IME" Type="String" />
                    <asp:Parameter Name="PRIIMEK" Type="String" />
                    <asp:Parameter Name="TELEFONSKA_STEVILKA" Type="String" />
                    <asp:Parameter Name="ULICA" Type="String" />
                    <asp:Parameter Name="HISNA_STEVILKA" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="POSTNA_ST" Type="Int32" />
                    <asp:Parameter Name="IME" Type="String" />
                    <asp:Parameter Name="PRIIMEK" Type="String" />
                    <asp:Parameter Name="TELEFONSKA_STEVILKA" Type="String" />
                    <asp:Parameter Name="ULICA" Type="String" />
                    <asp:Parameter Name="HISNA_STEVILKA" Type="Int32" />
                    <asp:Parameter Name="original_ID_KUPCA" Type="Int32" />
                    <asp:Parameter Name="original_POSTNA_ST" Type="Int32" />
                    <asp:Parameter Name="original_IME" Type="String" />
                    <asp:Parameter Name="original_PRIIMEK" Type="String" />
                    <asp:Parameter Name="original_TELEFONSKA_STEVILKA" Type="String" />
                    <asp:Parameter Name="original_ULICA" Type="String" />
                    <asp:Parameter Name="original_HISNA_STEVILKA" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <br />
        </div>
        <p>
            Vnos novega kupca:</p>
        <p>
            Ime kupca:<asp:TextBox ID="vnosImeKupec" runat="server"></asp:TextBox>
        </p>
        <p>
            Priimek kupca:<asp:TextBox ID="vnosPriimekKupec" runat="server"></asp:TextBox>
        </p>
        <p>
            Poštna številka:<asp:DropDownList ID="izbiraPostaKupec" runat="server" DataSourceID="SqlDataSource2" DataTextField="NAZIV" DataValueField="POSTNA_ST">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:islozardbConnectionString %>" SelectCommand="SELECT * FROM [POSTA]"></asp:SqlDataSource>
        </p>
        <p>
            Ulica in hišna številka:<asp:TextBox ID="vnosUlicaKupec" runat="server"></asp:TextBox>
            <asp:TextBox ID="vnosHisnaKupec" runat="server"></asp:TextBox>
        </p>
        <p>
            Telefonska_stevilka:<asp:TextBox ID="vnosTelefonskaStevilkaKupec" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="buttonVnosKupec" runat="server" OnClick="buttonVnosKupec_Click" Text="Vnesi Kupca" />
        </p>
        <p>
            Izbris kupca:</p>
        <p>
            Vnesi ID kupca:<asp:TextBox ID="vnosIzbrisKupec" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="buttonIzbrisKupec" runat="server" OnClick="buttonIzbrisKupec_Click" Text="Odstrani kupca" />
        </p>
    </form>
</body>
</html>
