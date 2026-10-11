<%@ Page Title="View and Validate Comments" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="BalFeedback.aspx.cs" Inherits="HexFeedback.BalFeedback" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/NewStyles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">


        function MM_openBrWindow1(path_url) {
            document.open(path_url, "Checklist", "toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,height=300,width=300,left=250,top=250");
        }

        function MM_openBrWindow2(path_url) {
            document.open(path_url, "Checklist", "toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,height=400,width=400,left=250,top=250");
        }
        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=200,width=150');
            if (window.focus) { newwindow.focus() }
            return false;
        }


    </script>
    <link rel="stylesheet" href="Styles/NewStyles.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <fieldset class="ForFieldset">
            <legend class="ForLegend">View and Validate Comments</legend>
            <asp:HiddenField ID="hdnPage" runat="server" Value="BalFeedback" />
            <table style="width: 100%; text-align: center;">
                <tr class="oddrow">
                    <td class="label" style="width: 40%; text-align: right;">
                        Select Course:
                    </td>
                    <td style="width: 60%; text-align: left;">
                        <asp:DropDownList ID="drpCourse" CssClass="DropDownListNew" runat="server" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged"
                            AutoPostBack="True" />
                    </td>
                </tr>
                <tr class="evenrow" style="width: 100%;">
                    <td colspan="2">
                    </td>
                </tr>
                <tr class="oddrow">
                    <td class="label" style="width: 40%; text-align: right;">
                        Select Session:
                    </td>
                    <td style="width: 60%; text-align: left;">
                        <asp:DropDownList ID="drpSession" CssClass="DropDownListNew" runat="server" OnSelectedIndexChanged="drpSession_SelectedIndexChanged"
                            AutoPostBack="True" />
                    </td>
                </tr>
                <tr class="evenrow" style="width: 100%;">
                    <td colspan="2">
                    </td>
                </tr>
                <tr class="oddrow" style="width: 100%;">
                    <td colspan="2">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 100%;">
                                    <asp:GridView ID="GrvBalance" runat="server" Width="100%" AutoGenerateColumns="False"
                                        OnRowDeleting="GrvBalance_RowDeleting" GridLines="None" CssClass="hdrow" RowStyle-CssClass="oddrow"
                                        AlternatingRowStyle-CssClass="evenrow">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Session">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblSess" runat="server" Text='<%# Bind("sessionid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nominated <br/> Employees">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblTot" runat="server" Text='<%# Bind("EMPLOYEES") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Session <br/> Trainers">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblBal" runat="server" Text='<%# Bind("Trainers") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total <br/> Feedbacks">
                                                <ItemTemplate>
                                                    <asp:Label ID="LbltotFeedbacks" runat="server" Text='<%# Bind("Tot_Feedbacks") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Completed <br/> Feedbacks">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCompFeed" runat="server" Text='<%# Bind("Comp_Feedback") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="lnkdelete" CssClass="button" CausesValidation="false" Width="130px"
                                                        runat="server" CommandName="Delete" Text="View Comments" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="evenrow" style="width: 100%; text-align: left">
                    <td colspan="2">
                        <asp:Label ID="lblerrMsg" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
            PopupControlID="pnlpopup" CancelControlID="ImageButton1" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="500px" Width="600px"
            Style="display: none" ScrollBars="Both">
            <table style="width: 100%; text-align: center;">
                <tr style="width: 100%; text-align: right;">
                    <td style="width: 90%; text-align: center;" class="labelHeader">
                    Comments
                    </td>
                    <td style="padding-right: 20px;width: 10%;">
                        <asp:ImageButton ID="ImageButton1" ToolTip="Close" ImageUrl="~/Images/imagesCA7JXO4K.jpg"
                            runat="server" />
                    </td>
                </tr>
            </table>
          
            <table style="width: 100%; text-align: center;">
                <tr class="oddrow" style="width: 100%;">
                    <td>
                        <asp:GridView ID="GrdComments" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="None" OnRowCommand="GrdComments_RowCommand" CssClass="hdrow" RowStyle-CssClass="oddrow"
                            AlternatingRowStyle-CssClass="evenrow">
                            <Columns>
                                <asp:TemplateField HeaderText="Emp Id">
                                    <ItemTemplate>
                                        <asp:Label ID="LblEmpId" runat="server" Width="50px" Text='<%# Bind("Employee_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <asp:Label ID="lblComments" Width="325px" runat="server" Text='<%# Bind("Comments") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Display">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkDisplay" Width="5px" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="btnSave" CssClass="button" runat="server" CommandName="Proceed" Text="Approve" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr class="oddrow" style="width: 100%; text-align: left">
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="labelErrMsg" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
          
        </asp:Panel>
    </div>
</asp:Content>
