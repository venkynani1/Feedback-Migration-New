using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using Hex.Vars.FB.HexFBBLL;
using Hex.Vars.FB.HexFBModule;

namespace HexFeedback
{
    public partial class FeedbackSup : System.Web.UI.Page
    {
     Table feedback = new Table();
     TableRow row = new TableRow();
     TableCell[] cell = new TableCell[12];
     DropDownList ddlTechSkills;
     DropDownList ddlSoftSkills;
     DropDownList ddlPerformance;
     DropDownList ddlOvrAllRating;
     TextBox txtOvrallFeed;
     TextBox txtWrkArea;
     Label Sno;
     Label subOrdName;
     Label subOrdId;
     ListItem[] rating = new ListItem[7];
     ListItem[] trnReq = new ListItem[3];
     int j = 5;
     string FromMailID; 
     //HtmlTableCell TdSaveMessage;
     //Label lblMsg;
     //Label Lb_Empname;
     //Label lblTraineeName;
     //Label Lb_StartEnddate;
     //Label lblTrainingDate;
     //Label Lb_Empid;
     //Label lblTraineeID;
     //Label Lb_Location;
     //Label lblTrainingLocation;
     //Label Lb_Coursename;
     //Label lblCourseName;
     //Label Label1;
     //Label Label2;
     //HtmlTableRow RowParamHead;
     //HtmlTableRow TrP3;
     //Label Label7;
     //HtmlTableRow TrP4;
     //Label Label8;
     //HtmlTableRow TrP5;
     //Label Label9;
     //HtmlTableRow TrP6;
     //Label Label10;
     //UpdatePanel UpdatePanel1;
     //Panel pBody;
     //Button Btn_submit;
     //Button BtnSave;
     //Label lblerrMsg;
     //HiddenField feedbackCount;

