<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="_4thHandin.Search" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div>
                <asp:GridView ID="GridViewDisplaySearch" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                    <asp:BoundField DataField="Genre" HeaderText="Genre" SortExpression="Genre" />
                    <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                    <asp:BoundField DataField="Viewcount" HeaderText="Viewcount" SortExpression="Viewcount" />
                    <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="SingleView.aspx?query={0}" DataTextField="Title" DataTextFormatString="{0}" HeaderText="Link" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT * FROM [MovieDBList] WHERE ([Title] = @Title)">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="234567890" Name="Title" QueryStringField="query" Type="String" />
                </SelectParameters>
                </asp:SqlDataSource>

            </div>
      </div>           
    </div>


</asp:Content>