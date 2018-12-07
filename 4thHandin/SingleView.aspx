<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SingleView.aspx.cs" Inherits="_4thHandin.SingleView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="resultspanel"> 
    <div>
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
     </asp:Panel>
</asp:Content>
