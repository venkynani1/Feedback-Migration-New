<%@ Page Title="Add Participants And Send Mail" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="NominationList.aspx.cs" Inherits="HexFeedback.NominationList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%-- <link href="css/style1.css" rel="stylesheet"/>--%>
    <link href="Styles/NewStyles.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function confirmmsg(str) {

            x = confirm(str);
            if (x == true)
                return true;
            else
                return false;

        }
    </script>
    <script type="text/javascript" language="javascript">

        function uploadError(sender, args) {
            document.getElementById('<%=lblStatus.ClientID%>').innerText = args.get_fileName(),
	"<span style='color:red;'>" + args.get_errorMessage() + "</span>";
        }

        function StartUpload(sender, args) {
            document.getElementById('<%=lblStatus.ClientID%>').innerText = 'Uploading Started...';
        }

        function UploadComplete(sender, args) {
            var filename = args.get_fileName();
//            var valid_extensions = /(\.jpg|\.jpeg|\.gif)$/i;  
            var valid_extensions = /(\.xls)$/i;
            if (valid_extensions.test(filename)) {
                document.getElementById('<%=lblStatus.ClientID%>').innerText = 'File Successfully Uploaded.';
            }
            else {
                document.getElementById('<%=lblStatus.ClientID%>').innerText = 'Your file was not uploaded because it does not have a .xls extension.';
                return false;
                  } 
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset class="ForFieldset">
        <legend class="ForLegend">Upload Participant List & Send Mail</legend>
        <asp:HiddenField ID="hdnPage" runat="server" Value="NominationList" />
        <table style="width: 100%; text-align: center;">
            <tr class="evenrow">
                <td class="label" style="width: 25%; text-align: right;">
                    Type:
                </td>
                <td style="width: 75%; text-align: left;" colspan="3">
                    <asp:RadioButtonList runat="server" ID="rdType" CssClass="RadioButtonList" 
                        RepeatDirection="Horizontal" 
                        onselectedindexchanged="rdType_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="0" Selected="True">Nomination List</asp:ListItem>
                    <asp:ListItem Value="1">Supervisor's List</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 25%; text-align: right;">
                    Course:
                </td>
                <td style="width: 25%; text-align: left;">
                    <asp:DropDownList ID="drpCourse" CssClass="DropDownListNew" runat="server" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged"
                        AutoPostBack="True" />
                </td>
                <td class="label" style="width: 20%; text-align: right;">
                    Start Date:
                </td>
                <td class="label" style="width: 30%; text-align: left;">
                    <asp:Label ID="lblStartDate" Font-Bold="true" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="4">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 25%; text-align: right;">
                    Session:
                </td>
                <td style="width: 25%; text-align: left;">
                    <asp:DropDownList ID="drpSession" CssClass="DropDownListNew" runat="server" OnSelectedIndexChanged="drpSession_SelectedIndexChanged"
                        AutoPostBack="True" />
                </td>
                <td class="label" style="width: 20%; text-align: right;">
                    End Date:
                </td>
                <td class="label" style="width: 30%; text-align: left;">
                    <asp:Label ID="lblEndDate" Font-Bold="true" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="4">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" colspan="4" style="width: 100%; text-align: left;">
                    <table>
                        <tr>
                            <td class="label" style="width: 41%; text-align: left;">
                            </td>
                            <td class="labelHeader" style="width: 59%; text-align: left;">
                                <asp:LinkButton ID="LnkDampleNominationLst" runat="server" OnClick="LnkDampleNominationLst_Click1">Download sample Participants list.</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="4">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" id="tdUPload" runat="server" style="width: 25%; text-align: right;">
                    Upload Participants list:
                </td>
                <td colspan="2" style="width: 25%; text-align: left;">
                    <asp:AsyncFileUpload ID="AsyncFileUpload1" Width="400px" runat="server" OnClientUploadError="uploadError"
                        OnClientUploadStarted="StartUpload" OnClientUploadComplete="UploadComplete" CompleteBackColor="#9bffff"
                        UploaderStyle="Modern" ErrorBackColor="#ff6640" ThrobberID="Throbber" UploadingBackColor="#b0ffe6"
                        OnUploadedComplete="AsyncFileUpload1_UploadedComplete" />
                </td>
                <td>
                    <asp:Label ID="Throbber" runat="server" Style="display: none">
                        <img src="Images/images_loading_animation.gif" align="middle" alt="loading" />
                    </asp:Label>
                </td>
            </tr>
            <tr class="oddrow" style="text-align: center">
                <td colspan="4">
                    <asp:Label ID="lblStatus" CssClass="labelErrMsg" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="4">
                </td>
            </tr>
            <tr class="oddrow">
                <td colspan="4" style="width: 100%; text-align: center;">
                    <asp:Button ID="butImport" runat="server" CssClass="button" ToolTip="Click here to import the Participants list "
                        Text="Import Participants list" OnClick="butImport_Click" Width="200px"></asp:Button>
                    <asp:Button ID="btnSupImport" runat="server" CssClass="button" ToolTip="Click here to import the Supervisor list "
                        Text="Import Supervisors list" Width="200px" onclick="btnSupImport_Click"></asp:Button>
                </td>
            </tr>
            <tr class="evenrow">
                <td colspan="4" style="width: 100%; text-align: center;">
                    <asp:Panel ID="pnlforrpt" runat="server">
                        <fieldset class="ForFieldset">
                            <legend id="NominateMembers" runat="server" class="ForLegend">Participants list</legend>
                            <asp:GridView ID="grvNominationList" runat="server" Width="100%" CssClass="hdrow"
                                RowStyle-CssClass="oddrow" AlternatingRowStyle-CssClass="evenrow">
                            </asp:GridView>
                            <br />
                            <asp:Button ID="butAdd" runat="server" CssClass="button" ToolTip="click here to send mail."
                                Text="Send Mail to above Participants" OnClick="butAdd_Click" OnClientClick="javascript:return confirmmsg('Are you sure want to send mail?');"
                                Width="220px"></asp:Button>
                        </fieldset>
                    </asp:Panel>
                </td>
            </tr>
            <tr class="oddrow" style="width: 100%;">
                <td colspan="4">
                </td>
            </tr>
            <tr class="evenrow">
                <td colspan="4" style="width: 100%; text-align: center;">
                    <asp:Panel ID="Pnlmail" runat="server">
                        <fieldset class="ForFieldset">
                            <legend class="ForLegend">Mail Status</legend>
                            <asp:GridView ID="GrvMailstatus" runat="server" Width="100%" CssClass="hdrow" RowStyle-CssClass="oddrow"
                                AlternatingRowStyle-CssClass="evenrow">
                            </asp:GridView>
                        </fieldset>
                    </asp:Panel>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%; text-align: left">
                <td colspan="4">
                    <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
