/// ***********************************************************************************************
/// File name: NominationListBLL.cs
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
using Hex.Vars.FB.HexFBIDAL;    
using System.Data.OleDb;

namespace Hex.Vars.FB.HexFBBLL
{
    public class NominationListBLL
    {
        #region private
        private static readonly INominationList NominationListDal  = Hex.Vars.FB.HexFBDALFactory.DataAccess.CreateNominationList();
        #endregion 

        #region Public Methods
        public OleDbDataReader importExcelFile(string FileLocation, out string errMsg)
        {
            return NominationListDal.importExcelFile(FileLocation,out errMsg);     
        }
        public bool insertNominationList(HexFBModule.NominationListInfo _nominationinfo, out string errmsg, out bool IsRecordExists, out int rowAffected)
        {
            return NominationListDal.insertNominationList(_nominationinfo, out errmsg, out IsRecordExists, out rowAffected);
        }
        public bool UpdateVaildComments(HexFBModule.NominationListInfo _nominationinfo, out string errmsg, out bool IsRecordExists, out int rowAffected)
        {
            return NominationListDal.UpdateVaildCommentsDetails(_nominationinfo, out errmsg, out IsRecordExists, out rowAffected);
        }
        public bool insertMasterDet(HexFBModule.MasterInfo _MasterMaillist, out string errmsg)
        {
            return NominationListDal.InsetMasterDet(_MasterMaillist, out errmsg);
        }
        public bool insertTrainerDet(HexFBModule.MasterInfo _MasterMaillist, out string errmsg, out bool IsRecordExists)
        {
            return NominationListDal.InsetTrainerDetails(_MasterMaillist, out errmsg, out IsRecordExists);
        }
        public bool insertVendorDet(HexFBModule.MasterInfo _MasterMaillist, out string errmsg, out bool IsRecordExists)
        {
            return NominationListDal.InsetVendorDetails(_MasterMaillist, out errmsg, out IsRecordExists);
        }
        public DataTable SelcMailMaster(HexFBModule.MasterInfo _MasterMaillist, out string errmsg)
        {
            return NominationListDal.SelectMasterDet(_MasterMaillist, out errmsg);
        }
        public bool insertMailstatu(HexFBModule.NominationListInfo nominationlist)
        {
            return NominationListDal.MailstatusDeli(nominationlist);
        }
        public DataTable SendmailList(string frommail, string tomail, string sub, string bdy, string ccc, string bcc)
        {
            return NominationListDal.Sendmailforfeed( frommail, tomail, sub, bdy, ccc, bcc);
        }
        public DataTable SelectNominateEmpl(HexFBModule.NominationListInfo _nominationinfo,out string errmsg)
        {
            return NominationListDal.SelectNominationList(_nominationinfo, out errmsg);
        }
        public DataTable SelectMailDeliverstat(HexFBModule.NominationListInfo _nominationinfo, out string errmsg)
        {
            return NominationListDal.SelectDeliveryList(_nominationinfo, out errmsg);
        }
        public DataSet MailRelatedDetails(HexFBModule.NominationListInfo nominationlist, out string errmsg)
        {
            return NominationListDal.MailRelatedDetails(nominationlist, out errmsg);
        }
        #endregion

    }
}
