/// ***********************************************************************************************
/// File name: TrainingListBLL.cs
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
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Hex.Vars.FB.HexFBBLL
{
    public class TrainingListBLL
    {
        #region private
        private static readonly ITrainingList TrainingListDAL = Hex.Vars.FB.HexFBDALFactory.DataAccess.CreateTrainingList();
        #endregion

        #region Public methods
        public SqlDataReader getTrainingsEmpIdWise(string EmpId, string IsAdmin)
        {
            return TrainingListDAL.getTrainingsEmpIdWise(EmpId, IsAdmin);
        }
        public DataTable getTrainingsEmpIdWiseNew(string EmpId, string IsAdmin)
        {
            return TrainingListDAL.getTrainingsEmpIdWiseNew(EmpId, IsAdmin);
        }
        #endregion
    }
}
