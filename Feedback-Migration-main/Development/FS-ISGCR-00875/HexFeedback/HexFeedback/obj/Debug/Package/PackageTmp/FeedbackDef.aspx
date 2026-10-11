<%@ Page Title="View FeedBack Defaulters" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="FeedbackDef.aspx.cs" Inherits="HexFeedback.FeedbackDef" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/NewStyles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset class="ForFieldset">
        <legend class="ForLegend">View FeedBack Defaulters</legend>
        <table style="width: 100%; text-align: center;">
            <tr class="oddrow">
                <td class="label" style="width: 40%; text-align: right;">
                    Select Course:
                </td>
                <td style="width: 60%; text-align: left;">
                    <asp:DropDownList ID="drpCourse" CssClass="DropDownListNew" runat="server" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged"
                        AutoPostBack="True" />
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="2">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 40%; text-align: right;">
                    Select Session:
                </td>
                <td style="width: 60%; text-align: left;">
                    <asp:DropDownList ID="ddlSession" CssClass="DropDownListNew" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" />
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="2">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlforrpt" runat="server">
                                    <fieldset class="ForFieldset">
                                        <legend class="ForLegend">Employee Details</legend>
                                        <asp:GridView ID="GrvEmplId" runat="server" CssClass="hdrow" RowStyle-CssClass="oddrow"
                                            AlternatingRowStyle-CssClass="evenrow" GridLines="None" Width="100%">
                                            <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <itemtemplate>
                                                <asp:Label ID="SNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                             </itemtemplate>
                                            </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </fieldset>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%; text-align: left">
                <td colspan="2">
                    <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
