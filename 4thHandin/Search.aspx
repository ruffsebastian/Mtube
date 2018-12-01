<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="_4thHandin.Search" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
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
                       
        <asp:GridView ID="GridViewMovies" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="ID" PageSize="50">
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT * FROM [MovieDBList] WHERE ([PosterPath] IS NULL)" ConnectionString="<%$ ConnectionStrings:MovieDBListConnectionStringAndreasHome %>"></asp:SqlDataSource>
        </div>
    </div>

</asp:Content>