<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Commercials.aspx.cs" Inherits="_4thHandin.Commercials" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">

            random commercial:
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/xml/commercialsTransformedAttr.xml"></asp:XmlDataSource>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" DataSourceID="XmlDataSource1">
            </asp:GridView>
            <br />

            commercials via dataset
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="True">
            </asp:GridView>
                                
           </div>
    </div>

</asp:Content>