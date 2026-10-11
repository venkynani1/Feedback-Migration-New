#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using log4net;
using System.Data.Common;
using System.Web.Services;

#endregion

namespace HexFeedback.SkillPort
{
    public partial class WebForm1 : Basepage
    {
        #region "Variables & Declarations"
        public string DBConnection;
        ILog objILog = log4net.LogManager.GetLogger("SkillportUpload.aspx.cs");
        string strExcellConnectionString = string.Empty;
        #endregion

        #region "Page_Load"
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                fdView.Visible = false;
                btnDelete.Visible = false;
                if (!IsPostBack)
                {
                    LoadMonths();
                    LoadYears();
                }
            }
            catch (Exception ex)
            {
                LogException(ex, objILog);
                ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                + " alert('An unexpected error occured. Please contact system administrator.');</script>");
            }
        }
        #endregion

        #region "Button Events"

        #region "btnUpload_Click"
        /// <summary>
        /// Handles the Click event of the btnUpload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                UploadExcelData();
            }
            catch (Exception ex)
            {
                ddlMonth.SelectedIndex = 0;
                ddlYear.SelectedIndex = 0;
                rblExcelUpload.SelectedIndex = -1;
                LogException(ex, objILog);
                ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                     + " alert('An unexpected error occured. Please contact system administrator.');</script>");

            }
        }
        #endregion

        #region "btnView_Click"
        protected void btnView_Click(object sender, EventArgs e)
        {

            Entity objEntity = new Entity();
            DataSet dsViewRecords = new DataSet();
            gvCertificatesView.Visible = false;
            try
            {
                if (null != Session["username"])
                {
                    objEntity.EmpID = Convert.ToString(Session["username"]);
                    objEntity.Month = ddlMonth.SelectedValue;
                    objEntity.Year = ddlYear.SelectedValue;
                    if (rblExcelUpload.Items[0].Selected == true)
                    {
                        dsViewRecords = skillport_DAO.GetBusinessDuplicatRecordDetails(objEntity);
                    }
                    else if (rblExcelUpload.Items[1].Selected == true)
                    {
                        dsViewRecords = skillport_DAO.GetTCDPDuplicatRecordDetails(objEntity);
                    }
                    else if (rblExcelUpload.Items[2].Selected == true)
                    {
                        dsViewRecords = skillport_DAO.GetCertificationsDuplicatRecordDetails(objEntity);
                    }
                    else if (rblExcelUpload.Items[3].Selected == true)
                    {
                        dsViewRecords = skillport_DAO.GetContributionsDuplicatRecordDetails(objEntity);
                    }
                    else if (rblExcelUpload.Items[4].Selected == true)
                    {
                        dsViewRecords = skillport_DAO.GetSkillPortDuplicatRecordDetails(objEntity);
                    }
                    else if (rblExcelUpload.Items[5].Selected == true)
                    {
                        dsViewRecords = skillport_DAO.GetTrainingHoursDuplicatRecordDetails(objEntity);
                    }

                    if (dsViewRecords.Tables[0].Rows.Count > 0)
                    {
                        fdView.Visible = true;
                        btnDelete.Visible = true;
                        gvCertificatesView.Visible = true;
                        gvCertificatesView.DataSource = dsViewRecords.Tables[0];
                        gvCertificatesView.DataBind();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                         + " alert('No records to display.');</script>");

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region "Private Methods"

        #region "UploadExcelData"
        /// <summary>
        /// Uploads the excel data.
        /// </summary>
        private void UploadExcelData()
        {
            string strExtension = string.Empty;
            string strFilePath = string.Empty;
            string strTempFileName = string.Empty;
            string strCommonPath = string.Empty;
            try
            {
                if (ddlMonth.SelectedIndex > 0 && ddlYear.SelectedIndex > 0)
                {
                    if (!(ddlYear.SelectedValue == Convert.ToString(DateTime.Now.Year) && Convert.ToInt32(ddlMonth.SelectedValue) > Convert.ToUInt32(DateTime.Now.Month)))
                    {
                        if (rblExcelUpload.SelectedIndex != -1)
                        {
                            if (FileUploadToServer.HasFile)
                            {
                                try
                                {
                                    string[] strallowdFile = { ".XLS", ".XLSX" };
                                    string strFileExt = Path.GetExtension(FileUploadToServer.PostedFile.FileName);
                                    bool isValidExtn = false;
                                    foreach (string strItem in strallowdFile)
                                    {
                                        isValidExtn = strItem.Contains(strFileExt.ToUpper());
                                        if (!isValidExtn)
                                            break;
                                    }
                                    if (FileUploadToServer.PostedFile != null && FileUploadToServer.PostedFile.ContentLength > 0)
                                    {
                                        string strFileName = Path.GetFileName(FileUploadToServer.PostedFile.FileName);

                                        strCommonPath = ConfigurationManager.AppSettings["COMMONPATH"];
                                        if (strFileExt == ".xlsx")
                                            strTempFileName = strFileName.Substring(0, strFileName.Length - 1);
                                        else
                                            strTempFileName = strFileName;
                                        strFilePath = strCommonPath + strTempFileName;
                                        Session["Filepath"] = strFilePath;
                                        if (Directory.Exists(strCommonPath))
                                        {

                                            FileUploadToServer.SaveAs(strFilePath);
                                            strExtension = Path.GetExtension(strFilePath);
                                        }
                                        else
                                        {
                                            Directory.CreateDirectory(strCommonPath);
                                            FileUploadToServer.SaveAs(strFilePath);
                                            strExtension = Path.GetExtension(strFilePath);
                                        }
                                        if (!string.IsNullOrEmpty(strTempFileName) && !string.IsNullOrEmpty(strFilePath) && !string.IsNullOrEmpty(strExtension))
                                        {
                                            if (strExtension == ".xls" || strExtension == ".xlsx")
                                            {
                                                strExcellConnectionString = skillport_DAO.GetExcellConnectionString(strFilePath, strExtension);
                                                if (!string.IsNullOrEmpty(strExcellConnectionString))
                                                {

                                                    if (rblExcelUpload.Items[0].Selected == true)
                                                    {
                                                        GetBehavioralSummaryDetails(strFilePath);
                                                    }
                                                    else if (rblExcelUpload.Items[1].Selected == true)
                                                    {
                                                        GetTCDPDetail(strFilePath);
                                                    }
                                                    else if (rblExcelUpload.Items[2].Selected == true)
                                                    {
                                                        GetCertificationsDetail(strFilePath);
                                                    }
                                                    else if (rblExcelUpload.Items[3].Selected == true)
                                                    {
                                                        GetContributionsDetail(strFilePath);
                                                    }
                                                    else if (rblExcelUpload.Items[4].Selected == true)
                                                    {
                                                        GetSkillportExcelDetail(strFilePath);
                                                    }
                                                    else if (rblExcelUpload.Items[5].Selected == true)
                                                    {
                                                        GetTrainingHoursDetail(strFilePath);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                                                    + " alert('The file should be in excel format only.');</script>");
                                            }
                                        }
                                        else
                                        {
                                            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                                       + " alert('Failed to save data. Please check the input or contact system administrator.');</script>");
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    if (File.Exists(strFilePath))
                                        File.Delete(strFilePath);
                                    LogException(ex, objILog);
                                    ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                                        + " alert('An unexpected error occured. Please contact system administrator.');</script>");
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                                         + " alert('Please select the file to upload');</script>");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                            + " alert('Please select Radio Button');</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                           + " alert('Please check the month of your request.');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                                + " alert('Please select the Month & Year');</script>");
                }
            }
            catch
            {
                throw;
            }
            progress.Style["display"] = "none";
        }
        #endregion

        #region "GetBehavioralSummaryDetails"
        /// <summary>
        /// Gets the Behavioral Summary Details .
        /// </summary>
        /// <param name="strFilePath">The STR file path.</param>
        private void GetBehavioralSummaryDetails(string strFilePath)
        {
            Entity objEntity = new Entity();
            int intResult = 0;
            DataSet dsDuplicateRecords;
            try
            {
                if (null != Session["username"])
                {
                    objEntity.EmpID = Convert.ToString(Session["username"]);
                    objEntity.Month = ddlMonth.SelectedValue;
                    objEntity.Year = ddlYear.SelectedValue;
                    objEntity.FilePath = strFilePath;
                    dsDuplicateRecords = skillport_DAO.GetBusinessDuplicatRecordDetails(objEntity);
                    if (dsDuplicateRecords.Tables[0].Rows.Count > 0)
                    {
                        //ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                        // + "fnConfirm1();</script>");
                        ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                         + "alert('Some records already exists. Please delete those records to add new ones')</script>");
                        fdView.Visible = true;
                        btnDelete.Visible = true;
                        gvCertificatesView.Visible = true;
                        gvCertificatesView.DataSource = dsDuplicateRecords.Tables[0];
                        gvCertificatesView.DataBind();
                    }
                    else
                    {
                        intResult = skillport_DAO.ImportBehavioralSummaryDetails(objEntity);
                        ddlMonth.SelectedIndex = 0;
                        ddlYear.SelectedIndex = 0;
                        rblExcelUpload.SelectedIndex = -1;
                        if (File.Exists(strFilePath))
                            File.Delete(strFilePath);
                        if (intResult > 0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                            + " alert('Totally " + Convert.ToString(intResult) + " Record(s) saved successfully');</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                         + " alert('Invalid File. Please Check the Columns of the File.');</script>");
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region "GetTCDPDetail"
        /// <summary>
        /// Gets the TCDP Details .
        /// </summary>
        /// <param name="strFilePath">The STR file path.</param>
        private void GetTCDPDetail(string strFilePath)
        {
            Entity objEntity = new Entity();
            int intResult = 0;
            DataSet dsDuplicateRecords;
            try
            {
                if (null != Session["username"])
                {
                    objEntity.EmpID = Convert.ToString(Session["username"]);
                    objEntity.Month = ddlMonth.SelectedValue;
                    objEntity.Year = ddlYear.SelectedValue;
                    objEntity.FilePath = strFilePath;
                    dsDuplicateRecords = skillport_DAO.GetTCDPDuplicatRecordDetails(objEntity);
                    if (dsDuplicateRecords.Tables[0].Rows.Count > 0)
                    {
                        //ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                        // + "fnConfirm1();</script>");
                        ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                         + "alert('Some records already exists. Please delete those records to add new ones')</script>");
                        fdView.Visible = true;
                        btnDelete.Visible = true;
                        gvCertificatesView.Visible = true;
                        gvCertificatesView.DataSource = dsDuplicateRecords.Tables[0];
                        gvCertificatesView.DataBind();
                    }
                    else
                    {
                        intResult = skillport_DAO.ImportTCDPDetails(objEntity);
                        ddlMonth.SelectedIndex = 0;
                        ddlYear.SelectedIndex = 0;
                        rblExcelUpload.SelectedIndex = -1;
                        if (File.Exists(strFilePath))
                            File.Delete(strFilePath);
                        if (intResult > 0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                            + " alert('Totally " + Convert.ToString(intResult) + " Record(s) saved successfully');</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                         + " alert('Invalid File. Please Check the Columns of the File.');</script>");
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region "GetCertificationsDetail"
        /// <summary>
        /// Gets the skillport excel detail.
        /// </summary>
        /// <param name="strFilePath">The STR file path.</param>
        private void GetCertificationsDetail(string strFilePath)
        {
            Entity objEntity = new Entity();
            int intResult = 0;
            DataSet dsDuplicateRecords;
            try
            {
                if (null != Session["username"])
                {
                    objEntity.EmpID = Convert.ToString(Session["username"]);
                    objEntity.Month = ddlMonth.SelectedValue;
                    objEntity.Year = ddlYear.SelectedValue;
                    objEntity.FilePath = strFilePath;
                    dsDuplicateRecords = skillport_DAO.GetCertificationsDuplicatRecordDetails(objEntity);

                    if (dsDuplicateRecords.Tables[0].Rows.Count > 0)
                    {
                        //ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                        // + "fnConfirm1();</script>");
                        ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                         + "alert('Some records already exists. Please delete those records to add new ones')</script>");
                        fdView.Visible = true;
                        btnDelete.Visible = true;
                        gvCertificatesView.Visible = true;
                        gvCertificatesView.DataSource = dsDuplicateRecords.Tables[0];
                        gvCertificatesView.DataBind();
                    }
                    else
                    {
                        intResult = skillport_DAO.ImportCertificationsDetails(objEntity);
                        ddlMonth.SelectedIndex = 0;
                        ddlYear.SelectedIndex = 0;
                        rblExcelUpload.SelectedIndex = -1;
                        if (File.Exists(strFilePath))
                            File.Delete(strFilePath);
                        if (intResult > 0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                            + " alert('Totally " + Convert.ToString(intResult) + " Record(s) saved successfully');</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                         + " alert('Invalid File. Please Check the Columns of the File.');</script>");
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region "GetContributionsDetail"
        /// <summary>
        /// Gets the skillport excel detail.
        /// </summary>
        /// <param name="strFilePath">The STR file path.</param>
        private void GetContributionsDetail(string strFilePath)
        {
            Entity objEntity = new Entity();
            int intResult = 0;
            DataSet dsDuplicateRecords;
            try
            {
                if (null != Session["username"])
                {
                    objEntity.EmpID = Convert.ToString(Session["username"]);
                    objEntity.Month = ddlMonth.SelectedValue;
                    objEntity.Year = ddlYear.SelectedValue;
                    objEntity.FilePath = strFilePath;
                    dsDuplicateRecords = skillport_DAO.GetContributionsDuplicatRecordDetails(objEntity);

                    if (dsDuplicateRecords.Tables[0].Rows.Count > 0)
                    {
                        //ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                        // + "fnConfirm1();</script>");
                        ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                         + "alert('Some records already exists. Please delete those records to add new ones')</script>");
                        fdView.Visible = true;
                        btnDelete.Visible = true;
                        gvCertificatesView.Visible = true;
                        gvCertificatesView.DataSource = dsDuplicateRecords.Tables[0];
                        gvCertificatesView.DataBind();
                    }
                    else
                    {
                        intResult = skillport_DAO.ImportContributionsDetails(objEntity);
                        ddlMonth.SelectedIndex = 0;
                        ddlYear.SelectedIndex = 0;
                        rblExcelUpload.SelectedIndex = -1;
                        if (File.Exists(strFilePath))
                            File.Delete(strFilePath);
                        if (intResult > 0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                            + " alert('Totally " + Convert.ToString(intResult) + " Record(s) saved successfully');</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                         + " alert('Invalid File. Please Check the Columns of the File.');</script>");
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region "GetTrainingHoursDetail"
        /// <summary>
        /// Gets the skillport excel detail.
        /// </summary>
        /// <param name="strFilePath">The STR file path.</param>
        private void GetTrainingHoursDetail(string strFilePath)
        {
            Entity objEntity = new Entity();
            int intResult = 0;
            DataSet dsDuplicateRecords;
            try
            {
                if (null != Session["username"])
                {
                    objEntity.EmpID = Convert.ToString(Session["username"]);
                    objEntity.Month = ddlMonth.SelectedValue;
                    objEntity.Year = ddlYear.SelectedValue;
                    objEntity.FilePath = strFilePath;
                    dsDuplicateRecords = skillport_DAO.GetTrainingHoursDuplicatRecordDetails(objEntity);

                    if (dsDuplicateRecords.Tables[0].Rows.Count > 0)
                    {
                        //ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                        // + "fnConfirm1();</script>");
                        ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                         + "alert('Some records already exists. Please delete those records to add new ones')</script>");
                        fdView.Visible = true;
                        btnDelete.Visible = true;
                        gvCertificatesView.Visible = true;
                        gvCertificatesView.DataSource = dsDuplicateRecords.Tables[0];
                        gvCertificatesView.DataBind();
                    }
                    else
                    {
                        intResult = skillport_DAO.ImportTrainingHoursDetails(objEntity);
                        ddlMonth.SelectedIndex = 0;
                        ddlYear.SelectedIndex = 0;
                        rblExcelUpload.SelectedIndex = -1;
                        if (File.Exists(strFilePath))
                            File.Delete(strFilePath);
                        if (intResult > 0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                            + " alert('Totally " + Convert.ToString(intResult) + " Record(s) saved successfully');</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                         + " alert('Invalid File. Please Check the Columns of the File.');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ddlMonth.SelectedIndex = 0;
                ddlYear.SelectedIndex = 0;
                rblExcelUpload.SelectedIndex = -1;
                if (File.Exists(strFilePath))
                    File.Delete(strFilePath);
                LogException(ex, objILog);
                ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                + " alert('Please check the columns before upload');</script>");
            }
        }

        #endregion

        #region "GetSkillportExcelDetail"
        /// <summary>
        /// Gets the skillport excel detail.
        /// </summary>
        /// <param name="strFilePath">The STR file path.</param>
        private void GetSkillportExcelDetail(string strFilePath)
        {
            Entity objEntity = new Entity();
            int intResult = 0;
            DataSet dsDuplicateRecords;
            try
            {
                if (null != Session["username"])
                {
                    objEntity.EmpID = Convert.ToString(Session["username"]);
                    objEntity.Month = ddlMonth.SelectedValue;
                    objEntity.Year = ddlYear.SelectedValue;
                    objEntity.FilePath = strFilePath;
                    dsDuplicateRecords = skillport_DAO.GetSkillPortDuplicatRecordDetails(objEntity);

                    if (dsDuplicateRecords.Tables[0].Rows.Count > 0)
                    {
                        //ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                        // + "fnConfirm1();</script>");
                        ClientScript.RegisterStartupScript(GetType(), "script", "<script language='javascript' type='text/javascript'>"
                         + "alert('Some records already exists. Please delete those records to add new ones')</script>");
                        fdView.Visible = true;
                        btnDelete.Visible = true;
                        gvCertificatesView.Visible = true;
                        gvCertificatesView.DataSource = dsDuplicateRecords.Tables[0];
                        gvCertificatesView.DataBind();
                    }
                    else
                    {
                        intResult = skillport_DAO.ImportSkillPortDetails(objEntity);
                        ddlMonth.SelectedIndex = 0;
                        ddlYear.SelectedIndex = 0;
                        rblExcelUpload.SelectedIndex = -1;
                        if (File.Exists(strFilePath))
                            File.Delete(strFilePath);
                        if (intResult > 0)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                            + " alert('Totally " + Convert.ToString(intResult) + " Record(s) saved successfully');</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "script", "<script language='javascript' type='text/javascript'>"
                         + " alert('Invalid File. Please Check the Columns of the File.');</script>");
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region "LoadMonths"
        /// <summary>
        /// Loads the months.
        /// </summary>
        public void LoadMonths()
        {
            Dictionary<string, string> dicMonths = new Dictionary<string, string>();
            DateTime dtCurDate = new DateTime();
            try
            {
                string[] strMonth = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

                dicMonths.Add("-1", "--Select--");

                for (int intCount = 1; intCount <= strMonth.Length; intCount++)
                {
                    if (intCount >= 10)
                    {
                        dicMonths.Add(Convert.ToString(intCount), strMonth[intCount - 1]);

                    }
                    else
                    {
                        dicMonths.Add(Convert.ToString(intCount), strMonth[intCount - 1]);
                    }
                }
                dtCurDate = DateTime.Now;
                ddlMonth.DataSource = dicMonths;
                ddlMonth.DataTextField = "Value";
                ddlMonth.DataValueField = "Key";
                ddlMonth.DataBind();
                if (dtCurDate.Month >= 10)
                    ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(Convert.ToString(dtCurDate.Month)));
                else
                    ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(Convert.ToString(dtCurDate.Month)));
                ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByText(Convert.ToString(dtCurDate.Month)));
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region "LoadYears"
        /// <summary>
        /// Loads the years.
        /// </summary>
        public void LoadYears()
        {
            DateTime dtCurDate = new DateTime();
            try
            {
                Dictionary<string, string> dicYears = new Dictionary<string, string>();
                dtCurDate = DateTime.Now;
                dicYears.Add("-1", "--Select--");
                for (int intCount = 2010; intCount <= dtCurDate.Year; intCount++)
                {
                    dicYears.Add(Convert.ToString(intCount), Convert.ToString(intCount));
                }
                ddlYear.DataSource = dicYears;
                ddlYear.DataTextField = "Value";
                ddlYear.DataValueField = "Key";
                ddlYear.DataBind();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region "WebMethod"
        //[WebMethod(EnableSession = true)]
        //public static int getExcelData(string ddlMonth, string ddlYear, string rblSelectedValue)
        //{
        //    Basepage objBasepage = new Basepage();
        //    ILog objILog = log4net.LogManager.GetLogger("SkillportUpload.aspx.cs");
        //    Entity objEntity = new Entity();
        //    Page objPage = new Page();
        //    int intResult = 0;
        //    try
        //    {
        //        objEntity.Month = ddlMonth;
        //        objEntity.Year = ddlYear;
        //        objEntity.EmpID = Convert.ToString(HttpContext.Current.Session["username"]);
        //        objEntity.FilePath = Convert.ToString(HttpContext.Current.Session["Filepath"]);

        //        if (rblSelectedValue == "SkillPort")
        //        {
        //            intResult = skillport_DAO.GetSkillPortDetails(objEntity);

        //        }
        //        else if (rblSelectedValue == "Learning Plan for Behavioral")
        //        {
        //            intResult = skillport_DAO.GetBehaviorlaSummaryDetails(objEntity);
        //        }
        //        else if (rblSelectedValue == "Learning Plan for TCDP")
        //        {
        //            intResult = skillport_DAO.GetTCDPDetails(objEntity);
        //        }
        //        else if (rblSelectedValue == "Certifications")
        //        {
        //            intResult = skillport_DAO.GetCertificationsDetails(objEntity);
        //        }
        //        else if (rblSelectedValue == "Contributions")
        //        {
        //            intResult = skillport_DAO.GetContributionsDetails(objEntity);
        //        }
        //        else if (rblSelectedValue == "TrainingHours")
        //        {
        //            intResult = skillport_DAO.GetTrainingHoursDetails(objEntity);
        //        }
        //        return intResult;
        //    }
        //    catch (Exception)
        //    {
        //        return intResult;
        //        throw;
        //    }
        //}

        [WebMethod(EnableSession = true)]
        public static int delExcelData(string ddlMonth, string ddlYear, string rblSelectedValue)
        {
            Basepage objBasepage = new Basepage();
            ILog objILog = log4net.LogManager.GetLogger("SkillportUpload.aspx.cs");
            Entity objEntity = new Entity();
            Page objPage = new Page();
            int intResult = 0;
            try
            {
                objEntity.Month = ddlMonth;
                objEntity.Year = ddlYear;
                objEntity.EmpID = Convert.ToString(HttpContext.Current.Session["username"]);

                if (rblSelectedValue == "Learning Plan for PMS")
                {
                    intResult = skillport_DAO.DeleteSkillPortDetails(objEntity);
                }
                    else if (rblSelectedValue == "Learning Plan for TCDP")
                {
                    intResult = skillport_DAO.DeleteTCDPDetails(objEntity);
                }
                else if (rblSelectedValue == "Learning Plan for Behavioral")
                {
                    intResult = skillport_DAO.DeleteBehaviorlaSummaryDetails(objEntity);
                }
                else if (rblSelectedValue == "Certifications")
                {
                    intResult = skillport_DAO.DeleteCertificationsDetails(objEntity);
                }
                else if (rblSelectedValue == "Contributions")
                {
                    intResult = skillport_DAO.DeleteContributionsDetails(objEntity);
                }
                else if (rblSelectedValue == "TrainingHours")
                {
                    intResult = skillport_DAO.DeleteTrainingHoursDetails(objEntity);
                }
                return intResult;
            }
            catch (Exception)
            {
                return intResult;
                throw;
            }
        }
        #endregion

        #endregion
    }
}