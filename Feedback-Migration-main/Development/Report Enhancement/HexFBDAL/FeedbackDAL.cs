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
                    objCmd.CommandText = "usp_Insertfeedbacks";
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
                    objCmd.Parameters.Add(PARAM_response4, SqlDbType.Int);
                    objCmd.Parameters[6].Value = _feedback.response4;
                    objCmd.Parameters.Add(PARAM_response5, SqlDbType.Int);
                    objCmd.Parameters[7].Value = _feedback.response5;
                    objCmd.Parameters.Add(PARAM_response6, SqlDbType.Int);
                    objCmd.Parameters[8].Value = _feedback.response6;
                    objCmd.Parameters.Add(PARAM_response7, SqlDbType.Int);
                    objCmd.Parameters[9].Value = _feedback.response7;
                    objCmd.Parameters.Add(PARAM_response8, SqlDbType.Int);
                    objCmd.Parameters[10].Value = _feedback.response8;
                    objCmd.Parameters.Add(PARAM_response9, SqlDbType.Int);
                    objCmd.Parameters[11].Value = _feedback.response9;
                    objCmd.Parameters.Add(PARAM_response10, SqlDbType.Int);
                    objCmd.Parameters[12].Value = _feedback.response10;
                    objCmd.Parameters.Add(PARAM_response11, SqlDbType.Int);
                    objCmd.Parameters[13].Value = _feedback.response11;
                    objCmd.Parameters.Add(PARAM_response12, SqlDbType.Int);
                    objCmd.Parameters[14].Value = _feedback.response12;
                    objCmd.Parameters.Add(PARAM_response13, SqlDbType.Int);
                    objCmd.Parameters[15].Value = _feedback.response13;
                    objCmd.Parameters.Add(PARAM_response14, SqlDbType.Int);
                    objCmd.Parameters[16].Value = _feedback.response14;
                    objCmd.Parameters.Add(PARAM_response15, SqlDbType.Int);
                    objCmd.Parameters[17].Value = _feedback.response15;
                    objCmd.Parameters.Add(PARAM_response16, SqlDbType.Int);
                    objCmd.Parameters[18].Value = _feedback.response16;
                    objCmd.Parameters.Add(PARAM_response17, SqlDbType.Int);
                    objCmd.Parameters[19].Value = _feedback.response17;
                    objCmd.Parameters.Add(PARAM_response18, SqlDbType.Int);
                    objCmd.Parameters[20].Value = _feedback.response18;
                    objCmd.Parameters.Add(PARAM_Comments,SqlDbType.VarChar);
                    objCmd.Parameters[21].Value = _feedback.comments;
                    objCmd.Parameters.Add(PARAM_TrainerId, SqlDbType.VarChar);
                    objCmd.Parameters[22].Value = _feedback.TrainerId;

                    objCmd.Parameters.Add(PARAM_CommonFlag, SqlDbType.Int);
                    objCmd.Parameters[23].Value = _feedback.CommonFlag;

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
        #endregion

    }
}
