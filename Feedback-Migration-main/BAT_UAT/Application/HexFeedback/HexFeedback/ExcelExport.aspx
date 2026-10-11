<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelExport.aspx.cs" Inherits="HexFeedback.ExcelExport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 155px;
        }
        #tdSession
        {
            font-weight: 700;
        }
        #tdDate
        {
            font-weight: 700;
        }
        #tdTrainer
        {
            font-weight: 700;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    &nbsp;<table id="tblDetails" runat="server" style="width:100%;">
            <tr>
                <td style="font-weight: 700" class="style1" >
                    Course Name</td>
                <td id="tdSession" runat="server" style="font-weight: 700" >
                    :</td>
            </tr>
            <tr>
                <td style="font-weight: 700" class="style1" >
                    Duration</td>
                <td runat="server" id="tdDate" style="font-weight: 700" >
                    :</td>

            </tr>
            <tr>
                <td style="font-weight: 700" class="style1" >
                    <b>Trainer</b></td>
                <td id="tdTrainer" runat="server" style="font-weight: 700" >
                    :</td>
            </tr>
        </table>
    <asp:GridView ID="gvFeedback" runat="server"  OnRowDataBound="gvFeedback_RowDataBound">
        </asp:GridView>  
    </div>
    </form>
</body>
</html>
