<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_4thHandin._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>This is the 4th handin home page!</h1>
        <p class="lead"></p>
        <p><a href="#" class="btn btn-primary btn-lg">Click Here For Absolution &laquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-12">
            <center>
            <h2><asp:Label ID="LabelJoke" runat="server" Text="Label"></asp:Label></h2>
            </center>
        </div>
    </div>

</asp:Content>