    protected void Page_Init(object sender, EventArgs e)
    {
            string EmpID;
            if (Session["username"] != null)
            {
                EmpID = Session["username"].ToString();
            }
            else
            {
                EmpID = User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1);
            }
            DataSet supFeedbackDetails = new SessionBLL().getSupFeedbackDetails(EmpID, Request.QueryString["s_id"], (string) null);
      feedbackCount.Value = supFeedbackDetails.Tables[0].Rows.Count.ToString();
      Session["SupFeedBackCount"] = (object) feedbackCount.Value;
      feedback.Attributes.Add("Style", "width: 100%; text-align: center;");
      feedback.ID = "T1";
      for (int index = 0; index < 9; ++index)
        cell[index] = new TableCell();
      row = new TableRow();
      Label label1 = new Label();
      Label label2 = new Label();
      Label label3 = new Label();
      Label label4 = new Label();
      Label label5 = new Label();
      Label label6 = new Label();
      Label label7 = new Label();
      Label label8 = new Label();
      Label label9 = new Label();
      label1.Text = "S. No.";
      label2.Text = "Trainee's Employee Id";
      label3.Text = "Trainee's Name";
      label4.Text = "Mention Work Area (1st 90 days). Specify the area covered by the Trainee.";
      label5.Text = "Technical Skills";
      label6.Text = "Soft Skills";
      label7.Text = "Performance";
      label8.Text = "Overall Feedback Rating";
      label9.Text = "Overall Comments for the trainee based on the specified parameters";
      cell[0].Controls.Add((Control) label1);
      cell[1].Controls.Add((Control) label2);
      cell[2].Controls.Add((Control) label3);
      cell[3].Controls.Add((Control) label4);
      cell[4].Controls.Add((Control) label5);
      cell[5].Controls.Add((Control) label6);
      cell[6].Controls.Add((Control) label7);
      cell[7].Controls.Add((Control) label8);
      cell[8].Controls.Add((Control) label9);
      row.Cells.Add(cell[0]);
      row.Cells.Add(cell[1]);
      row.Cells.Add(cell[2]);
      row.Cells.Add(cell[3]);
      row.Cells.Add(cell[4]);
      row.Cells.Add(cell[5]);
      row.Cells.Add(cell[6]);
      row.Cells.Add(cell[7]);
      row.Cells.Add(cell[8]);
      row.CssClass = "hdrow";
      feedback.Rows.Add(row);
      rating[0] = new ListItem("Select", "0");
      rating[6] = new ListItem("NA", "NA");
      for (int index = 1; index < 6; ++index)
      {
        rating[index] = new ListItem(j.ToString(), j.ToString());
        --j;
      }
      trnReq[0] = new ListItem("Select", "0");
      trnReq[1] = new ListItem("Yes", "1");
      trnReq[2] = new ListItem("No", "2");
      for (int index1 = 0; index1 < supFeedbackDetails.Tables[0].Rows.Count; ++index1)
      {
        for (int index2 = 0; index2 < 9; ++index2)
          cell[index2] = new TableCell();
        row = new TableRow();
        Sno = new Label();
        Sno.Text = (index1 + 1).ToString();
        subOrdName = new Label();
        subOrdName.Text = supFeedbackDetails.Tables[0].Rows[index1][1].ToString();
        subOrdName.ID = "lblSubOrd" + (index1 + 1).ToString();
        subOrdId = new Label();
        subOrdId.Text = supFeedbackDetails.Tables[0].Rows[index1][0].ToString();
        //Label subOrdId = subOrdId;
        int num = index1 + 1;
        string str1 = "lblSubOrdId" + num.ToString();
        subOrdId.ID = str1;
        ddlTechSkills = new DropDownList();
        //DropDownList ddlTechSkills = ddlTechSkills;
        num = index1 + 1;
        string str2 = "ddlTechSkills" + num.ToString();
        ddlTechSkills.ID = str2;
        ddlTechSkills.DataSource = (object) rating;
        ddlTechSkills.DataValueField = "Value";
        ddlTechSkills.DataTextField = "Text";
        ddlTechSkills.DataBind();
        ddlTechSkills.CssClass = "DropDownListNew";
        ddlSoftSkills = new DropDownList();
        //DropDownList ddlSoftSkills = ddlSoftSkills;
        num = index1 + 1;
        string str3 = "ddlSoftSkills" + num.ToString();
        ddlSoftSkills.ID = str3;
        ddlSoftSkills.DataSource = (object) rating;
        ddlSoftSkills.DataValueField = "Value";
        ddlSoftSkills.DataTextField = "Text";
        ddlSoftSkills.DataBind();
        ddlSoftSkills.CssClass = "DropDownListNew";
        ddlPerformance = new DropDownList();
        //DropDownList ddlPerformance = ddlPerformance;
        num = index1 + 1;
        string str4 = "ddlPerformance" + num.ToString();
        ddlPerformance.ID = str4;
        ddlPerformance.DataSource = (object) rating;
        ddlPerformance.DataValueField = "Value";
        ddlPerformance.DataTextField = "Text";
        ddlPerformance.DataBind();
        ddlPerformance.CssClass = "DropDownListNew";
        ddlOvrAllRating = new DropDownList();
        //DropDownList ddlOvrAllRating = ddlOvrAllRating;
        num = index1 + 1;
        string str5 = "ddlOvrAllRating" + num.ToString();
        ddlOvrAllRating.ID = str5;
        ddlOvrAllRating.DataSource = (object) rating;
        ddlOvrAllRating.DataValueField = "Value";
        ddlOvrAllRating.DataTextField = "Text";
        ddlOvrAllRating.DataBind();
        ddlOvrAllRating.CssClass = "DropDownListNew";
        txtOvrallFeed = new TextBox();
        //TextBox txtOvrallFeed = txtOvrallFeed;
        num = index1 + 1;
        string str6 = "txtOvrallFeed" + num.ToString();
        txtOvrallFeed.ID = str6;
        txtOvrallFeed.Height = (Unit) 70;
        txtOvrallFeed.Width = (Unit) 200;
        txtOvrallFeed.CssClass = "textboxNew";
        txtOvrallFeed.TextMode = TextBoxMode.MultiLine;
        txtWrkArea = new TextBox();
        //TextBox txtWrkArea = txtWrkArea;
        num = index1 + 1;
        string str7 = "txtWrkArea" + num.ToString();
        txtWrkArea.ID = str7;
        txtWrkArea.Width = (Unit) 200;
        txtWrkArea.CssClass = "textboxNew";
        foreach (DataRow row in (InternalDataCollectionBase) supFeedbackDetails.Tables[1].Rows)
        {
          if (row["Subordinate_id"].ToString() == subOrdId.Text)
          {
            ddlTechSkills.SelectedValue = row["TechSkills"].ToString();
            ddlSoftSkills.SelectedValue = row["SoftSkills"].ToString();
            ddlPerformance.SelectedValue = row["Performance"].ToString();
            ddlOvrAllRating.SelectedValue = row["OvrAllRating"].ToString();
            txtOvrallFeed.Text = row["Overall_Feedback"].ToString();
            txtWrkArea.Text = row["WorkArea"].ToString();
            break;
          }
        }
        cell[0].Controls.Add((Control) Sno);
        cell[1].Controls.Add((Control) subOrdId);
        cell[2].Controls.Add((Control) subOrdName);
        cell[3].Controls.Add((Control) txtWrkArea);
        cell[4].Controls.Add((Control) ddlTechSkills);
        cell[5].Controls.Add((Control) ddlSoftSkills);
        cell[6].Controls.Add((Control) ddlPerformance);
        cell[7].Controls.Add((Control) ddlOvrAllRating);
        cell[8].Controls.Add((Control) txtOvrallFeed);
        row.Cells.Add(cell[0]);
        row.Cells.Add(cell[1]);
        row.Cells.Add(cell[2]);
        row.Cells.Add(cell[3]);
        row.Cells.Add(cell[4]);
        row.Cells.Add(cell[5]);
        row.Cells.Add(cell[6]);
        row.Cells.Add(cell[7]);
        row.Cells.Add(cell[8]);
        if (index1 % 2 == 0)
          row.CssClass = "evenrow";
        else
          row.CssClass = "oddrow";
        feedback.Rows.Add(row);
      }
      Page.Form.FindControl("MainContent").FindControl("pBody").Controls.Add((Control) feedback);
      ScriptManager.RegisterStartupScript((Page) this, GetType(), "info", "javascript:enableSubmit();", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
            string EmpID;
            if (Session["username"] != null)
            {
                EmpID = Session["username"].ToString();
            }
            else
            {
                EmpID = User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1);
            }

