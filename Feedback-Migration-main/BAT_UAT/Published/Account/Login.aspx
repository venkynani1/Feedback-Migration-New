<%@ Page Title=" :: Log In :: " Language="C#" MasterPageFile="~/Account/Initial.Master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HexFeedback.Account.Login" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Images/NewStyles.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function isCapLockOn(e) {
            var charKeyCode = e.keyCode ? e.keyCode : e.which;
            var shiftKey = e.shiftKey ? e.shiftKey : ((charKeyCode == 16) ? true : false);
            // Check both the condition as described above
            if (((charKeyCode >= 65 && charKeyCode <= 90) && !shiftKey) || ((charKeyCode >= 97 && charKeyCode <= 122) && shiftKey)) {
                // Caps lock is on
                alert('Caps lock is on');
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="height: 430px">
                <table style="width: 100%; text-align: center; border-right: 0px dashed #FFFFFF;
                    padding: 10px 0 5px 0; background-image: url(Images/bottom_mid_bg.gif); background-position: bottom;
                    background-repeat: repeat-x; height: 100%">
                    <tr>
                       <%-- <td style="width: 40%; vertical-align: top;">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Account/Images/FB11.png" />
                        </td>--%>
                        <td style="width: 100%; vertical-align:middle;">
                            <table style="width: 350px;" align="center" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="right" valign="top" style="background-image: url(Images/quick_link_header_left.gif)"
                                        width="6px" height="31px">
                                    </td>
                                    <td width="100%" style="background-image: url(Images/quick_link_header_mid.gif)"
                                        align="center" class="quicklinkTXT">
                                        LOGIN
                                    </td>
                                    <td width="7px" height="31px" align="left" valign="top" style="background-image: url(Images/quick_link_header_right.gif)">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-image: url(Images/bit_lft_bg.png)" align="right">
                                        <img src="Images/spacer.gif" width="6" height="77" border="0" />
                                    </td>
                                    <td style="height: 200px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false"
                                                        OnAuthenticate="LoginUser_Authenticate">
                                                        <LayoutTemplate>
                                                            <table style="width: 100%; text-align: center;">
                                                                <tr>
                                                                    <td style="width: 100%;" colspan="3" class="labelErrMsg">
                                                                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" class="labelErrMsg">
                                                                        <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" ValidationGroup="LoginUserValidationGroup" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right">
                                                                        <asp:Label ID="UserNameLabel" CssClass="LoginLabel" runat="server" AssociatedControlID="UserName">User ID:</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="UserName" runat="server" CssClass="LoginTextBox"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                                            ErrorMessage="User Name is required." CssClass="labelErrMsg" ToolTip="User Name is required."
                                                                            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right">
                                                                        <asp:Label ID="PasswordLabel" CssClass="LoginLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Password" CssClass="LoginTextBox"
                                                                            runat="server" TextMode="Password" Font-Names="Verdana"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                                            ErrorMessage="Password is required." CssClass="labelErrMsg" ToolTip="Password is required."
                                                                            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td id="lblErrMsg" runat="server" style="width: 100%" class="labelErrMsg" colspan="4">
                                                                       <%--  <asp:Label ID="lblErrorMsg" runat="server" CssClass="labelErrMsg" Visible="false"></asp:Label>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align:center;padding:0px 0px 0px 75px;" colspan="3">
                                                                        <asp:ImageButton ID="ImageButton1" Width="100px" CommandName="Login" ValidationGroup="LoginUserValidationGroup"
                                                                            Height="30px" ImageUrl="~/Account/Images/Loginbutton_new.png" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </LayoutTemplate>
                                                    </asp:Login>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                      <asp:Label ID="lblErrorMsg" runat="server" CssClass="labelErrMsg"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="background-image: url(Images/bit_rht_bg.png)" align="right">
                                        <img src="images/spacer.gif" width="7px" height="40px" border="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" style="background-image: url(Images/bit_lft_bot.png)"
                                        width="6px" height="7px">
                                    </td>
                                    <td width="100%" style="background-image: url(Images/bit_botbg.png)" valign="bottom">
                                    </td>
                                    <td align="left" valign="top" style="background-image: url(Images/bit_rht_bot.png)"
                                        width="7px" height="7px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
