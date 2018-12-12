<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="_4thHandin.Search" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div>
                <br />
                <asp:GridView ID="GridViewDisplaySearch" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                    <asp:BoundField DataField="Genre" HeaderText="Genre" SortExpression="Genre" />
                    <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                    <asp:BoundField DataField="Viewcount" HeaderText="Viewcount" SortExpression="Viewcount" />
                    <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="SingleView.aspx?queryID={0}" DataTextField="Title" DataTextFormatString="{0}" HeaderText="Link" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT * FROM [MovieDBList] WHERE (CHARINDEX(@title, Title) > 0)" ConnectionString="<%$ ConnectionStrings:MovieDBListConnectionString %>">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="Fight Club" Name="Title" QueryStringField="queryName" Type="String" />
                </SelectParameters>
                </asp:SqlDataSource>

            <asp:Repeater ID="Repeater2" runat="server" DataSourceID="SqlDataSource1">
                <ItemTemplate>

                    <div class="col-md-5th-1 col-sm-4">
                    <div class="card card-1">
                        <a href='/SingleView.aspx?queryID=<%# Eval ("ID")%>'>
                            <div class="gradient"></div>

                            <asp:Image ID="Image2" Height="100%" runat="server" ImageUrl='<%# Eval ("PosterPath")%>' onerror="this.src='../Myfiles/default-img.jpg'" AlternateText='<%# Eval("Title") %>' />
                            <label class="card-bottom-year"><%# Eval("Year") %></label>
                            <label class="card-bottom-genre"><%# Eval("Genre") %></label>

                            <span class="text-middle">
                                <asp:Label ID="TitleLabel" CssClass="text-middle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </span>
                        </a>
                    </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            </div>
      </div>           
    </div>


</asp:Content>