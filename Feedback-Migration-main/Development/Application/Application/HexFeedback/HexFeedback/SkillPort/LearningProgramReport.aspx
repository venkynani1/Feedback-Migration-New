<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LearningProgramReport.aspx.cs" Inherits="HexFeedback.SkillPort.LearningProgramReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/NewStyles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <legend class="ForLegend">Learning Program Report</legend>
        <table cellspacing="0" cellpadding="0" width="99%" align="center" border="0">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgexcel" runat="server" ImageUrl="~/SkillPort/Images/excel_icon.gif"
                        Visible="true" OnClick="ImgExcelBtn_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="6">   
                    <div  id="divBorder" style="overflow:scroll;height:450px;width:1100px;" runat="server">
                    <asp:GridView ID="grdLearningProgramExcel" runat="server" Width="100%" CssClass="hdrow"
                        RowStyle-CssClass="oddrow" AlternatingRowStyle-CssClass="evenrow" EmptyDataText="No record found!"
                        AutoGenerateColumns="False" OnRowDataBound="grdLearningProgramExcel_RowDataBound">
                        <RowStyle CssClass="oddrow"></RowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="SNo" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblSerialNo" runat="server" Visible="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Employee ID" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label CssClass="lblnormal" ID="lblEmployeeID" Visible="true" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem,"EmployeeID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="4%">
                                <ItemTemplate>
                                    <asp:Label CssClass="lblnormal" ID="lblEmployeeName" Visible="true" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem,"EmployeeName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="PMS Competency Based Plan" ItemStyle-Width="7%">
                                <ItemTemplate>
                                    <asp:Label CssClass="lblnormal" ID="lblPmsCompetencyBasedPlan" Visible="true" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem,"PmsCompetencyBasedPlan")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Learning Program Completion Status" ItemStyle-Width="7%">
                                <ItemTemplate>
                                    <asp:Label CssClass="lblnormal" ID="lblLearningProgramCompletionStatus" Visible="true"
                                        runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LearningProgramCompletionStatus")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Learning Program Completion Date" ItemStyle-Width="7%">
                                <ItemTemplate>
                                    <asp:Label CssClass="lblnormal" ID="lblLearningProgramCompletionDate" Visible="true"
                                        runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LearningProgramcompletionDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assessment Completion Status" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label CssClass="lblnormal" ID="lblAssessmentCompletionStatus" Visible="true"
                                        runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AssessmentCompletionStatus")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assessment Completion Date" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label CssClass="lblnormal" ID="lblAssessmentCompletionDate" Visible="true" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem,"AssessmentCompletionDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Technology" ItemStyle-Width="4%">
                                <ItemTemplate>
                                    <asp:Label CssClass="lblnormal" ID="lblTechnology" Visible="true" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem,"Technology")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role" ItemStyle-Width="4%">
                                <ItemTemplate>
                                    <asp:Label CssClass="lblnormal" ID="lblRole" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Role")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Month" ItemStyle-Width="4%" Visible="false">
                                <ItemTemplate>
                                    <asp:Label CssClass="lblnormal" ID="lblMonth" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Month")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Year" ItemStyle-Width="4%" Visible="false">
                                <ItemTemplate>
                                    <asp:Label CssClass="lblnormal" ID="lblYear" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Year")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle CssClass="evenrow"></AlternatingRowStyle>
                    </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
         <div align="center" id="progress" runat="server" style="display:none;">
        <img src="Images/roller01.gif" />
        <asp:Label ID="lblWait" runat="server" ForeColor="Red" Text="Please wait"></asp:Label>
    </div>
</asp:Content>
