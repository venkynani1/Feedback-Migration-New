<%@ Page Title="Add New Session" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Session.aspx.cs" Inherits="HexFeedback._Default" UICulture="auto"
    Culture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function concatDocumentNumber() {
            var docNumber = document.getElementById('MainContent_drpCname').value;
            var docPrefix = document.getElementById('MainContent_LstTrainers').value;
            var docSuffix = document.getElementById('MainContent_txtFrmDt').value;

            var concatDocNumber = docNumber + "_" + docPrefix + "_" + docSuffix;
            document.getElementById('MainContent_txtSessionID').value = concatDocNumber;

        }
        function confirmmsg(str) {

            x = confirm(str);
            if (x == true)
                return true;
            else
                return false;

        }
        function FromDateValidation() {
            var FromDate = document.getElementById('<%=txtFrmDt.ClientID %>');
            FeatureDate = new Date(FromDate.value);
            today = new Date()
            if (today < FeatureDate) {
                alert('From Date should be current date or less than current date.')
                FromDate.value = "";
                return false;
            }
        }
        function CourseValidation() {
            //Course Validation
            var dropDownListRef = document.getElementById('<%=drpCname.ClientID %>');
            var itemIndex = dropDownListRef.selectedIndex;
            if (itemIndex == 0) {
                alert('Please Select Course Name.');
                return false;
            }

            //Location Validation
            var dropDownLocation = document.getElementById('<%=drplocation.ClientID %>');
            var itemIndex = dropDownLocation.selectedIndex;
            if (itemIndex == 0) {
                alert('Please Select Location Name.');
                return false;
            }
            //Trainer Type
            var dropDowndrptype = document.getElementById('<%=drptype.ClientID %>');
            var itemIndex = dropDowndrptype.selectedIndex;
            if (itemIndex == 0) {
                alert('Please Select Trainer Type.');
                return false;
            }
            //From Date validation
            var TxtFromDate = document.getElementById('<%=txtFrmDt.ClientID %>');
            if (TxtFromDate.value == "") {
                alert('Please Enter From Date.');
                return false;
            }
            //To Date validation
            var TxtToDate = document.getElementById('<%=txtToDt.ClientID %>');
            if (TxtToDate.value == "") {
                alert('Please Enter To Date.');
                return false;
            }
            //Date Validation
            var FromDate = document.getElementById('<%=txtFrmDt.ClientID %>');
            var ToDate = document.getElementById('<%=txtToDt.ClientID %>');
            FeatureDate = new Date(FromDate.value);
            Featuredates = new Date(ToDate.value);
            Now = new Date()
            if (FeatureDate > Featuredates) {
                alert('From Date should be less than or equal to To date.');
                return false;
            }
            var confrim = document.getElementById('<%=hdnConfirm.ClientID %>');
            if (confrim.value.toString() == "1") {
                ConfirmSession();
            }
            //To Date validation
            //Changed on 27 APR 2015 .. Max participants=100
//            var TxtmaxParticipants = document.getElementById('<%=txtmaxpat.ClientID %>');
//            if (TxtmaxParticipants.value == "") {
//                alert('Please Enter max No.of Participants.');
//                return false;
//            }

//            var TxtmaxParticipantsCount = document.getElementById('<%=txtmaxpat.ClientID %>');
//            if (TxtmaxParticipantsCount.value >= 1 && TxtmaxParticipantsCount.value <= 500) {
//                return true;
//            }
//            else {
//                alert('Please Enter max No.of Participants From 1 to 500.');
//                return false;
//            }

        }
        function ConfirmSession() {
            if (confirm("Session for the provided Course and Time is already available. Would you like to another?"))
            {
                document.getElementById('<%= hdnCreateNew.ClientID %>').value = "Y";
            }
            else
            {
                document.getElementById('<%= hdnCreateNew.ClientID %>').value = "N";
            }
        }
        function CreateSessionMsg() {
            alert("Session created successfully.\nSession ID: " + document.getElementById('<%= hdnSessionID.ClientID %>').value.toString()); 
        }
    </script>
    <link href="Styles/NewStyles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <asp:UpdatePanel runat="server" ID="SessionUpdate">
            <ContentTemplate>
                <fieldset class="ForFieldset">
                    <legend class="ForLegend">Add New Session</legend>
                    <table style="width: 100%; text-align: center;">
                        <tr class="oddrow">
                            <td class="label" style="width: 15%; text-align: right;">
                                Course Name:
                            </td>
                            <td style=" text-align: left;" colspan="3" >
                                <asp:DropDownList ID="drpCname" runat="server" CssClass="DropDownListNew" OnSelectedIndexChanged="drpCname_SelectedIndexChanged"
                                     TabIndex="1" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="evenrow" style="width: 100%;">
                            <td colspan="4">
                                &nbsp;</td>
                        </tr>
                        <tr class="oddrow">
                            <td class="label" style="width: 15%; text-align: right;">
                                Start Date:</td>
                            <td style="width: 35%; text-align: left;">
                                
                                <asp:TextBox ID="txtFrmDt" runat="server" AutoPostBack="True" 
                                    CssClass="textboxNew" OnTextChanged="txtFrmDt_TextChanged" TabIndex="3"></asp:TextBox>
                                <asp:CalendarExtender ID="txtFrmDt_CalendarExtender" runat="server" 
                                    Format="dd MMMM yyyy" PopupButtonID="ImgCalender" TargetControlID="txtFrmDt">
                                </asp:CalendarExtender>
                                <asp:ImageButton ID="ImgCalender" runat="Server" 
                                    AlternateText="Click here to display calendar" 
                                    ImageUrl="~/Images/calendar.gif" />
                            </td>
                            <td class="label" style="width: 15%; text-align: right;">
                                End Date:
                            </td>
                            <td style="width: 35%; text-align: left;">
                                <asp:TextBox ID="txtToDt" runat="server" CssClass="textboxNew" TabIndex="4" ToolTip="Enter The To Date"
                                    AutoPostBack="True" ontextchanged="txtToDt_TextChanged"></asp:TextBox>
                                <asp:ImageButton runat="Server" ID="ImgbtnCalender" ImageUrl="~/Images/calendar.gif"
                                    AlternateText="Click here to display calendar" />
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd MMMM yyyy"
                                    TargetControlID="txtToDt" PopupButtonID="ImgbtnCalender">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr class="evenrow">
                            <td colspan="4" style="width: 100%; text-align: right;">
                            </td>
                        </tr>
                        <tr class="oddrow">
                            <td class="label" style="width: 15%; text-align: right;">
                                Trainer Type:
                            </td>
                            <td style="width: 35%; text-align: left;">
                                <asp:DropDownList ID="drptype" runat="server" CssClass="DropDownListNew" TabIndex="1"
                                    OnSelectedIndexChanged="drptype_SelectedIndexChanged" AutoPostBack="True" >
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem Value="I">Internal</asp:ListItem>
                                    <asp:ListItem Value="E">External</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="label" style="width: 15%; text-align: right;">
                                Max No Of Participants:
                            </td>
                            <td style="width: 35%; text-align: left;" class="label">
                                <b>500</b>
                                <!--<asp:TextBox ID="txtmaxpat" runat="server" CssClass="textboxNew" TabIndex="4" ToolTip="Enter Maximum No.Of Participants" Visible="false" ></asp:TextBox>-->
                            </td>
                        </tr>
                        <tr class="evenrow">
                            <td colspan="4" style="width: 100%; text-align: right;">
                            </td>
                        </tr>
                        <tr class="oddrow">
                            <td class="label" style="width: 15%; text-align: right;">
                                Vendor Name:
                            </td>
                            <td style="width: 35%; text-align: left;">
                                <asp:DropDownList ID="ddlvendor" runat="server" CssClass="DropDownListNew" TabIndex="1"
                                    OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td class="label" style="width: 15%; text-align: right;">

                                Training Location:</td>
                            <td style="width: 35%; text-align: left;">
                                <asp:DropDownList ID="drplocation" runat="server" CssClass="DropDownListNew" 
                                    TabIndex="1" onselectedindexchanged="drplocation_SelectedIndexChanged" AutoPostBack="True" >
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>Bangalore</asp:ListItem>
                                    <asp:ListItem>Chennai</asp:ListItem>
                                    <asp:ListItem>Mumbai</asp:ListItem>
                                    <asp:ListItem>Pune</asp:ListItem>
                                    <asp:ListItem>Mexico</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="evenrow" style="width: 100%;">
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr class="oddrow">
                            <td class="label" style="width: 15%; text-align: right;">
                                Trainer ID:
                            </td>
                            <td style="width: 45%; text-align: left;">
                                <asp:ListBox ID="lSTTraineriD" runat="server" CssClass="ListboxNew" SelectionMode="Multiple">
                                </asp:ListBox>
                            </td>
                            <td style="width: 15%; text-align: center;">
                                <asp:Button ID="BtnTrainmdoe" runat="server" CssClass="button" Text="   >>   " OnClick="BtnTrainmdoe_Click" />
                                <br />
                                <br />
                                <asp:Button ID="BtnRemoveTrainer" runat="server" CssClass="button" Text="   <<   "
                                    OnClick="BtnRemoveTrainer_Click" />
                            </td>
                            <td style="width: 25%; text-align: left;">
                                <asp:ListBox ID="LstTrainers" runat="server" CssClass="ListboxNew" SelectionMode="Multiple">
                                </asp:ListBox>
                            </td>
                        </tr>
                        <tr class="evenrow" style="width: 100%;">
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr class="oddrow" id="tdSessionAuto" runat="server">
                            <td class="label" style="width: 15%; text-align: right;">
                                Session(Auto):
                            </td>
                            <td style="width: 45%; text-align: left;">
                                <asp:TextBox ID="txtSessionID" runat="server" CssClass="textboxNew" TabIndex="2"
                                    ToolTip="Enter the session ID" ReadOnly="True"></asp:TextBox>
                                <br />
                                <asp:Label ID="Lb_auto" runat="server" Font-Bold="True" Text="Auto Generated"></asp:Label>
                            </td>
                            <td style="width: 15%; text-align: right;">
                            </td>
                            <td style="width: 25%; text-align: left;">
                            </td>
                        </tr>
                        <tr class="oddrow">
                            <asp:Panel ID="Panel1" runat="server">
                                <td colspan="4" style="width: 100%; text-align: center;">
                                    <asp:Button ID="butCreate" runat="server" CssClass="button" OnClick="butSignIn_Click"
                                        TabIndex="6" Text="Submit" OnClientClick="javascript:return CourseValidation();"
                                        ToolTip="Click here to submit the Session Details" />
                                    &nbsp; &nbsp; &nbsp;
                                    <asp:Button ID="BtnReset" runat="server" CssClass="button" TabIndex="6" Text="Reset"
                                        ToolTip="Click here to Reset" OnClick="BtnReset_Click" />
                                </td>
                            </asp:Panel>
                        </tr>
                        <tr class="evenrow" style="width: 100%; text-align: left">
                            <td colspan="4">
                                <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField runat="server" ID="hdnCreateNew" />
                                <asp:HiddenField runat="server" ID="hdnConfirm" />
                                <asp:HiddenField runat="server" ID="hdnSessionID" />                             
                            </td>                        
                        </tr>
                    </table>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
