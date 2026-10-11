#region Header
/// ***********************************************************************************************
/// File name: IndividualAnalysis.aspx.cs
/// Description : 
/// Created date :18 Apr 2012
/// Created by :  Sureshkumar D, Hexaware Technologies Ltd.
///
///
/// Change History
/// -----------------------------------------------------------------------------------------------
/// Date            Author              Reviewed date      Reviewed by      Notes
/// -----------------------------------------------------------------------------------------------
/// 20-02-2014      Chirag Mota         20-02-2014         Karuppaiah K     Lines Modified 
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
using Hex.Vars.FB.HexFBBLL;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Novacode;
using System.IO;
using System.Drawing;
using System.Reflection;

#endregion


#region Class
namespace HexFeedback
{
    public partial class IndividAnalysis : System.Web.UI.Page
    {
        string urlparm = "";
        string urlparm1 = "";
        decimal score1;
        decimal score2;
        decimal score3;
        int Perc1;
        int Perc2;
        int Perc3;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    urlparm = "toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,height=300,width=300,left=250,top=250";
                    urlparm1 = "toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,height=650,width=850,left=250,top=250";
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
        protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Hex.Vars.FB.HexFBBLL.SessionBLL sessionbll = new SessionBLL();
                SqlDataReader dr1 = sessionbll.getSessionCourseWise(drpCourse.Text, Convert.ToInt32(Session["Year"]));
                ddlSession.DataSource = dr1;
                ddlSession.DataMember = "session";
                ddlSession.DataValueField = "sessionid";
                ddlSession.DataBind();

                ddlSession.Items.Insert(0, new ListItem("--Select--", String.Empty));
                ddlSession.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string tempEmpid = null;
                Hex.Vars.FB.HexFBBLL.SessionBLL _session = new Hex.Vars.FB.HexFBBLL.SessionBLL();
                SqlDataReader drtrainer = _session.getTrainers(ddlSession.Text, tempEmpid);
                ddltrainers.DataSource = drtrainer;
                ddltrainers.DataMember = "Session_Trainer";
                ddltrainers.DataValueField = "TrainerID";
                ddltrainers.DataBind();
                ddltrainers.Items.Insert(0, "Select the Trainer");
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
            //Commented By Chirag Mota on 20-02-2014
            //try
            //{
            //    Hex.Vars.FB.HexFBBLL.SessionBLL sessionbll = new SessionBLL();
            //    SqlDataReader dr1 = sessionbll.getSessionEmployeeIdWise(ddlSession.SelectedValue);
            //    ddlSession.DataSource = dr1;

            //    ddlemployee.DataSource = dr1;
            //    ddlemployee.DataMember = "Employee_id";
            //    ddlemployee.DataValueField = "Employee_id";
            //    ddlemployee.DataBind();
            //    //Added by Chirag 0n 20-02-2014
            //    ddlemployee.Items.Insert(0,new ListItem("---All---","All"));
            //    //ddlemployee.Items.Insert(0, new ListItem("--Select--", String.Empty));

            //    ddlemployee.SelectedIndex = 0;
            //}
            //catch (Exception ex)
            //{
            //    lblerrMsg.Text = "Error:- " + ex.Message;
            //}

        }

        protected void ddltrainers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlforrpt.Visible = false;
                Hex.Vars.FB.HexFBBLL.SessionBLL sessionbll = new SessionBLL();
                SqlDataReader dr1 = sessionbll.getSessionEmployeeIdWise(ddlSession.SelectedValue);
                ddlSession.DataSource = dr1;

                ddlemployee.DataSource = dr1;
                ddlemployee.DataMember = "Employee_id";
                ddlemployee.DataValueField = "Employee_id";
                ddlemployee.DataBind();
                ddlemployee.Items.Insert(0, new ListItem("--Select--", String.Empty));
                //Added by Chirag 0n 20-02-2014
                ddlemployee.Items.Insert(1, new ListItem("All Employees", "All"));


