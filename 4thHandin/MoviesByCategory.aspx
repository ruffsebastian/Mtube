<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MoviesByCategory.aspx.cs" Inherits="_4thHandin.MoviesByCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
            Search movie
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Genre" DataValueField="Genre" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            </asp:DropDownList>
            <br />
            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT DISTINCT [Genre] FROM [MovieDBList]"></asp:SqlDataSource>
            
            <br />

            <asp:Panel ID="Panel1" runat="server">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="351px" AllowPaging="True">

                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                        <asp:BoundField DataField="Genre" HeaderText="Genre" SortExpression="Genre" />
                        <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                    </Columns>

                </asp:GridView>
            </asp:Panel>

            <asp:Panel ID="Panel2" runat="server">
                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="348px">
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                        <asp:BoundField DataField="Genre" HeaderText="Genre" SortExpression="Genre" />
                        <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>



            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="SELECT * FROM [MovieDBList] WHERE ([Genre] = @Genre)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownList1" Name="Genre" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>





        </div>
</asp:Content>