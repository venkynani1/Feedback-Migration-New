#region "References"
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using log4net;
using log4net.Config;

#endregion

/// <summary>
/// Summary description for Base
/// </summary>
public class Basepage : System.Web.UI.Page
{
    public Basepage()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region "LogException"
    /// <summary>
    /// Logs the exception.
    /// </summary>
    /// <param name="objException">The obj exception.</param>
    /// <param name="objILog">The obj I log.</param>
    public void LogException(Exception objException, ILog objILog)
    {
        System.Text.StringBuilder sb = null;
        try
        {
            if (objException.Message != "Thread was being aborted.")
            {
                sb = new System.Text.StringBuilder();
                sb.Append("Exception Message : " + objException.Message + Environment.NewLine + Environment.NewLine);
                if (null != objException.InnerException)
                    sb.Append("Inner Exception   : " + objException.InnerException + Environment.NewLine + Environment.NewLine);
                else
                    sb.Append("Inner Exception   : N/A" + Environment.NewLine + Environment.NewLine);
                sb.Append("StackTrace        : " + Environment.NewLine + objException.StackTrace + Environment.NewLine + Environment.NewLine);
                sb.Append("Source of Method  : " + objException.TargetSite.ToString() + Environment.NewLine + Environment.NewLine);
                if (null != HttpContext.Current.Session["EMPID"])
                    sb.Append("User Details      : " + HttpContext.Current.Session["EMPID"] + Environment.NewLine + Environment.NewLine);
                objILog.Error(sb.ToString());
            }
        }
        catch (Exception ex)
        {
            sb = new System.Text.StringBuilder();
            sb.Append("Exception Message : Exception occured while logging the exception." + ex.Message + Environment.NewLine + Environment.NewLine);
            if (null != ex.InnerException)
                sb.Append("Inner Exception   : " + ex.InnerException + Environment.NewLine + Environment.NewLine);
            else
                sb.Append("Inner Exception   : N/A" + Environment.NewLine + Environment.NewLine);
            sb.Append("StackTrace        : " + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine);
            sb.Append("Source of Method  : " + ex.TargetSite.ToString() + Environment.NewLine + Environment.NewLine);
            if (null != HttpContext.Current.Session["EMPID"])
                sb.Append("User Details      : " + HttpContext.Current.Session["EMPID"] + Environment.NewLine + Environment.NewLine);
            objILog.Error(sb.ToString());
        }
    }
    #endregion
}
