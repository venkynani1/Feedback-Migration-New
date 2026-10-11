<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test2.aspx.cs" Inherits="HexFeedback.test2"  ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CollapsiblePanelExtender Tips </title>
    <script type="text/javascript" src="tiny_mce/tiny_mce.js"></script>  
   <script type="text/javascript">
       tinyMCE.init({
           // General options  
           mode: "textareas",
           theme: "advanced",
           plugins: "safari,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template",

           // Theme options  
           theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
           theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
           theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
           theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,pagebreak",
           theme_advanced_toolbar_location: "top",
           theme_advanced_toolbar_align: "left",
           theme_advanced_statusbar_location: "bottom",
           theme_advanced_resizing: true,

           // Example content CSS (should be your site CSS)  
           content_css: "css/content.css",

           // Drop lists for link/image/media/template dialogs  
           template_external_list_url: "lists/template_list.js",
           external_link_list_url: "lists/link_list.js",
           external_image_list_url: "lists/image_list.js",
           media_external_list_url: "lists/media_list.js",

           // Replace values for the template plugin  
           template_replace_values: {
               username: "Some User",
               staffid: "991234"
           }
       });  
  </script>  
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
    <style type="text/css">
        body
        {
            font-family: Tahoma;
        }
        div.AlertStyle
        {
            background-color: #FFC080;
            top: 95%;
            left: 1%;
            height: 20px;
            width: 270px;
            position: absolute;
            visibility: hidden;
        }
    </style>
       <script type="text/javascript">
           function textboxMultilineMaxNumber(txt, maxLen) {
               try {
                   if (txt.value.length > (maxLen - 1)) {
                       return false;
                   }
                   else {
                       var temp = maxLen - 1;
                       document.getElementById('<%= Label1.ClientID %>').innerHTML = +temp - txt.value.length + '&nbsp;Character remaining.';
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
                          document.getElementById('<%= Label1.ClientID %>').innerHTML = +temp - txt.value.length + '&nbsp;Character remaining.';
                      }
                  }
              } catch (e)
         { }

          } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <asp:GridView runat="server" ID="gvdetails" DataKeyNames="UserId" AutoGenerateColumns="false">
            <RowStyle BackColor="#EFF3FB" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtn" ImageUrl="~/Edit.jpg" runat="server" Width="25" Height="25"
                            OnClick="imgbtn_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="UserName" />
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" />
                <asp:BoundField DataField="LastName" HeaderText="LastName" />
                <asp:BoundField DataField="City" HeaderText="City" />
                <asp:BoundField DataField="Designation" HeaderText="Designation" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblresult" runat="server" />
        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
            PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="400px"
            Style="display: none">
            <table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%"
                cellpadding="0" cellspacing="0">
                <tr style="background-color: #D55500">
                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                        align="center">
                        User Details
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 45%">
                        UserId:
                    </td>
                    <td>
                        <asp:Label ID="lblID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        UserName:
                    </td>
                    <td>
                        <asp:Label ID="lblusername" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        FirstName:
                    </td>
                    <td>
                        <asp:TextBox ID="txtfname" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        LastName:
                    </td>
                    <td>
                        <asp:TextBox ID="txtlname" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        City:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCity" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Designation:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesg" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
        <div>
         <textarea id="elm1" name="elm1" rows="15" cols="80" style="width: 80%" runat="server" onkeypress="return textboxMultilineMaxNumber(document.myForm.message2,10)">  
    </textarea>  
        </div>
    <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>
