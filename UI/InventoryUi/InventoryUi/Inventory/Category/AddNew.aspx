<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNew.aspx.cs" MasterPageFile="~/Site.Master" Inherits="InventoryUi.Inventory.Category.AddNew" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Add Category</h3>
    <asp:Label ID="LblErrorMsg" runat="server" Text="Label" ForeColor="Red"></asp:Label>
    <asp:Label ID="LblMsg" runat="server" Text="Label" ForeColor="#009900"></asp:Label><br /><br />

<table class="table-condensed">
    <tr>
        <td>Name</td>
        <td><asp:TextBox ID="TxtCategoryName" runat="server" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtCategoryName" runat="server" ErrorMessage="RequiredFieldValidator">Category Name is required</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>Description</td>
        <td colspan="2" ><asp:TextBox ID="TxtDescription" runat="server" MaxLength="300"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtDescription" ErrorMessage="RequiredFieldValidator">Category Description is required</asp:RequiredFieldValidator>
        </td>
    </tr>
</table>

    <br /><br />

<asp:Button ID="CmdSave" runat="server" Text="Save" OnClick="CmdSave_Click" />

<script type="text/javascript">
    function confirmSubmit() {
        return confirm('Are you sure to submit the form ?');
    }
    document.getElementById('ctl01').onsubmit = function() {
    return confirmSubmit();
};
</script>
</asp:Content>


