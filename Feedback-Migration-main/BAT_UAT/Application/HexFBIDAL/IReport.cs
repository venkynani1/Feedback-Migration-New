/// ***********************************************************************************************
/// File name: IReport.cs
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
    public interface IReport
    {
        DataTable GetReportForfeedback(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg);
        DataTable GetAbscommForfeedback(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg);
        DataTable GetAbscommForfeedback1(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg);
        DataSet GetRatingForfeedback(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg);
        DataTable GetRatingForfeedbackCount(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg);
        DataTable GetPatternForfeedback(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg);
        DataTable GetConcernForfeedback(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg);
        DataTable GetBalanceForfeedback(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg);
        DataTable GetCommentsForfeedback(Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid, out string errMsg, string TrainerId=null);
        DataSet GetTrnEffectivenessReport(string _Sessionid, out string errMsg);
    }
}
