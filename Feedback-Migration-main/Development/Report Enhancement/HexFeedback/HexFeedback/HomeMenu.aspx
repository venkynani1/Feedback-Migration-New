<%@ Page Language="C#" Title="User Home" AutoEventWireup="true" CodeBehind="HomeMenu.aspx.cs" Inherits="HexFeedback.HomeMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Menu ID="menu" DataSourceID="xmlDataSource" runat="server" BackColor="#FFFBD6"
                        DynamicHorizontalOffset="2" Font-Names="Verdana" ForeColor="#990000" StaticSubMenuIndent="10px"
                        StaticDisplayLevels="1" Orientation="Horizontal">
                        <DataBindings>
                            <asp:MenuItemBinding DataMember="MenuItem" NavigateUrlField="NavigateUrl" TextField="Text"
                                ToolTipField="ToolTip" />
                        </DataBindings>
                        <StaticSelectedStyle BackColor="#FFCC66" />
                        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <DynamicMenuStyle BackColor="#FFFBD6" />
                        <DynamicSelectedStyle BackColor="#FFCC66" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <DynamicHoverStyle BackColor="#990000" Font-Bold="False" ForeColor="White" />
                        <StaticHoverStyle BackColor="#990000" Font-Bold="False" ForeColor="White" />
                    </asp:Menu>
                    <asp:XmlDataSource ID="xmlDataSource" TransformFile="~/TransformXSLT.xsl" XPath="MenuItems/MenuItem"
                        runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
