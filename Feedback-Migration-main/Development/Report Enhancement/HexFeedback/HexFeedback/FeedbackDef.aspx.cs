#region Header
/// ***********************************************************************************************
/// File name: FeedbackDef.aspx.cs
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

#endregion

#region class
namespace HexFeedback
{

    public partial class FeedbackDef : System.Web.UI.Page
    {
        int flag = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
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
                pnlforrpt.Visible = true;
                Getpattern();
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }

        }
        public void Getpattern()
        {
            string errMessage = string.Empty;
            int PendingCount = 0;
            Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
            _Sessionid.Sessionid = ddlSession.Text;
            Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();
            DataTable dt = reportbll.GetFeedbackabsEmp(_Sessionid, out errMessage);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PendingCount = Convert.ToInt32(dt.Rows[i]["Pending FeedBack"]);
                if (PendingCount == 0 || PendingCount == -1)
                {
                    dt.Rows[i].Delete();
                }
            }
            dt.AcceptChanges();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            if (ds.Tables[0].Rows.Count != 0)
            {
                pnlforrpt.Visible = true;
                GrvEmplId.DataSource = ds;
                GrvEmplId.DataBind();
                if (GrvEmplId.Rows.Count == 0)
                {
                    pnlforrpt.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('All the feedfacks are completed by employee.');", true);
                }
                else
                {
                    for (int i = 0; i <ds.Tables[0].Rows.Count; i++)
                    {
                            GrvEmplId.Rows[i].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                            GrvEmplId.Rows[i].Cells[1].HorizontalAlign = HorizontalAlign.Center;
                            GrvEmplId.Rows[i].Cells[2].HorizontalAlign = HorizontalAlign.Left;
                            GrvEmplId.Rows[i].Cells[3].HorizontalAlign = HorizontalAlign.Center;
                            GrvEmplId.Rows[i].Cells[4].HorizontalAlign = HorizontalAlign.Center;
                            GrvEmplId.Rows[i].Cells[5].HorizontalAlign = HorizontalAlign.Center;
                            GrvEmplId.Rows[i].Cells[6].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
            else
            {
                pnlforrpt.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Record Not found');", true);
            }
        }

    }
}

#endregion