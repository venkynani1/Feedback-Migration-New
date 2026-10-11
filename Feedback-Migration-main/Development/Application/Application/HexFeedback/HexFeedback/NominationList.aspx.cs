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
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Web.UI.HtmlControls;
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
            if (rdType.SelectedValue == "0")
            {
                btnSupImport.Visible = false;
                butImport.Visible = true;
                AsyncFileUpload1.Visible = true;
                LnkDampleNominationLst.Visible = true;
                tdUPload.Visible = true;
            }
            else
            {
                btnSupImport.Visible = true;
                butImport.Visible = false;
                AsyncFileUpload1.Visible = false;
                LnkDampleNominationLst.Visible = false;
                tdUPload.Visible = false;
            }
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
                if (extension == ".xlsx")
                {
                    AsyncFileUpload1.SaveAs(fileName);
                    string errMsg = string.Empty;
                    Hex.Vars.FB.HexFBBLL.NominationListBLL nominationbll = new NominationListBLL();

                        using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fileName, false))
                        {
                            //Read the first Sheet from Excel file.
                            Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();

                            //Get the Worksheet instance.
                            Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;

                            //Fetch all the rows present in the Worksheet.
                            IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                            //Create a new DataTable.
                            DataTable dt = new DataTable();

                            //Loop through the Worksheet rows.
                            foreach (Row row in rows)
                            {
                                //Use the first row to add columns to DataTable.
                                if (row.RowIndex.Value == 1)
                                {
                                    foreach (Cell cell in row.Descendants<Cell>())
                                    {
                                        dt.Columns.Add(GetValue(doc, cell));
                                    }
                                }
                                else
                                {
                                    //Add rows to DataTable.
                                    dt.Rows.Add();
                                    int i = 0;
                                    foreach (Cell cell in row.Descendants<Cell>())
                                    {
                                        dt.Rows[dt.Rows.Count - 1][i] = GetValue(doc, cell);
                                        i++;
                                    }
                                }
                            }
                            if (dt.Rows.Count > 0 && (dt.Rows[0]).ItemArray[0].ToString() != "")
                            {
                                grvNominationList.Visible = true;
                                grvNominationList.DataSource = dt;
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



                //OleDbDataReader dr = nominationbll.importExcelFile(fileName, out errMsg);
                    //dr = null;
                    //lblStatus.Text = dr == null ?"FALSE":dr.HasRows.ToString();
                    //lblerrMsg.Text = errMsg;
                    //if (dr != null)
                    //{
                    //    if (dr.HasRows == true)
                    //    {
                    //        grvNominationList.Visible = true;
                    //        grvNominationList.DataSource = dr;
                    //        grvNominationList.DataBind();
                    //        butAdd.Visible = true;
                    //        NominateMembers.Visible = true;
                    //        pnlforrpt.Visible = true;
                    //        grvNominationList.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        grvNominationList.Visible = false;
                    //    }
                    //}
                    //Pnlmail.Visible = false;
                }
                else
                {
                    lblStatus.Text = "Your file was not uploaded because it does not have a .xlsx extension.";
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
                SqlDataReader dr1 = sessionbll.getSessionCourseWise(drpCourse.Text, Convert.ToInt32(Session["Year"]));
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
                int count = 0;
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
                        count++;
                        nominationlist.CourseId = CourseValue;
                        nominationlist.SessionId = SessionValue;
                        nominationlist.EmployeeId = row.Cells[0].Text;
                        nominationlist.CreatedBy = User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1);
                        nominationlist.Type = Convert.ToInt32(rdType.SelectedValue);
                        if (nominationlist.EmployeeId != "&nbsp;")
                        {
                            nominationbll.insertNominationList(nominationlist, out errmessage, out IsRecordExist, out rowsAffected);
                            if ((nominationlist.CourseId != "FTP_001" && rdType.SelectedValue == "0")||rdType.SelectedValue == "1")
                            {
                                if (IsRecordExist)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Employee Already Nominated ');", true);
                                }
                                else
                                {
                                   // Mail-1
                                   // mailsend(nominationlist.EmployeeId, nominationlist.CourseId, nominationlist.SessionId);
                                }
                                if (count == grvNominationList.Rows.Count && rdType.SelectedValue == "1")
                                {
                                    nominationbll.insertMasterSupervisorList(nominationlist.SessionId);
                                }
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
                StringBuilder MailSubject = new StringBuilder();
                StringBuilder MailBody = new StringBuilder();
                if (rdType.SelectedValue == "0")
                {
                    nominationlist.Flag = 0;
                    //Start-Get All Mail Related Details

                    dsMailRelatedDetails = GetMailRelatedDetails.MailRelatedDetails(nominationlist, out errmessage);
                    //End-Get All Mail Related Details
                    //Start -Mail Subject Replace
                    
                    MailSubject.AppendFormat(dsMailRelatedDetails.Tables[0].Rows[0]["Subject"].ToString());
                    MailSubject.Replace("<Course Name>", "'" + Convert.ToString(dsMailRelatedDetails.Tables[1].Rows[0]["Course Name"]).ToUpper() + "'");
                    //End -Mail Subject Replace
                    //Start-Mail Body Replace
                    MailBody.AppendFormat(dsMailRelatedDetails.Tables[0].Rows[0]["Body"].ToString());
                    ToMailID = Convert.ToString(dsMailRelatedDetails.Tables[1].Rows[0]["To Mail ID"]);
                    CcMailID = Convert.ToString(dsMailRelatedDetails.Tables[0].Rows[0]["CccMail"]);//"veerag@hexaware.com;SumanthP@hexaware.com;Chiragm@hexaware.com";
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
                    MailBody.Replace("<replymailid>", FromMailID);
                    //End-Adding Signature

                    //End-Mail Body Replace
                    
                }
                else
                {
                    nominationlist.Flag = 2;
                    //Start-Get All Mail Related Details

                    dsMailRelatedDetails = GetMailRelatedDetails.MailRelatedDetails(nominationlist, out errmessage);
                    //End-Get All Mail Related Details
                    //Start -Mail Subject Replace

                    MailSubject.AppendFormat(dsMailRelatedDetails.Tables[0].Rows[0]["Subject"].ToString());
                    //End -Mail Subject Replace
                    //Start-Mail Body Replace
                    MailBody.AppendFormat(dsMailRelatedDetails.Tables[0].Rows[0]["Body"].ToString());
                    ToMailID = Convert.ToString(dsMailRelatedDetails.Tables[1].Rows[0]["To Mail ID"]);
                    CcMailID = Convert.ToString(dsMailRelatedDetails.Tables[0].Rows[0]["CccMail"]); //"veerag@hexaware.com;Chiragm@hexaware.com;SumanthP@hexaware.com";
                    BccMailID = Convert.ToString(dsMailRelatedDetails.Tables[0].Rows[0]["BccMail"]);
                    MailBody.Replace("<Supervisor Name>", Convert.ToString(dsMailRelatedDetails.Tables[1].Rows[0]["Traine Name"]).ToUpper());
                    if (dsMailRelatedDetails.Tables[1] != null)
                    {
                        int Count = 0;
                        Count = dsMailRelatedDetails.Tables[1].Rows.Count;
                        trainername = string.Empty;
                        if (Count == 1)
                        {
                            MailBody.Replace("<Singlerorplural>", "Sub-ordinate");
                        }
                        else
                        {
                            MailBody.Replace("<Singlerorplural>", "Sub-ordinates");
                        }
                    }

                    //MailBody.Replace("<trainername>", "'" + FinalName2 + "'");
                    //Start-Adding Signature
                    MailBody.Replace("<signature>", AdminSignature.ToUpper());
                    MailBody.Replace("<replymailid>", FromMailID);
                    //End-Adding Signature

                    //End-Mail Body Replace
                    
                }
                DataSet dsGetErrMsg = new DataSet();
                DataTable dtGetErrMsg = new DataTable();
                //TEST -1
                dtGetErrMsg = GetMailRelatedDetails.SendmailList(FromMailID, ToMailID, Convert.ToString(MailSubject), Convert.ToString(MailBody), CcMailID, BccMailID);
              //  dtGetErrMsg = GetMailRelatedDetails.SendmailList(FromMailID, "vinothg@hexaware.com", Convert.ToString(MailSubject), Convert.ToString(MailBody), "", "");
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
                Response.AppendHeader("Content-Disposition", "attachment; filename=SampleNominationList.xlsx");
                Response.TransmitFile(Server.MapPath("~/Images/SampleNominationList.xlsx"));
                Response.End();
            }
            catch (System.Threading.ThreadAbortException exf)
            {
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        protected void rdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pnlmail.Visible = false;
            butAdd.Visible = false;
            pnlforrpt.Visible = false;
            lblEndDate.Text = "";
            lblStartDate.Text = "";
            drpSession.Items.Clear();
            drpCourse.Items.Clear();
            if (rdType.SelectedValue == "1")
            {
                btnSupImport.Visible = true;
                butImport.Visible = false;
                AsyncFileUpload1.Visible = false;
                LnkDampleNominationLst.Visible = false;
                tdUPload.Visible = false;
                drpCourse.Items.Insert(0, new ListItem("Fresher Training Program", "FTP_001"));
                drpCourse.SelectedIndex = 0;
                
                Hex.Vars.FB.HexFBBLL.SessionBLL sessionbll = new SessionBLL();
                SqlDataReader dr1 = sessionbll.getSessionCourseWise(drpCourse.Text, Convert.ToInt32(Session["Year"]));
                drpSession.DataSource = dr1;
                drpSession.DataMember = "session";
                drpSession.DataValueField = "sessionid";
                drpSession.DataBind();
                drpSession.Items.Insert(0, new ListItem("--Select--", String.Empty));
                drpSession.SelectedIndex = 0;
                Pnlmail.Visible = false;
            }
            else
            {
                btnSupImport.Visible = false;
                butImport.Visible = true;
                AsyncFileUpload1.Visible = true;
                LnkDampleNominationLst.Visible = true;
                tdUPload.Visible = true;
                Hex.Vars.FB.HexFBBLL.CourseBLL coursebll = new CourseBLL();
                SqlDataReader dr = coursebll.getAllCourse();
                drpCourse.DataSource = dr;
                drpCourse.DataValueField = "courseid";
                drpCourse.DataTextField = "course";
                drpCourse.DataBind();
                drpCourse.Items.Insert(0, new ListItem("--Select--", String.Empty));
                drpCourse.SelectedIndex = 0;
            }
        }

        protected void btnSupImport_Click(object sender, EventArgs e)
        {
            string errmsg = string.Empty;
            Hex.Vars.FB.HexFBBLL.NominationListBLL supervisorList = new NominationListBLL();
            DataSet ds=supervisorList.GetSupervisorsForMail(drpSession.SelectedValue.ToString(),out errmsg);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grvNominationList.DataSource = ds;
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
            Pnlmail.Visible = false;
        }

        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue != null? cell.CellValue.InnerText : "";
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }
    }
}

#endregion