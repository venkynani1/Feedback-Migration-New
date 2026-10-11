/// ***********************************************************************************************
/// File name: SessionDAL.cs
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
using System.Data.SqlClient;
using Hex.Vars.FB.HexFBDBUtility;
using Hex.Vars.FB.HexFBModule;
using Hex.Vars.FB.HexFBIDAL;  

namespace Hex.Vars.FB.HexFBDAL
{
    public class sessionDAL:Hex.Vars.FB.HexFBIDAL.ISession
    {
        #region private
        internal const string PROC_InsertSession = "usp_InsertSession";
        internal const string PROC_UpdateSession = "usp_UpdateSession";
        internal const string PROC_DeleteSession = "usp_DeleteSession";
        internal const string PROC_getAllCourse = "usp_getAllCourse";
        internal const string PROC_getquestions = "usp_getquestions";
        internal const string PROC_getindividual = "usp_getindividual";
        internal const string PROC_getTrainersForSessionid = "usp_getTrainersForSessionid";
        internal const string PROC_getSessionCourseWise = "usp_getSessionCourseWise";
        internal const string PROC_getMaxnoofparticipants = "usp_getMaxnoofParticipants";
        internal const string PROC_getSessionSessionIdWise = "usp_getSessionSessionIdWise";
        internal const string PROC_getSessionEmpIdWise = "usp_getEmpIdForSessionid";
        internal const string PROC_getFeedbackfromEmpId = "usp_getFeedbackdetQuesansEmployeeid";
        internal const string PROC_getFeedbackfromEmpId_V1 = "usp_getFeedbackdetQuesansEmployeeid_V1";
        internal const string PROC_getInsertSessionTrainers = "usp_InsertSessionTrainerDetails";
        internal const string PROC_getFeedBackCount = "usp_getFeedBackCount";
        internal const string PROC_getYear = "usp_getYear";
        internal const string PROC_getindividual_sup = "usp_getindividual_Supervisor";
        internal const string PROC_getSupFeedbackDetails = "usp_SupervisorFeedback_Details";
        internal const string PROC_checkSessionAvailability = "usp_CheckSessionAvailability";

        private const string PARAM_courseid = "@courseid";
        private const string PARAM_courseName = "@courseName";
        private const string PARAM_sessionid = "@sessionid";
        private string PARAM_startdate = "@startdate";
        private string PARAM_enddate = "@enddate";
        private string PARAM_maxparticipants = "@max_participants";
        private string PARAM_Trainername = "@Trainername";
        private string PARAM_location = "@location";
        private const string PARAM_createdby = "@createdby";
        private const string PARAM_modifiedby = "@modifiedby ";
        private const string PARAM_rowcount = "@rowcount";
        private const string PARAM_empid = "@empid";
        private const string PARAM_TrainerID = "@TrainerID";
        private const string PARAM_Flag = "@Flag";
        private const string PARAM_IsExistsAlready = "@IsExistsAlready";
        private const string PARAM_year = "@year";
        private const string PARAM_pm_id = "@pm_id";
        #endregion

