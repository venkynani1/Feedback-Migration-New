using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace HexFeedback
{
    public partial class ExcelExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.Expires = -1;
            Response.ClearContent();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=FeedbackDetails.xls");
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            StringWriter sw1 = new StringWriter();
            HtmlTextWriter htw1 = new HtmlTextWriter(sw1);
            tdSession.InnerText = " : " + Session["CourseName"].ToString();
            tdDate.InnerText = " : " + Session["duration"].ToString();
            tdTrainer.InnerText = " : " + Session["trainer"].ToString();
            tblDetails.RenderControl(htw1);
            Response.Write(sw1.ToString());
            Page.EnableViewState = true;
            gvFeedback.DataSource = (DataTable)Session["Feedback_All"];
            gvFeedback.DataBind();
            gvFeedback.AllowPaging = false;
            gvFeedback.RenderControl(htw);
            Response.Write(sw.ToString());


            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void gvFeedback_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }

        }

    }
}