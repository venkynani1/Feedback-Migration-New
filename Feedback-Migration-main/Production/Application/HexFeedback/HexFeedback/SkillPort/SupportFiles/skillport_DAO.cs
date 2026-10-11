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
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
#endregion

/// <summary>
/// Summary description for skillport_DAO
/// </summary>
public class skillport_DAO
{
    public skillport_DAO()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region "GetLearningProgramReportDetail"
    /// <summary>
    /// Gets the learning program report detail.
    /// </summary>
    /// <returns></returns>
    public static DataSet  GetLearningProgramReportDetail()
    {
        DataSet dsLearningProgramData=new DataSet();
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(Procedures.SP_VIEW_HVLEARNINGREPORT, objSqlConnection))
                {
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.CommandTimeout =2500;
                    objSqlConnection.Open();
                    using (SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand))
                    {
                        objSqlDataAdapter.Fill(dsLearningProgramData);
                        objSqlConnection.Close();
                    }
                }
            }
            return dsLearningProgramData;
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region "GetExcellConnectionString"
    /// <summary>
    /// Gets the excell connection string.
    /// </summary>
    /// <param name="strFilePath">The STR file path.</param>
    /// <param name="strExtension">The STR extension.</param>
    /// <returns></returns>
    public static string GetExcellConnectionString(string strFilePath, string strExtension)
    {
        string strConstr = string.Empty;
        try
        {
            switch (strExtension)
            {
                case ".xls":

                    strConstr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                    .ConnectionString;
                    break;

                case ".xlsx":

                    strConstr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                    .ConnectionString;
                    break;
            }
            return String.Format(strConstr, strFilePath);
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region "ExportDataTableToExcel"
    /// <summary>
    /// Exports the data table to excel.
    /// </summary>
    /// <param name="dt">The dt.</param>
    public static void ExportDataTableToExcel(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                HttpResponse response = HttpContext.Current.Response;
                // first let's clean up the response.object   
                response.Clear();
                response.Charset = "";
                // set the response mime type for excel   
                response.ContentType = "application/vnd.ms-excel";
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + "LearningProgramReport.xls" + "\"");

                // create a string writer   
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        // instantiate a datagrid 

                        GridView gvMgrReport = new GridView();
                        gvMgrReport.DataSource = dt;
                        gvMgrReport.DataBind();
                        //Change the Header Row back to white color
                        gvMgrReport.HeaderRow.Style.Add("background-color", "#FFFFFF");
                        //Applying stlye to gridview header cells
                        for (int i = 0; i < gvMgrReport.HeaderRow.Cells.Count; i++)
                        {
                            gvMgrReport.HeaderRow.Cells[i].Style.Add("background-color", "#507CD1");
                        }
                        int j = 1;
                        //This loop is used to apply stlye to cells based on particular row
                        foreach (GridViewRow gvrow in gvMgrReport.Rows)
                        {
                            if (j <= gvMgrReport.Rows.Count)
                            {
                                if (j % 2 != 0)
                                {
                                    for (int k = 0; k < gvrow.Cells.Count; k++)
                                    {
                                        gvrow.Cells[k].Style.Add("background-color", "#EFF3FB");
                                    }
                                }
                            }
                            j++;
                        }
                        gvMgrReport.RenderControl(htw);
                        response.Write(Convert.ToString(sw));
                        response.End();
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

    #region "GetEmployeeRoleByID"
    /// <summary>
    /// Gets the employee role by ID.
    /// </summary>
    /// <param name="strEmpid">The STR empid.</param>
    /// <param name="IsSkillPortAdmin">if set to <c>true</c> [is skill port admin].</param>
    /// <returns></returns>
    public static int GetEmployeeRoleByID(string strEmpid)
    {
        int intResult = 0;
        object objReturnVal = null;
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(Procedures.SP_GETEMPLOYEEROLEBYID, objSqlConnection))
                {
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = strEmpid;
                    objSqlConnection.Open();
                    objReturnVal = objSqlCommand.ExecuteScalar();
                    if (null != objReturnVal)
                        intResult = 1;
                    objSqlConnection.Close();
                }
            }
            return intResult;
        }
        catch
        {
            throw;
        }
    }
    #endregion

    //SkillPort

    #region "ImportSkillPortDetails"
    /// <summary>
    /// Imports the skill port details.
    /// </summary>
    /// <param name="objEL">The obj EL.</param>
    /// <returns></returns>
    public static int ImportSkillPortDetails(Entity objEL)
    {
        object objRetVal = null;
        //int intResult = 0;
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(Procedures.SP_INSERTSKILLPORTDETAILS, objSqlConnection))
                {
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.CommandTimeout = 2500;
                    objSqlCommand.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = objEL.EmpID;
                    objSqlCommand.Parameters.Add("@FILEPATH", SqlDbType.VarChar).Value = objEL.FilePath;
                    objSqlCommand.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    objSqlCommand.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    objSqlConnection.Open();
                    objRetVal = objSqlCommand.ExecuteScalar();
                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    objSqlConnection.Close();
                }
                return objEL.intReturnValue;
            }
        }
        catch
        {
            return objEL.intReturnValue;
            throw;
        }
    }
    #endregion

    #region "GetSkillPortDuplicatRecordDetails"
    /// <summary>
    /// Gets the learning program report detail.
    /// </summary>
    /// <returns></returns>
    public static DataSet GetSkillPortDuplicatRecordDetails(Entity objEL)
    {
        DataSet dsLearningProgramData = new DataSet();
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(Procedures.SP_CHECKRECORDDUPLICATE, objSqlConnection))
                {
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    objSqlCommand.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    objSqlConnection.Open();
                    using (SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand))
                    {
                        objSqlDataAdapter.Fill(dsLearningProgramData);
                        objSqlConnection.Close();
                    }
                }
            }
            return dsLearningProgramData;
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region "GetSkillPortDetails"
    public static int GetSkillPortDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection con = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                SqlCommand cmd2 = con.CreateCommand();
                SqlTransaction tran;
                tran = con.BeginTransaction("Test");
                cmd1.Connection = con;
                cmd1.Transaction = tran;
                try
                {
                    cmd2.CommandTimeout = 2500;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = Procedures.SP_DELETEDUPLICATERECORDS;
                    cmd2.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd2.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd2.Transaction = tran;
                    objRetVal = cmd2.ExecuteNonQuery();

                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = Procedures.SP_INSERTSKILLPORTDETAILS;
                    cmd1.CommandTimeout = 2500;
                    cmd1.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = objEL.EmpID;
                    cmd1.Parameters.Add("@FILEPATH", SqlDbType.VarChar).Value = objEL.FilePath;
                    cmd1.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd1.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd1.Transaction = tran;
                    objRetVal = cmd1.ExecuteScalar();

                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    tran.Commit();
                    return objEL.intReturnValue;
                }

                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region "DeleteSkillPortDetails"
    public static int DeleteSkillPortDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection con = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlTransaction tran;
                tran = con.BeginTransaction("Test");
                cmd.Connection = con;
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandTimeout = 2500;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedures.SP_DELETEDUPLICATERECORDS;
                    cmd.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd.Transaction = tran;
                    objRetVal = cmd.ExecuteNonQuery();

                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    tran.Commit();
                    return objEL.intReturnValue;
                }

                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    //Behavioral

    #region "ImportBehavioralSummaryDetails"
    /// <summary>
    /// Imports the Behavioral Summary details.
    /// </summary>
    /// <param name="objEL">The obj EL.</param>
    /// <returns></returns>
    public static int ImportBehavioralSummaryDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(Procedures.SP_IMPORTBEHAVIORALSUMMARYDETAILS, objSqlConnection))
                {
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.CommandTimeout = 2500;
                    objSqlCommand.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = objEL.EmpID;
                    objSqlCommand.Parameters.Add("@FILEPATH", SqlDbType.VarChar).Value = objEL.FilePath;
                    objSqlCommand.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    objSqlCommand.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    objSqlConnection.Open();
                    objRetVal = objSqlCommand.ExecuteScalar();
                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    objSqlConnection.Close();
                }
               return objEL.intReturnValue;
            }
        }
        catch
        {
           return objEL.intReturnValue;
            throw;
        }
    }
    #endregion
    
    #region "GetBusinessDuplicatRecordDetails"
    /// <summary>
    /// Gets the learning program report detail.
    /// </summary>
    /// <returns></returns>
    public static DataSet GetBusinessDuplicatRecordDetails(Entity objEL)
    {
        DataSet dsLearningProgramData = new DataSet();
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(Procedures.SP_CHECKBEHAVIORALRECORDDUPLICATE, objSqlConnection))
                {
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    objSqlCommand.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    objSqlConnection.Open();
                    using (SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand))
                    {
                        objSqlDataAdapter.Fill(dsLearningProgramData);
                        objSqlConnection.Close();
                    }
                }
            }
            return dsLearningProgramData;
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region "GetBehaviorlaSummaryDetails"
    public static int GetBehaviorlaSummaryDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection con = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                SqlCommand cmd2 = con.CreateCommand();
                SqlTransaction tran;
                tran = con.BeginTransaction("Test");
                cmd1.Connection = con;
                cmd1.Transaction = tran;
                try
                {

                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = Procedures.SP_DELETEBEHAVIORALDUPLICATERECORDS;
                    cmd2.CommandTimeout = 2500;
                    cmd2.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd2.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd2.Transaction = tran;
                    objRetVal = cmd2.ExecuteNonQuery();

                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = Procedures.SP_IMPORTBEHAVIORALSUMMARYDETAILS;
                    cmd1.CommandTimeout = 2500;
                    cmd1.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = objEL.EmpID;
                    cmd1.Parameters.Add("@FILEPATH", SqlDbType.VarChar).Value = objEL.FilePath;
                    cmd1.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd1.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd1.Transaction = tran;
                    objRetVal = cmd1.ExecuteScalar();

                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    tran.Commit();
                    return objEL.intReturnValue;
                }

                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
                finally 
                {
                    con.Close();
                }  
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region "DeleteBehaviorlaSummaryDetails"
    public static int DeleteBehaviorlaSummaryDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection con = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlTransaction tran;
                tran = con.BeginTransaction("Test");
                cmd.Connection = con;
                cmd.Transaction = tran;
                try
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedures.SP_DELETEBEHAVIORALDUPLICATERECORDS;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd.Transaction = tran;
                    objRetVal = cmd.ExecuteNonQuery();

                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    tran.Commit();
                    return objEL.intReturnValue;
                }

                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    //TCDP

    #region "ImportTCDPDetails"
    /// <summary>
    /// Imports the Behavioral Summary details.
    /// </summary>
    /// <param name="objEL">The obj EL.</param>
    /// <returns></returns>
    public static int ImportTCDPDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(Procedures.SP_IMPORTTCDPDETAILS, objSqlConnection))
                {
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.CommandTimeout = 2500;
                    objSqlCommand.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = objEL.EmpID;
                    objSqlCommand.Parameters.Add("@FILEPATH", SqlDbType.VarChar).Value = objEL.FilePath;
                    objSqlCommand.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    objSqlCommand.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    objSqlConnection.Open();
                    objRetVal = objSqlCommand.ExecuteScalar();
                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    objSqlConnection.Close();
                }
                return objEL.intReturnValue;
            }
        }
        catch
        {
            return objEL.intReturnValue;
            throw;
        }
    }
    #endregion

    #region "GetTCDPDuplicatRecordDetails"
    /// <summary>
    /// Gets the learning program report detail.
    /// </summary>
    /// <returns></returns>
    public static DataSet GetTCDPDuplicatRecordDetails(Entity objEL)
    {
        DataSet dsLearningProgramData = new DataSet();
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(Procedures.SP_CHECKTCDPRECORDDUPLICATE, objSqlConnection))
                {
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    objSqlCommand.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    objSqlConnection.Open();
                    using (SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand))
                    {
                        objSqlDataAdapter.Fill(dsLearningProgramData);
                        objSqlConnection.Close();
                    }
                }
            }
            return dsLearningProgramData;
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region "GetTCDPDetails"
    public static int GetTCDPDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection con = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                SqlCommand cmd2 = con.CreateCommand();
                SqlTransaction tran;
                tran = con.BeginTransaction("Test");
                cmd1.Connection = con;
                cmd1.Transaction = tran;
                try
                {

                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = Procedures.SP_DELETETCDPDUPLICATERECORDS;
                    cmd2.CommandTimeout = 2500;
                    cmd2.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd2.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd2.Transaction = tran;
                    objRetVal = cmd2.ExecuteNonQuery();

                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = Procedures.SP_IMPORTTCDPDETAILS;
                    cmd1.CommandTimeout = 2500;
                    cmd1.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = objEL.EmpID;
                    cmd1.Parameters.Add("@FILEPATH", SqlDbType.VarChar).Value = objEL.FilePath;
                    cmd1.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd1.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd1.Transaction = tran;
                    objRetVal = cmd1.ExecuteScalar();

                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    tran.Commit();
                    return objEL.intReturnValue;
                }

                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region "DeleteTCDPDetails"
    public static int DeleteTCDPDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection con = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlTransaction tran;
                tran = con.BeginTransaction("Test");
                cmd.Connection = con;
                cmd.Transaction = tran;
                try
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedures.SP_DELETETCDPDUPLICATERECORDS;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd.Transaction = tran;
                    objRetVal = cmd.ExecuteNonQuery();

                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    tran.Commit();
                    return objEL.intReturnValue;
                }

                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    //Certifications

    #region "ImportCertificationsDetails"
    /// <summary>
    /// Imports the skill port details.
    /// </summary>
    /// <param name="objEL">The obj EL.</param>
    /// <returns></returns>
    public static int ImportCertificationsDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(Procedures.SP_IMPORTCERTIFICATIONSDETAILS, objSqlConnection))
                {
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.CommandTimeout = 2500;
                    objSqlCommand.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = objEL.EmpID;
                    objSqlCommand.Parameters.Add("@FILEPATH", SqlDbType.VarChar).Value = objEL.FilePath;
                    objSqlCommand.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    objSqlCommand.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    objSqlConnection.Open();
                    objRetVal = objSqlCommand.ExecuteScalar();
                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    objSqlConnection.Close();
                }
                return objEL.intReturnValue;
            }
        }
        catch
        {
            return objEL.intReturnValue;
            throw;
        }
    }
    #endregion

    #region "GetCertificationsDuplicatRecordDetails"
    /// <summary>
    /// Gets the learning program report detail.
    /// </summary>
    /// <returns></returns>
    public static DataSet GetCertificationsDuplicatRecordDetails(Entity objEL)
    {
        DataSet dsLearningProgramData = new DataSet();
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(Procedures.SP_CHECKCERTIFICATIONSRECORDDUPLICATE, objSqlConnection))
                {
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    objSqlCommand.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    objSqlConnection.Open();
                    using (SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand))
                    {
                        objSqlDataAdapter.Fill(dsLearningProgramData);
                        objSqlConnection.Close();
                    }
                }
            }
            return dsLearningProgramData;
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region "GetCertificationsDetails"
    public static int GetCertificationsDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection con = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                SqlCommand cmd2 = con.CreateCommand();
                SqlTransaction tran;
                tran = con.BeginTransaction("Test");
                cmd1.Connection = con;
                cmd1.Transaction = tran;
                try
                {
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = Procedures.SP_DELETECERTIFICATIONSDUPLICATERECORDS;
                    cmd2.CommandTimeout = 2500;
                    cmd2.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd2.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd2.Transaction = tran;
                    objRetVal = cmd2.ExecuteNonQuery();

                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = Procedures.SP_IMPORTCERTIFICATIONSDETAILS;
                    cmd1.CommandTimeout = 2500;
                    cmd1.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = objEL.EmpID;
                    cmd1.Parameters.Add("@FILEPATH", SqlDbType.VarChar).Value = objEL.FilePath;
                    cmd1.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd1.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd1.Transaction = tran;
                    objRetVal = cmd1.ExecuteScalar();

                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    tran.Commit();
                    return objEL.intReturnValue;
                }

                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }

                finally
                {
                    con.Close();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region "DeleteCertificationsDetails"
    public static int DeleteCertificationsDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection con = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlTransaction tran;
                tran = con.BeginTransaction("Test");
                cmd.Connection = con;
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedures.SP_DELETECERTIFICATIONSDUPLICATERECORDS;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd.Transaction = tran;
                    objRetVal = cmd.ExecuteNonQuery();

                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    tran.Commit();
                    return objEL.intReturnValue;
                }

                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    //Contributions

    #region "ImportContributionsDetails"
    /// <summary>
    /// Imports the skill port details.
    /// </summary>
    /// <param name="objEL">The obj EL.</param>
    /// <returns></returns>
    public static int ImportContributionsDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(Procedures.SP_IMPORTCONTRIBUTIONSDETAILS, objSqlConnection))
                {
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.CommandTimeout = 2500;
                    objSqlCommand.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = objEL.EmpID;
                    objSqlCommand.Parameters.Add("@FILEPATH", SqlDbType.VarChar).Value = objEL.FilePath;
                    objSqlCommand.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    objSqlCommand.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    objSqlConnection.Open();
                    objRetVal = objSqlCommand.ExecuteScalar();
                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    objSqlConnection.Close();
                }
                return objEL.intReturnValue;
            }
        }
        catch
        {
            return objEL.intReturnValue;
            throw;
        }
    }
    #endregion

    #region "GetContributionsDuplicatRecordDetails"
    /// <summary>
    /// Gets the learning program report detail.
    /// </summary>
    /// <returns></returns>
    public static DataSet GetContributionsDuplicatRecordDetails(Entity objEL)
    {
        DataSet dsLearningProgramData = new DataSet();
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(Procedures.SP_CHECKCONTRIBUTIONSRECORDDUPLICATE, objSqlConnection))
                {
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    objSqlCommand.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    objSqlConnection.Open();
                    using (SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand))
                    {
                        objSqlDataAdapter.Fill(dsLearningProgramData);
                        objSqlConnection.Close();
                    }
                }
            }
            return dsLearningProgramData;
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region "GetContributionsDetails"
    public static int GetContributionsDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection con = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                SqlCommand cmd2 = con.CreateCommand();
                SqlTransaction tran;
                tran = con.BeginTransaction("Test");
                cmd1.Connection = con;
                cmd1.Transaction = tran;
                try
                {
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = Procedures.SP_DELETECONTRIBUTIONSDUPLICATERECORDS;
                    cmd2.CommandTimeout = 2500;
                    cmd2.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd2.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd2.Transaction = tran;
                    objRetVal = cmd2.ExecuteNonQuery();

                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = Procedures.SP_IMPORTCONTRIBUTIONSDETAILS;
                    cmd1.CommandTimeout = 2500;
                    cmd1.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = objEL.EmpID;
                    cmd1.Parameters.Add("@FILEPATH", SqlDbType.VarChar).Value = objEL.FilePath;
                    cmd1.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd1.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd1.Transaction = tran;
                    objRetVal = cmd1.ExecuteScalar();

                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    tran.Commit();
                    return objEL.intReturnValue;
                }

                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region "DeleteContributionsDetails"
    public static int DeleteContributionsDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection con = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlTransaction tran;
                tran = con.BeginTransaction("Test");
                cmd.Connection = con;
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedures.SP_DELETECONTRIBUTIONSDUPLICATERECORDS;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd.Transaction = tran;
                    objRetVal = cmd.ExecuteNonQuery();

                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    tran.Commit();
                    return objEL.intReturnValue;
                }

                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    //Training Hours

    #region "ImportTrainingHoursDetails"
    /// <summary>
    /// Imports the skill port details.
    /// </summary>
    /// <param name="objEL">The obj EL.</param>
    /// <returns></returns>
    public static int ImportTrainingHoursDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(Procedures.SP_TRAININGHOURSDETAILS, objSqlConnection))
                {
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.CommandTimeout = 2500;
                    objSqlCommand.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = objEL.EmpID;
                    objSqlCommand.Parameters.Add("@FILEPATH", SqlDbType.VarChar).Value = objEL.FilePath;
                    objSqlCommand.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    objSqlCommand.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    objSqlConnection.Open();
                    objRetVal = objSqlCommand.ExecuteScalar();
                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    objSqlConnection.Close();
                }
                return objEL.intReturnValue;
            }
        }
        catch
        {
            return objEL.intReturnValue;
            throw;
        }
    }
    #endregion

    #region "GetTrainingHoursDuplicatRecordDetails"
    /// <summary>
    /// Gets the learning program report detail.
    /// </summary>
    /// <returns></returns>
    public static DataSet GetTrainingHoursDuplicatRecordDetails(Entity objEL)
    {
        DataSet dsLearningProgramData = new DataSet();
        try
        {
            using (SqlConnection objSqlConnection = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                using (SqlCommand objSqlCommand = new SqlCommand(Procedures.SP_CHECKTRAININGHOURSRECORDDUPLICATE, objSqlConnection))
                {
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    objSqlCommand.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    objSqlConnection.Open();
                    using (SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand))
                    {
                        objSqlDataAdapter.Fill(dsLearningProgramData);
                        objSqlConnection.Close();
                    }
                }
            }
            return dsLearningProgramData;
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region "GetTrainingHoursDetails"
    public static int GetTrainingHoursDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection con = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                SqlCommand cmd2 = con.CreateCommand();
                SqlTransaction tran;
                tran = con.BeginTransaction("Test");
                cmd1.Connection = con;
                cmd1.Transaction = tran;
                try
                {
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = Procedures.SP_DELETETRAININGHOURSDUPLICATERECORDS;
                    cmd2.CommandTimeout = 2500;
                    cmd2.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd2.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd2.Transaction = tran;
                    objRetVal = cmd2.ExecuteNonQuery();

                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = Procedures.SP_TRAININGHOURSDETAILS;
                    cmd1.CommandTimeout = 2500;
                    cmd1.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = objEL.EmpID;
                    cmd1.Parameters.Add("@FILEPATH", SqlDbType.VarChar).Value = objEL.FilePath;
                    cmd1.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd1.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd1.Transaction = tran;
                    objRetVal = cmd1.ExecuteScalar();

                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    tran.Commit();
                    return objEL.intReturnValue;
                }

                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region "DeleteTrainingHoursDetails"
    public static int DeleteTrainingHoursDetails(Entity objEL)
    {
        object objRetVal = null;
        try
        {
            using (SqlConnection con = new SqlConnection(Procedures.SkillPortConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlTransaction tran;
                tran = con.BeginTransaction("Test");
                cmd.Connection = con;
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedures.SP_DELETETRAININGHOURSDUPLICATERECORDS;
                    cmd.CommandTimeout = 2500;
                    cmd.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = objEL.Month;
                    cmd.Parameters.Add("@YEAR", SqlDbType.VarChar).Value = objEL.Year;
                    cmd.Transaction = tran;
                    objRetVal = cmd.ExecuteNonQuery();

                    if (null != objRetVal)
                        objEL.intReturnValue = Convert.ToInt32(objRetVal);
                    tran.Commit();
                    return objEL.intReturnValue;
                }

                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion
}
