<%@ Page Title="Record My Feedback" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="HexFeedback.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/NewStyles.css" rel="stylesheet" />
    <script type="text/javascript">
        function confirmmsg(str) {

            x = confirm(str);
            if (x == true)
                return true;
            else
                return false;

        }
    </script>
    <script type="text/javascript">
        function ClearTrainer() {
            var dropDownListRef = document.getElementById('<%= ddlTrainer.ClientID %>');
            var optionsList = '';

            if (dropDownListRef.value.length > 0) {
                var itemIndex = dropDownListRef.selectedIndex;

                if (itemIndex >= 0)
                    dropDownListRef.remove(itemIndex);
                CheckTrainerSelection();
            }

        }
    </script>
    <script type="text/javascript">
        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1)) {
                    return false;
                }
                else {
                    var temp = maxLen - 1;
                    document.getElementById('<%= lblcharcount.ClientID %>').innerHTML = +temp - txt.value.length + '&nbsp;Character remaining.';
                }
            } catch (e)
         { }

        } 
    </script>
      <script type="text/javascript">
          function textboxMultilineMinNumber(txt, maxLen) {
              try {
                  if (txt.value.length > (maxLen - 1)) {
                      return false;
                  }
                  else {
                      if (txt.value.length == 0) {
                          return false
                      }
                      else {
                          var temp = maxLen + 1;
                          document.getElementById('<%= lblcharcount.ClientID %>').innerHTML = +temp - txt.value.length + '&nbsp;Character remaining.';
                      }
                  }
              } catch (e)
         { }

          } 
    </script>
   

    <script type="text/javascript">
        function CheckTrainerSelection() {
            //1.Training Administration
            var Radio1_1 = document.getElementById('<%= RB_ans1_1.ClientID %>');
            var RB_ans1_2 = document.getElementById('<%= RB_ans1_2.ClientID %>');
            var RB_ans1_3 = document.getElementById('<%= RB_ans1_3.ClientID %>');
            //2.Training Design And Methodology
            var RB_ans2_1 = document.getElementById('<%= RB_ans2_1.ClientID %>');
            var RB_ans2_2 = document.getElementById('<%= RB_ans2_2.ClientID %>');
            var RB_ans2_3 = document.getElementById('<%= RB_ans2_3.ClientID %>');
            var RB_ans2_4 = document.getElementById('<%= RB_ans2_4.ClientID %>');
            var RB_ans2_5 = document.getElementById('<%= RB_ans2_5.ClientID %>');
            var RB_ans2_6 = document.getElementById('<%= RB_ans2_6.ClientID %>');
            var RB_ans2_7 = document.getElementById('<%= RB_ans2_7.ClientID %>');
            var RB_ans2_8 = document.getElementById('<%= RB_ans2_8.ClientID %>');
            var RB_ans2_9 = document.getElementById('<%= RB_ans2_9.ClientID %>');
            //3.Training Delivery
            var RB_ans3_1 = document.getElementById('<%= RB_ans3_1.ClientID %>');
            var RB_ans3_2 = document.getElementById('<%= RB_ans3_2.ClientID %>');
            var RB_ans3_3 = document.getElementById('<%= RB_ans3_3.ClientID %>');
            var RB_ans3_4 = document.getElementById('<%= RB_ans3_4.ClientID %>');
            var RB_ans3_5 = document.getElementById('<%= RB_ans3_5.ClientID %>');
            var RB_ans3_6 = document.getElementById('<%= RB_ans3_6.ClientID %>');
            var BtnSubmit = document.getElementById('<%= Btn_submit.ClientID %>');
            var BtnNextTrainers = document.getElementById('<%= BtnNextTrainer.ClientID %>');

            var dropDownListTrainer = document.getElementById('<%= ddlTrainer.ClientID %>');
            var itemIndexVal = dropDownListTrainer.selectedIndex;
            if (itemIndexVal == 0) {
                document.getElementById('<%= lblMsg.ClientID %>').innerHTML = 'Please select trainer to enable the options.';
                //1.Training Administration
                if (Radio1_1 != null) {
                    Radio1_1.disabled = true;
                    RB_ans1_2.disabled = true;
                    RB_ans1_3.disabled = true;
                    //2.Training Design And Methodology
                    RB_ans2_1.disabled = true;
                    RB_ans2_2.disabled = true;
                    RB_ans2_3.disabled = true;
                    RB_ans2_4.disabled = true;
                    RB_ans2_5.disabled = true;
                    RB_ans2_6.disabled = true;
                    RB_ans2_7.disabled = true;
                    RB_ans2_8.disabled = true;
                    RB_ans2_9.disabled = true;
                }
                //3.Training Delivery
                RB_ans3_1.disabled = true;
                RB_ans3_2.disabled = true;
                RB_ans3_3.disabled = true;
                RB_ans3_4.disabled = true;
                RB_ans3_5.disabled = true;
                RB_ans3_6.disabled = true;
                if (BtnSubmit != null) {
                    BtnSubmit.disabled = true;
                }
                if (BtnNextTrainers != null) {
                    BtnNextTrainers.disabled = true;
                }
                return false;
            }
            else {
                document.getElementById('<%= lblMsg.ClientID %>').innerHTML = '';
                //1.Training Administration
                if (Radio1_1 != null) {
                    Radio1_1.disabled = false;
                    RB_ans1_2.disabled = false;
                    RB_ans1_3.disabled = false;
                    //2.Training Design And Methodology
                    RB_ans2_1.disabled = false;
                    RB_ans2_2.disabled = false;
                    RB_ans2_3.disabled = false;
                    RB_ans2_4.disabled = false;
                    RB_ans2_5.disabled = false;
                    RB_ans2_6.disabled = false;
                    RB_ans2_7.disabled = false;
                    RB_ans2_8.disabled = false;
                    RB_ans2_9.disabled = false;
                }
                //3.Training Delivery
                RB_ans3_1.disabled = false;
                RB_ans3_2.disabled = false;
                RB_ans3_3.disabled = false;
                RB_ans3_4.disabled = false;
                RB_ans3_5.disabled = false;
                RB_ans3_6.disabled = false;
                if (BtnSubmit != null) {
                    BtnSubmit.disabled = false;
                }
                if (BtnNextTrainers != null) {
                    BtnNextTrainers.disabled = false;
                }
                return true;
            }
        }
    </script>
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
                <asp:Label ID="Lb_Empname" runat="server">Trainee Name:</asp:Label>
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
                <asp:Label ID="Lb_Empid" runat="server">Trainee ID:</asp:Label>
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
                <asp:Label ID="Label1" runat="server">Trainer Name:</asp:Label>
            </td>
            <td style="width: 25%;">
                <asp:DropDownList ID="ddlTrainer" CssClass="DropDownListNew" runat="server" OnSelectedIndexChanged="ddlTrainer_SelectedIndexChanged">
                </asp:DropDownList>
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
            <asp:Panel ID="PnlHeader" runat="server">
                <table style="width: 100%; text-align: center;">
                    <tr class="hdrow">
                        <td align="center" style="width: 5%;">
                            Sec No
                        </td>
                        <td align="center" style="width: 43%;">
                            Questions
                        </td>
                        <td align="center" style="width: 52%;">
                            Options
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pHeader" runat="server">
                <table style="width: 100%; text-align: center;">
                    <tr id="RowFirstHeader" runat="server" class="hdrow">
                        <td align="center" style="width: 5%;">
                            1
                        </td>
                        <td style="width: 43%; text-align: left">
                            Training Administration
                        </td>
                        <td align="left" style="width: 52%;">
                            <asp:Label ID="lblText" runat="server" Font-Underline="True" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pBody" runat="server" CssClass="cpBody">
                <table style="width: 100%; text-align: center;">
                    <tr id="Row11" runat="server" class="oddrow">
                        <td align="center" style="width: 5%" class="label">
                            1.1
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_ques1_1" runat="server"></asp:Label><br />
                        </td>
                        <td align="left" style="width: 52%;">
                            <asp:RadioButtonList ID="RB_ans1_1" runat="server" CssClass="RadioButtonList" OnSelectedIndexChanged="RB_ans1_1_SelectedIndexChanged"
                                TabIndex="1" RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="Row12" runat="server" class="evenrow">
                        <td align="center" style="width: 5%" class="label">
                            1.2
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_ques1_2" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans1_2" runat="server" CssClass="RadioButtonList" OnSelectedIndexChanged="RB_ans1_1_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="Row13" runat="server" class="oddrow">
                        <td align="center" style="width: 5%" class="label">
                            1.3
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_ques1_3" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans1_3" runat="server" CssClass="RadioButtonList" OnSelectedIndexChanged="RB_ans1_1_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="PnlDesignAndMethodHeader" runat="server">
                <table style="width: 100%; text-align: center;">
                    <tr id="RowSecondHearer" runat="server" class="hdrow">
                        <td align="center" style="width: 5%;">
                            2
                        </td>
                        <td style="width: 43%; text-align: left">
                            Training Design And Methodology
                        </td>
                        <td style="width: 52%; text-align: left">
                            <asp:Label ID="lblDesignAndMethod" runat="server" Font-Underline="True" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="PnlDesignAndMethodBody" runat="server">
                <table style="width: 100%; text-align: center;">
                    <tr id="Row21" runat="server" class="oddrow">
                        <td align="center" style="width: 5%" class="label">
                            2.1
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_qus2_1" runat="server"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans2_1" runat="server" CssClass="RadioButtonList" OnSelectedIndexChanged="RB_ans1_1_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="Row22" runat="server" class="evenrow">
                        <td align="center" style="width: 5%" class="label">
                            2.2
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_qus2_2" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans2_2" runat="server" CssClass="RadioButtonList" OnSelectedIndexChanged="RB_ans1_1_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="Row23" runat="server" class="oddrow">
                        <td align="center" style="width: 5%" class="label">
                            2.3
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_qus2_3" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans2_3" runat="server" CssClass="RadioButtonList" OnSelectedIndexChanged="RB_ans1_1_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="Row24" runat="server" class="evenrow">
                        <td align="center" style="width: 5%" class="label">
                            2.4
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_qus2_4" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans2_4" runat="server" CssClass="RadioButtonList" OnSelectedIndexChanged="RB_ans1_1_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="Row25" runat="server" class="oddrow">
                        <td align="center" style="width: 5%;" class="label">
                            2.5
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_qus2_5" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans2_5" runat="server" CssClass="RadioButtonList" OnSelectedIndexChanged="RB_ans1_1_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="Row26" runat="server" class="evenrow">
                        <td align="center" style="width: 5%" class="label">
                            2.6
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_qus2_6" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans2_6" runat="server" CssClass="RadioButtonList" OnSelectedIndexChanged="RB_ans1_1_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="Row27" runat="server" class="oddrow">
                        <td align="center" style="width: 5%;" class="label">
                            2.7
                        </td>
                        <td align="left" style="width: 43%;" class="label">
                            <asp:Label ID="Lb_qus2_7" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans2_7" runat="server" CssClass="RadioButtonList" OnSelectedIndexChanged="RB_ans1_1_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="Row28" runat="server" class="evenrow">
                        <td align="center" style="width: 5%" class="label">
                            2.8
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_qus2_8" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans2_8" runat="server" CssClass="RadioButtonList" OnSelectedIndexChanged="RB_ans1_1_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="Row29" runat="server" class="oddrow">
                        <td align="center" style="width: 5%" class="label">
                            2.9
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_qus2_9" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans2_9" runat="server" CssClass="RadioButtonList" OnSelectedIndexChanged="RB_ans1_1_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlTrainingDeliveryHeader" runat="server">
                <table style="width: 100%; text-align: center;">
                    <tr id="RowThirdHeader" runat="server" class="hdrow">
                        <td align="center" style="width: 5%;">
                            3
                        </td>
                        <td style="width: 43%; text-align: left">
                            Trainer Delivery
                        </td>
                        <td style="width: 52%; text-align: left">
                            <asp:Label ID="lblTrainingDelivery" runat="server" Font-Underline="True" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlTrainingDeliveryBody" runat="server">
                <table style="width: 100%; text-align: center;">
                    <tr class="oddrow">
                        <td align="center" style="width: 5%" class="label">
                            3.1
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_qus3_1" runat="server"></asp:Label>
                            <br />
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans3_1" runat="server" CssClass="RadioButtonList" RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="evenrow">
                        <td align="center" style="width: 5%" class="label">
                            3.2
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_qus3_2" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans3_2" runat="server" CssClass="RadioButtonList" RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="oddrow">
                        <td align="center" style="width: 5%" class="label">
                            3.3
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_qus3_3" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans3_3" runat="server" CssClass="RadioButtonList" RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="evenrow">
                        <td align="center" style="width: 5%" class="label">
                            3.4
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_qus3_4" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans3_4" runat="server" CssClass="RadioButtonList" RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="oddrow">
                        <td align="center" style="width: 5%" class="label">
                            3.5
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_qus3_5" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans3_5" runat="server" CssClass="RadioButtonList" RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="evenrow">
                        <td align="center" style="width: 5%" class="label">
                            3.6
                        </td>
                        <td align="left" style="width: 43%" class="label">
                            <asp:Label ID="Lb_qus3_6" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td align="left" style="width: 52%">
                            <asp:RadioButtonList ID="RB_ans3_6" runat="server" CssClass="RadioButtonList" RepeatDirection="Horizontal">
                                <asp:ListItem Value="4">Strongly Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="3">Agree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="1">Strongly Disagree&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="5">Not Applicable&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 100%; text-align: center;" class="oddrow">
                <tr>
                    <td style="text-align: left; width: 42%" colspan="2" class="labelErrMsg">
                        <asp:Label ID="lblcharcount" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="text-align: left; width: 58%">
                        <asp:Label ID="Lb_traineecomments" runat="server" CssClass="labelHeader" Text="Overall Comments"></asp:Label>
                </tr>
                <tr>
                    <td colspan="3" align="center" style="width: 95%">
                        <asp:TextBox ID="Tb_traineecomments" runat="server" CssClass="textboxNew" TextMode="MultiLine"
                            Height="90px" MaxLength="2" Width="99%" onkeypress="return textboxMultilineMaxNumber(this,1000)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center" style="width: 100%">
                        <asp:Button ID="Btn_submit" runat="server" OnClick="Btn_submit_Click" CssClass="button"
                            OnClientClick="javascript:return confirmmsg('Are you sure to submit?');" Text="Submit" />
                        <asp:Button ID="BtnNextTrainer" runat="server" CssClass="button" OnClientClick="javascript:return confirmmsg('Are you sure to submit?');"
                            Text="Submit & proceed" Width="170px" OnClick="BtnNextTrainer_Click" />
                        &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="BtnClose" runat="server" CssClass="button" OnClientClick="javascript:return confirmmsg('Are you sure to close?');"
                            Text="Close" OnClick="BtnClose_Click" />
                    </td>
                </tr>
                <tr style="width: 100%; text-align: left">
                    <td colspan="3">
                        <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pBody"
                CollapseControlID="pHeader" ExpandControlID="pHeader" Collapsed="false" TextLabelID="lblText"
                CollapsedText="Expand.." ExpandedText="Collapse" CollapsedSize="0">
            </asp:CollapsiblePanelExtender>
            <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" TargetControlID="PnlDesignAndMethodBody"
                CollapseControlID="PnlDesignAndMethodHeader" ExpandControlID="PnlDesignAndMethodHeader"
                Collapsed="false" TextLabelID="lblDesignAndMethod" CollapsedText="Expand.." ExpandedText="Collapse"
                CollapsedSize="0">
            </asp:CollapsiblePanelExtender>
            <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" TargetControlID="pnlTrainingDeliveryBody"
                CollapseControlID="pnlTrainingDeliveryHeader" ExpandControlID="pnlTrainingDeliveryHeader"
                Collapsed="false" TextLabelID="lblTrainingDelivery" CollapsedText="Expand.."
                ExpandedText="Collapse" CollapsedSize="0">
            </asp:CollapsiblePanelExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
