<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FeedbackSup.aspx.cs" Inherits="HexFeedback.FeedbackSup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" language="javascript">
    function validate() {
        var count = parseInt(document.getElementById('ctl00_MainContent_feedbackCount').value);
        for (var i = 0; i < count; i++) {
            if ((document.getElementById('ctl00_MainContent_txtWrkArea' + (i + 1).toString()).value).trim() == "") {
                alert("Please provide Work Area for " + document.getElementById('ctl00_MainContent_lblSubOrd' + (i + 1).toString()).innerText)
                return false;
            }
            else if (document.getElementById('ctl00_MainContent_ddlPrbSA' + (i + 1).toString()).value == "0") {
                alert("Please provide rating for Problem Saving Ability of" + document.getElementById('ctl00_MainContent_lblSubOrd' + (i + 1).toString()).innerText)
                return false;
            }
            else if (document.getElementById('ctl00_MainContent_ddlPrgSkills' + (i + 1).toString()).value == "0") {
                alert("Please provide rating for Programming skills of" + document.getElementById('ctl00_MainContent_lblSubOrd' + (i + 1).toString()).innerText)
                return false;
            }
            else if (document.getElementById('ctl00_MainContent_ddlCommSkills' + (i + 1).toString()).value == "0") {
                alert("Please provide rating for Communication / Writing Skills of" + document.getElementById('ctl00_MainContent_lblSubOrd' + (i + 1).toString()).innerText)
                return false;
            }
            else if (document.getElementById('ctl00_MainContent_ddlBehSkills' + (i + 1).toString()).value == "0") {
                alert("Please provide rating for Behavioural Skills of" + document.getElementById('ctl00_MainContent_lblSubOrd' + (i + 1).toString()).innerText)
                return false;
            }
            else if (document.getElementById('ctl00_MainContent_ddlPerformance' + (i + 1).toString()).value == "0") {
                alert("Please provide rating for Performance of" + document.getElementById('ctl00_MainContent_lblSubOrd' + (i + 1).toString()).innerText)
                return false;
            }
            else if (document.getElementById('ctl00_MainContent_ddlOvrAllRating' + (i + 1).toString()).value == "0") {
                alert("Please provide Overall rating for " + document.getElementById('ctl00_MainContent_lblSubOrd' + (i + 1).toString()).innerText)
                return false;
            }
            else if (document.getElementById('ctl00_MainContent_ddlTrnReq' + (i + 1).toString()).value == "0") {
                alert("Please provide Training requirement for " + document.getElementById('ctl00_MainContent_lblSubOrd' + (i + 1).toString()).innerText)
                return false;
            }
            else if ((document.getElementById('ctl00_MainContent_txtOvrallFeed' + (i + 1).toString()).value).trim() == "") {
                alert("Please provide Feedback for " + document.getElementById('ctl00_MainContent_lblSubOrd' + (i + 1).toString()).innerText)
                return false;
            } 
        }
    }
    function enableSubmit() {
        var count = parseInt(document.getElementById('ctl00_MainContent_feedbackCount').value);
        var flag = 1;
        for (var i = 0; i < count; i++) {
            
            if (document.getElementById('ctl00_MainContent_ddlPrbSA' + (i+1).toString()).value == "0") {
                flag = 0;
                break;
            }
            else if (document.getElementById('ctl00_MainContent_ddlPrgSkills' + (i + 1).toString()).value == "0") {
                flag = 0;
                break;
            }
            else if (document.getElementById('ctl00_MainContent_ddlCommSkills' + (i + 1).toString()).value == "0") {
                flag = 0;
                break;
            }
            else if (document.getElementById('ctl00_MainContent_ddlBehSkills' + (i + 1).toString()).value == "0") {
                flag = 0;
                break;
            }
            else if (document.getElementById('ctl00_MainContent_ddlPerformance' + (i + 1).toString()).value == "0") {
                flag = 0;
                break;
            }
            else if (document.getElementById('ctl00_MainContent_ddlOvrAllRating' + (i + 1).toString()).value == "0") {
                flag = 0;
                break;
            }
            else if ((document.getElementById('ctl00_MainContent_txtOvrallFeed' + (i + 1).toString()).value).trim() == "") {
                flag = 0;
                break;
            }
            else if ((document.getElementById('ctl00_MainContent_txtWrkArea' + (i + 1).toString()).value).trim() == "") {
                flag = 0;
                break;
            }
            else if (document.getElementById('ctl00_MainContent_ddlTrnReq' + (i + 1).toString()).value =="0") {
                flag = 0;
                break;
            }
        }
        if (flag == 1) {
            document.getElementById("ctl00_MainContent_Btn_submit").disabled = false;
        }
        else{
            document.getElementById("ctl00_MainContent_Btn_submit").disabled = true;
        }   
    }

</script>
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
        <tr class="evenrow">
            <td colspan="4" style="width: 100%; text-align: center" class="labelHeader">
                HEXAVARSITY VALUES YOUR COMMENTS
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pBody" runat="server" CssClass="cpBody" Width="100%">
            </asp:Panel>
            <table style="width: 100%; text-align: center;" class="oddrow">
                <tr>
                    <td colspan="3" align="center" style="width: 100%">
                        <asp:Button ID="Btn_submit" runat="server"  CssClass="button" 
                             Text="Submit" ValidationGroup="SupFeed"  
                            OnClientClick="return validate()" onclick="Btn_submit_Click"/>
                        <asp:Button ID="BtnSave" runat="server" CssClass="button" Text="Save" 
                            onclick="BtnSave_Click" />
                    </td>
                </tr>
                <tr style="width: 100%; text-align: left">
                    <td colspan="3">
                        <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="feedbackCount" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
