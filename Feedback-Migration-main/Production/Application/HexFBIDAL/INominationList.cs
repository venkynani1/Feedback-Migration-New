
/// ***********************************************************************************************
/// File name: INominationList.cs
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
/// *************************************************************************************************using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using Hex.Vars.FB.HexFBModule;     

namespace Hex.Vars.FB.HexFBIDAL
{
    public interface INominationList
    {
        bool insertNominationList(NominationListInfo nominationinfo,out string errmsg,out bool IsRecordExists,out int rowAffected);
        bool UpdateVaildCommentsDetails(NominationListInfo nominationinfo, out string errmsg, out bool IsRecordExists, out int rowAffected);
        bool InsetMasterDet(MasterInfo MasterMaillist, out string errmsg);
        bool InsetTrainerDetails(MasterInfo MasterMaillist, out string errmsg, out bool IsRecordExists);
        bool InsetVendorDetails(MasterInfo MasterMaillist, out string errmsg, out bool IsRecordExists);
        DataTable SelectMasterDet(MasterInfo MasterMaillist, out string errmsg);
        bool MailstatusDeli(NominationListInfo nominationlist);
        DataTable SelectNominationList(NominationListInfo nominationinfo, out string errmsg);
        DataTable SelectDeliveryList(NominationListInfo nominationinfo, out string errmsg);
        DataTable Sendmailforfeed(string frommail, string tomail, string sub, string bdy, string ccc, string bcc);
        OleDbDataReader importExcelFile(string FileLocation, out string errMsg);
        DataSet MailRelatedDetails(NominationListInfo nominationlist, out string errmsg);
        DataSet GetSupervisorsForMail(string sessionId, out string errmsg);
        bool insertMasterSupervisorList(string sessionId);
    }
}
