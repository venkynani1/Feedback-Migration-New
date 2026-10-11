
/// ***********************************************************************************************
/// File name: TrainingDAL.cs
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
    public class TrainingListDAL:ITrainingList
    {
        #region private
        internal const string PROC_getTrainingEmpidWise = "usp_getTrainingEmpidWise";

        private const string PARAM_empid = "@empid";
        private const string PARAM_IsAdmin = "@IsAdmin";
        #endregion

        #region public Methods
        public SqlDataReader getTrainingsEmpIdWise(string EmpId,string IsAdmin)
        {

            SqlParameter[] sqlPar = new SqlParameter[2];
            
            sqlPar[0] = (new SqlParameter(PARAM_empid, SqlDbType.VarChar, 7));
            sqlPar[0].Value = EmpId;
            sqlPar[1] = (new SqlParameter(PARAM_IsAdmin, SqlDbType.VarChar, 20));
            sqlPar[1].Value = IsAdmin;

            return SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getTrainingEmpidWise, sqlPar); 
        }
        public DataTable getTrainingsEmpIdWiseNew(string EmpId, string IsAdmin)
        {

            SqlParameter[] sqlPar = new SqlParameter[2];

            sqlPar[0] = (new SqlParameter(PARAM_empid, SqlDbType.VarChar, 7));
            sqlPar[0].Value = EmpId;

            sqlPar[1] = (new SqlParameter(PARAM_IsAdmin, SqlDbType.VarChar, 20));
            sqlPar[1].Value = IsAdmin;

            return SqlHelper.getvalue(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getTrainingEmpidWise, sqlPar);
        }
        #endregion
    }
}
