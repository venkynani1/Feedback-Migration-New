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
    public partial class FeedbackReview : System.Web.UI.Page
    {
        Hex.Vars.FB.HexFBBLL.FeedbackBLL _feedback = new Hex.Vars.FB.HexFBBLL.FeedbackBLL();
        string[] sup = new string[2];
        protected void Page_Load(object sender, EventArgs e)
        {
            Hex.Vars.FB.HexFBBLL.SessionBLL _session = new Hex.Vars.FB.HexFBBLL.SessionBLL();
            sup=Request.QueryString["Supervisor"].Split('-');
            string EmpID;
            if (Session["username"] != null)
            {
                EmpID = Session["username"].ToString();
            }
            else
            {
                EmpID = User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1).ToString();
            }
            DataSet dsDetails = _session.getSupFeedbackDetails(sup[1], Request.QueryString["s_id"],EmpID);
            int i = 0;
            dsDetails.Tables[1].AcceptChanges();
            foreach (object row_loopVariable in dsDetails.Tables[1].Rows)
            {
                if (dsDetails.Tables[1].Rows[i]["Status"].ToString() != "1")
                {
                    dsDetails.Tables[1].Rows[i].Delete();
                }
                i++;
            }
            dsDetails.Tables[1].AcceptChanges();
            gvReviews.DataSource = dsDetails.Tables[1];
            gvReviews.DataBind();
            lblCourseName.Text = dsDetails.Tables[1].Rows[0]["Course"].ToString();
            lblTraineeID.Text = sup[1];
            lblTraineeName.Text = sup[0];
            lblTrainingDate.Text = dsDetails.Tables[1].Rows[0]["startdate"].ToString() + " To " + dsDetails.Tables[1].Rows[0]["enddate"].ToString();
            lblTrainingLocation.Text = dsDetails.Tables[1].Rows[0]["Location"].ToString();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string errMessage = string.Empty;
            string EmpID;
            if (Session["username"] != null)
            {
                EmpID = Session["username"].ToString();
            }
            else
            {
                EmpID = User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1).ToString();
            }

            if (_feedback.updatePMRemarks(3, Tb_traineecomments.Text,EmpID , Request.QueryString["s_id"].ToString(), lblTraineeID.Text, out errMessage))
            {
                MailSend(sup[1], Request.QueryString["s_id"].ToString(), Request.QueryString["c_id"].ToString(), "Approved");
                if (Session["Admin"] == null)
                {
                    Response.Redirect("TrainingList.aspx?IsAdmin=false");
                }
                else
                {
                    Response.Redirect("TrainingList.aspx?IsAdmin=true");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "info", "javascript:alert('Submitting Feedback failed due to some issues.');", true);
            }

        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            string errMessage = string.Empty;
            string EmpID;
            if (Session["username"] != null)
            {
                EmpID = Session["username"].ToString();
            }
            else
            {
                EmpID = User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1).ToString();
            }

            if (_feedback.updatePMRemarks(0, Tb_traineecomments.Text,EmpID, Request.QueryString["s_id"].ToString(),lblTraineeID.Text, out errMessage))
            {
                MailSend(sup[1], Request.QueryString["s_id"].ToString(), Request.QueryString["c_id"].ToString(),"Rejected");
                if (Session["Admin"] == null)
                {
                    Response.Redirect("TrainingList.aspx?IsAdmin=false");
                }
                else
                {
                    Response.Redirect("TrainingList.aspx?IsAdmin=true");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "info", "javascript:alert('Submitting Feedback failed due to some issues.');", true);
            }
        }
        public void MailSend(string employeeID, string sessionId, string courseID, string status)
        {
            try
            {
                string FromMailID = string.Empty;

                int SendMailReturnValue;
                string errorMsg, mailTo = string.Empty, mailCC = string.Empty, mailBCC = string.Empty;
                StringBuilder mailSubject = new StringBuilder();
                StringBuilder mailBody = new StringBuilder();
                Hex.Vars.FB.HexFBModule.NominationListInfo nominationList = new Hex.Vars.FB.HexFBModule.NominationListInfo();
                Hex.Vars.FB.HexFBBLL.NominationListBLL sendMail = new Hex.Vars.FB.HexFBBLL.NominationListBLL();
                nominationList.Flag = 4;
                nominationList.EmployeeId = employeeID;
                nominationList.SessionId = sessionId;
                nominationList.CourseId = courseID;
                DataSet dsMailDetails = sendMail.MailRelatedDetails(nominationList, out errorMsg);
                FromMailID = Convert.ToString(dsMailDetails.Tables[1].Rows[0]["PM_MAIL"]); 
                mailSubject.AppendFormat(dsMailDetails.Tables[0].Rows[0]["Subject"].ToString());
                mailBody.AppendFormat(dsMailDetails.Tables[0].Rows[0]["Body"].ToString());
                mailTo = Convert.ToString(dsMailDetails.Tables[1].Rows[0]["To Mail ID"]);
                mailCC = Convert.ToString(dsMailDetails.Tables[1].Rows[0]["PM_MAIL"]); //"veerag@hexaware.com;SumanthP@hexaware.com;Chiragm@hexaware.com";
                mailBCC = Convert.ToString(dsMailDetails.Tables[0].Rows[0]["BccMail"]);
                mailBody.Replace("<Reporting Manager Name>", dsMailDetails.Tables[1].Rows[0]["Supervisor Name"].ToString());
                mailBody.Replace("<remarks>", dsMailDetails.Tables[1].Rows[0]["PM_Remarks"].ToString());
                mailBody.Replace("<signature>", dsMailDetails.Tables[1].Rows[0]["PM Name"].ToString());
                mailBody.Replace("<replymailid>", Convert.ToString(dsMailDetails.Tables[1].Rows[0]["PM_MAIL"]));
                string trainee = "";
                if (status == "Rejected")
                {
                    mailBody.Replace("<row>", "<tr> <td bgcolor=\"White\" height=\"25\" style=\"color: #0033CC; background-color: #FFFFFF;\">Please re-submit.</td></tr>");
                    mailBody.Replace("<status>", " has completed the Rating Review process. Please refer the PM Remarks for further details.");
                }
                else if (status == "Auto Approved")
                {
                    mailBody.Replace("<PM Name>", "");
                    mailBody.Replace("<row>", "");
                    mailBody.Replace("<status>", "The rating has been submitted and auto approved for");
                    trainee = "<ul>";
                    foreach (DataRow r in dsMailDetails.Tables[1].Rows)
                    {
                        trainee = trainee + "<li>" + r["Subordinate Name"] + "</li>";
                    }
                    trainee = trainee + "</ul>";
                    mailCC = "";
                }
                else
                {
                    mailBody.Replace("<row>", "");
                    mailBody.Replace("<status>", "has "+ status + " the rating submitted for");
                    trainee = "<ul>";
                    foreach (DataRow r in dsMailDetails.Tables[1].Rows)
                    {
                        trainee = trainee + "<li>" + r["Subordinate Name"] + "</li>";
                    }
                    trainee = trainee + "</ul>";
                }
                mailBody.Replace("<PM Name>", dsMailDetails.Tables[1].Rows[0]["PM Name"].ToString());
                mailBody.Replace("<Trainee Details>", trainee);
                if (status == "Auto Approved")
                {
                    mailSubject.Replace("<status>", "Approved");
                }
                else
                {
                    mailSubject.Replace("<status>", status);
                }
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}