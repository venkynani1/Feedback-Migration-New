#region Header
/// ***********************************************************************************************
/// File name: BalFeedback.aspx.cs
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
#endregion

#region class
namespace HexFeedback
{

    public partial class BalFeedback : System.Web.UI.Page
    {
        string urlparm = "";
        string urlparm1 = "";
        string Tempvar = "Temp";
        static string Tempsessionid=string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    Hex.Vars.FB.HexFBBLL.CourseBLL coursebll = new CourseBLL();
                    SqlDataReader dr = coursebll.getAllCourse();
                    drpCourse.DataSource = dr;
                    drpCourse.DataValueField = "courseid";
                    drpCourse.DataTextField = "course";
                    drpCourse.DataBind();
                    drpCourse.Items.Insert(0, new ListItem("--Select--", String.Empty));
                    drpCourse.SelectedIndex = 0;
                    urlparm = "toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,height=400px,width=150px,left=250px,top=250px";
                    urlparm1 = "toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,height=420,width=420,left=250,top=250";
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
                drpSession.DataSource = dr1;
                drpSession.DataMember = "session";
                drpSession.DataValueField = "sessionid";
                drpSession.DataBind();

                drpSession.Items.Insert(0, new ListItem("--Select--", String.Empty));

                drpSession.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }


        public void Getpattern(string sesid)
        {
            try
            {
                string errMessage = string.Empty;
                Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
                _Sessionid.Sessionid = sesid;
                Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();

                DataTable dt = reportbll.GetfeedbackBalance(_Sessionid, out errMessage);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                GrvBalance.DataSource = ds;
                GrvBalance.DataBind();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    GrvBalance.Rows[i].Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    GrvBalance.Rows[i].Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    GrvBalance.Rows[i].Cells[3].HorizontalAlign = HorizontalAlign.Center;
                    GrvBalance.Rows[i].Cells[4].HorizontalAlign = HorizontalAlign.Center;
                    GrvBalance.Rows[i].Cells[5].HorizontalAlign = HorizontalAlign.Center;
                    if (Convert.ToInt32(ds.Tables[0].Rows[i][4].ToString()) < 70)
                    {
                        int temp = Convert.ToInt32(ds.Tables[0].Rows[i][4].ToString());
                        GrvBalance.Rows[i].Cells[4].ForeColor = System.Drawing.Color.Red;

                    }
                    else
                    {
                        GrvBalance.Rows[i].Cells[4].ForeColor = System.Drawing.Color.Green;
                    }


                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }
        protected void GrvBalance_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                Label SelSessionid = (Label)GrvBalance.Rows[e.RowIndex].FindControl("LblSess");
                Label Complete = (Label)GrvBalance.Rows[e.RowIndex].FindControl("LblCompFeed");
                Tempsessionid = SelSessionid.Text;
                string CompleteFd = Complete.Text;
                if (CompleteFd != "0")
                {
                    Getpattern();
                    this.ModalPopupExtender1.Show();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('No Comments are given for session :" + Tempsessionid + "');", true);
                }

            }
            catch (Exception ex)
            {

                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }
        protected void drpSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSession.Text != "")
            {
                Getpattern(drpSession.Text);
            }
        }

        protected void GrdComments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                foreach (GridViewRow objects1 in GrdComments.Rows)
                {
                    if (e.CommandName == "Proceed")
                    {
                        string errMessage = string.Empty;

                        Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
                        _Sessionid.Sessionid = Tempsessionid;
                        Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();

                        DataTable dt = reportbll.GetfeedbackComments(_Sessionid, out errMessage);
                        DataSet ds = new DataSet();
                        ds.Tables.Add(dt);
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {

                            string EmployeeId = ((Label)GrdComments.Rows[j].Cells[0].FindControl("LblEmpId")).Text;
                            CheckBox sample = ((CheckBox)GrdComments.Rows[j].Cells[0].FindControl("ChkDisplay"));
                            Hex.Vars.FB.HexFBModule.NominationListInfo nominationlist = new Hex.Vars.FB.HexFBModule.NominationListInfo();
                            NominationListBLL nominationbll = new NominationListBLL();
                            string errmessage = string.Empty;
                            bool IsRecordExist = false;
                            int rowsAffected = 0;
                            nominationlist.EmployeeId = EmployeeId;

                            if (sample.Checked == true)
                            {
                                nominationlist.Comments = "1";
                            }
                            else
                            {
                                nominationlist.Comments = "0";
                            }
                            nominationlist.SessionId = Tempsessionid;
                            nominationbll.UpdateVaildComments(nominationlist, out errmessage, out IsRecordExist, out rowsAffected);
                            ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Valid Comments are updated.');", true);


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }

        }
        public void Getpattern()
        {
            string errMessage = string.Empty;

            Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
            _Sessionid.Sessionid = Tempsessionid;
            Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();

            DataTable dt = reportbll.GetfeedbackComments(_Sessionid, out errMessage);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            GrdComments.DataSource = ds;
            GrdComments.DataBind();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["CommentsValid"].ToString() == "1")
                {
                    CheckBox sample = ((CheckBox)GrdComments.Rows[i].Cells[0].FindControl("ChkDisplay"));
                    sample.Checked = true;
                }
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GrdComments.Rows[i].Cells[2].HorizontalAlign = HorizontalAlign.Center;
                GrdComments.Rows[i].Cells[1].HorizontalAlign = HorizontalAlign.Left;
                GrdComments.Rows[i].Cells[3].HorizontalAlign = HorizontalAlign.Center;
            }

        }
    }
}
#endregion


