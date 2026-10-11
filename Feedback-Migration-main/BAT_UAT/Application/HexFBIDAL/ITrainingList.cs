/// ***********************************************************************************************
/// File name: ITrainingList.cs
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

namespace Hex.Vars.FB.HexFBIDAL
{
    public interface ITrainingList
    {
        
        /// <summary>
        /// get all TrainigDetails with req
        /// </summary>
        /// <returns></returns>
        SqlDataReader getTrainingsEmpIdWise(string EmpId,string IsAdmin);
        DataTable getTrainingsEmpIdWiseNew(string EmpId, string IsAdmin);
        DataTable getTrainingsSupWiseNew(string EmpId, string IsAdmin);
        DataTable getReviews(string EmpId);
    }
}
