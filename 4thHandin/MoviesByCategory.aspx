﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MoviesByCategory.aspx.cs" Inherits="_4thHandin.MoviesByCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
            <br />
        <div class="col-md-12" id="main">
             <a class="btn btn-primary-c" href="Default.aspx" tabindex="1" style="margin-left:10px;">
                <i class="icon-chevron-left"></i>Go Back</a>
            <asp:DropDownList TabIndex="2" CssClass="btn btn-default dropdown-toggle" ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Genre" DataValueField="Genre" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            </asp:DropDownList>
            
          </div>  
        <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT DISTINCT [Genre] FROM [MovieDBList]"></asp:SqlDataSource>
            
            <br />
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" SelectCommand="SELECT * FROM [MovieDBList] WHERE ([Genre] = @Genre)" ConnectionString="<%$ ConnectionStrings:MovieDBListConnectionString %>">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownList1" Name="Genre" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
            </asp:SqlDataSource>


        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource5">
                <ItemTemplate>
                         
                    <div class="col-md-5th-1 col-sm-4">
                     <div class="card card-1">
                         <a href='SingleView.aspx?queryID=<%# Eval ("ID")%>' tabindex="3">
                         <div class="gradient"></div>
                    
                            <label class="card-bottom-genre"><%# Eval("Genre") %></label>
                    <asp:Image ID="PosterPath" Height="100%" runat="server" ImageUrl='<%# Eval ("PosterPath")%>' onerror="this.src='../Myfiles/default-img.jpg'" AlternateText="Movie"/>
                             <!--<asp:Image ID="Image1" CssClass="default-pic" Height="100%" runat="server" ImageUrl="../Myfiles/default-img.jpg" />-->
                    <br />
                        <span class="text-middle">
                   <asp:Label ID="TitleLabel" CssClass="text-middle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </span>
                             <label class="card-bottom-year"><%# Eval("Year") %></label>
                        </a>
                         </div> 
                       </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
</asp:Content>