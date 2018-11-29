<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayTopMovies.aspx.cs" Inherits="_4thHandin.DisplayTopMovies" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:GridView ID="GridViewDisplayTopMovies" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                    <asp:BoundField DataField="Genre" HeaderText="Genre" SortExpression="Genre" />
                    <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                    <asp:BoundField DataField="ViewCount" HeaderText="ViewCount" SortExpression="ViewCount" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MovieDBListConnectionString2 %>" SelectCommand="SELECT TOP (10) ID, Title, Genre, Year, ViewCount FROM MovieDB ORDER BY ViewCount DESC, Year DESC"></asp:SqlDataSource>

        </div>
    </form>
</body>
</html>
