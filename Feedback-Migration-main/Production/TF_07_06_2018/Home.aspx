<%@ Page Language="C#" Title="Admin Home"  AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Home.aspx.cs" Inherits="HexFeedback.Home1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="Styles/NewStyles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <table style="width: 100%; text-align: center;" >
           
            <tr>
                <td  id="Td1" runat="server">
                   
                </td>
            </tr>
            <tr  style="width: 100%;text-align:center">
                <td>
                    <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    
</asp:Content>