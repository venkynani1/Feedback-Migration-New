<%@ Page Title="View and Approve the commends" Language="C#" AutoEventWireup="true"
    CodeBehind="DispComments.aspx.cs" Inherits="HexFeedback.DispComments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Approve ommends</title>
    <link rel="stylesheet" href="Styles/NewStyles.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <fieldset class="ForFieldset">
        <legend class="ForLegend" style="font-size: 13px">Approve Commends</legend>
        <table style="width: 200px; text-align: center;">
            <tr class="oddrow" style="width: 100%;">
                <asp:GridView ID="GrdComments" runat="server" Width="100px" AutoGenerateColumns="False"
                    GridLines="None" OnRowCommand="GrdComments_RowCommand" CssClass="hdrow" RowStyle-CssClass="oddrow"
                    ShowFooter="true" AlternatingRowStyle-CssClass="evenrow">
                    <Columns>
                        <asp:TemplateField HeaderText="Emp Id">
                            <ItemTemplate>
                                <asp:Label ID="LblEmpId" runat="server" Text='<%# Bind("Employee_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments">
                            <ItemTemplate>
                                <asp:Label ID="lblComments" runat="server" Text='<%# Bind("Comments") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Display">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkDisplay" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                            <FooterTemplate>
                                <asp:Button ID="btnSave" CssClass="button" Width="200px" runat="server" CommandName="Proceed"
                                    Text="Approve Comments" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </tr>
            <tr class="evenrow" style="width: 100%; text-align: left">
                <td>
                    <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
    </form>
</body>
</html>
