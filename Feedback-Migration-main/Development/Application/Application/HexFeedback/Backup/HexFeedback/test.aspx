<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="test.aspx.cs"
    Inherits="HexFeedback.test" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    

 <link href="Styles/NewStyles.css" rel="stylesheet" type="text/css" />
 
</head>
<body style="height: 366px">
    <form id="form1" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    <asp:scriptmanager id="ScriptManager1" runat="server">
</asp:scriptmanager>

<table style="width:100%">
<tr>
<td>
 <asp:Timer ID="Timer1" runat="server" Interval="5000" Enabled="false" OnTick="timer1_Ontick"> 
        </asp:Timer> 


   <asp:Panel ID="Panel1" runat="server"> 
   <asp:Button ID="Button2" runat="server"    Text="CreateModal" 
           onclick="Button2_Click1"/>  
   </asp:Panel>
   <asp:Panel ID="ModalPanel" runat="server" HorizontalAlign="Center" Style="display: none" BackColor="White"> 
     <asp:Label ID="Throbber" runat="server">
                        <img src="Images/loading_wh.gif"  align="middle" alt="loading" />
                    </asp:Label>
         
          </asp:Panel> 
           


</td>
</tr>
</table>
   
    <table style="width: 100%; font-family: Verdana; font-size: 12px">
        <tr>
        <td>
        <img src="Images/arrow1.gif" width="318px" height="103"/>
        </td>
            <td style="text-align: center; font-weight: bold; background-color: #1a3c6a; color: #ffffff;">
                Training Feedback Analysis
            </td>
        </tr>
        <tr>
            <td style="height: 14px">
            </td>
        </tr>
        <tr style="text-align: center; font-weight: bold; background-color: #1a3c6a; color: #ffffff;">
            <td style="border-color: #000000; border-width: 1px">
                Level 1 Reactionaries Feedback Analysis
            </td>
        </tr>
        <tr>
            <td style="height: 14px">
            </td>
        </tr>
        <tr>
            <td>
                <table border="0.5em" style="border-color: #000000; width: 100%; font-family: Verdana;">
                    <tr style="width: 100%;">
                        <td colspan="4" style="border-color: #000000; border-width: 1px; text-align: center;
                            font-weight: bold;">
                            Course Details
                        </td>
                    </tr>
                    <tr >
                        <td style="width: 25%;text-align:right">
                        Course Title:  
                        </td>
                        <td style="width: 25%;text-align:left">
                        <CourseTitle> 
                        </td>
                        <td style="width: 25%;text-align:right">
                        Course Date(s): 
                        </td>
                        <td style="width: 25%;text-align:left">
                        <CourseDate(s)>
                        </td>
                    </tr>
                    <tr >
                        <td style="width: 25%;text-align:right">
                        Trainer’s Name:   
                        </td>
                        <td style="width: 25%;text-align:left">
                        <TrainerName> 
                        </td>
                        <td style="width: 25%;text-align:right">
                        No. of Participants:
                        </td>
                        <td style="width: 25%;text-align:left">
                        <NOP>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
         <tr>
            <td style="height: 14px">
            </td>
        </tr>
        <tr>
            <td>
             <table style="border-color: #000000; width: 100%; font-family: Verdana;">
                   
                    <tr style="font-weight: bold;">
                        <td style="width: 25%;text-align:right">
                         
                        </td>
                        <td style="width: 25%;text-align:center">
                        Maximum Score 
                        </td>
                        <td style="width: 25%;text-align:center">
                       Score
                        </td>
                        <td style="width: 25%;text-align:center">
                        Percentage
                        </td>
                    </tr>
                    <tr >
                        <td style="width: 25%;text-align:right">
                       Training Administration:   
                        </td>
                        <td style="width: 25%;text-align:center">
                        4.0
                        </td>
                        <td style="width: 25%;text-align:center">
                       <S1>
                        </td>
                        <td style="width: 25%;text-align:center">
                        <P1>
                        </td>
                    </tr>
                    <tr >
                        <td style="width: 25%;text-align:right">
                       Training Design & Methodology:   
                        </td>
                        <td style="width: 25%;text-align:center">
                        4.0
                        </td>
                        <td style="width: 25%;text-align:center">
                       <S1>
                        </td>
                        <td style="width: 25%;text-align:center">
                        <P1>
                        </td>
                    </tr>
                    <tr >
                        <td style="width: 25%;text-align:right">
                       Trainer Delivery:   
                        </td>
                        <td style="width: 25%;text-align:center">
                        4.0
                        </td>
                        <td style="width: 25%;text-align:center">
                       <S1>
                        </td>
                        <td style="width: 25%;text-align:center">
                        <P1>
                        </td>
                    </tr>
                </table>
           </td>
        </tr>
         <tr>
            <td style="height: 14px">
            </td>
        </tr>
         <tr>
            <td>
             <table style="border-color: #000000; width: 100%; font-family: Verdana;">
                   
                    <tr >
                        <td style="width: 25%;text-align:right">
                         Overall Rating: 
                        </td>
                        <td style="width: 25%;text-align:left">
                       <S4>
                        </td>
                        <td style="width: 25%;text-align:right">
                      Percentage: 
                        </td>
                        <td style="width: 25%;text-align:left">
                        <p4>
                        </td>
                    </tr>
                    </table>

                    </td>
                    </tr>
                     <tr>
            <td style="height: 14px">
            </td>
        </tr>
        <tr style="text-align:left;height:60px;">
            <td style="vertical-align:top">
                Based on overall feedback received from participants, record areas of improvements if any:<br />
                <comments>
            </td>
        </tr>
        <tr>
            <td style="height: 14px">
            </td>
        </tr>
       <tr>
            <td>
             <table style="border-color: #000000; width: 100%; font-family: Verdana;">
                   
                    <tr>
                        <td style="width: 16%;text-align:right">
                         Prepared by: 
                        </td>
                        <td style="width: 16%;text-align:left">
                       ____________________
                        </td>
                        <td style="width: 16%;text-align:right">
                     Signature:
                        </td>
                        <td style="width: 16%;text-align:left">
                      ____________________
                        </td>
                        <td style="width: 16%;text-align:right">
                      Date:
                        </td>
                        <td style="width: 16%;text-align:left">
                      ____________________
                        </td>
                    </tr>
                         <tr>
                        <td style="width: 16%;text-align:right">
                         Reviewed by:  
                        </td>
                        <td style="width: 16%;text-align:left">
                       ____________________
                        </td>
                        <td style="width: 16%;text-align:right">
                     Signature:
                        </td>
                        <td style="width: 16%;text-align:left">
                      ____________________
                        </td>
                        <td style="width: 16%;text-align:right">
                      Date:
                        </td>
                        <td style="width: 16%;text-align:left">
                      ____________________
                        </td>
                    </tr>
                    </table>
                    </td>
                    </tr>
    </table>

    <div style="font-family: Calibri; font-size: small; color: #000000">
        <table style="width: 100%; font-family: Calibri; font-size: 15px;" bgcolor="White">
            <tr>
                <td bgcolor="White" height="25" rowspan="1" style="color: #0033CC; background-color: #FFFFFF;">
                    Dear
                    <traineename>
                    ,
                </td>
            </tr>
            <tr>
                <td bgcolor="White" height="25" style="color: #0033CC; background-color: #FFFFFF;">
                    Request you to give feedback for the training "<strong style="color: Red"><coursename></strong>"
                    held from
                    <startdate>&nbsp; to <EndDate>.
                </td>
            </tr>
            <tr>
                <td bgcolor="White" height="25" style="color: #0033CC; background-color: #FFFFFF;">
                    Please use the following link to give feedbacks for the
                    <Singlerorplural> <strong style="color:Red"> <trainername> </strong>
                    .
                </td>
            </tr>
            <tr>
                <td bgcolor="White" height="25" style="color: #0033CC; background-color: #FFFFFF;">
                   Intranet :<a href="http://StationH/TrainingFeedback/Account/Login.aspx">http://StationH/TrainingFeedback/Account/Login.aspx</a>
                </td>
            </tr>
            <tr>
                <td bgcolor="White" height="25" style="color: #0033CC; background-color: #FFFFFF;">
                   Internet :<a href="https://stationh.hexaware.com/portal/TrainingFeedback/Account/Login.aspx">https://stationh.hexaware.com/portal/TrainingFeedback/Account/Login.aspx</a>
                </td>
            </tr>
            <tr>
                <td bgcolor="White" height="25" style="color: #0033CC; background-color: #FFFFFF;">
                    The <strong style="color: Red">User ID </strong>and <strong style="color: Red">Password
                    </strong>are as same as your Corp credentials.
                </td>
            </tr>
            <tr>
                <td bgcolor="White" height="25" style="color: #0033CC; background-color: #FFFFFF;">
                   Kindly Mail me if you have any concern.
                </td>
            </tr>
            <tr>
                <td bgcolor="White" style="color: #0033CC; background-color: #FFFFFF;" class="style1">
                </td>
            </tr>
            <tr>
                <td bgcolor="White" height="25" style="color:#0033CC;background-color: #FFFFFF;font-family:Lucida Handwriting" >
                   <strong style="color:Blue">Happy Learning !</strong> 
                </td>
            </tr>
           <tr>
                <td bgcolor="White" height="20" style="color: #0033CC; background-color: #FFFFFF;">
                     <strong>Thanks and regards,</strong>
                </td>
            </tr>
            <tr>
                <td bgcolor="White" height="20" style="color: #0033CC; background-color: #FFFFFF;">
                    <strong> <signature> </strong>
                </td>
            </tr>
            <tr>
                <td bgcolor="White" height="20" style="color: #0033CC; background-color: #FFFFFF;">
                   <strong> Hexavarsity</strong>
                </td>
            </tr>
            <tr>
                <td bgcolor="White" height="20" style="color: #0033CC; background-color: #FFFFFF;">
                    <a href="https://chn.mail.hexaware.com/owa/">
                        <replymailid>
                    </a>
                </td>
            </tr>
            <tr>
                <td bgcolor="White" height="20" style="color: #FF0909; background-color: #FFFFFF;">
                    Note:- This is auto generated mail from HEXAVARSITY.
                </td>
            </tr>
        </table>
    </div>
   

    </form>
</body>
</html>
