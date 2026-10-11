using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace HexFeedback
{
    public partial class FeedbackSup : System.Web.UI.Page
    {
        Table feedback = new Table();
        TableRow row = new TableRow();
        TableCell[] cell = new TableCell[12];
        DropDownList ddlPrbSA,ddlPrgSkills,ddlCommSkills,ddlBehSkills,ddlPerformance,ddlOvrAllRating,ddlTrnReq;
        TextBox txtOvrallFeed, txtWrkArea;
        Label Sno, subOrdName, subOrdId;
        ListItem[] rating = new ListItem[7];
        ListItem[] trnReq = new ListItem[3];
        int j = 5;
        string FromMailID; 

        protected void Page_Init(object sender, EventArgs e)
        {
            Hex.Vars.FB.HexFBBLL.SessionBLL _session = new Hex.Vars.FB.HexFBBLL.SessionBLL();
            DataSet dsDetails = _session.getSupFeedbackDetails(User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1), Request.QueryString["s_id"],null);
            feedbackCount.Value = dsDetails.Tables[0].Rows.Count.ToString();
            Session["SupFeedBackCount"] = feedbackCount.Value;
            feedback.Attributes.Add("Style", "width: 100%; text-align: center;");
            feedback.ID = "T1";
            //Head Row Starts
            for (int i = 0; i < 12; i++)
                cell[i] = new TableCell();
            row = new TableRow();
            Label lblHead1 = new Label();
            Label lblHead2 = new Label();
            Label lblHead3 = new Label();
            Label lblHead4 = new Label();
            Label lblHead5 = new Label();
            Label lblHead6 = new Label();
            Label lblHead7 = new Label();
            Label lblHead8 = new Label();
            Label lblHead9 = new Label();
            Label lblHead10 = new Label();
            Label lblHead11 = new Label();
            Label lblHead12 = new Label();
            lblHead1.Text = "S. No.";
            lblHead2.Text = "Trainee's Employee Id";
            lblHead3.Text = "Trainee's Name";
            lblHead4.Text = "Mention Work Area (1st 90 days). Specify the area covered by the Trainee.";
            lblHead5.Text = "Problem Solving Ability";
            lblHead6.Text = "Programming skill / ability or Skill in Automation Tools";
            lblHead7.Text = "Communication / Writing Skill Ability";
            lblHead8.Text = "Behavioural Skill [Team Work, Assertiveness]";
            lblHead9.Text = "Performance";
            lblHead10.Text = "Overall Feedback Rating";
            lblHead11.Text = "Is retraining required?If yes, please specify on what skills, in the comments field";
            lblHead12.Text = "Overall Comments for the trainee based on the specified parameters";
            cell[0].Controls.Add(lblHead1);
            cell[1].Controls.Add(lblHead2);
            cell[2].Controls.Add(lblHead3);
            cell[3].Controls.Add(lblHead4);
            cell[4].Controls.Add(lblHead5);
            cell[5].Controls.Add(lblHead6);
            cell[6].Controls.Add(lblHead7);
            cell[7].Controls.Add(lblHead8);
            cell[8].Controls.Add(lblHead9);
            cell[9].Controls.Add(lblHead10);
            cell[10].Controls.Add(lblHead11);
            cell[11].Controls.Add(lblHead12);
            row.Cells.Add(cell[0]);
            row.Cells.Add(cell[1]);
            row.Cells.Add(cell[2]);
            row.Cells.Add(cell[3]);
            row.Cells.Add(cell[4]);
            row.Cells.Add(cell[5]);
            row.Cells.Add(cell[6]);
            row.Cells.Add(cell[7]);
            row.Cells.Add(cell[8]);
            row.Cells.Add(cell[9]);
            row.Cells.Add(cell[10]);
            row.Cells.Add(cell[11]);
            row.CssClass = "hdrow";
            feedback.Rows.Add(row);
            //Head Row ends
            //For rest rows starts
            rating[0] = new ListItem("Select", "0");
            rating[6] = new ListItem("NA", "NA");
            for (int i = 1; i < 6; i++)
            {
                rating[i] = new ListItem(j.ToString(), j.ToString());
                j--;
            }
            trnReq[0] = new ListItem("Select", "0");
            trnReq[1] = new ListItem("Yes", "1");
            trnReq[2] = new ListItem("No", "2");

            for (int i = 0; i < dsDetails.Tables[0].Rows.Count; i++)
            {
                for (int x = 0; x < 12; x++)
                    cell[x] = new TableCell();
                row = new TableRow();
                Sno = new Label();
                Sno.Text = (i + 1).ToString();
                subOrdName = new Label();
                subOrdName.Text = dsDetails.Tables[0].Rows[i][1].ToString();
                subOrdName.ID = "lblSubOrd" + (i + 1).ToString();
                subOrdId = new Label();
                subOrdId.Text = dsDetails.Tables[0].Rows[i][0].ToString();
                subOrdId.ID = "lblSubOrdId" + (i + 1).ToString();
                ddlPrbSA = new DropDownList();
                ddlPrbSA.ID = "ddlPrbSA" + (i + 1).ToString();
                ddlPrbSA.DataSource = rating;
                ddlPrbSA.DataValueField = "Value";
                ddlPrbSA.DataTextField = "Text";
                ddlPrbSA.DataBind();
                ddlPrbSA.CssClass = "DropDownListNew";
                ddlPrgSkills = new DropDownList();
                ddlPrgSkills.ID = "ddlPrgSkills" + (i + 1).ToString();
                ddlPrgSkills.DataSource = rating;
                ddlPrgSkills.DataValueField = "Value";
                ddlPrgSkills.DataTextField = "Text";
                ddlPrgSkills.DataBind();
                ddlPrgSkills.CssClass = "DropDownListNew";
                ddlCommSkills = new DropDownList();
                ddlCommSkills.ID = "ddlCommSkills" + (i + 1).ToString();
                ddlCommSkills.DataSource = rating;
                ddlCommSkills.DataValueField = "Value";
                ddlCommSkills.DataTextField = "Text";
                ddlCommSkills.DataBind();
                ddlCommSkills.CssClass = "DropDownListNew";
                ddlBehSkills = new DropDownList();
                ddlBehSkills.ID = "ddlBehSkills" + (i + 1).ToString();
                ddlBehSkills.DataSource = rating;
                ddlBehSkills.DataValueField = "Value";
                ddlBehSkills.DataTextField = "Text";
                ddlBehSkills.DataBind();
                ddlBehSkills.CssClass = "DropDownListNew";
                ddlPerformance = new DropDownList();
                ddlPerformance.ID = "ddlPerformance" + (i + 1).ToString();
                ddlPerformance.DataSource = rating;
                ddlPerformance.DataValueField = "Value";
                ddlPerformance.DataTextField = "Text";
                ddlPerformance.DataBind();
                ddlPerformance.CssClass = "DropDownListNew";
                ddlOvrAllRating = new DropDownList();
                ddlOvrAllRating.ID = "ddlOvrAllRating" + (i + 1).ToString();
                ddlOvrAllRating.DataSource = rating;
                ddlOvrAllRating.DataValueField = "Value";
                ddlOvrAllRating.DataTextField = "Text";
                ddlOvrAllRating.DataBind();
                ddlOvrAllRating.CssClass = "DropDownListNew";
                ddlTrnReq = new DropDownList();
                ddlTrnReq.ID = "ddlTrnReq" + (i + 1).ToString();
                ddlTrnReq.DataSource = trnReq;
                ddlTrnReq.DataValueField = "Value";
                ddlTrnReq.DataTextField = "Text";
                ddlTrnReq.DataBind();
                ddlTrnReq.CssClass = "DropDownListNew";
                txtOvrallFeed = new TextBox();
                txtOvrallFeed.ID = "txtOvrallFeed" + (i + 1).ToString();
                txtOvrallFeed.Height = 70;
                txtOvrallFeed.Width = 200;
                txtOvrallFeed.CssClass = "textboxNew";
                txtOvrallFeed.TextMode = TextBoxMode.MultiLine;
                txtWrkArea = new TextBox();
                txtWrkArea.ID = "txtWrkArea" + (i + 1).ToString();
                txtWrkArea.Width = 200;
                txtWrkArea.CssClass = "textboxNew";
                foreach (DataRow r in dsDetails.Tables[1].Rows)
                {
                    if (r["Subordinate_id"].ToString() == subOrdId.Text)
                    {
                        ddlPrbSA.SelectedValue = r["PrbSA"].ToString();
                        ddlPrgSkills.SelectedValue = r["PrgSkills"].ToString();
                        ddlCommSkills.SelectedValue = r["CommSkills"].ToString();
                        ddlBehSkills.SelectedValue = r["BehSkills"].ToString();
                        ddlPerformance.SelectedValue = r["Performance"].ToString();
                        ddlOvrAllRating.SelectedValue = r["OvrAllRating"].ToString();
                        ddlTrnReq.SelectedValue = r["Training_Required"].ToString();
                        txtOvrallFeed.Text = r["Overall_Feedback"].ToString();
                        txtWrkArea.Text = r["WorkArea"].ToString();
                        break;
                    }
                }
                cell[0].Controls.Add(Sno);
                cell[1].Controls.Add(subOrdId);
                cell[2].Controls.Add(subOrdName);
                cell[3].Controls.Add(txtWrkArea);
                cell[4].Controls.Add(ddlPrbSA);
                cell[5].Controls.Add(ddlPrgSkills);
                cell[6].Controls.Add(ddlCommSkills);
                cell[7].Controls.Add(ddlBehSkills);
                cell[8].Controls.Add(ddlPerformance);
                cell[9].Controls.Add(ddlOvrAllRating);
                cell[10].Controls.Add(ddlTrnReq);
                cell[11].Controls.Add(txtOvrallFeed);
                row.Cells.Add(cell[0]);
                row.Cells.Add(cell[1]);
                row.Cells.Add(cell[2]);
                row.Cells.Add(cell[3]);
                row.Cells.Add(cell[4]);
                row.Cells.Add(cell[5]);
                row.Cells.Add(cell[6]);
                row.Cells.Add(cell[7]);
                row.Cells.Add(cell[8]);
                row.Cells.Add(cell[9]);
                row.Cells.Add(cell[10]);
                row.Cells.Add(cell[11]);
                if (i % 2 == 0)
                    row.CssClass = "evenrow";
                else
                    row.CssClass = "oddrow";
                feedback.Rows.Add(row);
            }
            //rest rows ends
            Control cnt = Page.Form.FindControl("MainContent");
            Control cnt2 = cnt.FindControl("pBody");
            cnt2.Controls.Add(feedback);
            ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:enableSubmit();", true);//for enabling/disabling Submit button.
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fill(User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1), Request.QueryString["s_id"], Request.QueryString["c_id"], Request.QueryString["c_Name"]);
            }
            
        }

        public void fill(string emp_id, string sessionid, string courseid, string courseName)
        {
            try
            {
                Hex.Vars.FB.HexFBBLL.SessionBLL _session = new Hex.Vars.FB.HexFBBLL.SessionBLL();
                Hex.Vars.FB.HexFBModule.NominationListInfo nominationlist = new Hex.Vars.FB.HexFBModule.NominationListInfo();
                nominationlist.Flag = 0;
                SqlDataReader dr = _session.getindividual_sup(emp_id, sessionid, courseid, nominationlist.Flag);
                
                while (dr.Read())
                {
                    lblCourseName.Text = courseName.ToString();
                    lblTraineeID.Text = dr["Employee_id"].ToString();
                    lblTraineeName.Text = dr["Supervisor"].ToString();
                    lblTrainingDate.Text = dr["startdate"].ToString() + " To " + dr["enddate"].ToString();
                    string date = lblTrainingDate.Text;
                    lblTrainingLocation.Text = dr["location"].ToString();
                    break;
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string errMessage = string.Empty;
            Hex.Vars.FB.HexFBModule.feedbackSupInfo supervisorFeed = new Hex.Vars.FB.HexFBModule.feedbackSupInfo();
            Hex.Vars.FB.HexFBBLL.FeedbackBLL _feedback = new Hex.Vars.FB.HexFBBLL.FeedbackBLL();
            supervisorFeed.Courseid = Request.QueryString["c_id"];
            supervisorFeed.Sessionid = Request.QueryString["s_id"];
            supervisorFeed.SupervisorId= User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1);
            Control cnt = Page.Form.FindControl("MainContent");
            Control cnt2 = cnt.FindControl("pBody");
            for(int i=0;i<Convert.ToInt32(Session["SupFeedBackCount"]);i++)
            {
                subOrdId = (Label)cnt2.FindControl("lblSubOrdId" + (i + 1).ToString());
                ddlPrbSA = (DropDownList)cnt2.FindControl("ddlPrbSA" + (i + 1).ToString());
                ddlPrgSkills = (DropDownList)cnt2.FindControl("ddlPrgSkills" + (i + 1).ToString());
                ddlCommSkills = (DropDownList)cnt2.FindControl("ddlCommSkills" + (i + 1).ToString());
                ddlBehSkills = (DropDownList)cnt2.FindControl("ddlBehSkills" + (i + 1).ToString());
                ddlPerformance = (DropDownList)cnt2.FindControl("ddlPerformance" + (i + 1).ToString());
                ddlOvrAllRating = (DropDownList)cnt2.FindControl("ddlOvrAllRating" + (i + 1).ToString());
                ddlTrnReq = (DropDownList)cnt2.FindControl("ddlTrnReq" + (i + 1).ToString());
                txtOvrallFeed = (TextBox)cnt2.FindControl("txtOvrallFeed" + (i + 1).ToString());
                txtWrkArea = (TextBox)cnt2.FindControl("txtWrkArea" + (i + 1).ToString());
                supervisorFeed.SubordinateId = subOrdId.Text;
                supervisorFeed.PrbSA = ddlPrbSA.SelectedValue;
                supervisorFeed.PrgSkills = ddlPrgSkills.SelectedValue;
                supervisorFeed.CommSkills = ddlCommSkills.SelectedValue;
                supervisorFeed.BehSkills = ddlBehSkills.SelectedValue;
                supervisorFeed.Performance = ddlPerformance.SelectedValue;
                supervisorFeed.OvrAllRating = ddlOvrAllRating.SelectedValue;
                supervisorFeed.TrainingRequired = ddlTrnReq.SelectedValue;
                supervisorFeed.OverallFeedback = txtOvrallFeed.Text;
                supervisorFeed.WorkArea = txtWrkArea.Text;
                supervisorFeed.Status = "0";// Status 0 means saved feedback
                if (_feedback.createSupFeedbacks(supervisorFeed, out errMessage))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "info", "javascript:alert('Feedback Saved SuccessFully');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "info", "javascript:alert('Saving Feedback failed due to some issues.');", true);
                }
            }
        }

        protected void Btn_submit_Click(object sender, EventArgs e)
        {
            string errMessage = string.Empty;
            Hex.Vars.FB.HexFBModule.feedbackSupInfo supervisorFeed = new Hex.Vars.FB.HexFBModule.feedbackSupInfo();
            Hex.Vars.FB.HexFBBLL.FeedbackBLL _feedback = new Hex.Vars.FB.HexFBBLL.FeedbackBLL();
            supervisorFeed.Courseid = Request.QueryString["c_id"];
            supervisorFeed.Sessionid = Request.QueryString["s_id"];
            supervisorFeed.SupervisorId = User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1);
            Control cnt = Page.Form.FindControl("MainContent");
            Control cnt2 = cnt.FindControl("pBody");
            for (int i = 0; i < Convert.ToInt32(Session["SupFeedBackCount"]); i++)
            {
                subOrdId = (Label)cnt2.FindControl("lblSubOrdId" + (i + 1).ToString());
                ddlPrbSA = (DropDownList)cnt2.FindControl("ddlPrbSA" + (i + 1).ToString());
                ddlPrgSkills = (DropDownList)cnt2.FindControl("ddlPrgSkills" + (i + 1).ToString());
                ddlCommSkills = (DropDownList)cnt2.FindControl("ddlCommSkills" + (i + 1).ToString());
                ddlBehSkills = (DropDownList)cnt2.FindControl("ddlBehSkills" + (i + 1).ToString());
                ddlPerformance = (DropDownList)cnt2.FindControl("ddlPerformance" + (i + 1).ToString());
                ddlOvrAllRating = (DropDownList)cnt2.FindControl("ddlOvrAllRating" + (i + 1).ToString());
                ddlTrnReq = (DropDownList)cnt2.FindControl("ddlTrnReq" + (i + 1).ToString());
                txtOvrallFeed = (TextBox)cnt2.FindControl("txtOvrallFeed" + (i + 1).ToString());
                txtWrkArea = (TextBox)cnt2.FindControl("txtWrkArea" + (i + 1).ToString());
                supervisorFeed.SubordinateId = subOrdId.Text;
                supervisorFeed.PrbSA = ddlPrbSA.SelectedValue;
                supervisorFeed.PrgSkills = ddlPrgSkills.SelectedValue;
                supervisorFeed.CommSkills = ddlCommSkills.SelectedValue;
                supervisorFeed.BehSkills = ddlBehSkills.SelectedValue;
                supervisorFeed.Performance = ddlPerformance.SelectedValue;
                supervisorFeed.OvrAllRating = ddlOvrAllRating.SelectedValue;
                supervisorFeed.TrainingRequired = ddlTrnReq.SelectedValue;
                supervisorFeed.OverallFeedback = txtOvrallFeed.Text;
                supervisorFeed.WorkArea = txtWrkArea.Text;
                supervisorFeed.Status = "1";// Status 1 means submit feedback
                if (_feedback.createSupFeedbacks(supervisorFeed, out errMessage))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "info", "javascript:alert('Feedback Submitted SuccessFully');", true);
                    if (i == (Convert.ToInt32(Session["SupFeedBackCount"]) - 1))
                    {
                        MailSend(User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1), supervisorFeed.Sessionid, supervisorFeed.Courseid);
                        if (Session["Admin"] == null)
                        {
                            Response.Redirect("TrainingList.aspx?IsAdmin=false");
                        }
                        else
                        {
                            Response.Redirect("TrainingList.aspx?IsAdmin=true");
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "info", "javascript:alert('Submitting Feedback failed due to some issues.');", true);
                }
            }
        }

        public void MailSend(string employeeID, string sessionId, string courseID)
        {
            try
            {
                int SendMailReturnValue;
                string errorMsg, mailTo = string.Empty, mailCC = string.Empty, mailBCC = string.Empty;
                StringBuilder mailSubject=new StringBuilder();
                StringBuilder mailBody = new StringBuilder();
                Hex.Vars.FB.HexFBModule.NominationListInfo nominationList = new Hex.Vars.FB.HexFBModule.NominationListInfo();
                Hex.Vars.FB.HexFBBLL.NominationListBLL sendMail=new Hex.Vars.FB.HexFBBLL.NominationListBLL();
                nominationList.Flag = 3;
                nominationList.EmployeeId = employeeID;
                nominationList.SessionId = sessionId;
                nominationList.CourseId = courseID;
                DataSet dsMailDetails = sendMail.MailRelatedDetails(nominationList, out errorMsg);
                if (Convert.ToString(dsMailDetails.Tables[1].Rows[0]["PM_Remarks"]).Trim() == "Auto Approved")
                {
                    FeedbackReview mail = new FeedbackReview();
                    mail.MailSend(employeeID, sessionId, courseID, "Auto Approved");
                }
                else
                {
                    FromMailID = Convert.ToString(dsMailDetails.Tables[1].Rows[0]["SUPERVISOR_MAIL"]);
                    mailSubject.AppendFormat(dsMailDetails.Tables[0].Rows[0]["Subject"].ToString());
                    mailBody.AppendFormat(dsMailDetails.Tables[0].Rows[0]["Body"].ToString());
                    mailTo = Convert.ToString(dsMailDetails.Tables[1].Rows[0]["To Mail ID"]);
                    mailCC = Convert.ToString(dsMailDetails.Tables[1].Rows[0]["SUPERVISOR_MAIL"]);//"veerag@hexaware.com;SumanthP@hexaware.com;Chiragm@hexaware.com";
                    mailBCC = Convert.ToString(dsMailDetails.Tables[0].Rows[0]["BccMail"]);
                    mailBody.Replace("< PM Name>", dsMailDetails.Tables[1].Rows[0]["PM Name"].ToString());
                    mailBody.Replace("<Reporting Manager Name>", dsMailDetails.Tables[1].Rows[0]["Supervisor Name"].ToString());
                    string trainee = "<ul>";
                    foreach (DataRow r in dsMailDetails.Tables[1].Rows)
                    {
                        trainee = trainee + "<li>" + r["Subordinate Name"] + "</li>";
                    }
                    trainee = trainee + "</ul>";
                    mailBody.Replace("<Trainee Details>", trainee);
                    mailBody.Replace("<signature>", Convert.ToString(dsMailDetails.Tables[1].Rows[0]["Supervisor Name"]));
                    mailBody.Replace("<replymailid>", Convert.ToString(dsMailDetails.Tables[1].Rows[0]["SUPERVISOR_MAIL"]));
                    DataSet dsGetErrMsg = new DataSet();
                    DataTable dtGetErrMsg = new DataTable();
                    dtGetErrMsg = sendMail.SendmailList(FromMailID, mailTo, Convert.ToString(mailSubject), Convert.ToString(mailBody), mailCC, mailBCC);
                    dsGetErrMsg.Tables.Add(dtGetErrMsg);
                    if (dsGetErrMsg.Tables[0].Rows.Count == 1)
                        SendMailReturnValue = Convert.ToInt32(dsGetErrMsg.Tables[0].Rows[0][0].ToString());
                    else
                        SendMailReturnValue = 0;
                    nominationList.EmployeeId = employeeID;
                    if (SendMailReturnValue == 0)
                    {
                        nominationList.Status = "Delivered";
                    }
                    else
                    {
                        nominationList.Status = "Failed";
                    }
                    nominationList.CreatedDate = DateTime.Now.ToString();
                    nominationList.FromMailID = FromMailID;
                    nominationList.ToMailID = mailTo;
                    nominationList.BccMailID = mailBCC;
                    nominationList.CcMailID = mailCC;
                    nominationList.SubJect = Convert.ToString(mailSubject);
                    nominationList.Body = Convert.ToString(mailBody);

                    sendMail.insertMailstatu(nominationList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}