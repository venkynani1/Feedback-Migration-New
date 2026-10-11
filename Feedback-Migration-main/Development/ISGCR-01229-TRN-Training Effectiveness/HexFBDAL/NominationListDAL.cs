/// ***********************************************************************************************
/// File name: NominationListDAL.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Hex.Vars.FB.HexFBDBUtility;

namespace Hex.Vars.FB.HexFBDAL
{
    public class NominationListDAL :Hex.Vars.FB.HexFBIDAL.INominationList
    {
        #region private
        internal const string PROC_InsertNominationList = "usp_InsertNominationList";
        internal const string PROC_UpdatesValidComments = "usp_UpdateCommentsvaildDetails";
        internal const string PROC_SendmaildList = "usp_sendmail";
        internal const string PROC_SelectNominateEmplo = "usp_getNomiatedEmplForMailsend";
        internal const string PROC_SelectMailMaster = "usp_SelectMailMasterDetails";
        internal const string PROC_SendMailStat = "usp_InsertEmpMailstat";
        internal const string PROC_SelectListSendmailStat = "usp_getEmplForMailsendStatus";
        internal const string PROC_MailMasterInsert = "usp_InsertMailDetails";
        internal const string PROC_TrainerMasterInsert = "usp_InsertTrainerDetails";
        internal const string PROC_VendorMasterInsert = "usp_InsertVendorDetails";
        internal const string PROC_getMailRelatedDetails = "usp_getMailRelatedDetails";
        internal const string PROC_getSupervisorsForMail = "usp_getSupervisorsForMail";
        private const string PARAM_CourseId = "@course_Id";
        private const string PARAM_Sessionid = "@session_id";
        private const string PARAM_EmployeeId = "@Employee_id";
        private const string PARAM_Flag = "@Flag";

        private const string PARAM_CreatedBy = "@createdby";
        private const string PARAM_CreatedDate = "@Nominatedate";  
        private const string PARAM_RowAffected = "@RowAffected";
        private const string PARAM_ValidComments = "@Commentsvalid";
        private const string PARAM_FromMail= "@FrmMail";
        private const string PARAM_Bcc = "@BccMail";
        private const string PARAM_Ccc = "@CccMail";        
        private const string PARAM_Subject = "@Subject";
        private const string PARAM_Body = "@Body";
        private const string PARAM_Url = "@URL";  
        private const string PARAM_Stat = "@Status";
        private const string PARAM_TrainerId = "@TrainerId";
        private const string PARAM_TrainerName = "@TrainerName";
        private const string PARAM_TrainerType = "@TrainerType";
        private const string PARAM_VendorName = "@VendorName";
        private const string PARAM_PrefixID = "@PrefixID";
        private const string PARAM_IsExistsAlready = "@IsExistsAlready";
        private const string PARAM_ModifiedBy = "@modifiedby";

        private const string PARAM_FrmMail1 = "@FromMail";
        private const string PARAM_ToMail1 = "@ToMail";
        private const string PARAM_Bcc1 = "@BccMail";
        private const string PARAM_Ccc1 = "@CcMail";
        private const string PARAM_Subject1 = "@Subject";
        private const string PARAM_Body1 = "@Body";

        private const string PARAM_FrmMail2 = "@from";
        private const string PARAM_ToMail2= "@to";
        private const string PARAM_Bcc2="@bcc";
        private const string PARAM_Ccc2 = "@cc";
        private const string PARAM_Subject2 = "@subject";
        private const string PARAM_Body2 = "@body";
        private const string PARAM_OutputResul2 = "@result";
        private const string PARAM_Type = "@Type";
        #endregion

