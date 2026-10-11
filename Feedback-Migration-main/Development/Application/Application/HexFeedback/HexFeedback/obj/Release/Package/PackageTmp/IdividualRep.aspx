<%@ Page Title="View Individual and Overall Trainer report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="IdividualRep.aspx.cs" Inherits="HexFeedback.IdividualRep" %>

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
        <legend class="ForLegend">View Feedback Analysis Report - Overall  & Individual</legend>
        <asp:HiddenField ID="hdnPage" runat="server" Value="IndividualReport" />
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
                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="DropDownListNew" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" />
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="2">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 40%; text-align: right;">
                    Select Trainers:
                </td>
                <td style="width: 60%; text-align: left;">
                    <asp:DropDownList ID="ddltrainers" CssClass="DropDownListNew" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddltrainers_SelectedIndexChanged" />
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="2">
                </td>
            </tr>
            <tr class="oddrow" style="width: 100%;">
                <td colspan="2">
                    <table>
                        <tr class="oddrow" style="width: 100%;">
                            <td style="width: 50%;">
                               <%-- <fieldset id="FieldOverall" class="ForFieldset" runat="server">
                                    <legend id="lendOverallviews" class="ForLegend" runat="server">Pattern Table</legend>--%>
                                    <asp:GridView ID="GrvOverallViews" runat="server" Width="100%" CssClass="hdrow" RowStyle-CssClass="oddrow"
                                        AlternatingRowStyle-CssClass="evenrow" AutoGenerateColumns="False" Visible="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblSess" runat="server" Text='<%# Bind("S_No") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QUESTION">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblTot" runat="server" Text='<%# Bind("QUESTION") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="1">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblBal" runat="server" Text='<%# Bind("A") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="2">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblBal" runat="server" Text='<%# Bind("B") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="3">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblBal" runat="server" Text='<%# Bind("C") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="4">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblBal" runat="server" Text='<%# Bind("D") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PATTERN" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblBal" Visible="false"  runat="server" Text='<%# Bind("PATTERN") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                               <%-- </fieldset>--%>
                            </td>
                            <td style="width: 50%;">
                                <fieldset class="ForFieldset" id="fldRating" runat="server">
                                    <legend class="ForLegend" id="legRating" runat="server">Rating Table</legend>
                                    <asp:GridView  ID="GrvRating" runat="server" Width="100%" CssClass="hdrow" RowStyle-CssClass="oddrow"
                                        AlternatingRowStyle-CssClass="evenrow" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblSess" runat="server" Text='<%# Bind("SNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblTot" runat="server" Text='<%# Bind("SECTION") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Score">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblBal" runat="server" Text='<%# Bind("SCORE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <asp:LinkButton ID="LnkRating" runat="server" Text="Click here to Export  PDF Documents"
                                        OnClick="LnkRating_Click"></asp:LinkButton>
                                </fieldset>
                               
                                    <asp:GridView Visible="false" ID="GrvPattern" runat="server" Width="100%" CssClass="hdrow" RowStyle-CssClass="oddrow"
                                        AlternatingRowStyle-CssClass="evenrow">
                                    </asp:GridView>
                               
                               
                                    <asp:GridView Visible="false" ID="GrvConcern" runat="server" Width="100%" CssClass="hdrow" RowStyle-CssClass="oddrow"
                                        AlternatingRowStyle-CssClass="evenrow">
                                    </asp:GridView>
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;text-align:left">
                <td colspan="2">
                    <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
