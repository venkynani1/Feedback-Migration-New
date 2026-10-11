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

namespace HexFeedback
{
    public partial class TrainingEffectivenessReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populateMasterDdl();
            }
        }
        protected void populateMasterDdl()
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
            string error=string.Empty;
            ReportBLL Report=new ReportBLL();
            DataSet dsReport = Report.GetTrnEffectivenessReport(ddlSession.SelectedValue.ToString(),out error);
            if (dsReport.Tables.Count > 0)
            {
                gvTrnEffectReport.DataSource = dsReport.Tables[0];
                gvTrnEffectReport.DataBind();
                DataSet ds = dsReport.Copy();
                ds.Tables[0].Columns.Remove("serial number");
                Session["TrnEffectiveReport"] = ds;
                btnExcel.Enabled = true;
            }
            else
            {
                btnExcel.Enabled = false;
            }

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ExcelExport.aspx?ReportType=TrnEffective"); // For Test
            //Response.Redirect("http://stationh.hexaware.com/portal/TrainingFeedback/ExcelExport.aspx?ReportType=TrnEffective"); //for Production
        }

        protected void gvTrnEffectReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

    }
}