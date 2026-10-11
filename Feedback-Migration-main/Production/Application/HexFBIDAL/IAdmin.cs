/// ***********************************************************************************************
/// File name: IAdmin.cs
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
using System.Data.SqlClient;
using System.Data;

namespace Hex.Vars.FB.HexFBIDAL
{
    public interface IAdmin
    {
        SqlDataReader searchEmployee(string EmpID,string EmpName);
        bool IsAdmin(string empid, out bool IsSuperAdmin);

        string FiveDigitID(string empid, out string FiveDigitID);
    }
}
