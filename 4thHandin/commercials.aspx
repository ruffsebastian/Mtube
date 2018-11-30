<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="commercials.aspx.cs" Inherits="_4thHandin.commercials" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
           <asp:Xml ID="XmlCommercials" runat="server" DocumentSource="~/xml/commercials.xml" TransformSource="~/xml/commercialsXSLT.xslt"></asp:Xml>
        </div>
    </div>

</asp:Content>