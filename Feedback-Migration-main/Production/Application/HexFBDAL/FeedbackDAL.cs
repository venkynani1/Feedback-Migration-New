/// ***********************************************************************************************
/// File name: FeedbackDAL.cs
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
using System.Data.Sql;
using System.Data.SqlClient;
using Hex.Vars.FB.HexFBDBUtility;
using Hex.Vars.FB.HexFBModule;
using Hex.Vars.FB.HexFBIDAL;  

namespace Hex.Vars.FB.HexFBDAL
{
    public class FeedbackDAL : Hex.Vars.FB.HexFBIDAL.IFeedback
    {
        #region private
        internal const string PROC_InsertSession = "usp_InsertFeedback";
        internal const string PROC_InsertSessions = "usp_InsertFeedbacks";
        private const string PARAM_course = "@courseid";
        private const string PARAM_session = "@sessionid";
        private const string PARAM_Employeeid = "@employee";
        private const string PARAM_Question = "@question_no";
        private const string PARAM_response = "@response";
        private const string PARAM_response1 = "@response1";
        private const string PARAM_response2 = "@response2";
        private const string PARAM_response3 = "@response3";
        private const string PARAM_response4 = "@response4";
        private const string PARAM_response5 = "@response5";
        private const string PARAM_response6 = "@response6";
        private const string PARAM_response7 = "@response7";
        private const string PARAM_response8 = "@response8";
        private const string PARAM_response9 = "@response9";
        private const string PARAM_response10= "@response10";
        private const string PARAM_response11 = "@response11";
        private const string PARAM_response12 = "@response12";
        private const string PARAM_response13 = "@response13";
        private const string PARAM_response14 = "@response14";
        private const string PARAM_response15 = "@response15";
        private const string PARAM_response16 = "@response16";
        private const string PARAM_response17 = "@response17";
        private const string PARAM_response18 = "@response18";
        private const string PARAM_Comments = "@comments";
        private const string PARAM_TrainerId = "@TrainerId";
        private const string PARAM_CommonFlag = "@CommonFlag";
        private const string PARAM_PrbSA = "@PrbSA";
        private const string PARAM_TrnReq = "@TrnReq";
        private const string PARAM_OverallFeed = "@OverallFeed";
        private const string PARAM_Remarks = "@Remarks";
        private const string PARAM_Status = "@Status";
        private const string PARAM_SupervisorId = "@SupervisorId";
        private const string PARAM_PrgSkills = "@PrgSkills";
        private const string PARAM_CommSkills = "@CommSkills";
        private const string PARAM_BehSkills = "@BehSkills";
        private const string PARAM_Performance = "@Performance";
        private const string PARAM_OvrAllRating = "@OvrAllRating";
        private const string PARAM_WorkArea = "@WorkArea";
        private const string PARAM_SubOrdId = "@SubOrdId";
        private const string PARAM_PM_ID = "@PM_ID";
        private const string PARAM_TechSkills = "@TechSkills";
        private const string PARAM_SoftSkills = "@SoftSkills";
        #endregion
        #region public Methods
        public bool createFeedback(HexFBModule.feedbackinfo _feedback, out string errMsg)
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
                    objCmd.CommandText = "usp_Insertfeedback";
                    objCmd.Parameters.Add(PARAM_course, SqlDbType.VarChar);
                    objCmd.Parameters[0].Value = _feedback.courseid;
                    objCmd.Parameters.Add(PARAM_session, SqlDbType.VarChar);
                    objCmd.Parameters[1].Value = _feedback.sessionid;
                    objCmd.Parameters.Add(PARAM_Employeeid, SqlDbType.VarChar);
                    objCmd.Parameters[2].Value = _feedback.traineeid;
                    objCmd.Parameters.Add(PARAM_Question, SqlDbType.Int);
                    objCmd.Parameters[3].Value = _feedback.question1;
                    objCmd.Parameters.Add(PARAM_response, SqlDbType.Int);
                    objCmd.Parameters[4].Value = _feedback.response1;
           
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

        public bool createFeedbacks(HexFBModule.feedbackinfo _feedback, out string errMsg)
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
                    objCmd.CommandText = "usp_Insertfeedbacks_V1";
                    objCmd.Parameters.Add(PARAM_course, SqlDbType.VarChar);
                    objCmd.Parameters[0].Value = _feedback.courseid;
                    objCmd.Parameters.Add(PARAM_session, SqlDbType.VarChar);
                    objCmd.Parameters[1].Value = _feedback.sessionid;
                    objCmd.Parameters.Add(PARAM_Employeeid, SqlDbType.VarChar);
                    objCmd.Parameters[2].Value = _feedback.traineeid;

                    objCmd.Parameters.Add(PARAM_response1, SqlDbType.Int);
                    objCmd.Parameters[3].Value = _feedback.response1;

                    objCmd.Parameters.Add(PARAM_response2, SqlDbType.Int);
                    objCmd.Parameters[4].Value = _feedback.response2;

                    objCmd.Parameters.Add(PARAM_response3, SqlDbType.Int);
                    objCmd.Parameters[5].Value = _feedback.response3;

                    objCmd.Parameters.Add(PARAM_Comments,SqlDbType.VarChar);
                    objCmd.Parameters[6].Value = _feedback.comments;

