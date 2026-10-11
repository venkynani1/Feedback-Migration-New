<%@ Page Title="SkillPortUpload" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SkillPortUpload.aspx.cs" Inherits="HexFeedback.SkillPort.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Scripts/jquery-1.11.0.min.js" type="text/javascript"></script>
    <link href="../Styles/NewStyles.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function fnShowProgrees() {
            var e = document.getElementById("ctl00_MainContent_progress");
            e.style.display = 'block';
        }
        function fnStopProgrees() {
            var e = document.getElementById("ctl00_MainContent_progress");
            e.style.display = 'none';
        }

//        function fnConfirm1() {
//            var ddlMonth = document.getElementById("<%=ddlMonth.ClientID%>");
//            var monthText = ddlMonth.options[ddlMonth.selectedIndex].text;
//            var monthValue = ddlMonth.options[ddlMonth.selectedIndex].value;

//            var ddlYear = document.getElementById("<%=ddlYear.ClientID%>");
//            var yearText = ddlYear.options[ddlYear.selectedIndex].text;
//            var yearValue = ddlYear.options[ddlYear.selectedIndex].value;

//            var reasons = document.getElementsByName("<%=rblExcelUpload.UniqueID%>");
//            var rblSelectedValue=null;
//            for (var j = 0; j < reasons.length; j++) {
//                if (reasons[j].checked) {
//                    rblSelectedValue = reasons[j].value;
//                }
//            }

