<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingEffectivenessReport.aspx.cs" Inherits="HexFeedback.TrainingEffectivenessReport" 
MasterPageFile="~/Site.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/NewStyles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<fieldset class="ForFieldset">
        <legend class="ForLegend">View Training Effectiveness Report</legend>
        <asp:HiddenField ID="hdnPage" runat="server" Value="TrnEffectiveness" />
        <table style="width: 100%; text-align: center;">
            <tr class="oddrow">
                <td class="label" style="width: 40%; text-align: right;">
                    Select Course:
                </td>
                <td style="width: 60%; text-align: left;">
                    <asp:DropDownList ID="drpCourse" CssClass="DropDownListNew" runat="server">
                        <asp:ListItem Text="Fresher Training Program" Value="FTP_001"></asp:ListItem>
                    </asp:DropDownList>
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
                                        <div style="max-height: 330px; width:1120px; overflow: auto; margin:auto;">
                                        <asp:GridView ID="gvTrnEffectReport" runat="server" GridLines="None" CssClass="hdrow" AutoGenerateColumns="false"
                                            RowStyle-CssClass="oddrow" AlternatingRowStyle-CssClass="evenrow" >
                                            <AlternatingRowStyle CssClass="evenrow" />
                                            <Columns>
                                                <asp:BoundField HeaderText="serial number" DataField="serial number" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                                                <asp:BoundField HeaderText="Course" DataField="course" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Session ID" DataField="Session_id" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Supervisor ID" DataField="S_SUPERVISOR_ID" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField HeaderText="Supervisor Name" DataField="Supervisor Name" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Subordinate ID" DataField="Subordinate_ID" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField HeaderText="Subordinate Name" DataField="Subordinate Name" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField HeaderText="Work Area" DataField="Work Area" ItemStyle-HorizontalAlign="Center"/>
                                             <%--   <asp:BoundField HeaderText="Problem Solving Ability" DataField="Problem Solving Ability" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField HeaderText="Programming skill / ability or Skill in Automation Tools" DataField="Programming skill / ability or Skill in Automation Tools" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField HeaderText="Communication / Writing Skill Ability" DataField="Communication / Writing Skill Ability" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField HeaderText="Behavioural Skill" DataField="Behavioural Skill" ItemStyle-HorizontalAlign="Center"/>--%>
                                                <asp:BoundField HeaderText="Performance" DataField="Performance" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField HeaderText="Overall Feedback Rating" DataField="Overall Feedback Rating" ItemStyle-HorizontalAlign="Center"/>
                                                <%--<asp:BoundField HeaderText="Is retraining required?" DataField="Is retraining required?" ItemStyle-HorizontalAlign="Center"/>--%>
                                                <asp:BoundField HeaderText="Overall Comments" DataField="Overall Comments" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField HeaderText="Status" DataField="Status" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField HeaderText="Feedback Date" DataField="Feedback Date" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField HeaderText="PM ID" DataField="PM_ID" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField HeaderText="PM Name" DataField="PM Name" ItemStyle-HorizontalAlign="Center"/>
                                                <asp:BoundField HeaderText="PM Remarks" DataField="PM Remarks" ItemStyle-HorizontalAlign="Center"/>
                                            </Columns>
                                            <RowStyle CssClass="oddrow" />
                                        </asp:GridView>
                                        </div>
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
            </table>
</asp:Content>
