<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_4thHandin._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <div class="col-md-12 middle-logo">
        <img src="img/mtube.png" alt="Mtube" />
    </div>
  

    <div class="row">
    <div class="col-lg-4 col-lg-offset-4">
        <div class="input-group">
            <asp:TextBox ID="TextBoxSearch" runat="server" CssClass="form-control sizing-addon1"></asp:TextBox>
            <span class="input-group-btn">
            <asp:Button ID="ButtonSearch" runat="server" Text="Search" OnClick="ButtonSearch_Click" CssClass="btn btn-primary-c btn-outline-secondary top-search-button" />
            </span>
        </div><!-- /input-group -->
    </div><!-- /.col-lg-4 -->
</div><!-- /.row -->

    <div class="row">
        <div class="col-md-12">
            <h1>Top 10 Movies</h1>
            <hr>
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource2">
                <ItemTemplate>

                    <div class="col-md-5th-1 col-sm-4">
                    <div class="card card-1">
                        <a href='SingleView.aspx?queryID=<%# Eval ("ID")%>'>
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



            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MovieDBListConnectionString %>" SelectCommand="SELECT TOP 10 * FROM [MovieDBList] ORDER BY ViewCount DESC, Year DESC"></asp:SqlDataSource>
              </div>
    </div>

</asp:Content>



