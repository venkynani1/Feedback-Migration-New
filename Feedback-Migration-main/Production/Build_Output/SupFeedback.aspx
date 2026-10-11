<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupFeedback.aspx.cs" Inherits="HexFeedback.SupFeedback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script language="javascript" type="text/javascript">

    function ValidateFeed(sender, args) {
        if (args.Value.trim() == "" || args.Value.Length <=0) {
            return args.IsValid = false;
        }
        else {
            return args.IsValid = true;
        }
    }
    function ValidateRemarks(sender, args) {
        if (document.getElementById("ctl00_MainContent_ddlTrnReq").value == "1" && (args.Value.trim() == "" || args.Value.Length <= 0)) {
            return args.IsValid = false;
        }
        else {
            return args.IsValid = true;
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
                <asp:Label ID="Label1" runat="server">Sub-ordinate Name:</asp:Label>
            </td>
            <td style="width: 25%;">
                <asp:DropDownList ID="ddlSubOrd" CssClass="DropDownListNew" runat="server">
                <asp:ListItem Value="-1">Select</asp:ListItem>
                <asp:ListItem Value="0">Chainu</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSubOrd" 
                    ID="rfvSubOrd" ValidationGroup="SupFeed" 
                    ErrorMessage="Please select your Sub-ordinate" InitialValue="-1" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="oddrow">
            <td colspan="4" style="width: 100%; text-align: center" class="labelHeader">
                HEXAVARSITY VALUES YOUR COMMENTS
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pBody" runat="server" CssClass="cpBody" Width="100%">
                <table style="width: 70%; text-align: center; float:left">
                    <tr id="trHead" runat="server" class="hdrow">
                         <td style="width: 50%; text-align: center;">
                              Assessment Basis
                         </td>
                         <td style="width: 50%; text-align: center;">
                              Options
                         </td>
                    </tr>
                    <tr id="Row11" runat="server" class="oddrow">
                        
                        <td align="left" style="width: 50%" class="label">
                            <asp:Label ID="Lb_ques1_1" runat="server">Rating</asp:Label><br />
                        </td>
                        <td align="left" style="width: 50%;">
                            <asp:DropDownList runat="server" ID="ddlRating" CssClass="DropDownListNew">
                            <asp:ListItem Value="-1">Select</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="1">1</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRating" ID="rfvRating" 
                            ValidationGroup="SupFeed" ErrorMessage="Please select Rating" InitialValue="-1" Display="None">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="Row12" runat="server" class="evenrow">
                        <td align="left" style="width: 50%" class="label">
                            <asp:Label ID="Label2" runat="server">Is Training Required?</asp:Label><br />
                        </td>
                        <td align="left" style="width: 50%;">
                            <asp:DropDownList runat="server" ID="ddlTrnReq" CssClass="DropDownListNew">
                            <asp:ListItem Value="-1">Select</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTrnReq" ID="rfvTrnReq" 
                            ValidationGroup="SupFeed" 
                                ErrorMessage="Please select whether training is required" InitialValue="-1" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="Row13" runat="server" class="oddrow">
                        <td align="left" style="width: 50%" class="label">
                            <asp:Label ID="Label3" runat="server">Overall Feedback</asp:Label><br />
                        </td>
                        <td align="left" style="width: 50%;">
                            <asp:TextBox runat="server" ID="txtOFeedback" TextMode="MultiLine" Height="90px" Width="98%"></asp:TextBox>
                            <asp:CustomValidator ID="cvOvrallFeed" runat="server" 
                                ErrorMessage="Please provide Overall Feedback" 
                                ControlToValidate="txtOFeedback" ValidationGroup="SupFeed" 
                                ClientValidationFunction="ValidateFeed" ValidateEmptyText="True" Display="None">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr id="Row14" runat="server" class="evenrow">
                        <td align="left" style="width: 50%" class="label">
                            <asp:Label ID="Label4" runat="server">Remarks</asp:Label><br />
                        </td>
                        <td align="left" style="width: 50%;">
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Height="90px" Width="98%"></asp:TextBox>
                            <asp:CustomValidator ID="cvRemark" runat="server" 
                                ErrorMessage="Please provide Remarks" 
                                ControlToValidate="txtRemarks" ValidationGroup="SupFeed" 
                                ClientValidationFunction="ValidateRemarks" ValidateEmptyText="True" Display="None">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
                <table style="width: 29%; text-align: center; float:left; margin:60px 5px">
                                <tr id="RowParamHead" runat="server" class="hdrow">
                                    <td style="width: 43%; text-align: center;">
                                        Parameters for Assessing
                                    </td>
                                </tr>
                                <tr id="TrP1" runat="server" class="oddrow">
                                    <td  style="width: 100%; margin-left:10px;" class="label">
                                         <asp:Label ID="Label5" runat="server">Objectives of the Training course met</asp:Label><br />
                                    </td>
                                </tr>
                                <tr id="TrP2" runat="server" class="evenrow">
                                    <td align="left" style="width: 100%" class="label">
                                         <asp:Label ID="Label6" runat="server">Problem solving ability</asp:Label><br />
                                    </td>
                                </tr>
                                <tr id="TrP3" runat="server" class="oddrow">
                                    <td align="left" style="width: 100%" class="label">
                                         <asp:Label ID="Label7" runat="server">Programming skill / ability</asp:Label><br />
                                    </td>
                                </tr>
                                <tr id="TrP4" runat="server" class="evenrow">
                                    <td align="left" style="width: 100%" class="label">
                                         <asp:Label ID="Label8" runat="server">Communication\Writing Skill Ability</asp:Label><br />
                                    </td>
                                </tr>
                                <tr id="TrP5" runat="server" class="oddrow">
                                    <td align="left" style="width: 100%" class="label">
                                         <asp:Label ID="Label9" runat="server">Performance of Members after Training</asp:Label><br />
                                    </td>
                                </tr>
                                <tr id="TrP6" runat="server" class="evenrow">
                                    <td align="left" style="width: 100%" class="label">
                                         <asp:Label ID="Label10" runat="server">Behavioural skill</asp:Label><br />
                                    </td>
                                </tr>
                            </table>
            </asp:Panel>
            <table style="width: 100%; text-align: center;" class="oddrow">
                <tr>
                    <td colspan="3" align="center" style="width: 100%">
                        <asp:Button ID="Btn_submit" runat="server"  CssClass="button"
                             Text="Submit" ValidationGroup="SupFeed" />
                        <asp:Button ID="BtnNextTrainer" runat="server" CssClass="button" OnClientClick="javascript:return confirmmsg('Are you sure to submit?');"
                            Text="Submit & proceed" Width="170px"  />
                        &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="BtnClose" runat="server" CssClass="button" OnClientClick="javascript:return confirmmsg('Are you sure to close?');"
                            Text="Close"  />
                    </td>
                </tr>
                <tr style="width: 100%; text-align: left">
                    <td colspan="3">
                        <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                ValidationGroup="SupFeed" ShowMessageBox="True" ShowSummary="False" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
