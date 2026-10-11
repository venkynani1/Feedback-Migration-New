
/// ***********************************************************************************************
/// File name: AdminDAL.cs
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
    public class AdminDAL : IAdmin
    {
        #region private
        internal const string PROC_getHexaViewEmployees = "usp_getHexaViewEmployees";
        internal const string PROC_IsAdmin = "usp_IsAdmin";
        private const string PARAM_EmplID = "@empid";
        private const string PARAM_EmplName = "@EmplName";
        #endregion

        #region public Methods
        public SqlDataReader searchEmployee(string EmpID, string EmpName)
        {
            SqlParameter[] sqlPar = new SqlParameter[2];

            sqlPar[0] = (new SqlParameter(PARAM_EmplID, SqlDbType.VarChar, 7));
            sqlPar[0].Value = EmpID;

            sqlPar[1] = (new SqlParameter(PARAM_EmplName, SqlDbType.VarChar, 300));
            sqlPar[1].Value = EmpName;

            return SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_getHexaViewEmployees, sqlPar);
        }


        public bool IsAdmin(string empid, out bool IsSuperAdmin)
        {
            SqlParameter[] sqlPar = new SqlParameter[1];

            sqlPar[0] = (new SqlParameter(PARAM_EmplID, SqlDbType.VarChar, 7));
            sqlPar[0].Value = empid;
            SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, PROC_IsAdmin, sqlPar);
            if (dr != null)
            {
                if (dr.Read())
                {
                    int result = Convert.ToInt32(dr["IsSuperAdmin"]);
                    IsSuperAdmin = Convert.ToBoolean(result);

                    return true;
                }
                else
                {
                    IsSuperAdmin = false;
                    return false;
                }
            }
            else
            {
                IsSuperAdmin = false;
                return false;
            }
        }
        #endregion

    }
}
