<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_4thHandin._Default" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

     <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                 ConnectionString="<%$ ConnectionStrings:MovieDBListConnectionString %>" 
                 SelectCommand="MovieSelectTop10Query" SelectCommandType="StoredProcedure"></asp:SqlDataSource> 

                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        <asp:TextBox ID="TextBoxSearch" cssClass="navsearch" runat="server"></asp:TextBox>
                        <asp:Button ID="ButtonSearch" runat="server" Text="Search" OnClick="ButtonSearch_Click" />
                    </div>
                    <div class="col-md-3"></div>
                 </div>
        <div class="row">
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource2">
                <ItemTemplate>
                     <div class="card card-1">
                         <a href='SingleView.aspx?queryID=<%# Eval ("ID")%>'>
                         <div class="gradient"></div>
                    
                    <asp:Image ID="Image2" Height="100%" runat="server" ImageUrl='<%# Eval ("PosterPath")%>' />
                    <br />
                        <span class="text-middle">
                   <asp:Label ID="Label1" CssClass="text-middle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </span>
                        </a>
                         </div> 
                </ItemTemplate>
            </asp:Repeater>
        </div>
</asp:Content>



