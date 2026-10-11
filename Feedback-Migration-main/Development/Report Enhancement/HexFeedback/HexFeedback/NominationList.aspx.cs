#region Header
/// ***********************************************************************************************
/// File name: NominationList.aspx.cs
/// Description : 
/// Created date :18 Apr 2012
/// Created by :  Sureshkumar D, Hexaware Technologies Ltd.
///
///
/// Change History
/// -----------------------------------------------------------------------------------------------
/// Date            Author              Reviewed date      Reviewed by      Notes
/// -----------------------------------------------------------------------------------------------
/// 
/// *************************************************************************************************
#endregion


#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using Hex.Vars.FB.HexFBBLL;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;

#endregion

#region Class
namespace HexFeedback
{
    public partial class NominationList : System.Web.UI.Page
    {
        string trainername = string.Empty;
        string TraineeName = string.Empty;
        string ToMailID = string.Empty;
        string ReplyMailID = string.Empty;
        string FromMailID = string.Empty;
        string AdminSignature = string.Empty;
        static string CourseName = string.Empty;
        string ChgBody4;
        string ChgBody7;
        string ChgBody8;
        string ChgBody9;
        int SendMailReturnValue;
        static string CourseValue = string.Empty;
        static string SessionValue = string.Empty;
        string CcMailID = string.Empty;
        string BccMailID = string.Empty;
        DataSet dsMailRelatedDetails = new DataSet();
        NominationListBLL GetMailRelatedDetails = new NominationListBLL();
        Hex.Vars.FB.HexFBModule.NominationListInfo nominationlist = new Hex.Vars.FB.HexFBModule.NominationListInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    Pnlmail.Visible = false;
                    butAdd.Visible = false;
                    pnlforrpt.Visible = false;
                    Hex.Vars.FB.HexFBBLL.CourseBLL coursebll = new CourseBLL();
                    SqlDataReader dr = coursebll.getAllCourse();
                    drpCourse.DataSource = dr;
                    drpCourse.DataValueField = "courseid";
                    drpCourse.DataTextField = "course";
                    drpCourse.DataBind();
                    drpCourse.Items.Insert(0, new ListItem("--Select--", String.Empty));
                    drpCourse.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    lblerrMsg.Text = "Error:- " + ex.Message;
                }

            }
        }

        protected void butImport_Click(object sender, EventArgs e)
        {
            try
            {
                pnlforrpt.Visible = true;
                if (!AsyncFileUpload1.HasFile)
                {
                    NominateMembers.Visible = false;
                    pnlforrpt.Visible = false;
                    grvNominationList.Visible = false;
                    return;
                }
                string fileName = System.IO.Path.Combine(Server.MapPath(ResolveUrl("~/Images/")), AsyncFileUpload1.FileName);
                string extension = System.IO.Path.GetExtension(fileName);
                if (extension == ".xls")
                {
                    AsyncFileUpload1.SaveAs(fileName);
                    string errMsg = string.Empty;
                    Hex.Vars.FB.HexFBBLL.NominationListBLL nominationbll = new NominationListBLL();

                    OleDbDataReader dr = nominationbll.importExcelFile(fileName, out errMsg);
                    if (dr != null)
                    {
                        if (dr.HasRows == true)
                        {
                            grvNominationList.Visible = true;
                            grvNominationList.DataSource = dr;
                            grvNominationList.DataBind();
                            butAdd.Visible = true;
                            NominateMembers.Visible = true;
                            pnlforrpt.Visible = true;
                            grvNominationList.Visible = true;
                        }
                        else
                        {
                            grvNominationList.Visible = false;
                        }
                    }
                    Pnlmail.Visible = false;
                }
                else
                {
                    lblStatus.Text = "Your file was not uploaded because it does not have a .xls extension.";
                    NominateMembers.Visible = false;
                    pnlforrpt.Visible = false;
                    grvNominationList.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }
        protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Hex.Vars.FB.HexFBBLL.SessionBLL sessionbll = new SessionBLL();
                SqlDataReader dr1 = sessionbll.getSessionCourseWise(drpCourse.Text);
                drpSession.DataSource = dr1;
                drpSession.DataMember = "session";
                drpSession.DataValueField = "sessionid";
                drpSession.DataBind();

                drpSession.Items.Insert(0, new ListItem("--Select--", String.Empty));
                drpSession.SelectedIndex = 0;
                Pnlmail.Visible = false;
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        protected void drpSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Hex.Vars.FB.HexFBBLL.SessionBLL sessionbll = new SessionBLL();
                SqlDataReader dr = sessionbll.getSessionSessionIdWise(drpSession.SelectedValue);
                if (dr != null)
                {

                    if (dr.Read())
                    {
                        lblStartDate.Text = dr["startdate"].ToString();
                        lblEndDate.Text = dr["enddate"].ToString();
                        trainername = dr["Trainer Name"].ToString();
                    }
                    else
                    {
                        lblStartDate.Text = string.Empty;
                        lblEndDate.Text = string.Empty;
                    }
                }
                else
                {
                    lblStartDate.Text = string.Empty;
                    lblEndDate.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }


        protected void butAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (grvNominationList.Rows.Count == 0)
                    return;
                string empcount = "";
                string errmessage = string.Empty;
                bool IsRecordExist = false;
                int rowsAffected = 0;
                CourseValue = drpCourse.Text;
                CourseName = drpCourse.SelectedValue.ToString();
                SessionValue = drpSession.SelectedValue.ToString();
                NominationListBLL nominationbll = new NominationListBLL();
                //Get Replymail ID-Start
                nominationlist.EmployeeId = Session["username"].ToString();
                nominationlist.CourseId = CourseValue;
                nominationlist.SessionId = SessionValue;
                nominationlist.Flag = 1;
                DataSet dsSignatureDetails = new DataSet();
                dsSignatureDetails =GetMailRelatedDetails.MailRelatedDetails(nominationlist, out errmessage);
                AdminSignature = Convert.ToString(dsSignatureDetails.Tables[1].Rows[0]["AdminName"]).ToUpper();
                FromMailID= Convert.ToString(dsSignatureDetails.Tables[1].Rows[0]["ReplyMailID"]);
                //Get Replymail ID-End
                Hex.Vars.FB.HexFBBLL.SessionBLL sessionbll = new SessionBLL();
                string maxno = Convert.ToString(sessionbll.GetmaxnoofParticipants(CourseValue));
                empcount = maxno;
                if (Convert.ToInt32(empcount) >= grvNominationList.Rows.Count - 1)
                {
                    foreach (GridViewRow row in grvNominationList.Rows)
                    {
                        nominationlist.CourseId = CourseValue;
                        nominationlist.SessionId = SessionValue;
                        nominationlist.EmployeeId = row.Cells[0].Text;
                        nominationlist.CreatedBy = User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1);
                        if (nominationlist.EmployeeId != "&nbsp;")
                        {
                            nominationbll.insertNominationList(nominationlist, out errmessage, out IsRecordExist, out rowsAffected);
                            if (IsRecordExist)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Employee Already Nominated ');", true);
                            }
                            else
                            {
                                mailsend(nominationlist.EmployeeId, nominationlist.CourseId, nominationlist.SessionId);
                            }
                        }
                    }
                    DataTable dtMailStatus = nominationbll.SelectMailDeliverstat(nominationlist, out errmessage);
                    DataSet dsMailStatus = new DataSet();
                    dsMailStatus.Tables.Add(dtMailStatus);
                    GrvMailstatus.DataSource = dsMailStatus;
                    GrvMailstatus.DataBind();
                    drpCourse.SelectedIndex = 0;
                    drpSession.SelectedIndex = 0;
                    lblStartDate.Text = string.Empty;
                    lblEndDate.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('All nominated peoples are successfully added and mail has been send.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Nominate Employee strength should not greater than :" + maxno + "');", true);
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }
        public void mailsend(string EmployeeID, string CourseID, string SessionID)
        {
            Pnlmail.Visible = true;
            try
            {
                string errmessage = string.Empty;
                nominationlist.EmployeeId = EmployeeID;
                nominationlist.CourseId = CourseID;
                nominationlist.SessionId = SessionID;
                nominationlist.Flag = 0;
                //Start-Get All Mail Related Details

                dsMailRelatedDetails = GetMailRelatedDetails.MailRelatedDetails(nominationlist, out errmessage);
                //End-Get All Mail Related Details
                //Start -Mail Subject Replace
                StringBuilder MailSubject = new StringBuilder();
                MailSubject.AppendFormat(dsMailRelatedDetails.Tables[0].Rows[0]["Subject"].ToString());
                MailSubject.Replace("<Course Name>", "'" + Convert.ToString(dsMailRelatedDetails.Tables[1].Rows[0]["Course Name"]).ToUpper() + "'");
                //End -Mail Subject Replace
                //Start-Mail Body Replace
                StringBuilder MailBody = new StringBuilder();
                MailBody.AppendFormat(dsMailRelatedDetails.Tables[0].Rows[0]["Body"].ToString());
                ToMailID = Convert.ToString(dsMailRelatedDetails.Tables[1].Rows[0]["To Mail ID"]);
                CcMailID = Convert.ToString(dsMailRelatedDetails.Tables[0].Rows[0]["CccMail"]);
                BccMailID = Convert.ToString(dsMailRelatedDetails.Tables[0].Rows[0]["BccMail"]);
                MailBody.Replace("<traineename>", Convert.ToString(dsMailRelatedDetails.Tables[1].Rows[0]["Traine Name"]).ToUpper());
                MailBody.Replace("<coursename>", Convert.ToString(dsMailRelatedDetails.Tables[1].Rows[0]["Course Name"]).ToUpper());
                MailBody.Replace("<startdate>", Convert.ToString(dsMailRelatedDetails.Tables[1].Rows[0]["StartDate"]));
                MailBody.Replace("<EndDate>", Convert.ToString(dsMailRelatedDetails.Tables[1].Rows[0]["EndDate"]));
                if (dsMailRelatedDetails.Tables[1] != null)
                {
                    int Count = 0;
                    Count = dsMailRelatedDetails.Tables[1].Rows.Count;
                    trainername = string.Empty;
                    if (Count == 1)
                    {
                        trainername = dsMailRelatedDetails.Tables[1].Rows[0]["Trainer Name"].ToString().ToUpper();
                        MailBody.Replace("<Singlerorplural>", "trainer");
                    }
                    else
                    {
                        MailBody.Replace("<Singlerorplural>", "trainers");
                        for (int TrainerCount = 0; TrainerCount < dsMailRelatedDetails.Tables[1].Rows.Count; TrainerCount++)
                        {
                            if (Count == 1)
                                trainername = trainername + dsMailRelatedDetails.Tables[1].Rows[TrainerCount]["Trainer Name"].ToString().ToUpper();
                            else if (Count == 2)
                                trainername = trainername + dsMailRelatedDetails.Tables[1].Rows[TrainerCount]["Trainer Name"].ToString().ToUpper() + " and ";
                            else
                                trainername = trainername + dsMailRelatedDetails.Tables[1].Rows[TrainerCount]["Trainer Name"].ToString().ToUpper() + ",";
                            Count--;
                        }
                    }
                }


                // Added By Pavithra
                if (trainername != "ALL TRAINER(ALLTR)")
                {
                    string FinalName1 = Regex.Replace(trainername, @"[()]", String.Empty);
                    string FinalName2 = Regex.Replace(FinalName1, @"[0-9]", String.Empty);
                    MailBody.Replace("<trainername>", "'" + FinalName2 + "'");
                }
                else
                {
                    int n = trainername.IndexOf('(');
                    string FinalName2 = trainername.Substring(0, n);
                    MailBody.Replace("<trainername>", "'" + FinalName2 + "'");

                }
                // End
                //MailBody.Replace("<trainername>", "'" + FinalName2 + "'");
                //Start-Adding Signature
                MailBody.Replace("<signature>", AdminSignature.ToUpper());
                MailBody.Replace("<replymailid>",FromMailID );
                //End-Adding Signature

                //End-Mail Body Replace
                DataSet dsGetErrMsg = new DataSet();
                DataTable dtGetErrMsg = new DataTable();
                dtGetErrMsg = GetMailRelatedDetails.SendmailList(FromMailID, ToMailID, Convert.ToString(MailSubject), Convert.ToString(MailBody), CcMailID, BccMailID);
                dsGetErrMsg.Tables.Add(dtGetErrMsg);
                if (dsGetErrMsg.Tables[0].Rows.Count == 1)
                    SendMailReturnValue = Convert.ToInt32(dsGetErrMsg.Tables[0].Rows[0][0].ToString());
                else
                    SendMailReturnValue = 0;
                nominationlist.EmployeeId = EmployeeID;
                if (SendMailReturnValue == 0)
                {
                    nominationlist.Status = "Delivered";
                }
                else
                {
                    nominationlist.Status = "Failed";
                }
                nominationlist.CreatedDate = DateTime.Now.ToString();
                nominationlist.FromMailID = FromMailID;
                nominationlist.ToMailID = ToMailID;
                nominationlist.BccMailID = BccMailID;
                nominationlist.CcMailID = CcMailID;
                nominationlist.SubJect = Convert.ToString(MailSubject);
                nominationlist.Body = Convert.ToString(MailBody);

                GetMailRelatedDetails.insertMailstatu(nominationlist);
                pnlforrpt.Visible = false;
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        protected void AsyncFileUpload1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            System.Threading.Thread.Sleep(500);
        }

        protected void LnkDampleNominationLst_Click1(object sender, EventArgs e)
        {
            try
            {
                Response.ContentType = "application/vnd.ms-exce";
                Response.AppendHeader("Content-Disposition", "attachment; filename=SampleNominationList.xls");
                Response.TransmitFile(Server.MapPath("~/Images/SampleNominationList.xls"));
                Response.End();
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }
    }
}

#endregion