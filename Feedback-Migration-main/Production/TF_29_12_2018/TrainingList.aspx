<%@ Page Title="Record My FeedBack" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="TrainingList.aspx.cs" Inherits="HexFeedback.TrainingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/NewStyles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div  style="height: 100%">
                <fieldset class="ForFieldset">
                    <legend class="ForLegend">Record My FeedBack</legend>
                    <table style="width: 100%; text-align: center;" class="evenrow">
                        <tr>
                            <td id="Td1" runat="server">
                                <asp:GridView ID="grvTraining" runat="server" AutoGenerateColumns="FALSE" CssClass="hdrow"
                                    Width="100%" RowStyle-CssClass="oddrow" AlternatingRowStyle-CssClass="evenrow"
                                    OnSelectedIndexChanged="grvTraining_SelectedIndexChanged1">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Trainings">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCourse" runat="server" Text='<%#Eval("Course")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStartDate" runat="server" Text='<%#Eval("startdate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("enddate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Feedbacks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNooffd" runat="server" Text='<%#Eval("Tot_Feedbacks")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pending Feedbacks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNooffd1" runat="server" Text='<%#Eval("Bal_Feedbacks")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Submit&lt;br/&gt;Feedback">
                                            <ItemTemplate>
                                                <asp:Button ID="l1" runat="server" Width="160px" CssClass="button" PostBackUrl='<%#"~/Feedback.aspx?s_id="+Eval("sessionid")+"&c_id="+Eval("courseid")+"&c_Name="+Eval("Course")+"&TotalFeedbacks="+Eval("Tot_Feedbacks")+"&PendingFeedbacks="+Eval("Bal_Feedbacks")%>'
                                                    Text="Record My Feedback" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr style="width: 100%; text-align: center">
                            <td>
                                <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset class="ForFieldset">
                    <legend class="ForLegend">Training Effectiveness</legend>
                    <fieldset class="ForFieldset">
                    <legend class="ForLegend">Supervisor Feedback</legend>
                    <table style="width: 100%; text-align: center;" class="evenrow">
                        <tr>
                            <td id="Td2" runat="server">
                                <asp:GridView ID="gvTrainingEffteciveness" runat="server" AutoGenerateColumns="FALSE" CssClass="hdrow"
                                    Width="100%" RowStyle-CssClass="oddrow" AlternatingRowStyle-CssClass="evenrow">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Trainings">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCourse" runat="server" Text='<%#Eval("Course")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStartDate" runat="server" Text='<%#Eval("startdate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("enddate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Feedbacks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNooffd" runat="server" Text='<%#Eval("Tot_Feedbacks")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pending Feedbacks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNooffd1" runat="server" Text='<%#Eval("Bal_Feedbacks")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Submit&lt;br/&gt;Rating">
                                            <ItemTemplate>
                                                <asp:Button ID="l1" runat="server" Width="160px" CssClass="button" PostBackUrl='<%#"~/FeedbackSup.aspx?s_id="+Eval("sessionid")+"&c_id="+Eval("courseid")+"&c_Name="+Eval("Course")+"&TotalFeedbacks="+Eval("Tot_Feedbacks")+"&PendingFeedbacks="+Eval("Bal_Feedbacks")%>'
                                                    Text="Record Rating" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr style="width: 100%; text-align: center">
                            <td>
                                <asp:Label ID="lblErrMsg1" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    </fieldset>
                    <fieldset class="ForFieldset">
                    <legend class="ForLegend">Review Feedback</legend>
                    <table style="width: 100%; text-align: center;" class="evenrow">
                        <tr>
                            <td id="Td3" runat="server">
                                <asp:GridView ID="gvReviews" runat="server" AutoGenerateColumns="FALSE" CssClass="hdrow"
                                    Width="100%" RowStyle-CssClass="oddrow" AlternatingRowStyle-CssClass="evenrow">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Supervisors">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupervisor" runat="server" Text='<%#Eval("Supervisors")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Trainings">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCourse" runat="server" Text='<%#Eval("Course")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStartDate" runat="server" Text='<%#Eval("startdate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("enddate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Submit&lt;br/&gt;Review">
                                            <ItemTemplate>
                                                <asp:Button ID="l1" runat="server" Width="160px" CssClass="button" PostBackUrl='<%#"~/FeedbackReview.aspx?s_id="+Eval("Session_id")+"&c_id="+Eval("Course_id")+"&c_Name="+Eval("Course")+"&Supervisor="+Eval("Supervisors")%>'
                                                    Text="Review Ratings" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr style="width: 100%; text-align: center">
                            <td>
                                <asp:Label ID="lblErrMsg2" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    </fieldset>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
