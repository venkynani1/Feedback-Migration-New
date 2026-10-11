<%@ Page Title="Add New Trainer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MasterTrainer.aspx.cs" Inherits="HexFeedback.MasterTrainer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/NewStyles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset class="ForFieldset">
        <legend class="ForLegend">Add New Trainer</legend>
        <table style="width: 100%; text-align: center;">
            <tr class="oddrow">
                <td class="label" style="width: 35%; text-align: right;">
                    Course Name:
                </td>
                <td style="width: 35%; text-align: left;">
                    <asp:DropDownList ID="drpCname" runat="server" CssClass="DropDownListNew" TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td style="width: 30%; text-align: left;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpCname"
                        Display="Dynamic" ErrorMessage="Please Select a Course" CssClass="labelErrMsg"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="evenrow">
                <td colspan="3">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 35%; text-align: right;">
                    Trainer Type:
                </td>
                <td style="width: 35%; text-align: left;">
                    <asp:DropDownList ID="drptype" runat="server" CssClass="DropDownListNew" TabIndex="1"
                        OnSelectedIndexChanged="drptype_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem>Select Type of Trainer</asp:ListItem>
                        <asp:ListItem Value="I">Internal</asp:ListItem>
                        <asp:ListItem Value="E">External</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 30%; text-align: left;">
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="3">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 35%; text-align: right;">
                    Vendor Name:
                </td>
                <td style="width: 35%; text-align: left;">
                    <asp:DropDownList ID="ddlvendor" runat="server" CssClass="DropDownListNew" TabIndex="1"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td style="width: 30%; text-align: left;">
                </td>
            </tr>
            <tr class="evenrow" style=" width: 100%;">
                <td colspan="3">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 35%; text-align: right;">
                    TrainerID:
                </td>
                <td style="width: 35%; text-align: left;">
                    <asp:TextBox ID="txtID" runat="server" CssClass="textboxNew" 
                        AutoPostBack="True" MaxLength="6" ontextchanged="txtID_TextChanged"></asp:TextBox>
                </td>
                <td style="width: 30%; text-align: left;">
                    <asp:Label ID="lblempMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="3">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 35%; text-align: right;">
                    Trainer Name:
                </td>
                <td style="width: 35%; text-align: left;">
                    <asp:TextBox ID="txtName" runat="server" CssClass="textboxNew" MaxLength="50"></asp:TextBox>
                </td>
                <td style="width: 30%; text-align: left;">
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="3">
                </td>
            </tr>
            <tr class="oddrow">
                <td colspan="3" style="width: 100%; text-align: center;">
                    <asp:Button ID="BtnSave" runat="server" Text="Submit" CssClass="button" OnClick="BtnSave_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="BtnReset" runat="server" Text="Reset" CssClass="button" 
                        onclick="BtnReset_Click" />
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;text-align:left">
                <td colspan="3">
                    <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
