#region Header
/// ***********************************************************************************************
/// File name: Site.Master.cs
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
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;

#endregion

#region Class
namespace HexFeedback
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        Hex.Vars.FB.HexFBBLL.SessionBLL _session = new Hex.Vars.FB.HexFBBLL.SessionBLL();
        Hex.Vars.FB.HexFBModule.NominationListInfo nominationlist = new Hex.Vars.FB.HexFBModule.NominationListInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] != null || Session["IsSuperAdmin"] != null)
            {
                AdminMenu.Visible = true;
                year1.Visible = true;
                year2.Visible = true;
            }
            else
            {
                AdminMenu.Visible = false;
                year1.Visible = false;
                year2.Visible = false;
            }
            if (Session["username"] != null)
            {
                nominationlist.Flag = 1;
                SqlDataReader dr = _session.getindividual(Session["username"].ToString(), null, null, nominationlist.Flag);
                while (dr.Read())
                {
                    lblWelcomeMsg.Text = "Welcome, " + dr["AdminName"].ToString();
                }
                if (!IsPostBack)
                {
                    DataTable dtYear = _session.getYear();
                    ddlYear.DataSource = dtYear;
                    ddlYear.DataTextField = dtYear.Columns["code_id"].ToString();
                    ddlYear.DataValueField = dtYear.Columns["code_id"].ToString();
                    ddlYear.DataBind();
                    if (Session["Year"] == null)
                    {
                        Session["Year"] = ddlYear.SelectedValue;
                    }
                    else
                    {
                        ddlYear.SelectedValue = Session["Year"].ToString();
                    }
                }
            }
        }

        protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
        {

        }

        protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect(FormsAuthentication.LoginUrl);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["Year"] = ddlYear.SelectedValue;
                HiddenField hdnPage = (HiddenField)MainContent.FindControl("hdnPage");
                if (hdnPage != null)
                {
                    DataTable dt = new DataTable();
                    DropDownList ddlCourse = (DropDownList)MainContent.FindControl("drpCourse");
                    DropDownList ddlSession = new DropDownList();
                    SqlDataReader dr1 = _session.getSessionCourseWise(ddlCourse.SelectedValue, Convert.ToInt32(Session["Year"]));
                    if (hdnPage.Value == "FeedbackDef" || hdnPage.Value == "IndividualReport" || hdnPage.Value == "IndividAnalysis" || hdnPage.Value == "TrnEffectiveness")
                    {
                        ddlSession = (DropDownList)MainContent.FindControl("ddlSession");
                    }
                    else if (hdnPage.Value == "NominationList" || hdnPage.Value == "BalFeedback")
                    {
                        ddlSession = (DropDownList)MainContent.FindControl("drpSession");
                    }
                    ddlSession.DataSource = dr1;
                    ddlSession.DataMember = "session";
                    ddlSession.DataValueField = "sessionid";
                    ddlSession.DataBind();
                    ddlSession.Items.Insert(0, new ListItem("--Select--", String.Empty));
                    ddlSession.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
#endregion