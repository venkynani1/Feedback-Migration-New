<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FeedbackReview.aspx.cs" Inherits="HexFeedback.FeedbackReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table style="width: 100%; text-align: center;">
        <tr class="evenrow">
            <td colspan="4" id="TdSaveMessage" runat="server" class="labelErrMsg">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="evenrow" style="height: 25px;">
            <td style="width: 25%; text-align: right;" class="label">
                <asp:Label ID="Lb_Empname" runat="server">Supervisor Name:</asp:Label>
            </td>
            <td style="width: 25%;">
                <asp:Label ID="lblTraineeName" Font-Bold="true" CssClass="label" runat="server"></asp:Label>
            </td>
            <td style="width: 25%; text-align: right;" class="label">
                <asp:Label ID="Lb_StartEnddate" runat="server">Training Date(s):</asp:Label>
            </td>
            <td style="width: 25%;">
                <asp:Label ID="lblTrainingDate" runat="server" Font-Bold="true" CssClass="label"></asp:Label>
            </td>
        </tr>
        <tr class="oddrow" style="height: 25px;">
            <td style="width: 25%; text-align: right;" class="label">
                <asp:Label ID="Lb_Empid" runat="server">Supervisor ID:</asp:Label>
            </td>
            <td style="width: 25%;">
                <asp:Label ID="lblTraineeID" runat="server" Font-Bold="true" CssClass="label"></asp:Label>
            </td>
            <td style="width: 25%; text-align: right;" class="label">
                <asp:Label ID="Lb_Location" runat="server">Training Location:</asp:Label>
            </td>
            <td style="width: 25%;">
                <asp:Label ID="lblTrainingLocation" runat="server" Font-Bold="true" CssClass="label"></asp:Label>
            </td>
        </tr>
        <tr class="evenrow">
            <td style="width: 25%; text-align: right;" class="label">
                <asp:Label ID="Lb_Coursename" runat="server">Course Name:</asp:Label>
            </td>
            <td style="width: 25%;">
                <asp:Label ID="lblCourseName" runat="server" Font-Bold="true" CssClass="label"></asp:Label>
            </td>
            <td style="width: 25%; text-align: right;" class="label">
            </td>
            <td style="width: 25%;">
            </td>
        </tr>
        <tr class="oddrow">
            <td colspan="4" style="text-align: center;" class="label">
                <asp:Label ID="Label1" runat="server">Please enter the rating on a scale of 1 (Lowest) to 5 (Highest). Please write "NA" if items are not applicable.</asp:Label> 
            </td>
        </tr>
        <tr class="evenrow">
            <td colspan="4" style="text-align: center;" class="label">
                <asp:Label ID="Label2" runat="server"><b>(1- Poor                 2- Fair                3- Good           4- Very good            5- Excellent            NA- Not applicable)</b></asp:Label> 
            </td>
        </tr>
        <tr class="oddrow">
        <td colspan="4" style="width: 100%; text-align: center" class="labelHeader">
        <table style="width: 85%; text-align: center; margin:0 auto;">
                                <tr id="RowParamHead" runat="server" class="hdrow">
                                    <td colspan="2" style="width: 43%; text-align: center;">
                                        Objectives of the Training course met
                                    </td>
                                </tr>
                                <tr id="TrP1" runat="server" class="oddrow">
                                    <td  style="width: 30%; margin-left:10px;" class="label">
                                         <asp:Label ID="Label5" runat="server">Problem solving ability</asp:Label><br />
                                    </td>
                                    <td style="width: 70%" class="label">
                                        <ul>
                                        <li>To understand the roles & responsibilities of a software developer and play the role effectively</li>
                                        <li>To be able to solve any given problem by  identifying the need of the problem solving and able to classify the problem based on problem solving techniques</li>
                                        <li>To be able to identify the need of  Data structures , its types and the operations associated with these data structures and able to apply the data structures appropriately.</li>
                                        <li>To be able to apply different algorithms & problem solving techniques to solve a given problem</li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr id="TrP2" runat="server" class="evenrow">
                                    <td align="left" style="width: 30%" class="label">
                                         <asp:Label ID="Label6" runat="server">Programming skill / ability</asp:Label><br />
                                    </td>
                                    <td style="width: 70%" class="label">
                                        <ul>
                                        <li>To be able to use the basics of programming constructs such as data types, operators, looping and decision making structures appropriately during programming.</li>
                                        <li>To use the proper naming convention and standard coding practices</li>
                                        <li>To use arrays, structures , pointers, functions appropriately</li>
                                        <li>To introduce various standard I/O library functions</li>
                                        <li>To be able to apply file handling techniques</li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr id="TrP3" runat="server" class="oddrow">
                                    <td align="left" style="width: 30%" class="label">
                                         <asp:Label ID="Label7" runat="server">Communication\Writing Skill Ability</asp:Label><br />
                                    </td>
                                    <td style="width: 70%" class="label">
                                        <ul>
                                        <li>Listening & Questioning skills</li>
                                        <li>Conduct to be followed during meeting & while using the telephone or mobile</li>
                                        <li>Ability to use appropriate email etiquette in business emails</li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr id="TrP4" runat="server" class="evenrow">
                                    <td align="left" style="width: 30%" class="label">
                                         <asp:Label ID="Label8" runat="server">Behavioural Skill</asp:Label><br />
                                    </td>
                                    <td style="width: 70%" class="label">
                                        <ul>
                                        <li>Manage Time and resources</li>
                                        <li>Learn to plan and prioritize work/tasks</li>
                                        <li>Ability to work in a team</li>
                                        <li>Demonstrate appropriate workplace etiquette</li>
                                        <li>Conduct to be followed in public places – e.g. the elevator, cafeteria, etc.</li>
                                        <li>Personal attire  and grooming, person-to-person etiquette and dining etiquette</li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr id="TrP5" runat="server" class="oddrow">
                                    <td align="left" style="width: 30%" class="label">
                                         <asp:Label ID="Label9" runat="server">Performance</asp:Label><br />
                                    </td>
                                    <td style="width: 70%" class="label">
                                        <ul>
                                        <li>How productive the trainee is? Rate the quality of output</li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr id="TrP6" runat="server" class="evenrow">
                                    <td align="left" style="width: 30%" class="label">
                                         <asp:Label ID="Label10" runat="server">Parameters for Overall Comments</asp:Label><br />
                                    </td>
                                    <td style="width: 70%" class="label">
                                        <ul>
                                        <li>How fast the trainee is productive?</li>
                                        <li>Any improvement areas for the trainee?</li>
                                        <li>If retraining is required, please specify on what skills</li>
                                        </ul>
                                    </td>
                                 </tr>
                            </table>
        </td>
        </tr>
        <tr>
        <td colspan="4">
        <asp:GridView runat="server" ID="gvReviews" AutoGenerateColumns="FALSE" CssClass="hdrow" 
        Width="100%" RowStyle-CssClass="oddrow" AlternatingRowStyle-CssClass="evenrow" >
        <Columns>
                <asp:TemplateField HeaderText="Trainee's Employee Id">
                    <ItemTemplate>
                        <asp:Label ID="lblTrnEmpID" runat="server" Text='<%#Eval("Subordinate_Id")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Trainee's Name">
                    <ItemTemplate>
                        <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("Subordinate_Name")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mention Work Area (1st 90 days). Specify the area covered by the Trainee.">
                    <ItemTemplate>
                        <asp:Label ID="lblWorkArea" runat="server" Text='<%#Eval("WorkArea")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Problem Solving Ability">
                    <ItemTemplate>
                        <asp:Label ID="lblPrbSA" runat="server" Text='<%#Eval("PrbSA")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Programming skill / ability or Skill in Automation Tools">
                    <ItemTemplate>
                        <asp:Label ID="lblPrgSkills" runat="server" Text='<%#Eval("PrgSkills")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Communication / Writing Skill Ability">
                    <ItemTemplate>
                        <asp:Label ID="lblCommSkills" runat="server" Text='<%#Eval("CommSkills")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Behavioural Skill [Team Work, Assertiveness]">
                    <ItemTemplate>
                        <asp:Label ID="lblBehSkills" runat="server" Text='<%#Eval("BehSkills")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Performance">
                    <ItemTemplate>
                        <asp:Label ID="lblPerformance" runat="server" Text='<%#Eval("Performance")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Overall Feedback Rating">
                    <ItemTemplate>
                        <asp:Label ID="lblOvrAllRating" runat="server" Text='<%#Eval("OvrAllRating")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Is retraining required?If yes, please specify on what skills, in the comments field">
                    <ItemTemplate>
                        <asp:Label ID="lblTrnReq" runat="server" Text='<%#Eval("TrnReq")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Overall Comments for the trainee based on the specified parameters">
                    <ItemTemplate>
                        <asp:Label ID="lblOverall_Feedback" runat="server" Text='<%#Eval("Overall_Feedback")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
         </Columns>
        </asp:GridView>
        </td>
        </tr>
        <tr class="oddrow">
        <td colspan="4" style="text-align: center;">
                <asp:Label ID="Lb_traineecomments" runat="server" CssClass="labelHeader" Text="Remarks"></asp:Label>
        </td>
        </tr>
        <tr>
        <td colspan="4" align="center" style="width: 95%">
            <asp:TextBox ID="Tb_traineecomments" runat="server" CssClass="textboxNew" TextMode="MultiLine" 
            Height="90px" MaxLength="2" Width="99%" ></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td colspan="2">
            <asp:Button runat="server" ID="btnApprove" Text="Approve" CssClass="button" 
                onclick="btnApprove_Click" ValidationGroup="GrpValidate"/>
            <asp:RequiredFieldValidator ID="rfvRemarks" runat="server" 
                ControlToValidate="Tb_traineecomments" ErrorMessage="Please provide Remarks" Display="None" 
                ValidationGroup="GrpValidate"></asp:RequiredFieldValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                ShowMessageBox="True" ShowSummary="False" ValidationGroup="GrpValidate" />
        </td>
        <td colspan="2">
            <asp:Button runat="server" ID="btnReject" Text="Reject" CssClass="button" 
                onclick="btnReject_Click" ValidationGroup="GrpValidate"/>
        </td>
        </tr>
</table>
</asp:Content>
