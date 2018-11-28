<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="commercials.aspx.cs" Inherits="_4thHandin.commercials" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Xml ID="XmlCommercials" runat="server" DocumentSource="~/xml/commercials.xml" TransformSource="~/xml/commercialsXSLT.xslt"></asp:Xml>

        </div>
    </form>
</body>
</html>
