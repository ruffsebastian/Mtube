<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GetImages.aspx.cs" Inherits="_4thHandin.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div>  
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
         <asp:Image ID="ImagePoster" runat="server" Height="356px" ImageUrl="~/MyFiles/default-img.png" />
    <asp:Label ID="LabelMessages" runat="server" Text="Result"></asp:Label>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT * FROM [MovieDBList]" ConnectionString="<%$ ConnectionStrings:MovieDBListConnectionStringStan %>"></asp:SqlDataSource>
        </div>
</asp:Content>
