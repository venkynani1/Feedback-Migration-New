<%@ Page Title="Add New Vendor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MasterVendor.aspx.cs" Inherits="HexFeedback.MasterVendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/NewStyles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset class="ForFieldset">
        <legend class="ForLegend">Add New Vendor</legend>
        <table style="width: 100%; text-align: center;">
            <tr class="oddrow">
                <td class="label" style="width: 40%; text-align: right;">
                    Vendor Name:
                </td>
                <td style="width: 60%; text-align: left;">
                    <asp:TextBox ID="txtvendor" runat="server" CssClass="textboxNew"></asp:TextBox>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="4">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 40%; text-align: right;">
                    Vendor ID:
                </td>
                <td style="width: 60%; text-align: left;">
                    <asp:TextBox ID="txtName" runat="server" CssClass="textboxNew"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" 
                        ErrorMessage="Please Enter Numeric Only" ControlToValidate="txtName" 
                        MaximumValue="10000" MinimumValue="0" CssClass="labelErrMsg"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="4">
                </td>
            </tr>
            <tr class="oddrow">
                <td style="width: 100%; text-align: center;" colspan="2">
                    <asp:Button ID="BtnSave" runat="server" CssClass="button" Text="Submit" OnClick="BtnSave_Click" />
                   
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;text-align:left">
                <td colspan="4">
                    <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
