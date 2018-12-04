<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="commercials.aspx.cs" Inherits="_4thHandin.commercials" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/xml/commercialsTransformed.xml"></asp:XmlDataSource>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1">
                <Columns>
                    <asp:BoundField DataField="company" HeaderText="company" SortExpression="company" />
                    <asp:BoundField DataField="webpage" HeaderText="webpage" SortExpression="webpage" />
                    <asp:BoundField DataField="logo" HeaderText="logo" SortExpression="logo" />
                </Columns>
            </asp:GridView>
            <br />
        </div>
    </div>

</asp:Content>