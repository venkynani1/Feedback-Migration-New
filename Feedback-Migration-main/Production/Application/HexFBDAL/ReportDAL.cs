/// ***********************************************************************************************
/// File name: ReportDAL.cs
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
using System.Data.OleDb;
using Hex.Vars.FB.HexFBDBUtility;

namespace Hex.Vars.FB.HexFBDAL
{
    public class ReportDAL : Hex.Vars.FB.HexFBIDAL.IReport
    {
        #region private
        internal const string PROC_GetFeedBalEmpNAme = "usp_getFeedbackBalanceEmpId";
        internal const string PROC_GetFeedbackRpt = "usp_getFeedbackForOverallrpt";
        internal const string PROC_GetFeedbackRpt_V1 = "usp_getFeedbackForOverallrpt_V1";

        internal const string PROC_GetFeedBalEmplRpt = "usp_getFeedbackBalanceEmpIdForSessionid";
        internal const string PROC_GetFeedbackRating = "usp_rATINGForOverallrpt";
        internal const string PROC_GetFeedbackRating_V1 = "usp_rATINGForOverallrpt_V1";
        internal const string PROC_GetFeedbackCount = "usp_TrainingDeliveryCount";
        internal const string PROC_GetFeedbackPattern = "usp_getFeedbackForOverallPattern";
        internal const string PROC_GetFeedbackConcern = "usp_getFeedbackForAreasconcern";
        internal const string PROC_GetFeedbackBalance = "usp_getFeedbackBalanceForSessionid";
        internal const string PROC_GetFeedbackComments = "usp_getFeedbackCommentsForSessionid";
        internal const string PROC_GetTrnEffectivenessReport = "usp_getTrnEffectivenessReport";
        private const string PARAM_sessionid = "@sessionid";
        private const string PARAM_Flag = "@flag";
        private const string PARAM_TrainerId = "@TrainerId";
        private const string PARAM_Flags = "@Flags";
        #endregion

        #region public Methods
        public DataTable GetReportForfeedback(HexFBModule.Reportinfo _Sessionid, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;
            SqlParameter[] spl = new SqlParameter[2];
            spl[0] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
            spl[0].Value = _Sessionid.Sessionid;

            spl[1] = new SqlParameter(PARAM_TrainerId, SqlDbType.VarChar);
            spl[1].Value = _Sessionid.TrainerID;

            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_GetFeedbackRpt_V1, spl);




        }
        public DataTable GetAbscommForfeedback(HexFBModule.Reportinfo _Sessionid, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;
            SqlParameter[] spl = new SqlParameter[1];
            spl[0] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
            spl[0].Value = _Sessionid.Sessionid;
            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_GetFeedBalEmplRpt, spl);




        }
        public DataTable GetAbscommForfeedback1(HexFBModule.Reportinfo _Sessionid, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;
            SqlParameter[] spl = new SqlParameter[1];
            spl[0] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
            spl[0].Value = _Sessionid.Sessionid;
            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_GetFeedBalEmpNAme, spl);




        }
        public DataSet GetRatingForfeedback(HexFBModule.Reportinfo _Sessionid, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;
            SqlParameter[] spl = new SqlParameter[2];
            DataSet Average=new DataSet();

            try
            {
                spl[0] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
                spl[0].Value = _Sessionid.Sessionid;

                spl[1] = new SqlParameter(PARAM_TrainerId, SqlDbType.VarChar);
                spl[1].Value = _Sessionid.TrainerID;

                Average=SqlHelper.getvalue1(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_GetFeedbackRating_V1, spl);
            }
            catch (Exception ex)
            {
                errmsg = ex.Message.ToString();
                //throw ex;
            }
            return Average;


            
        }
        public DataTable GetRatingForfeedbackCount(HexFBModule.Reportinfo _Sessionid, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;
            SqlParameter[] spl = new SqlParameter[1];
            spl[0] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
            spl[0].Value = _Sessionid.Sessionid;

            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_GetFeedbackCount, spl);

        }
        public DataTable GetPatternForfeedback(HexFBModule.Reportinfo _Sessionid, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;
            SqlParameter[] spl = new SqlParameter[2];
            spl[0] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
            spl[0].Value = _Sessionid.Sessionid;

            spl[1] = new SqlParameter(PARAM_TrainerId, SqlDbType.VarChar);
            spl[1].Value = _Sessionid.TrainerID;

            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_GetFeedbackPattern, spl);
        }
        public DataTable GetConcernForfeedback(HexFBModule.Reportinfo _Sessionid, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;
            SqlParameter[] spl = new SqlParameter[2];
            spl[0] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
            spl[0].Value = _Sessionid.Sessionid;

            spl[1] = new SqlParameter(PARAM_TrainerId, SqlDbType.VarChar);
            spl[1].Value = _Sessionid.TrainerID;



            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_GetFeedbackConcern, spl);




        }
        public DataTable GetBalanceForfeedback(HexFBModule.Reportinfo _Sessionid, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;
            SqlParameter[] spl = new SqlParameter[1];
            spl[0] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
            spl[0].Value = _Sessionid.Sessionid;
            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_GetFeedbackBalance, spl);




        }
        public DataTable GetCommentsForfeedback(HexFBModule.Reportinfo _Sessionid, out string errmsg, string TrainerId=null)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;
            SqlParameter[] spl = new SqlParameter[2];
            spl[0] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
            spl[0].Value = _Sessionid.Sessionid;
            spl[1] = new SqlParameter(PARAM_TrainerId, SqlDbType.VarChar);
            spl[1].Value = TrainerId;
            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_GetFeedbackComments, spl);
        }
        public DataSet GetTrnEffectivenessReport(string _Sessionid, out string errmsg)
        {
            string errMessage = string.Empty;
            errmsg = errMessage;
            SqlParameter[] spl = new SqlParameter[1];
            DataSet Report = new DataSet();

            try
            {
                spl[0] = new SqlParameter(PARAM_sessionid, SqlDbType.VarChar);
                spl[0].Value = _Sessionid;
                Report = SqlHelper.getvalue1(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_GetTrnEffectivenessReport, spl);
            }
            catch (Exception ex)
            {
                errmsg = ex.Message.ToString();
                //throw ex;
            }
            return Report;



        }
        #endregion
    }
}
