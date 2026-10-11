<%@ Page Title="View Individual Analysis Report" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="IndividAnalysis.aspx.cs" Inherits="HexFeedback.IndividAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/NewStyles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function MM_openBrWindow1(path_url) {
            document.open(path_url, "Checklist", "toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,height=300,width=300,left=250,top=250");
        }

        function MM_openBrWindow2(path_url) {
            document.open(path_url, "Checklist", "toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,height=400,width=400,left=250,top=250");
        } 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset class="ForFieldset">
        <legend class="ForLegend">View Individual Analysis Report</legend>
        <asp:HiddenField ID="hdnPage" runat="server" Value="IndividAnalysis" />
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
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 40%; text-align: right;">
                    &nbsp;Select Trainers:</td>
                <td style="width: 60%; text-align: left;">
                    <asp:DropDownList ID="ddltrainers" CssClass="DropDownListNew" runat="server" AutoPostBack="True"
                       OnSelectedIndexChanged="ddltrainers_SelectedIndexChanged" />
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="2">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 40%; text-align: right;">
                    Select Employee ID:
                </td>
                <td style="width: 60%; text-align: left;">
                    <asp:DropDownList ID="ddlemployee" CssClass="DropDownListNew" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged" />
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="2">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 40%; text-align: right;">
                   
                </td>
                <td style="width: 60%; text-align: left;">
                    <asp:Button ID="btnExcel" runat="server" Font-Bold="True" 
                        Height="20px" onclick="btnExcel_Click" Text="Export To Excel" 
                        Width="120px" Enabled="False" />
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="2">
                    <asp:Panel ID="pnlforrpt" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 100%;">
                                    <fieldset class="ForFieldset">
                                        <legend class="ForLegend">Feedback Details</legend>
                                        <div style="width: 100%; max-height: 330px; overflow: auto">
                                        <asp:GridView ID="GrvOverallViews" runat="server" Width="100%" GridLines="None" CssClass="hdrow"
                                            RowStyle-CssClass="oddrow" AlternatingRowStyle-CssClass="evenrow">
                                        </asp:GridView>
                                        </div>
                                    </fieldset>
                                </td>
                              </tr>
                              </table>
                              <table style="width: 100%;">
                              <tr>
                              <td style="width: 70%;" id="tdQuestions" runat="server">
                                     <fieldset class="ForFieldset">
                                        <legend class="ForLegend">Questions</legend>
                                        <asp:GridView ID="gvQuestions" runat="server" Width="100%" GridLines="None" CssClass="hdrow"
                                            RowStyle-CssClass="oddrow" AlternatingRowStyle-CssClass="evenrow">
                                        </asp:GridView>
                                    </fieldset>
                              </td>
                                <td style="width: 30%;">
                                    <fieldset class="ForFieldset">
                                        <legend class="ForLegend">Response </legend>
                                        <asp:Panel ID="PnlTypeopt" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="oddrow">
                                                        1.Poor
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="evenrow">
                                                        2.Fair
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="oddrow">
                                                        3.Average
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="evenrow">
                                                        4.Good
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="oddrow">
                                                        5.Excellent
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="evenrow" style="width: 100%; text-align: center;">
                        <tr>
                            <td align="center" style="width: 100%" class="labelHeader">
                                <asp:Label ID="lblOverAllcmt" runat="server" Text="Overall Comments" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  align="left" style="width: 100%">
                                <asp:Label ID="lblComments" runat="server" CssClass="label" Text="Label" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 100%; text-align: left">
                            <td >
                                <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
