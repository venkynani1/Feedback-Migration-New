#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
#endregion

namespace HexFeedback.SkillPort
{
    public partial class LearningProgramReport : Basepage
    {
        #region "Variables & Declarations"
        DataSet dsLearningProgram = new DataSet();
        DataTable dtLearningProgram = new DataTable();
        ILog objILog = log4net.LogManager.GetLogger("LearningProgramReport.aspx.cs");
        #endregion

        #region "Page_Load"
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        getLearningProgramDetail();
                    }
                }
                catch(Exception ex)
                {
                    LogException(ex, objILog);
                    ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                    + " alert('An unexpected error occured. Please contact system administrator.');</script>");
                }
            }
        }
        #endregion

        #region "Image Button Events"

        #region "ImgExcelBtn_Click"
        /// <summary>
        /// Handles the Click event of the ImgExcelBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImgExcelBtn_Click(object sender, ImageClickEventArgs e)
        {
            dsLearningProgram = skillport_DAO.GetLearningProgramReportDetail();
            dtLearningProgram = dsLearningProgram.Tables[0];
            try
            {
                skillport_DAO.ExportDataTableToExcel(dtLearningProgram);
            }
            catch (Exception ex)
            {
                LogException(ex, objILog);
                ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                + " alert('An unexpected error occured. Please contact system administrator.');</script>");
            }
        }
        #endregion

        #endregion

        #region "Private Methods"

        #region "getLearningProgramDetail"
        /// <summary>
        /// Gets the learning program detail.
        /// </summary>
        public void getLearningProgramDetail()
        {
            dsLearningProgram = skillport_DAO.GetLearningProgramReportDetail();
            try
            {
                if (dsLearningProgram.Tables[0].Rows.Count > 0)
                {
                    grdLearningProgramExcel.DataSource = dsLearningProgram.Tables[0];
                    grdLearningProgramExcel.DataBind();
                }
                else
                {
                    divBorder.Visible = false;
                    imgexcel.Enabled = false;   
                }
            }
            catch (Exception ex)
            {
                LogException(ex, objILog);
                ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                + " alert('An unexpected error occured. Please contact system administrator.');</script>");
            }
        }
        #endregion

        #endregion

        #region "GridView Events"

        #region "grdLearningProgramExcel_RowDataBound"
        /// <summary>
        /// Handles the RowDataBound event of the grdLearningProgramExcel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdLearningProgramExcel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblSerialNo = (Label)e.Row.FindControl("lblSerialNo");
                    lblSerialNo.Text = Convert.ToString(grdLearningProgramExcel.PageIndex * grdLearningProgramExcel.PageSize
                                + e.Row.RowIndex + 1) + ".";
                }
            }
            catch (Exception ex)
            {
                LogException(ex, objILog);
                ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                + " alert('An unexpected error occured. Please contact system administrator.');</script>");
            }
        }
        #endregion

        #endregion
    }
}