/// ***********************************************************************************************
/// File name: CourseDAL.cs
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
    public class CourseDAL : ICourse
    {
        #region private
        internal const string PROC_InsertCourse = "usp_InsertCourse";
        internal const string PROC_UpdateCourse = "usp_UpdateCourse";
        internal const string PROC_DeleteCourse = "usp_DeleteCourse";
        internal const string PROC_getAllCourse = "usp_getAllCourse";
        internal const string PROC_getAllTrainer = "usp_SelectTrainerDetails";
        internal const string PROC_GetVendorDetails = "usp_SelectVendorTrainerDetails";
        internal const string PROC_GetAllCourseTypes = "usp_getCourseType";

        private const string PARAM_CourseId = "@courseId";
        private const string PARAM_CourseName = "@course";
        private const string PARAM_CourseDesc = "@Description";
        private const string PARAM_InternalExternal = "@InternalExternal";
        private const string PARAM_CourseType = "@course_type";
        private const string PARAM_Max_Participants = "@max_participants";
        private const string PARAM_CreatedBy = "@createdby";
        private const string PARAM_IsCourseIdExist = "@IsCourseIdExist";
        private const string PARAM_IsCourseNameExist = "@IsCourseNameExist";
        private const string PARAM_RowAffected = "@RowAffected";
        private const string PARAM_ModifiedBy = "@modifiedby";
        #endregion

        #region public Methods
        public bool createCourse(CourseInfo _course, out string errMsg, out int IsCourseIdExist, out int IsCourseNameExist, out int RowAffected)
        {
            string errMessage = string.Empty;
            errMsg = errMessage;
            bool rtnObj = false;

            IsCourseIdExist = 0;
            IsCourseNameExist = 0;
            RowAffected = 0;

            SqlCommand objCmd = new SqlCommand();
            try
            {

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
                {
                    objCmd.Connection = conn;
                    conn.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = PROC_InsertCourse;

                    objCmd.Parameters.Add(PARAM_CourseId, SqlDbType.VarChar, 50);
                    objCmd.Parameters[0].Value = _course.CourseId;

                    objCmd.Parameters.Add(PARAM_CourseName, SqlDbType.VarChar, 500);
                    objCmd.Parameters[1].Value = _course.CourseName;

                    objCmd.Parameters.Add(PARAM_CourseDesc, SqlDbType.VarChar, 100);
                    objCmd.Parameters[2].Value = _course.CourseDesc;

                    objCmd.Parameters.Add(PARAM_InternalExternal, SqlDbType.Char, 1);
                    objCmd.Parameters[3].Value = _course.InternalExternal;

                    objCmd.Parameters.Add(PARAM_CourseType, SqlDbType.NVarChar, 100);
                    objCmd.Parameters[4].Value = _course.CourseType;

                    objCmd.Parameters.Add(PARAM_CreatedBy, SqlDbType.VarChar, 7);
                    objCmd.Parameters[5].Value = _course.CreatedBy;

                    objCmd.Parameters.Add(PARAM_IsCourseIdExist, SqlDbType.Int);
                    objCmd.Parameters[6].Direction = ParameterDirection.Output;

                    objCmd.Parameters.Add(PARAM_IsCourseNameExist, SqlDbType.Int);
                    objCmd.Parameters[7].Direction = ParameterDirection.Output;

                    objCmd.Parameters.Add(PARAM_RowAffected, SqlDbType.Int);
                    objCmd.Parameters[8].Direction = ParameterDirection.Output;

                    objCmd.ExecuteNonQuery();
                    rtnObj = true;
                    IsCourseIdExist = Convert.ToInt32(objCmd.Parameters[PARAM_IsCourseIdExist].Value);
                    IsCourseNameExist = Convert.ToInt32(objCmd.Parameters[PARAM_IsCourseNameExist].Value);
                    RowAffected = Convert.ToInt32(objCmd.Parameters[PARAM_RowAffected].Value);
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

        public bool updateCourse(CourseInfo _course, out string errMsg, out int IsCourseNameExist, out int RowAffected)
        {
            throw new NotImplementedException();
        }

        public bool deleteCourse(CourseInfo _course, out string errMsg, out int RowAffected)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataRow getCourseDetail(string _courseId)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet getCourseDetail()
        {
            throw new NotImplementedException();
        }

        public SqlDataReader getAllCourse()
        {

            return SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getAllCourse, null);
        }

        public SqlDataReader getAllTtrainerDet(Hex.Vars.FB.HexFBModule.MasterInfo MasterMaillist)
        {
            SqlParameter[] sqlPar = new SqlParameter[3];

            sqlPar[0] = (new SqlParameter("@TrainerType", SqlDbType.VarChar, 1));
            sqlPar[0].Value = MasterMaillist.TrainerType;

            sqlPar[1] = (new SqlParameter("@vendorName", SqlDbType.VarChar, 50));
            sqlPar[1].Value = MasterMaillist.Vendorname;

            sqlPar[2] = (new SqlParameter("@courseid", SqlDbType.VarChar, 50));
            sqlPar[2].Value = MasterMaillist.CourseId;

            return SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getAllTrainer, sqlPar);
        }

        public SqlDataReader getVendorDet(int flag, Hex.Vars.FB.HexFBModule.MasterInfo _masterinfo)
        {

            SqlParameter[] sqlPar = new SqlParameter[2];

            sqlPar[0] = (new SqlParameter("@vendorName", SqlDbType.VarChar, 50));
            sqlPar[0].Value = _masterinfo.Vendorname;
            sqlPar[1] = (new SqlParameter("@flag", SqlDbType.VarChar, 1));
            sqlPar[1].Value = flag;

            return SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_GetVendorDetails, sqlPar);
        }

        public SqlDataReader getAllCourseType()
        {
            SqlParameter[] sqlPar = new SqlParameter[0];


            return SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_GetAllCourseTypes, sqlPar);
        }

        public DataTable SelectCourseAll()
        {
            SqlParameter[] spl = new SqlParameter[0];
            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getAllCourse, spl);
        }
        #endregion
    }
}
