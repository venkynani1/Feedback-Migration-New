<%@ Page Title="View and Update Mail Master" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="MasterMail.aspx.cs" Inherits="HexFeedback.MasterMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/NewStyles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset class="ForFieldset">
        <legend class="ForLegend">View and Update Mail Master</legend>
        <table style="width: 100%; text-align: center;">
             <tr class="evenrow">
                <td class="label" style="width: 25%; text-align: right;">
                    Type:
                </td>
                <td style="width: 75%; text-align: left;" colspan="3">
                    <asp:RadioButtonList runat="server" ID="rdType" CssClass="RadioButtonList" 
                        RepeatDirection="Horizontal" 
                        onselectedindexchanged="rdType_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="0" Selected="True">Nomination List</asp:ListItem>
                    <asp:ListItem Value="1">Supervisor's List</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 25%; text-align: right;">
                    From:
                </td>
                <td style="width: 50%; text-align: left;">
                    <asp:TextBox ID="TxtFrom" runat="server" Width="320px"   CssClass="textboxNew"></asp:TextBox>
                </td>
                <td style="width: 25%;">
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="3">
                </td>
            </tr>
            
            <tr class="oddrow">
                <td class="label" style="width: 25%; text-align: right;">
                    CC:
                </td>
                <td style="width: 50%; text-align: left;">
                    <asp:TextBox ID="txtCcc" runat="server" Width="320px"   CssClass="textboxNew"></asp:TextBox>
                </td>
                <td style="width: 25%; text-align: left">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([;])*)*"
                        ControlToValidate="txtCcc" CssClass="labelErrMsg" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="3">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 25%; text-align: right;">
                    Subject:
                </td>
                <td style="width: 50%; text-align: left;">
                    <asp:TextBox ID="txtSubj" runat="server" Width="320px"  CssClass="textboxNew"></asp:TextBox>
                </td>
                <td style="width: 25%; text-align: left">
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="3">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 25%; text-align: right;">
                    Body:
                </td>
                <td style="width: 50%; text-align: left;">
                    <asp:TextBox ID="txtbody" runat="server" CssClass="textboxNew" TextMode="MultiLine"
                        Width="320px" Height="150px"></asp:TextBox>
                </td>
                <td style="width: 25%; text-align: left">
                 <asp:Button ID="btnEditUpdatebody" runat="server" Text="Edit" Visible="true" 
                        CssClass="button" onclick="btnEditUpdatebody_Click" />
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="3">
                </td>
            </tr>
           
          
            <tr class="oddrow">
                <td style="width: 50%; text-align: center;" colspan="3">
                    <asp:Button ID="BtnSave" runat="server" Text="Submit" CssClass="button" OnClick="BtnSave_Click" />
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
