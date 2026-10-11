/// ***********************************************************************************************
/// File name: AdminBLL.cs
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
using Hex.Vars.FB.HexFBIDAL;
using System.Data.SqlClient; 

namespace Hex.Vars.FB.HexFBBLL
{
    public class AdminBLL
    {
        #region private
        private static readonly IAdmin AdminDal = Hex.Vars.FB.HexFBDALFactory.DataAccess.CreateAdmin();
        #endregion //private

        #region public methods
        public SqlDataReader searchEmployee(string EmpID, string EmpName)
        {
            return AdminDal.searchEmployee(EmpID, EmpName);  
        }
        public bool IsAdmin(string empid, out bool IsSuperAdmin)
        {
            return AdminDal.IsAdmin(empid, out IsSuperAdmin);
        }

        #endregion

    }
}