                    objCmd.Parameters.Add(PARAM_TrainerId, SqlDbType.VarChar);
                    objCmd.Parameters[7].Value = _feedback.TrainerId;

                    objCmd.Parameters.Add(PARAM_CommonFlag, SqlDbType.Int);
                    objCmd.Parameters[8].Value = _feedback.CommonFlag;

                    objCmd.ExecuteNonQuery();
                    rtnObj = true;
                    conn.Close();
                    _feedback.success = "sucessfully given Feedback";
                   
                }

            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
                _feedback.failure = errMsg;
                //throw ex;
            }
            finally
            {
                if (objCmd != null)
                    objCmd = null;

            }
            return rtnObj;
        }

        public bool createSupFeedbacks(HexFBModule.feedbackSupInfo _supFeedback, out string errMsg)
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
                    objCmd.CommandText = "usp_InsertSupFeedbacks";
                    objCmd.Parameters.Add(PARAM_course, SqlDbType.VarChar);
                    objCmd.Parameters[0].Value = _supFeedback.Courseid;
                    objCmd.Parameters.Add(PARAM_session, SqlDbType.VarChar);
                    objCmd.Parameters[1].Value = _supFeedback.Sessionid;
                    objCmd.Parameters.Add(PARAM_SupervisorId, SqlDbType.VarChar);
                    objCmd.Parameters[2].Value = _supFeedback.SupervisorId;
                    objCmd.Parameters.Add(PARAM_SubOrdId, SqlDbType.VarChar);
                    objCmd.Parameters[3].Value = _supFeedback.SubordinateId;
                    objCmd.Parameters.Add("@TechSkills", SqlDbType.VarChar);
                    objCmd.Parameters[4].Value = (object)_supFeedback.TechSkills;
                    objCmd.Parameters.Add("@SoftSkills", SqlDbType.VarChar);
                    objCmd.Parameters[5].Value = (object)_supFeedback.SoftSkills;

                    //objCmd.Parameters.Add(PARAM_PrbSA, SqlDbType.VarChar);
                    //objCmd.Parameters[4].Value = _supFeedback.PrbSA;
                    //objCmd.Parameters.Add(PARAM_PrgSkills, SqlDbType.VarChar);
                    //objCmd.Parameters[5].Value = _supFeedback.PrgSkills;
                    //objCmd.Parameters.Add(PARAM_CommSkills, SqlDbType.VarChar);
                    //objCmd.Parameters[6].Value = _supFeedback.CommSkills;
                    //objCmd.Parameters.Add(PARAM_BehSkills, SqlDbType.VarChar);
                    //objCmd.Parameters[7].Value = _supFeedback.BehSkills;

                    objCmd.Parameters.Add(PARAM_Performance, SqlDbType.VarChar);
                    objCmd.Parameters[6].Value = _supFeedback.Performance;
                    objCmd.Parameters.Add(PARAM_OvrAllRating, SqlDbType.VarChar);
                    objCmd.Parameters[7].Value = _supFeedback.OvrAllRating;
                    //objCmd.Parameters.Add(PARAM_TrnReq, SqlDbType.Int);
                    //objCmd.Parameters[8].Value = _supFeedback.TrainingRequired;
                    objCmd.Parameters.Add(PARAM_OverallFeed, SqlDbType.VarChar);
                    objCmd.Parameters[8].Value = _supFeedback.OverallFeedback;
                    objCmd.Parameters.Add(PARAM_WorkArea, SqlDbType.VarChar);
                    objCmd.Parameters[9].Value = _supFeedback.WorkArea;
                    objCmd.Parameters.Add(PARAM_Status, SqlDbType.VarChar);
                    objCmd.Parameters[10].Value = _supFeedback.Status;
                    objCmd.ExecuteNonQuery();
                    rtnObj = true;
                    conn.Close();
                    _supFeedback.Success = "sucessfully given Feedback";

                }

            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
                _supFeedback.Failure = errMsg;
                //throw ex;
            }
            finally
            {
                if (objCmd != null)
                    objCmd = null;

            }
            return rtnObj;
        }
        public bool updatePMRemarks(int status, string remarks, string PM_ID, string sessionId, string supervisorId, out string errMsg)
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
                    objCmd.CommandText = "usp_UpdatePMRemarks";
                    objCmd.Parameters.Add(PARAM_Status, SqlDbType.Int);
                    objCmd.Parameters[0].Value = status;
                    objCmd.Parameters.Add(PARAM_Remarks, SqlDbType.VarChar);
                    objCmd.Parameters[1].Value = remarks;
                    objCmd.Parameters.Add(PARAM_PM_ID, SqlDbType.Int);
                    objCmd.Parameters[2].Value = PM_ID;
                    objCmd.Parameters.Add(PARAM_session, SqlDbType.VarChar);
                    objCmd.Parameters[3].Value = sessionId;
                    objCmd.Parameters.Add(PARAM_SupervisorId, SqlDbType.VarChar);
                    objCmd.Parameters[4].Value = supervisorId;
                    objCmd.ExecuteNonQuery();
                    rtnObj = true;
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
                throw ex;
            }
            finally
            {
                if (objCmd != null)
                    objCmd = null;

            }
            return rtnObj;
        }
        #endregion

    }
}
