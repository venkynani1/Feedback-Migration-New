/// ***********************************************************************************************
/// File name: ReportBLL.cs
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
using System.Data;

namespace Hex.Vars.FB.HexFBBLL
{
    public class ReportBLL
    {
        #region private
        private static readonly IReport ReportDal  = Hex.Vars.FB.HexFBDALFactory.DataAccess.GetoverallRpt();
        #endregion

        #region public methods
        public DataTable GetFeedback(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg)
        {
            return ReportDal.GetReportForfeedback(_Sessionid,out errMsg);
        }
        public DataTable GetFeedbackabsEmp(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid ,out string errMsg)
        {
            return ReportDal.GetAbscommForfeedback(_Sessionid,out errMsg);
        }
        public DataTable GetFeedbackabsEmp1(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg)
        {
            return ReportDal.GetAbscommForfeedback1(_Sessionid, out errMsg);
        }
        public DataSet Getfeedbackrating(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg)
        {
            return ReportDal.GetRatingForfeedback(_Sessionid, out errMsg);
        }
        public DataTable GetfeedbackratingCount(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg)
        {
            return ReportDal.GetRatingForfeedbackCount(_Sessionid, out errMsg);
        }
        public DataTable GetfeedbackPattern(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg)
        {
            return ReportDal.GetPatternForfeedback(_Sessionid, out errMsg);
        }
        public DataTable GetfeedbackConcern(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg)
        {
            return ReportDal.GetConcernForfeedback(_Sessionid, out errMsg);
        }
        public DataTable GetfeedbackBalance(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg)
        {
        return ReportDal.GetBalanceForfeedback(_Sessionid, out errMsg);
        }
        public DataTable GetfeedbackComments(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg)
        {
          return ReportDal.GetCommentsForfeedback(_Sessionid, out errMsg);
        }
        #endregion
    }
}