        #region public Methods
        public bool addNominationList()
        {
            throw new NotImplementedException();
        }
        public System.Data.OleDb.OleDbDataReader importExcelFile(string FileLocation, out string errMsg)
        {
            errMsg = string.Empty;
            bool rtnObj = false;
           string excelConnStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + FileLocation + "; Extended Properties =Excel 8.0;";
            //string excelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

            OleDbConnection conn = new OleDbConnection(excelConnStr);
            string cmd = "Select * from [Sheet1$]";
            OleDbCommand cmmd = new OleDbCommand(cmd, conn);
            OleDbDataReader rd;
            try
            {
                conn.Open();
                rd = cmmd.ExecuteReader(CommandBehavior.CloseConnection);
                return rd;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                conn.Close();
                conn.Dispose();
            }
            finally
            {
                
            }
            return null;
        }
        public bool insertNominationList(HexFBModule.NominationListInfo _nominationinfo, out string errmsg, out bool IsRecordExists,out int rowAffected)
        {
            errmsg = string.Empty;            
            bool rtnObj = false;            
            IsRecordExists = false;
            rowAffected = 0;

            SqlCommand objCmd = new SqlCommand();
            try
            {

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
                {
                    objCmd.Connection = conn;
                    conn.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = PROC_InsertNominationList;

                    objCmd.Parameters.Add(PARAM_CourseId, SqlDbType.VarChar, 50);
                    objCmd.Parameters[0].Value = _nominationinfo.CourseId;

                    objCmd.Parameters.Add(PARAM_Sessionid, SqlDbType.VarChar, 50);
                    objCmd.Parameters[1].Value = _nominationinfo.SessionId;

                    objCmd.Parameters.Add(PARAM_EmployeeId, SqlDbType.VarChar, 7);
                    objCmd.Parameters[2].Value = _nominationinfo.EmployeeId;

                    objCmd.Parameters.Add(PARAM_CreatedBy, SqlDbType.VarChar, 7);
                    objCmd.Parameters[3].Value = _nominationinfo.CreatedBy;

                    objCmd.Parameters.Add(PARAM_Type, SqlDbType.Int, 7);
                    objCmd.Parameters[4].Value = _nominationinfo.Type;

                    objCmd.Parameters.Add(PARAM_IsExistsAlready, SqlDbType.Int);
                    objCmd.Parameters[5].Direction = ParameterDirection.Output;
                                        
                    objCmd.Parameters.Add(PARAM_RowAffected, SqlDbType.Int);
                    objCmd.Parameters[6].Direction = ParameterDirection.Output;

                    objCmd.ExecuteNonQuery();
                    rtnObj = true;
                    IsRecordExists = Convert.ToBoolean(objCmd.Parameters[PARAM_IsExistsAlready].Value);                    
                    rowAffected = Convert.ToInt32(objCmd.Parameters[PARAM_RowAffected].Value);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message.ToString();
                //throw ex;
            }
            finally
            {
                if (objCmd != null)
                    objCmd = null;

            }


            return rtnObj;
        }
        public bool UpdateVaildCommentsDetails(HexFBModule.NominationListInfo _nominationinfo, out string errmsg, out bool IsRecordExists, out int rowAffected)
        {
            errmsg = string.Empty;            
            bool rtnObj = false;            
            IsRecordExists = false;
            rowAffected = 0;

            SqlCommand objCmd = new SqlCommand();
            try
            {

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
                {
                    objCmd.Connection = conn;
                    conn.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = PROC_UpdatesValidComments;
                    objCmd.Parameters.Add(PARAM_EmployeeId, SqlDbType.VarChar, 7);
                    objCmd.Parameters[0].Value = _nominationinfo.EmployeeId;

                    objCmd.Parameters.Add(PARAM_Sessionid, SqlDbType.VarChar, 50);
                    objCmd.Parameters[1].Value = _nominationinfo.SessionId;




                    objCmd.Parameters.Add(PARAM_ValidComments, SqlDbType.Int);
                    objCmd.Parameters[2].Value = _nominationinfo.Comments;

                    

                    objCmd.ExecuteNonQuery();
                    rtnObj = true;
                   
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message.ToString();
                //throw ex;
            }
            finally
            {
                if (objCmd != null)
                    objCmd = null;

            }


            return rtnObj;
        }
        public bool InsetMasterDet(HexFBModule.MasterInfo _MasterInfo, out string errmsg)
        {
            errmsg = string.Empty;            
            bool rtnObj = false;            
           

            SqlCommand objCmd = new SqlCommand();
            try
            {

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
                {
                    objCmd.Connection = conn;
                    conn.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = PROC_MailMasterInsert;

                    objCmd.Parameters.Add(PARAM_FromMail, SqlDbType.NVarChar, 1000);
                    objCmd.Parameters[0].Value = _MasterInfo.FromMail;

                    objCmd.Parameters.Add(PARAM_Bcc, SqlDbType.NVarChar, 1000);
                    objCmd.Parameters[1].Value = _MasterInfo.Bcc;

                    objCmd.Parameters.Add(PARAM_Ccc, SqlDbType.NVarChar, 1000);
                    objCmd.Parameters[2].Value = _MasterInfo.Ccc;


                    objCmd.Parameters.Add(PARAM_Subject, SqlDbType.NVarChar, 300);
                    objCmd.Parameters[3].Value = _MasterInfo.Subject;

                    objCmd.Parameters.Add(PARAM_Body, SqlDbType.NVarChar, 4000);
                    objCmd.Parameters[4].Value = _MasterInfo.Body;

                    objCmd.Parameters.Add(PARAM_Url, SqlDbType.NVarChar, 4000);
                    objCmd.Parameters[5].Value = _MasterInfo.URL;

                    objCmd.Parameters.Add(PARAM_Type, SqlDbType.Int, 2);
                    objCmd.Parameters[6].Value = _MasterInfo.Type;

                    objCmd.ExecuteNonQuery();
                    rtnObj = true;
                
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message.ToString();
                //throw ex;
            }
            finally
            {
                if (objCmd != null)
                    objCmd = null;

            }


            return rtnObj;
        }
        public bool InsetTrainerDetails(HexFBModule.MasterInfo _MasterInfo, out string errmsg, out bool IsRecordExists)
        {
            errmsg = string.Empty;            
            bool rtnObj = false;
            IsRecordExists = false;
            SqlCommand objCmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
                {
                    objCmd.Connection = conn;
                    conn.Open();

                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = PROC_TrainerMasterInsert;

                    objCmd.Parameters.Add(PARAM_TrainerId, SqlDbType.VarChar, 50);
                    objCmd.Parameters[0].Value = _MasterInfo.TrainerId;

                    objCmd.Parameters.Add(PARAM_TrainerName, SqlDbType.VarChar, 50);
                    objCmd.Parameters[1].Value = _MasterInfo.TrainerName;

                    objCmd.Parameters.Add(PARAM_TrainerType, SqlDbType.VarChar, 50);
                    objCmd.Parameters[2].Value = _MasterInfo.TrainerType;

                    objCmd.Parameters.Add(PARAM_VendorName, SqlDbType.VarChar, 50);
                    objCmd.Parameters[3].Value = _MasterInfo.Vendorname;

                    objCmd.Parameters.Add(PARAM_CourseId, SqlDbType.VarChar, 50);
                    objCmd.Parameters[4].Value = _MasterInfo.CourseId;

                    objCmd.Parameters.Add(PARAM_IsExistsAlready, SqlDbType.Int);
                    objCmd.Parameters[5].Direction = ParameterDirection.Output;

                    objCmd.ExecuteNonQuery();
                    rtnObj = true;
                    IsRecordExists = Convert.ToBoolean(objCmd.Parameters[PARAM_IsExistsAlready].Value);       
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message.ToString();
                //throw ex;
            }
            finally
            {
                if (objCmd != null)
                    objCmd = null;

            }
            return rtnObj;
        }
        public bool InsetVendorDetails(HexFBModule.MasterInfo _MasterInfo, out string errmsg, out bool IsRecordExists)
        {
            errmsg = string.Empty;
            bool rtnObj = false;
            IsRecordExists = false;
            SqlCommand objCmd = new SqlCommand();
            try
            {

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
                {
                    objCmd.Connection = conn;
                    conn.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = PROC_VendorMasterInsert;

                    objCmd.Parameters.Add(PARAM_VendorName, SqlDbType.VarChar, 50);
                    objCmd.Parameters[0].Value = _MasterInfo.Vendorname;

                    objCmd.Parameters.Add(PARAM_PrefixID, SqlDbType.VarChar, 50);
                    objCmd.Parameters[1].Value = _MasterInfo.PrefixID;
                    objCmd.Parameters.Add(PARAM_IsExistsAlready, SqlDbType.Int);
                    objCmd.Parameters[2].Direction = ParameterDirection.Output;

                   
                  

                    objCmd.ExecuteNonQuery();
                    rtnObj = true;
                    IsRecordExists = Convert.ToBoolean(objCmd.Parameters[PARAM_IsExistsAlready].Value);       
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message.ToString();
                //throw ex;
            }
            finally
            {
                if (objCmd != null)
                    objCmd = null;

            }


            return rtnObj;
        }
        public bool MailstatusDeli(HexFBModule.NominationListInfo nominationlist)
        {
            bool rtnObj = false;
            SqlCommand objCmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
                {
                    objCmd.Connection = conn;
                    conn.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = PROC_SendMailStat;

                    objCmd.Parameters.Add(PARAM_CourseId, SqlDbType.VarChar, 50);
                    objCmd.Parameters[0].Value = nominationlist.CourseId;

                    objCmd.Parameters.Add(PARAM_Sessionid, SqlDbType.VarChar, 50);
                    objCmd.Parameters[1].Value = nominationlist.SessionId;

                    objCmd.Parameters.Add(PARAM_CreatedDate, SqlDbType.DateTime, 7);
                    objCmd.Parameters[2].Value = nominationlist.CreatedDate;

                    objCmd.Parameters.Add(PARAM_EmployeeId, SqlDbType.VarChar, 7);
                    objCmd.Parameters[3].Value = nominationlist.EmployeeId;

                    objCmd.Parameters.Add(PARAM_Stat, SqlDbType.VarChar, 50);
                    objCmd.Parameters[4].Value = nominationlist.Status;

                    objCmd.Parameters.Add(PARAM_FrmMail1, SqlDbType.NVarChar, 50);
                    objCmd.Parameters[5].Value = nominationlist.FromMailID;

                    objCmd.Parameters.Add(PARAM_ToMail1, SqlDbType.NVarChar, 50);
                    objCmd.Parameters[6].Value = nominationlist.ToMailID;

                    objCmd.Parameters.Add(PARAM_Bcc1, SqlDbType.NVarChar, 1000);
                    objCmd.Parameters[7].Value = nominationlist.BccMailID;

                    objCmd.Parameters.Add(PARAM_Ccc1, SqlDbType.NVarChar, 1000);
                    objCmd.Parameters[8].Value = nominationlist.CcMailID;

                    objCmd.Parameters.Add(PARAM_Subject1, SqlDbType.NVarChar, 300);
                    objCmd.Parameters[9].Value = nominationlist.SubJect;

                    objCmd.Parameters.Add(PARAM_Body1, SqlDbType.NVarChar, 4000);
                    objCmd.Parameters[10].Value = nominationlist.Body;
                   

                    objCmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //errmsg = ex.Message.ToString();
                //throw ex;
            }
            finally
            {
                if (objCmd != null)
                    objCmd = null;

            }


            return rtnObj;
        }
        public DataTable SelectNominationList(HexFBModule.NominationListInfo _nominationinfo, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;

            SqlParameter[] spl = new SqlParameter[2];

            spl[0] = new SqlParameter(PARAM_CourseId, SqlDbType.VarChar);
            spl[0].Value = _nominationinfo.CourseId;

            spl[1] = new SqlParameter(PARAM_Sessionid, SqlDbType.VarChar);
            spl[1].Value = _nominationinfo.SessionId;

            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_SelectNominateEmplo, spl);
        }
        public DataTable SelectMasterDet(HexFBModule.MasterInfo _MasterInfo, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;

            SqlParameter[] spl = new SqlParameter[1];

            spl[0] = new SqlParameter(PARAM_Type, SqlDbType.Int);
            spl[0].Value = _MasterInfo.Type;

            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_SelectMailMaster, spl);
        }
        public DataTable SelectDeliveryList(HexFBModule.NominationListInfo _nominationinfo, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;
            SqlParameter[] spl = new SqlParameter[2];

            spl[0] = new SqlParameter(PARAM_CourseId, SqlDbType.VarChar);
            spl[0].Value = _nominationinfo.CourseId;

            spl[1] = new SqlParameter(PARAM_Sessionid, SqlDbType.VarChar);
            spl[1].Value = _nominationinfo.SessionId;
            

            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_SelectListSendmailStat, spl);
        }
        public DataTable Sendmailforfeed(string frommail, string tomail, string sub, string bdy, string ccc, string bcc)
        {

            string errMessage = string.Empty;
            //errmsg = errMessage;
            SqlParameter[] spl = new SqlParameter[6];

            spl[0] = new SqlParameter(PARAM_FrmMail2, SqlDbType.VarChar);
            spl[0].Value = frommail;

            spl[1] = new SqlParameter(PARAM_ToMail2, SqlDbType.VarChar);
            spl[1].Value = tomail;

            spl[2] = new SqlParameter(PARAM_Subject2, SqlDbType.VarChar);
            spl[2].Value = sub;

            spl[3] = new SqlParameter(PARAM_Body2, SqlDbType.VarChar);
            spl[3].Value = bdy;

            spl[4] = new SqlParameter(PARAM_Ccc2, SqlDbType.VarChar);
            spl[4].Value = ccc;

            spl[5] = new SqlParameter(PARAM_Bcc2, SqlDbType.VarChar);
            spl[5].Value = bcc;

            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_SendmaildList, spl);
           
        }
        public DataSet MailRelatedDetails(HexFBModule.NominationListInfo nominationlist, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;
                    SqlParameter[] spl = new SqlParameter[4];

                    spl[0] = new SqlParameter(PARAM_CourseId, SqlDbType.VarChar);
                    spl[0].Value = nominationlist.CourseId;

                    spl[1] = new SqlParameter(PARAM_Sessionid, SqlDbType.VarChar);
                    spl[1].Value = nominationlist.SessionId;

                    spl[2] = new SqlParameter(PARAM_EmployeeId, SqlDbType.VarChar);
                    spl[2].Value = nominationlist.EmployeeId;

                    spl[3] = new SqlParameter(PARAM_Flag, SqlDbType.Int);
                    spl[3].Value = nominationlist.Flag;

                    return SqlHelper.getvalue1(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getMailRelatedDetails, spl);

        }

        public DataSet GetSupervisorsForMail(string sessionId, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;
            SqlParameter[] spl = new SqlParameter[1];

            spl[0] = new SqlParameter(PARAM_Sessionid, SqlDbType.VarChar);
            spl[0].Value = sessionId;

            return SqlHelper.getvalue1(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getSupervisorsForMail, spl);
        }
        #endregion
    }
    }

