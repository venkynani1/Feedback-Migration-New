<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateAdmin.aspx.cs" Inherits="HexFeedback.CreateAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table  width="100%">
<tr>
<td align="center">
    <table width="70%">
        
        <tr>

            <td colspan="1" align="left">
            <asp:Label ID="Label1" Text ="Employee ID :" runat="server" />
            </td>
            <td colspan="1" align="left">
            <asp:Label ID="Label6" Text ="Employee Name :" runat="server" />
            </td>
           

        </tr>
        <tr>

            <td align="left">
            <asp:TextBox ID="txtEmpID" runat="server"></asp:TextBox> 
            </td>
            <td align="left">
            <asp:TextBox ID="txtFName" runat="server"></asp:TextBox> 
            </td>            
            <td align="left">
            <asp:Button ID="btnSearch" Text="Search" runat="server" onclick="btnSearch_Click"></asp:Button> 
            </td>

        </tr>
    </table> 
</td>
</tr>
<tr>

<td align="center">
    <table>
        <tr align="center">
            <td align="center">
            <asp:GridView ID="grvResult" runat="server" 
                    onselectedindexchanged="grvResult_SelectedIndexChanged"></asp:GridView> 
            </td>
        </tr>
    </table>     
</td>
</tr>
<tr>
<td align="center">
    <asp:Button ID="btnAdd" Text="Add" runat="server" onclick="btnAdd_Click" /> 
</td>
</tr>
<tr>
<td align="center">
<h3>ADMINISTRATORS LIST</h3>
</td>
</tr>
<td align="center" >
<asp:GridView ID="grdAdminList" runat="server"></asp:GridView> 
</td>
</tr>
</table> 
</asp:Content>
