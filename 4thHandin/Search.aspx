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
      </div>           
    </div>


</asp:Content>