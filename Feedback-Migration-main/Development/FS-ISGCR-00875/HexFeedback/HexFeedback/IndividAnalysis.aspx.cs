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
                SqlDataReader dr1 = sessionbll.getSessionCourseWise(drpCourse.Text);
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
                Hex.Vars.FB.HexFBBLL.SessionBLL sessionbll = new SessionBLL();
                SqlDataReader dr1 = sessionbll.getSessionEmployeeIdWise(ddlSession.SelectedValue);
                ddlSession.DataSource = dr1;

                ddlemployee.DataSource = dr1;
                ddlemployee.DataMember = "Employee_id";
                ddlemployee.DataValueField = "Employee_id";
                ddlemployee.DataBind();

                ddlemployee.Items.Insert(0, new ListItem("--Select--", String.Empty));

                ddlemployee.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }

        }

        protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
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

        }
        protected void ddltrainers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string errMessage = string.Empty;
                Hex.Vars.FB.HexFBModule.Sessioninfo _Session = new Hex.Vars.FB.HexFBModule.Sessioninfo();


                _Session.session = ddlSession.SelectedItem.ToString();
                _Session.empid = ddlemployee.Text;
                _Session.Trainerid = ddltrainers.Text;
                Hex.Vars.FB.HexFBBLL.SessionBLL sessionbll = new SessionBLL();
                DataTable dt = sessionbll.GetFeedbackForEmployeeId(_Session, out errMessage);
                if (dt.Rows.Count == 12)
                {
                    pnlforrpt.Visible = false;
                    lblOverAllcmt.Visible = false;
                    lblComments.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Selected employee Not provides feedbacks for these trainers');", true);
                }
                else
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    pnlforrpt.Visible = true;
                    GrvOverallViews.DataSource = ds.Tables[0];
                    GrvOverallViews.DataBind();
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            GrvOverallViews.Rows[i].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                            GrvOverallViews.Rows[i].Cells[2].HorizontalAlign = HorizontalAlign.Center;
                        }
                        //Start-Get Comments Details
                        string Comments = string.Empty;
                        Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
                        _Sessionid.Sessionid = ddlSession.Text;
                        Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();
                        DataTable dtComments = reportbll.GetfeedbackComments(_Sessionid, out errMessage);
                        DataSet dsComments = new DataSet();
                        dsComments.Tables.Add(dtComments);
                        if (dsComments.Tables[0].Rows.Count != 0)
                        {
                            StringBuilder builder = new StringBuilder();
                            for (int i = 0; i < dsComments.Tables[0].Rows.Count; i++)
                            {
                                if (dsComments.Tables[0].Rows[i]["CommentsValid"].ToString() == "1")
                                {
                                    string tempComments = (i + 1).ToString() + "." + dsComments.Tables[0].Rows[i]["Comments"].ToString().Trim();

                                    builder.Append(tempComments);
                                    builder.Append(Environment.NewLine);
                                }
                                Comments = builder.ToString();
                            }
                            lblOverAllcmt.Visible = true;
                            lblComments.Visible = true;
                            lblComments.Text = Comments;
                        }
                        else
                        {
                            lblOverAllcmt.Visible = true;
                            lblComments.Visible = true;
                            lblComments.Text = "Comments Not Yet approved.";
                        }
                        //End-Get Comments Details
                    }
                    else
                    {
                        pnlforrpt.Visible = false;
                        lblOverAllcmt.Visible = false;
                        lblComments.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Selected employee Not provides feedbacks for these trainers');", true);
                    }
                }
                
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }
    }

}

#endregion