                ddlemployee.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
            //Commented By Chirag Mota on 20-02-2014
            //try
            //{
            //    string tempEmpid = null;
            //    Hex.Vars.FB.HexFBBLL.SessionBLL _session = new Hex.Vars.FB.HexFBBLL.SessionBLL();
            //    SqlDataReader drtrainer = _session.getTrainers(ddlSession.Text, tempEmpid);
            //    ddltrainers.DataSource = drtrainer;
            //    ddltrainers.DataMember = "Session_Trainer";
            //    ddltrainers.DataValueField = "TrainerID";
            //    ddltrainers.DataBind();
            //    ddltrainers.Items.Insert(0, "Select the Trainer");
            //}
            //catch (Exception ex)
            //{
            //    lblerrMsg.Text = "Error:- " + ex.Message;
            //}

        }
        protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlforrpt.Visible = true;
                string errMessage = string.Empty;
                Hex.Vars.FB.HexFBModule.Sessioninfo _Session = new Hex.Vars.FB.HexFBModule.Sessioninfo();
                int EmployeeCount = 1;

                _Session.session = ddlSession.SelectedItem.ToString();
                _Session.empid = ddlemployee.Text;
                _Session.Trainerid = ddltrainers.Text;
                Hex.Vars.FB.HexFBBLL.SessionBLL sessionbll = new SessionBLL();
                DataTable dt = sessionbll.GetFeedbackForEmployeeId(_Session, out errMessage);

                //Start-Get Comments Details // displaced on 25-02-2014 by Chirag
                string Comments = string.Empty;
                Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
                _Sessionid.Sessionid = ddlSession.Text;
                Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();
                DataTable dtComments = reportbll.GetfeedbackComments(_Sessionid, out errMessage);
                DataSet dsComments = new DataSet();
                dsComments.Tables.Add(dtComments);
                //Commented By Chirag on 28-02-2014
                //if (dsComments.Tables[0].Rows.Count != 0)
                //{
                //    StringBuilder builder = new StringBuilder();
                //    for (int i = 0; i < dsComments.Tables[0].Rows.Count; i++)
                //    {
                //        //if (dsComments.Tables[0].Rows[i]["CommentsValid"].ToString() == "1")  //COmmented By Chirag Mota on 25-02-2014
                //        {
                //            string tempComments = (i + 1).ToString() + "." + dsComments.Tables[0].Rows[i]["Comments"].ToString().Trim();

                //            builder.Append(tempComments);
                //            builder.Append(Environment.NewLine);
                //        }
                //        Comments = builder.ToString();
                //    }
                //    lblOverAllcmt.Visible = true;
                //    lblComments.Visible = true;
                //    lblComments.Text = Comments;
                //}
                //else
                //{
                //    lblOverAllcmt.Visible = true;
                //    lblComments.Visible = true;
                //    lblComments.Text = "Comments Not Yet approved.";
                //}
                //End-Get Comments Details

                //Added By Chirag Mota on 20-02-2014
                DataTable questions = new DataTable();
                questions.Columns.Add("Question No");
                questions.Columns.Add("Question");
                DataTable t = new DataTable("exceltable");
                if (ddlemployee.SelectedValue == "All")
                {
                    int count = 0;
                    string empId = null, sessionId = null;
                    t.Columns.Add("SessionId");
                    t.Columns.Add("Trainer");
                    t.Columns.Add("Employee Id");
                    t.Columns.Add("Employee Name");
                    t.Columns.Add("Q1");
                    t.Columns.Add("Q2");
                    t.Columns.Add("Q3");
                    t.Columns.Add("Q4");
                    t.Columns.Add("Q5");
                    t.Columns.Add("Q6");
                    t.Columns.Add("Q7");
                    t.Columns.Add("Q8");
                    t.Columns.Add("Q9");
                    t.Columns.Add("Q10");
                    t.Columns.Add("Q11");
                    t.Columns.Add("Q12");
                    t.Columns.Add("Q13");
                    t.Columns.Add("Q14");
                    t.Columns.Add("Q15");
                    t.Columns.Add("Q16");
                    t.Columns.Add("Q17");
                    t.Columns.Add("Q18");
                    t.Columns.Add("Comments");
                    DataRow newRow = t.NewRow();
                    DataRow newQuestion = questions.NewRow();
                    foreach (DataRow r in dt.Rows)
                    {
                        if (count == 0)
                        {
                            count++;
                            empId = r.ItemArray[4].ToString();
                            sessionId = r.ItemArray[3].ToString();
                            newRow["Employee Id"] = empId;
                            newRow["SessionId"] = sessionId;
                            newRow["Employee Name"] = r.ItemArray[5].ToString();
                            newRow["Trainer"] = _Session.Trainerid;
                        }
                        if (count <= 18)
                        {
                            newQuestion["Question No"] = r.ItemArray[0];
                            newQuestion["Question"] = r.ItemArray[1];
                            questions.Rows.Add(newQuestion);
                            newQuestion = questions.NewRow();
                            count++;
                        }
                        if (empId == r.ItemArray[4].ToString() && sessionId == r.ItemArray[3].ToString())
                        {
                            newRow["Q" + r.ItemArray[0].ToString()] = r.ItemArray[2].ToString();
                        }
                        else
                        {
                            foreach (DataRow comm in dsComments.Tables[0].Rows)
                            {
                                if (comm.ItemArray[0].ToString() == empId && comm.ItemArray[3].ToString() == _Session.Trainerid)
                                {
                                    newRow["Comments"] = comm.ItemArray[1].ToString();
                                }
                            }
                            t.Rows.Add(newRow);
                            newRow = t.NewRow();
                            empId = r.ItemArray[4].ToString();
                            sessionId = r.ItemArray[3].ToString();
                            newRow["SessionId"] = sessionId;
                            newRow["Employee Id"] = empId;
                            newRow["Employee Name"] = r.ItemArray[5].ToString();
                            newRow["Trainer"] = _Session.Trainerid;
                            newRow["Q" + r.ItemArray[0].ToString()] = r.ItemArray[2].ToString();
                            EmployeeCount += 1;
                        }
                    }
                    foreach (DataRow comm in dsComments.Tables[0].Rows)
                    {
                        if (comm.ItemArray[0].ToString() == empId && comm.ItemArray[3].ToString() == _Session.Trainerid)
                        {
                            newRow["Comments"] = comm.ItemArray[1].ToString();
                        }
                    }
                    t.Rows.Add(newRow);
                    //Added by Chirag on 26-02-2014
                    string errMessage1 = string.Empty;
                    int PendingCount = 0;
                    Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid1 = new Hex.Vars.FB.HexFBModule.Reportinfo();
                    _Sessionid1.Sessionid = ddlSession.Text;
                    Hex.Vars.FB.HexFBBLL.ReportBLL reportbll1 = new ReportBLL();
                    DataTable dt1 = reportbll1.GetFeedbackabsEmp(_Sessionid1, out errMessage1);
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        PendingCount = Convert.ToInt32(dt1.Rows[i]["Pending FeedBack"]);
                        if (PendingCount == 0 || PendingCount == -1)
                        {
                            dt1.Rows[i].Delete();
                        }
                    }
                    dt1.AcceptChanges();
                    DataTable t1 = new DataTable();
                    t1 = t.Copy();
                    int cnt = 0;
                    foreach (DataRow r1 in dt1.Rows)
                    {
                        foreach (DataRow r2 in t1.Rows)
                        {
                            cnt = 0;
                            if (r1.ItemArray[0].ToString() == r2.ItemArray[2].ToString())
                            {
                                cnt++;
                                break;
                            }
                        }
                        if (cnt == 0)
                        {
                            newRow = t.NewRow();
                            newRow["Employee Id"] = r1.ItemArray[0];
                            newRow["SessionId"] = _Sessionid1.Sessionid;
                            newRow["Employee Name"] = r1.ItemArray[1].ToString();
                            newRow["Trainer"] = _Session.Trainerid;
                            for (int i = 1; i <= 18; i++)
                            {
                                newRow["Q" + i.ToString()] = 0.ToString();
                            }
                            newRow["Comments"] = "";
                            t.Rows.Add(newRow);
                        }
                    }//end
                }
                //end 
                if (dt.Rows.Count == 12 && EmployeeCount !=2)
                {
                    pnlforrpt.Visible = false;
                    lblOverAllcmt.Visible = false;
                    lblComments.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Selected employee Not provides feedbacks for these trainers');", true);
                }
                else
                {
                    DataSet ds = new DataSet();
                    //Added by Chirag Mota on 20-02-2014
                    Session["CourseName"] = drpCourse.SelectedItem.ToString();
                    string[] ss = ddlSession.SelectedValue.Split('_');
                    Session["duration"] = ss[ss.Length-1];
                    Session["trainer"] = ddltrainers.SelectedItem.ToString();
                    if (_Session.empid == "All")
                    {
                        ds.Tables.Add(t);
                        Session["Feedback_All"] = t;
                        tdQuestions.Visible = true;
                        btnExcel.Enabled = true;
                    }
                    else
                    {
                        dt.Columns.Add("Comments");
                        dt.AcceptChanges();
                        foreach (DataRow comm in dsComments.Tables[0].Rows)
                        {

                            if (comm.ItemArray[0].ToString() == _Session.empid.ToString() && comm.ItemArray[3].ToString() == _Session.Trainerid)
                            {
                                dt.Rows[0]["Comments"] = comm.ItemArray[1];
                                break;
                            }
                        }
                        tdQuestions.Visible = false;
                        ds.Tables.Add(dt);
                        Session["Feedback_All"] = dt;
                        btnExcel.Enabled = true;
                    }
                    //end
                    pnlforrpt.Visible = true;
                    GrvOverallViews.DataSource = ds.Tables[0];
                    GrvOverallViews.DataBind();
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            GrvOverallViews.Rows[i].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                            GrvOverallViews.Rows[i].Cells[2].HorizontalAlign = HorizontalAlign.Center;
                            //Added by Chirag Mota on 20-02-2014
                            GrvOverallViews.Rows[i].Cells[1].HorizontalAlign = HorizontalAlign.Center;
                            GrvOverallViews.Rows[i].Cells[3].HorizontalAlign = HorizontalAlign.Center;
                            if (_Session.empid == "All")
                                GrvOverallViews.Rows[i].Cells[4].HorizontalAlign = HorizontalAlign.Center;
                            //end
                        }
                        ////Commented By Chirag
                        ////Start-Get Comments Details
                        //string Comments = string.Empty;
                        //Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
                        //_Sessionid.Sessionid = ddlSession.Text;
                        //Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();
                        //DataTable dtComments = reportbll.GetfeedbackComments(_Sessionid, out errMessage);
                        //DataSet dsComments = new DataSet();
                        //dsComments.Tables.Add(dtComments);
                        //if (dsComments.Tables[0].Rows.Count != 0)
                        //{
                        //    StringBuilder builder = new StringBuilder();
                        //    for (int i = 0; i < dsComments.Tables[0].Rows.Count; i++)
                        //    {
                        //        //if (dsComments.Tables[0].Rows[i]["CommentsValid"].ToString() == "1")  //COmmented By Chirag Mota on 25-02-2014
                        //        {
                        //            string tempComments = (i + 1).ToString() + "." + dsComments.Tables[0].Rows[i]["Comments"].ToString().Trim();

                        //            builder.Append(tempComments);
                        //            builder.Append(Environment.NewLine);
                        //        }
                        //        Comments = builder.ToString();
                        //    }
                        //    lblOverAllcmt.Visible = true;
                        //    lblComments.Visible = true;
                        //    lblComments.Text = Comments;
                        //}
                        //else
                        //{
                        //    lblOverAllcmt.Visible = true;
                        //    lblComments.Visible = true;
                        //    lblComments.Text = "Comments Not Yet approved.";
                        //}
                        ////End-Get Comments Details
                        //Session["Comments_All"] = Comments;//Added By Chirag Mota 20-02-2014
                    }
                    else
                    {
                        pnlforrpt.Visible = false;
                        lblOverAllcmt.Visible = false;
                        lblComments.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Selected employee Not provides feedbacks for these trainers');", true);
                    }
                    gvQuestions.DataSource = questions;
                    gvQuestions.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExcelExport.aspx");
        }
    }

}

#endregion