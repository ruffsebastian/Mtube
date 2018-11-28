<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="_4thHandin.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBoxInput" runat="server" Width="250px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="ButtonSearchName" runat="server" OnClick="ButtonSearchName_Click" Text="Search Name" />
            <br />
        </div>
        <asp:Label ID="LabelMessages" runat="server" Text="Result"></asp:Label>
        <asp:GridView ID="GridViewMovies" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridViewMovies_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Genre" HeaderText="Genre" SortExpression="Genre" />
                <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MovieDBListConnectionString %>" SelectCommand="SELECT * FROM [MovieList]"></asp:SqlDataSource>
    </form>
</body>
</html>
