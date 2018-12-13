﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SingleView.aspx.cs" Inherits="_4thHandin.SingleView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container container-spacing ">
        <div class="col-md-12 button-box">
            <a class="btn btn-primary-c" href="Default.aspx" tabindex="10" style="margin-left:-17px;">
                <i class="icon-chevron-left"></i>Go Back</a>
        </div>
        <div class="col-md-9 card-single-view radius" style="padding: 10px;">


            <div class="col-md-4 col-sm-12 image-poster-box">
                <asp:Image ID="ImagePoster" CssClass="single-view-img-center" runat="server" Height="356px" AlternateText="Poster" ImageUrl="~/MyFiles/default-img.png" />
            </div>

            <div class="col-md-8 col-sm-12" style="padding-top: 10px; padding-bottom: 40px;"  >
                <p tabindex="0" id="main">
                    <asp:Label ID="LabelMessages" runat="server" Text="Result"></asp:Label></p>
                <p tabindex="2">
                    <asp:Label ID="LabelResultTitle" CssClass="h1" runat="server" Text="Result"></asp:Label></p>
                <p tabindex="3">
                    <asp:Label ID="LabelResultRating" CssClass="h1" runat="server" Text="Result"></asp:Label></p>
                <p tabindex="4">
                    <asp:Label ID="LabelResultChildRating" CssClass="h1" runat="server" Text="Result"></asp:Label></p>
                <p tabindex="5">
                    <asp:Label ID="LabelResultYear" CssClass="h1" runat="server" Text="Result"></asp:Label></p>
                <hr />
                <p tabindex="6">
                    <asp:Label ID="LabelResultActors" runat="server" Text="Result"></asp:Label></p>
                <br />
                <p tabindex="7">
                    <asp:Label ID="LabelResultDescription" runat="server" Text="Result"></asp:Label></p>
                <br />
            </div>
        </div>
        <div class="col-md-3 col-sm-12  offset-md-1" tabindex="8">
            <asp:Repeater ID="rpMyRepeater" runat="server">
                <HeaderTemplate>
                    <table border="0">
                </HeaderTemplate>
                <ItemTemplate>
                    <item>
        <a href='https://<%# DataBinder.Eval(Container.DataItem, "webpage") %>' target="_blank">
           <div class=" commercial-box radius">
    <div class="centerer">
               <i class=" icon-bookmark" style="color:#FFF; font-size:30px; text-align:center;"></i>
   <h1 style="color:#fff; position:center; text-align:center;">
         <%# DataBinder.Eval(Container.DataItem, "company") %>
       <!--  <%# DataBinder.Eval(Container.DataItem, "viewcount") %>-->
        <!-- <%# DataBinder.Eval(Container.DataItem, "logo") %> -->
   </h1>
        </div>
        </div>
        </a> 
           </item>
                </ItemTemplate>
                <FooterTemplate>
                    </Table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>



    <div class="row">
        <div class="col-md-12">
            <h1 tabindex="11">Top 10 Movies</h1>
            <hr>
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource2">
                <ItemTemplate>

                    <div class="col-md-5th-1 col-sm-4">
                        <div class="card card-1">
                            <a href='SingleView.aspx?queryID=<%# Eval ("ID")%>' tabindex="12">
                                <div class="gradient"></div>

                                <label class="card-bottom-genre"><%# Eval("Genre") %></label>
                                <asp:Image ID="Image2" Height="100%" runat="server" ImageUrl='<%# Eval ("PosterPath")%>' onerror="this.src='../Myfiles/default-img.jpg'" AlternateText="Movie" />

                                <span class="text-middle">
                                    <asp:Label ID="TitleLabel" CssClass="text-middle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                </span>
                                <label class="card-bottom-year"><%# Eval("Year") %></label>
                            </a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>


            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MovieDBListConnectionString %>" SelectCommand="SELECT TOP 10 * FROM [MovieDBList] ORDER BY ViewCount DESC, Year DESC"></asp:SqlDataSource>
        </div>
    </div>




</asp:Content>
