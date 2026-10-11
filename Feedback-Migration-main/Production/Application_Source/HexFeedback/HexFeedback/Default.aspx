<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HexFeedback._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<link href="css/style1.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table align="center" cellpadding="0" cellspacing="3" width="98%">
        <tr align="center">
            <td colspan="2">
                <asp:Label ID="Label1" runat="server" CssClass="cap">COURSE INFORMATION</asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                yes<br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td width="30%">
                <img align="left" src="Images/course_info.gif" /></td>
            <td width="70%">
                <table id="Table1" align="center" bgcolor="#f1f1f1" border="0" 
                    bordercolor="#999999" cellpadding="0" cellspacing="3" width="98%">
                    <tr>
                        <td width="30%">
                            Course ID</td>
                        <td width="70%">
                            <asp:TextBox ID="txtCourseID" Runat="server" CssClass="Textbox" tabIndex="1" 
                                ToolTip="enter the course id" Width="108px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtCourseID" Display="Dynamic" 
                                ErrorMessage="Please Enter The Course Id" ForeColor="RoyalBlue"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            Course Name
                        </td>
                        <td width="70%">
                            <asp:TextBox ID="txtCourseName" Runat="server" CssClass="Textbox" tabIndex="2" 
                                ToolTip="enter the course name" Width="108px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtCourseName" Display="Dynamic" 
                                ErrorMessage="Please Enter The Course Name" ForeColor="RoyalBlue"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="30%">
                            Short Description
                        </td>
                        <td width="70%">
                            <asp:TextBox ID="txtDesc" Runat="server" CssClass="Textbox" tabIndex="3" 
                                ToolTip="enter the short description of the course" Width="108px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="30%">
                            Internal/External
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="drpIntExt" runat="server" CssClass="Textbox" 
                                Height="24px" tabIndex="4" Width="108px">
                                <asp:ListItem Value="Select One">Select One</asp:ListItem>
                                <asp:ListItem Value="I">Internal</asp:ListItem>
                                <asp:ListItem Value="E">External</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="drpIntExt" Display="Dynamic" 
                                ErrorMessage="Please Select Int or Ext" ForeColor="RoyalBlue" 
                                InitialValue="Select One"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="30%">
                            Course Type
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="drpCtype" runat="server" CssClass="Textbox" Height="24px" 
                                tabIndex="5" Width="108px">
                                <asp:ListItem Value="Select a Type">Select a Type</asp:ListItem>
                                <asp:ListItem Value="T">Technical</asp:ListItem>
                                <asp:ListItem Value="N">Non-Technical</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="drpCtype" Display="Dynamic" 
                                ErrorMessage="Please Select a Type" ForeColor="RoyalBlue" 
                                InitialValue="Select a Type" Width="147px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="30%">
                            Maximum No. of Participants
                        </td>
                        <td width="70%">
                            <asp:TextBox ID="txtMax" Runat="server" CssClass="Textbox" tabIndex="6" 
                                ToolTip="enter number of participants for the course" Width="108px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ControlToValidate="txtMax" Display="Dynamic" 
                                ErrorMessage="Please Enter the Value" ForeColor="RoyalBlue" Width="147px"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                ControlToValidate="txtMax" ErrorMessage="Enter value between 1 to 500" 
                                MaximumValue="500" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
            </td>
        </tr>
        <tr align="center">
            <td align="center" colspan="2">
                <asp:Button ID="butSignIn" Runat="server" CssClass="forButton" Height="25px" 
                     tabIndex="7" Text="Submit" 
                    ToolTip=" click here to submit the details" Width="80px" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                Copyright © 2005 Hexaware Technologies</td>
            <td align="center">
            </td>
        </tr>
    </table>
</asp:Content>
