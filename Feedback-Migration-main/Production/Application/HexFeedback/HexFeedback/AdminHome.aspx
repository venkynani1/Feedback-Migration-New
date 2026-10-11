<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="HexFeedback.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="Styles/newstyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div id="maincontainer">
 
<div class ="inner-content">
<div style="clear:both;">&nbsp;</div>

<div style="clear:both;">&nbsp;</div>
<div class ="de-panel">
   
 

<div style="clear:both;">&nbsp;</div>

<div class="master-inner-label" style=" color: #125390; font-size:13px; font-weight: bold;margin-left:30px;">ADMIN HOME</div>
<div style="clear:both;height:50px;">&nbsp;</div>
<div class="cpanel-list"><a href="MasterMail.aspx"><img  src="Images/arrow.png" alt="" />Mail Master</a></div>
<div class="cpanel-list"><a href="MasterVendor.aspx"><img  src="Images/arrow.png" alt="" />Vendor Master</a></div>
<div class="cpanel-list"><a href="MasterTrainer.aspx"><img  src="Images/arrow.png" alt="" />Trainer Master</a></div>



<div style="clear:both;">&nbsp;</div>
<div class="cpanel-list"><a href="Course.aspx"><img  src="Images/arrow.png" alt="" />Course Creation</a></div>
<div class="cpanel-list"><a href="Session.aspx"><img  src="Images/arrow.png" alt="" />Session Creation</a></div>
<div class="cpanel-list"><a href="NominationList.aspx"><img  src="Images/arrow.png" alt="" />Prepare Nomination List</a></div>


<div style="clear:both;">&nbsp;</div>


<div class="cpanel-list"><a href="BalFeedback.aspx"><img  src="Images/arrow.png" alt="" />View Comments</a></div>

<div class="cpanel-list"><a href="FeedbackDef.aspx"><img  src="Images/arrow.png" alt="" />Feedback Defaulters</a></div>
<div class="cpanel-list"><a href="TrainingList.aspx?IsAdmin=1" ><img  src="Images/arrow.png" alt="" /> Record My Feedback</a></div>
<div style="clear:both;">&nbsp;</div>
<div class="cpanel-list"><a href="IdividualRep.aspx"><img  src="Images/arrow.png" alt="" />Overall Reports</a></div>
<div class="cpanel-list"><a href="IndividAnalysis.aspx"><img  src="Images/arrow.png" alt="" />Individual Reports</a></div>

<div style="clear:both;">&nbsp;</div>
<asp:Panel ID="pnlFrSupAdmin" runat="server">
<div class="cpanel-list"><a href="CreateAdmin.aspx">
    <img  src="Images/arrow.png" alt=""/>Create/Remove Admin</a></div>
  </asp:Panel>
<div style="clear:both;">&nbsp;</div>

 
  

</div>

<div style="clear:both;">&nbsp;</div>


</div>
</div>

</asp:Content>