//            var a = confirm("Press \"Ok\" to delete existing records and save new ones.\nPress \"Cancel\" to view the existing records.");
//            if (a == 1) {
//               fnShowProgrees();
//               fnConfirm(monthValue, yearValue, rblSelectedValue);
//            }
//            else {
//                return false;
//            }
//        }
//        $.noConflict();
//        function fnConfirm(monthValue, yearValue, rblSelectedValue) {
//            jQuery.ajax({
//                type: "POST",
//                url: "SkillPortUpload.aspx/getExcelData",
//                data: '{ ddlMonth: ' + monthValue + ', ddlYear: ' + yearValue + ', rblSelectedValue: "' + rblSelectedValue + '"}',
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",
//                success: OnSuccess,
//                failure: function (response) {
//                    alert(response.d);
//                }
//            });
//        }
//        function OnSuccess(response) {
//            if (response.d != 0) {
//                var val = response.d;
//                alert("Totally " + response.d + " Record(s) saved successfully");
//                var ddlYear = document.getElementById("<%=ddlYear.ClientID%>");
//                ddlYear.selectedIndex = 0;
//                var ddlMonth = document.getElementById("<%=ddlMonth.ClientID%>");
//                ddlMonth.selectedIndex = 0;
//                var reasons = document.getElementsByName("<%=rblExcelUpload.UniqueID%>");
//                for (var j = 0; j < reasons.length; j++) {
//                    if (reasons[j].checked==true) {
//                        reasons[j].checked = false;
//                    }
//                }
//            }
//            else {
//                var ddlYear = document.getElementById("<%=ddlYear.ClientID%>");
//                ddlYear.selectedIndex = 0;
//                var ddlMonth = document.getElementById("<%=ddlMonth.ClientID%>");
//                ddlMonth.selectedIndex = 0;
//                var reasons = document.getElementsByName("<%=rblExcelUpload.UniqueID%>");
//                for (var j = 0; j < reasons.length; j++) {
//                    if (reasons[j].checked == true) {
//                        reasons[j].checked = false;
//                    }
//                }
//                alert("Invalid File. Please Check the Columns of the File.");
//            }
//            fnStopProgrees();
//        }

        /* New addtion for del */
        function fnConfirmDel() {
            var ddlMonth = document.getElementById("<%=ddlMonth.ClientID%>");
            var monthText = ddlMonth.options[ddlMonth.selectedIndex].text;
            var monthValue = ddlMonth.options[ddlMonth.selectedIndex].value;

            var ddlYear = document.getElementById("<%=ddlYear.ClientID%>");
            var yearText = ddlYear.options[ddlYear.selectedIndex].text;
            var yearValue = ddlYear.options[ddlYear.selectedIndex].value;

            var reasons = document.getElementsByName("<%=rblExcelUpload.UniqueID%>");
            var rblSelectedValue = null;
            for (var j = 0; j < reasons.length; j++) {
                if (reasons[j].checked) {
                    rblSelectedValue = reasons[j].value;
                }
            }

            var a = confirm("Do you want to delete ther records?");
            if (a == 1) {
                fnShowProgrees();
                fnDel(monthValue, yearValue, rblSelectedValue);
            }
            else {
                return false;
            }
        }

        function fnDel(monthValue, yearValue, rblSelectedValue) {
            jQuery.ajax({
                type: "POST",
                url: "SkillPortUpload.aspx/delExcelData",
                data: '{ ddlMonth: ' + monthValue + ', ddlYear: ' + yearValue + ', rblSelectedValue: "' + rblSelectedValue + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnDelete,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnDelete(response) {
            if (response.d != 0) {
                var val = response.d;
                alert("Totally " + response.d + " Record(s) deleted successfully");
                var ddlYear = document.getElementById("<%=ddlYear.ClientID%>");
                ddlYear.selectedIndex = 0;
                var ddlMonth = document.getElementById("<%=ddlMonth.ClientID%>");
                ddlMonth.selectedIndex = 0;
                var reasons = document.getElementsByName("<%=rblExcelUpload.UniqueID%>");
                for (var j = 0; j < reasons.length; j++) {
                    if (reasons[j].checked == true) {
                        reasons[j].checked = false;
                    }
                }
            }
            else {
                var ddlYear = document.getElementById("<%=ddlYear.ClientID%>");
                ddlYear.selectedIndex = 0;
                var ddlMonth = document.getElementById("<%=ddlMonth.ClientID%>");
                ddlMonth.selectedIndex = 0;
                var reasons = document.getElementsByName("<%=rblExcelUpload.UniqueID%>");
                for (var j = 0; j < reasons.length; j++) {
                    if (reasons[j].checked == true) {
                        reasons[j].checked = false;
                    }
                }
                alert("Deletion failed. Please try after some time");
            }
            fnStopProgrees();
        }
        //End
 
    </script>
    <style type="text/css">
        .style4
        {
            font-family: Verdana, sans-serif;
            font-size: 12px;
            color: #333333;
            font-weight: normal;
            width: 10%;
            height: 7px;
        }
        .style5
        {
            width: 40%;
            height: 7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%; text-align: center;">
        <tr>
            <td align="center" valign="top" style="padding: 0 15px" class="style3">
                <div id="confdiv">
                    <fieldset class="ForFieldset">
                        <legend class="ForLegend">Excel Data Upload/View</legend>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr class="oddrow" style="width: 100%;">
                                <td class="label" style="width: 40%; text-align: right;">
                                    <strong>Month & Year :</strong>
                                </td>
                                <td style="width: 60%; text-align: left;" class="evenrw">
                                    <asp:DropDownList ID="ddlMonth" runat="server" class="DropDownListNew" Width="20%">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlYear" runat="server" class="DropDownListNew" Width="20%">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvMonth" runat="server" 
                                        ControlToValidate="ddlMonth" ErrorMessage="Please select month" 
                                        InitialValue="-1" ValidationGroup="GrpView" Display="None" ></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="rfvYear" runat="server" 
                                        ControlToValidate="ddlYear" ErrorMessage="RPlease select year" 
                                        InitialValue="-1" ValidationGroup="GrpView" Display="None"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <td height="5px" colspan="2"></td>
                             <tr class="oddrow">
                             <td class="style4" style="text-align: right;">
                                    <strong>Select the Option :</strong>
                                </td>
                                 <td style="text-align: left;" class="style5" colspan="3">
                                     <asp:RequiredFieldValidator ID="rfvOptions" runat="server" 
                                         ControlToValidate="rblExcelUpload" 
                                         ErrorMessage="Please select any one of the options" ValidationGroup="GrpView" Display="None"></asp:RequiredFieldValidator>
                                    <asp:RadioButtonList ID="rblExcelUpload" runat="server" Height="16px" 
                                         CausesValidation="true" Width="685px">
                                        <asp:ListItem>Learning Plan for Behavioral</asp:ListItem>
                                        <asp:ListItem>Learning Plan for TCDP</asp:ListItem>
                                        <asp:ListItem>Certifications</asp:ListItem>
                                        <asp:ListItem>Contributions</asp:ListItem>
                                        <asp:ListItem>Learning Plan for PMS</asp:ListItem>
                                        <asp:ListItem>TrainingHours</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                
                            </tr>
                            <td height="5px" colspan="2"></td>
                             <tr class="oddrow">
                                <td class="label" style="width: 40%; text-align: right;">
                                    <strong>Excel File Upload :</strong>
                                </td>
                                <td style="width: 60%; text-align: left;" class="evenrw" colspan="2">
                                    <asp:FileUpload ID="FileUploadToServer" runat="server" Width="282px"></asp:FileUpload>
                                    &nbsp;
                                    </td>
                            </tr>
                            <td height="5px" colspan="2"></td>
                            <tr>
                                <td height="5px" colspan="2">
                                <asp:Button ID="btnUpload" CssClass="button" runat="server" Text="Save" OnClientClick="fnShowProgrees();"  OnClick="btnUpload_Click"
                                        ValidationGroup="valGroup1" />
                                <asp:Button ID="btnView" CssClass="button" runat="server" Text="View" 
                                        OnClientClick="fnShowProgrees();" onclick="btnView_Click" 
                                        ValidationGroup="GrpView"/>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="GrpView" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>
    
    <fieldset id='fdView' runat="server" class="ForFieldset" style="margin:0 18px;">
     <legend class="ForLegend">Excel Data View</legend>
       <div style=" width:inherit; max-height:250px; overflow: auto;">
           <asp:GridView ID="gvCertificatesView" runat="server" Width="100%"  CssClass="hdrow"
                RowStyle-CssClass="oddrow" AlternatingRowStyle-CssClass="evenrow">
            </asp:GridView>
       </div>
       <br />
       <div style=" width:100px; margin: 0 auto;"><asp:Button ID="btnDelete" CssClass="button" runat="server" Text="Delete" 
       OnClientClick="fnShowProgrees(); fnConfirmDel();"/></div>
     </fieldset>

    <div align="center" id="progress" runat="server" style="display:none;">
        <img src="Images/roller01.gif" alt="" />
        <asp:Label ID="lblWait" runat="server" ForeColor="Red" Text="Please wait"></asp:Label>
    </div>
</asp:Content>