            if (IsPostBack)
        return;

      fill(EmpID, Request.QueryString["s_id"], Request.QueryString["c_id"], Request.QueryString["c_Name"]);
    }

    public void fill(string emp_id, string sessionid, string courseid, string courseName)
    {
      try
      {
        SqlDataReader sqlDataReader = new SessionBLL().getindividual_sup(emp_id, sessionid, courseid, new NominationListInfo()
        {
          Flag = 0
        }.Flag);
        if (!sqlDataReader.Read())
          return;
        lblCourseName.Text = courseName.ToString();
        lblTraineeID.Text = sqlDataReader["Employee_id"].ToString();
        lblTraineeName.Text = sqlDataReader["Supervisor"].ToString();
        lblTrainingDate.Text = sqlDataReader["startdate"].ToString() + " To " + sqlDataReader["enddate"].ToString();
        string text = lblTrainingDate.Text;
        lblTrainingLocation.Text = sqlDataReader["location"].ToString();
      }
      catch (Exception ex)
      {
        lblerrMsg.Text = "Error:- " + ex.Message;
      }
    }


    protected  void BtnSave_Click(object sender, EventArgs e)
    {
      string errMsg = string.Empty;
      feedbackSupInfo _supFeedback = new feedbackSupInfo();
      FeedbackBLL feedbackBll = new FeedbackBLL();
      _supFeedback.Courseid = Request.QueryString["c_id"];
      _supFeedback.Sessionid = Request.QueryString["s_id"];
            string EmpID;
            if (Session["username"] != null)
            {
                EmpID = Session["username"].ToString();
            }
            else
            {
                EmpID = User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1);
            }
            _supFeedback.SupervisorId = EmpID;
      Control control1 = Page.Form.FindControl("MainContent").FindControl("pBody");
      for (int index = 0; index < Convert.ToInt32(Session["SupFeedBackCount"]); ++index)
      {
        Control control2 = control1;
        int num = index + 1;
        string id1 = "lblSubOrdId" + num.ToString();
        subOrdId = (Label) control2.FindControl(id1);
        Control control3 = control1;
        num = index + 1;
        string id2 = "ddlTechSkills" + num.ToString();
        ddlTechSkills = (DropDownList) control3.FindControl(id2);
        Control control4 = control1;
        num = index + 1;
        string id3 = "ddlSoftSkills" + num.ToString();
        ddlSoftSkills = (DropDownList) control4.FindControl(id3);
        Control control5 = control1;
        num = index + 1;
        string id4 = "ddlPerformance" + num.ToString();
        ddlPerformance = (DropDownList) control5.FindControl(id4);
        Control control6 = control1;
        num = index + 1;
        string id5 = "ddlOvrAllRating" + num.ToString();
        ddlOvrAllRating = (DropDownList) control6.FindControl(id5);
        Control control7 = control1;
        num = index + 1;
        string id6 = "txtOvrallFeed" + num.ToString();
        txtOvrallFeed = (TextBox) control7.FindControl(id6);
        Control control8 = control1;
        num = index + 1;
        string id7 = "txtWrkArea" + num.ToString();
        txtWrkArea = (TextBox) control8.FindControl(id7);
        _supFeedback.SubordinateId = subOrdId.Text;
        _supFeedback.TechSkills = ddlTechSkills.SelectedValue;
        _supFeedback.SoftSkills = ddlSoftSkills.SelectedValue;
        _supFeedback.Performance = ddlPerformance.SelectedValue;
        _supFeedback.OvrAllRating = ddlOvrAllRating.SelectedValue;
        _supFeedback.OverallFeedback = txtOvrallFeed.Text;
        _supFeedback.WorkArea = txtWrkArea.Text;
        _supFeedback.Status = "0";
        if (feedbackBll.createSupFeedbacks(_supFeedback, out errMsg))
          ScriptManager.RegisterClientScriptBlock((Page) this, GetType(), "info", "javascript:alert('Feedback Saved SuccessFully');", true);
        else
          ScriptManager.RegisterClientScriptBlock((Page) this, GetType(), "info", "javascript:alert('Saving Feedback failed due to some issues.');", true);
      }
    }

    protected void Btn_submit_Click(object sender, EventArgs e)
    {
      string errMsg = string.Empty;
      feedbackSupInfo _supFeedback = new feedbackSupInfo();
      FeedbackBLL feedbackBll = new FeedbackBLL();
      _supFeedback.Courseid = Request.QueryString["c_id"];
      _supFeedback.Sessionid = Request.QueryString["s_id"];
            string EmpID;
            if (Session["username"] != null)
            {
                EmpID = Session["username"].ToString();
            }
            else
            {
                EmpID = User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1);
            }
            _supFeedback.SupervisorId = EmpID;
      Control control1 = Page.Form.FindControl("MainContent").FindControl("pBody");
      for (int index = 0; index < Convert.ToInt32(Session["SupFeedBackCount"]); ++index)
      {
        Control control2 = control1;
        int num = index + 1;
        string id1 = "lblSubOrdId" + num.ToString();
        subOrdId = (Label) control2.FindControl(id1);
        Control control3 = control1;
        num = index + 1;
        string id2 = "ddlTechSkills" + num.ToString();
        ddlTechSkills = (DropDownList) control3.FindControl(id2);
        Control control4 = control1;
        num = index + 1;
        string id3 = "ddlSoftSkills" + num.ToString();
        ddlSoftSkills = (DropDownList) control4.FindControl(id3);
        Control control5 = control1;
        num = index + 1;
        string id4 = "ddlPerformance" + num.ToString();
        ddlPerformance = (DropDownList) control5.FindControl(id4);
        Control control6 = control1;
        num = index + 1;
        string id5 = "ddlOvrAllRating" + num.ToString();
        ddlOvrAllRating = (DropDownList) control6.FindControl(id5);
        Control control7 = control1;
        num = index + 1;
        string id6 = "txtOvrallFeed" + num.ToString();
        txtOvrallFeed = (TextBox) control7.FindControl(id6);
        Control control8 = control1;
        num = index + 1;
        string id7 = "txtWrkArea" + num.ToString();
        txtWrkArea = (TextBox) control8.FindControl(id7);
        _supFeedback.SubordinateId = subOrdId.Text;
        _supFeedback.TechSkills = ddlTechSkills.SelectedValue;
        _supFeedback.SoftSkills = ddlSoftSkills.SelectedValue;
        _supFeedback.Performance = ddlPerformance.SelectedValue;
        _supFeedback.OvrAllRating = ddlOvrAllRating.SelectedValue;
        _supFeedback.OverallFeedback = txtOvrallFeed.Text;
        _supFeedback.WorkArea = txtWrkArea.Text;
        _supFeedback.Status = "1";
        if (feedbackBll.createSupFeedbacks(_supFeedback, out errMsg))
        {
          ScriptManager.RegisterClientScriptBlock((Page) this, GetType(), "info", "javascript:alert('Feedback Submitted SuccessFully');", true);
          if (index == Convert.ToInt32(Session["SupFeedBackCount"]) - 1)
          {
            MailSend(User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1), _supFeedback.Sessionid, _supFeedback.Courseid);
            if (Session["Admin"] == null)
              Response.Redirect("TrainingList.aspx?IsAdmin=false");
            else
              Response.Redirect("TrainingList.aspx?IsAdmin=true");
          }
        }
        else
          ScriptManager.RegisterClientScriptBlock((Page) this, GetType(), "info", "javascript:alert('Submitting Feedback failed due to some issues.');", true);
      }
    }

    public void MailSend(string employeeID, string sessionId, string courseID)
    {
      try
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        StringBuilder stringBuilder1 = new StringBuilder();
        StringBuilder stringBuilder2 = new StringBuilder();
        NominationListInfo nominationlist = new NominationListInfo();
        NominationListBLL nominationListBll = new NominationListBLL();
        nominationlist.Flag = 3;
        nominationlist.EmployeeId = employeeID;
        nominationlist.SessionId = sessionId;
        nominationlist.CourseId = courseID;
        DataSet dataSet1 = nominationListBll.MailRelatedDetails(nominationlist, out string _);
        if (Convert.ToString(dataSet1.Tables[1].Rows[0]["PM_Remarks"]).Trim() == "Auto Approved")
        {
          new FeedbackReview().MailSend(employeeID, sessionId, courseID, "Auto Approved");
        }
        else
        {
          FromMailID = Convert.ToString(dataSet1.Tables[1].Rows[0]["SUPERVISOR_MAIL"]);
          stringBuilder1.AppendFormat(dataSet1.Tables[0].Rows[0]["Subject"].ToString());
          stringBuilder2.AppendFormat(dataSet1.Tables[0].Rows[0]["Body"].ToString());
          string tomail = Convert.ToString(dataSet1.Tables[1].Rows[0]["To Mail ID"]);
          string ccc = Convert.ToString(dataSet1.Tables[1].Rows[0]["SUPERVISOR_MAIL"]);
          string bcc = Convert.ToString(dataSet1.Tables[0].Rows[0]["BccMail"]);
          stringBuilder2.Replace("< PM Name>", dataSet1.Tables[1].Rows[0]["PM Name"].ToString());
          stringBuilder2.Replace("<Reporting Manager Name>", dataSet1.Tables[1].Rows[0]["Supervisor Name"].ToString());
          string str = "<ul>";
          foreach (DataRow row in (InternalDataCollectionBase) dataSet1.Tables[1].Rows)
            str = str + "<li>" + row["Subordinate Name"] + "</li>";
          string newValue = str + "</ul>";
          stringBuilder2.Replace("<Trainee Details>", newValue);
          stringBuilder2.Replace("<signature>", Convert.ToString(dataSet1.Tables[1].Rows[0]["Supervisor Name"]));
          stringBuilder2.Replace("<replymailid>", Convert.ToString(dataSet1.Tables[1].Rows[0]["SUPERVISOR_MAIL"]));
          DataSet dataSet2 = new DataSet();
          DataTable dataTable = new DataTable();
          DataTable table = nominationListBll.SendmailList(FromMailID, tomail, Convert.ToString((object) stringBuilder1), Convert.ToString((object) stringBuilder2), ccc, bcc);
          dataSet2.Tables.Add(table);
          int num = dataSet2.Tables[0].Rows.Count != 1 ? 0 : Convert.ToInt32(dataSet2.Tables[0].Rows[0][0].ToString());
          nominationlist.EmployeeId = employeeID;
          nominationlist.Status = num != 0 ? "Failed" : "Delivered";
          nominationlist.CreatedDate = DateTime.Now.ToString();
          nominationlist.FromMailID = FromMailID;
          nominationlist.ToMailID = tomail;
          nominationlist.BccMailID = bcc;
          nominationlist.CcMailID = ccc;
          nominationlist.SubJect = Convert.ToString((object) stringBuilder1);
          nominationlist.Body = Convert.ToString((object) stringBuilder2);
          nominationListBll.insertMailstatu(nominationlist);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
