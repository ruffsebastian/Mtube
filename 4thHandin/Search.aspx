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
            <asp:Label ID="LabelMessages" runat="server" Text="Result"></asp:Label>
            <br />
            <asp:Label ID="LabelResultTitle" runat="server" Text="Result"></asp:Label>
            <br />
            <asp:Label ID="LabelResultRating" runat="server" Text="Result"></asp:Label>
            <br />
            <asp:Label ID="LabelResultYear" runat="server" Text="Result"></asp:Label>
            <br />
            <asp:Label ID="LabelResultActors" runat="server" Text="Result"></asp:Label>
            <br />
            <asp:Label ID="LabelResultDescription" runat="server" Text="Result"></asp:Label>
            <br />
            <asp:Label ID="LabelResultChildRating" runat="server" Text="Result"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Image ID="ImagePoster" runat="server" Height="356px" ImageUrl="~/MyFiles/default-img.png" />
        </div>



        <asp:GridView ID="GridViewMovies" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" InsertVisible="False" ReadOnly="True" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Genre" HeaderText="Genre" SortExpression="Genre" />
                <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                <asp:BoundField DataField="ViewCount" HeaderText="ViewCount" SortExpression="ViewCount" />

                <asp:ImageField DataImageUrlField="PosterPath">
                </asp:ImageField>

            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MovieDBListConnectionStringSebastian %>" SelectCommand="SELECT * FROM [MovieDBList] ORDER BY [Year]"></asp:SqlDataSource>
    </form>
</body>
</html>
