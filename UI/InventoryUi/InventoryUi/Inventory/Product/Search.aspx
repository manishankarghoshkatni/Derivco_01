<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" MasterPageFile="~/Site.Master" Inherits="InventoryUi.Inventory.Product.Search" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Search/Update/Delete Product</h3>
    <asp:Label ID="LblErrorMsg" runat="server" Text="Label" ForeColor="Red"></asp:Label>
    <asp:Label ID="LblMsg" runat="server" Text="Label" ForeColor="#009900"></asp:Label><br /><br />
    <br />
<table class="table-condensed">
    <tr>
        <td>Id</td>
        <td><asp:TextBox ID="TxtId" runat="server"></asp:TextBox></td>
        <td><asp:Button ID="CmdSearchById" runat="server" Text="🔍" Font-Names="Arial Unicode MS" OnClick="CmdSearchById_Click" /></td>
    </tr>
    <tr>
        <td>Name</td>
        <td><asp:TextBox ID="TxtProductName" runat="server" MaxLength="100"></asp:TextBox></td>
        <td><asp:Button ID="CmdSearchByName" runat="server" Text="🔍" /></td>
    </tr>
    <tr>
        <td>Description</td>
        <td colspan="2" ><asp:TextBox ID="TxtDescription" runat="server" MaxLength="300"></asp:TextBox></td>
    </tr>

    <tr>
        <td>Category</td>
        <td colspan="2" >
            <asp:DropDownList ID="CboCategory" runat="server"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Unit</td>
        <td colspan="2" ><asp:DropDownList ID="CboUnit" runat="server"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Price</td>
        <td colspan="2" ><asp:TextBox ID="TxtPrice" runat="server" MaxLength="300"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Currency</td>
        <td colspan="2" ><asp:DropDownList ID="CboCurrency" runat="server">
            <asp:ListItem>$</asp:ListItem>
            <asp:ListItem>Rs</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
</table>

    <br />

 <div>  
    <asp:GridView ID="ProductGridView" class="table-condensed" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductId" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"  >  
        <Columns>  
            <asp:BoundField DataField="ProductId" HeaderText="Id" />  
            <asp:BoundField DataField="ProductName" HeaderText="Name" />  
            <asp:BoundField DataField="ProductDescription" HeaderText="Description" />  
            <asp:BoundField DataField="CategoryName" HeaderText="Category" />  
            <asp:BoundField DataField="UnitName" HeaderText="Unit" />  
            <asp:BoundField DataField="Price" HeaderText="Price" />  
            <asp:BoundField DataField="Currency" HeaderText="Currency" />  
            <asp:CommandField ShowEditButton="true" />  
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirm('Are you sure you want to delete?'); " CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>  
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
    </asp:GridView>  
 </div> 

</asp:Content>

