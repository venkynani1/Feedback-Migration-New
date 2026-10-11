<%@ Page Title="Add New Course" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Course.aspx.cs" Inherits="HexFeedback._Course" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/NewStyles.css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 21%;
        }
        .style2
        {
            width: 79%;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function confirmmsg(str) {

            x = confirm(str);
            if (x == true)
                return true;
            else
                return false;

        }
        function ExceptionMessage(Msg) {
            alert(Msg);
            return false;
        }
    </script>
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <fieldset class="ForFieldset">
        <legend class="ForLegend">Add New Course</legend>
        <table style="width: 100%; text-align: center;">
            <tr class="oddrow">
                <td class="label" style="width: 30%; text-align: right;">
                    Course ID:
                </td>
                <td class="label" style="width: 30%; text-align: left;">
                    <asp:TextBox ID="txtCourseID" runat="server" CssClass="textboxNew" TabIndex="1" ToolTip="Enter the course ID"></asp:TextBox>
                </td>
                <td style="width: 40%; text-align: left;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="labelErrMsg" runat="server"
                        ControlToValidate="txtCourseID" Display="Dynamic" ErrorMessage="Please Enter The Course ID."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="3">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 30%; text-align: right;">
                    Course Name:
                </td>
                <td class="label" style="width: 30%; text-align: left;">
                    <asp:TextBox ID="txtCourseName" runat="server" CssClass="textboxNew" TabIndex="2"
                        ToolTip="enter the course name"></asp:TextBox>
                </td>
                <td style="width: 40%; text-align: left;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="labelErrMsg" runat="server"
                        ControlToValidate="txtCourseName" Display="Dynamic" ErrorMessage="Please Enter The Course Name."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="3">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 30%; text-align: right;">
                    Short Description:
                </td>
                <td class="label" style="width: 30%; text-align: left;">
                    <asp:TextBox ID="txtDesc" runat="server" CssClass="textboxNew" TabIndex="3" ToolTip="enter the short description of the course"></asp:TextBox>
                </td>
                <td style="width: 40%; text-align: left;">
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="3">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 30%; text-align: right;">
                    Internal/External:
                </td>
                <td class="label" style="width: 30%; text-align: left;">
                    <asp:DropDownList ID="drpIntExt" runat="server" CssClass="DropDownListNew" TabIndex="4">
                        <asp:ListItem Value="Select One">Select</asp:ListItem>
                        <asp:ListItem Value="I">Internal</asp:ListItem>
                        <asp:ListItem Value="E">External</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 40%; text-align: left;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="labelErrMsg" runat="server"
                        ControlToValidate="drpIntExt" Display="Dynamic" ErrorMessage="Please Select Internal/External."
                        InitialValue="Select"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="3">
                </td>
            </tr>
            <tr class="oddrow">
                <td class="label" style="width: 30%; text-align: right;">
                    Course Type:
                </td>
                <td class="label" style="width: 30%; text-align: left;">
                    <asp:DropDownList ID="drpCtype" runat="server" CssClass="DropDownListNew" TabIndex="5">
                    </asp:DropDownList>
                </td>
                <td style="width: 40%; text-align: left;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="labelErrMsg" runat="server"
                        ControlToValidate="drpCtype" Display="Dynamic" ErrorMessage="Please Select Cource Type."
                        InitialValue="Select a Type"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;">
                <td colspan="3">
                </td>
            </tr>
            <tr class="oddrow">
                <td>
                </td>
                <td style="width: 40%; text-align: center;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 70%;">
                                <asp:Button ID="btnCreate" runat="server" TabIndex="7" Text="Submit" CssClass="button"
                                    ToolTip=" click here to submit the details" OnClick="btnCreate_Click" OnClientClick="javascript:return confirmmsg('Are you sure to Create?');" />
                                &nbsp; 
                                <asp:Button ID="BtnCancel" runat="server" TabIndex="8" Text="Reset" CssClass="button"
                                    ToolTip=" click here to Cancel the details" 
                                    CausesValidation="False" OnClick="BtnCancel_Click" />
                            </td>
                            <td style="width: 30%;">
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr class="evenrow" style="width: 100%;text-align:left">
                <td colspan="3">
                    <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