        #region public Methods
        public bool createSession(Sessioninfo _Session, out string errMsg,out bool IsRecordExist)
        {
            string errMessage = string.Empty;
            errMsg = errMessage;
            bool rtnObj = false;
            IsRecordExist = false;

            SqlCommand objCmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
                {
                    objCmd.Connection = conn;
                    conn.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "usp_InsertSession";

                    objCmd.Parameters.Add(PARAM_courseid, SqlDbType.VarChar, 50);
                    objCmd.Parameters[0].Value = _Session.courseid;

                    objCmd.Parameters.Add(PARAM_courseName, SqlDbType.VarChar, 500);
                    objCmd.Parameters[1].Value = _Session.courseName;

                    objCmd.Parameters.Add(PARAM_sessionid, SqlDbType.VarChar, 50);
                    objCmd.Parameters[2].Value = _Session.session;

                    objCmd.Parameters.Add(PARAM_startdate, SqlDbType.Date, 3);
                    objCmd.Parameters[3].Value = _Session.startdate;

                    objCmd.Parameters.Add(PARAM_enddate, SqlDbType.Date, 3);
                    objCmd.Parameters[4].Value = _Session.Enddate;

                    objCmd.Parameters.Add(PARAM_maxparticipants, SqlDbType.Int, 25);
                    objCmd.Parameters[5].Value = _Session.maximumparticipants;

                    objCmd.Parameters.Add(PARAM_Trainername, SqlDbType.VarChar, 25);
                    objCmd.Parameters[6].Value = _Session.Trainername;

                    objCmd.Parameters.Add(PARAM_location, SqlDbType.VarChar, 25);
                    objCmd.Parameters[7].Value = _Session.location;

                    objCmd.Parameters.Add(PARAM_createdby, SqlDbType.VarChar, 7);
                    objCmd.Parameters[8].Value = _Session.createdby;

                    objCmd.Parameters.Add(PARAM_IsExistsAlready, SqlDbType.Int);
                    objCmd.Parameters[9].Direction = ParameterDirection.Output;

                                                       
                    objCmd.ExecuteNonQuery();
                    rtnObj = true;
                    IsRecordExist = Convert.ToBoolean(objCmd.Parameters[PARAM_IsExistsAlready].Value);
                    conn.Close();
                }
                
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
                //throw ex;
            }
            finally
            {
                if (objCmd != null)
                    objCmd = null;

            }
            return rtnObj;
        }
        public bool InsertSessionTrainersDet(Sessioninfo _Session, out string errMsg)
        {
            string errMessage = string.Empty;
            errMsg = errMessage;
            bool rtnObj = false;

            SqlCommand objCmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
                {
                    objCmd.Connection = conn;
                    conn.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "usp_InsertSessionTrainerDetails";

                    objCmd.Parameters.Add(PARAM_sessionid, SqlDbType.VarChar, 50);
                    objCmd.Parameters[0].Value = _Session.session;

                    objCmd.Parameters.Add(PARAM_TrainerID, SqlDbType.VarChar, 200);
                    objCmd.Parameters[1].Value = _Session.Trainerid;

                   

                                                       
                    objCmd.ExecuteNonQuery();
                    rtnObj = true;
                    conn.Close();
                }
                
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
                //throw ex;
            }
            finally
            {
                if (objCmd != null)
                    objCmd = null;

            }
            return rtnObj;
        }
        public bool updateSession(Sessioninfo _course, out string errMsg)
        {
            throw new NotImplementedException();
        }
        public bool deleteSession(Sessioninfo _course, out string errMsg)
        {
            throw new NotImplementedException();
        }
        public SqlDataReader getAllCourse()
        {

            return SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getAllCourse, null);
        }
        public DataTable getquestions()
        {
            return SqlHelper.getvalue(SqlHelper.ConnectionString,CommandType.StoredProcedure,PROC_getquestions,null);
        }
        public SqlDataReader getindividual(string emp_id, string sessionid, string courseid, int flag)
        {
                        
            SqlParameter[] spl=new SqlParameter[4];

            spl[0] = new SqlParameter(PARAM_empid, SqlDbType.VarChar);
            spl[0].Value = emp_id;

            spl[1] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
            spl[1].Value = sessionid;

            spl[2] = new SqlParameter(PARAM_courseid, SqlDbType.VarChar);
            spl[2].Value = courseid;

            spl[3] = new SqlParameter(PARAM_Flag, SqlDbType.Int);
            spl[3].Value = flag;

            return SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getindividual, spl);
         }
        public SqlDataReader getTrainersDets(string sessionid, string empid)
        {
            SqlParameter[] spl=new SqlParameter[2];

            spl[0] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
            spl[0].Value = sessionid;
            spl[1] = new SqlParameter(PARAM_empid, SqlDbType.VarChar);
            spl[1].Value = empid;


            return SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getTrainersForSessionid, spl);
            }
        public SqlDataReader getSessionCourseWise(string _courseId,int _year)
        {

            SqlParameter[] sqlPar = new SqlParameter[2];

            sqlPar[0] = (new SqlParameter(PARAM_courseid, SqlDbType.VarChar, 50));
            sqlPar[0].Value = _courseId;
            sqlPar[1] = (new SqlParameter(PARAM_year, SqlDbType.Int, 11));
            sqlPar[1].Value = _year;

            return SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getSessionCourseWise, sqlPar);
        }
        public int GetmaxnoofParticipants(string _courseId)
        {
            SqlParameter[] sqlPar = new SqlParameter[1];

            sqlPar[0] = (new SqlParameter(PARAM_courseid, SqlDbType.VarChar, 100));
            sqlPar[0].Value = _courseId;

            int MaxNoofparticipants;
            return MaxNoofparticipants = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getMaxnoofparticipants, sqlPar));
        }
        public SqlDataReader getSessionSessionIdWise(string _sessionId)
        {

            SqlParameter[] sqlPar = new SqlParameter[1];

            sqlPar[0] = (new SqlParameter(PARAM_sessionid, SqlDbType.VarChar, 50));
            sqlPar[0].Value = _sessionId;

            return SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getSessionSessionIdWise, sqlPar);
        }
        public DataTable getSessionSessionIdWiseNew1(string _sessionId)
        {

            SqlParameter[] sqlPar = new SqlParameter[1];

            sqlPar[0] = (new SqlParameter(PARAM_sessionid, SqlDbType.VarChar, 50));
            sqlPar[0].Value = _sessionId;

            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getSessionSessionIdWise, sqlPar);
        }
        public SqlDataReader getSessionEmpIdWise(string _sessionId)
        {

            SqlParameter[] sqlPar = new SqlParameter[1];

            sqlPar[0] = (new SqlParameter(PARAM_sessionid, SqlDbType.VarChar, 50));
            sqlPar[0].Value = _sessionId;

            return SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getSessionEmpIdWise, sqlPar);
        }
        public DataTable GetEmployeeForfeedback(Sessioninfo _Session, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;
            SqlParameter[] spl = new SqlParameter[3];
            spl[0] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);

            spl[0].Value = _Session.session;
            spl[1] = new SqlParameter(PARAM_empid, SqlDbType.VarChar);
            
            spl[1].Value = _Session.empid;
            spl[2] = new SqlParameter(PARAM_TrainerID, SqlDbType.VarChar);
            spl[2].Value = _Session.Trainerid;

            //return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getFeedbackfromEmpId, spl);
            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getFeedbackfromEmpId_V1, spl);
        }
        public DataTable GetFeedbackCount(Sessioninfo _Session)
        {
            string errMessage = string.Empty;
            SqlParameter[] spl = new SqlParameter[2];
            spl[0] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
            spl[0].Value = _Session.session;

            spl[1] = new SqlParameter(PARAM_empid, SqlDbType.VarChar);
            spl[1].Value = _Session.empid;

            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getFeedBackCount, spl);
        }
        public DataTable GetAllTrainers(Sessioninfo _Session)
        {
            string errMessage = string.Empty;
            SqlParameter[] spl = new SqlParameter[1];
            spl[0] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
            spl[0].Value = _Session.session;

            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getTrainersForSessionid, spl);
        }
        public DataTable getSessionSessionIdWiseNew(string _sessionId)
        {

            SqlParameter[] sqlPar = new SqlParameter[1];

            sqlPar[0] = (new SqlParameter(PARAM_sessionid, SqlDbType.VarChar, 50));
            sqlPar[0].Value = _sessionId;

            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getTrainersForSessionid, sqlPar);
        }
        public DataTable getYear()
        {
            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getYear, null);
        }

        public SqlDataReader getindividual_sup(string emp_id, string sessionid, string courseid, int flag)
        {
                        
            SqlParameter[] spl=new SqlParameter[4];

            spl[0] = new SqlParameter(PARAM_empid, SqlDbType.VarChar);
            spl[0].Value = emp_id;

            spl[1] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
            spl[1].Value = sessionid;

            spl[2] = new SqlParameter(PARAM_courseid, SqlDbType.VarChar);
            spl[2].Value = courseid;

            spl[3] = new SqlParameter(PARAM_Flag, SqlDbType.Int);
            spl[3].Value = flag;

            return SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getindividual_sup, spl);
         }

        public DataSet getSupFeedbackDetails(string emp_id, string sessionid,string pm_id)
        {
                        
            SqlParameter[] spl=new SqlParameter[3];

            spl[0] = new SqlParameter(PARAM_empid, SqlDbType.VarChar);
            spl[0].Value = emp_id;

            spl[1] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
            spl[1].Value = sessionid;

            spl[2] = new SqlParameter(PARAM_pm_id, SqlDbType.VarChar);
            spl[2].Value = pm_id;

            return SqlHelper.getvalue1(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getSupFeedbackDetails, spl);
         }
        public DataTable getSessionAvailability(string _sessionId)
        {

            SqlParameter[] sqlPar = new SqlParameter[1];

            sqlPar[0] = (new SqlParameter(PARAM_sessionid, SqlDbType.VarChar, 50));
            sqlPar[0].Value = _sessionId;

            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_checkSessionAvailability, sqlPar);
        }
        
        #endregion

        
    }
}
