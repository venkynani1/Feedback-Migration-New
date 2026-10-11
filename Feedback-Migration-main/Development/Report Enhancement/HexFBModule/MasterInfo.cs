/// ***********************************************************************************************
/// File name: MasterInfo.cs
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

namespace Hex.Vars.FB.HexFBModule
{
    public class MasterInfo
    {
        #region Constructors
        public MasterInfo() { }
        #endregion Constructors

        #region declarations
        private string _courseId = string.Empty;  
        private string _FromMail = string.Empty;
        private string _Ccc = string.Empty;
        private string _Bcc = string.Empty;
        private string _Subject = string.Empty;
        private string _Body = string.Empty;
        private string _Url = string.Empty;
        private string _TrainerName = string.Empty;
        private string _TrainerId = string.Empty;
        private string _Vendorname = string.Empty;
        private string _TrainerType = string.Empty;
        private string _PrefixID = string.Empty;
        #endregion declarations

        #region Property
        public string FromMail
        {
            get { return _FromMail; }
            set { _FromMail = value; }
        }
        public string Ccc
        {
            get { return _Ccc; }
            set { _Ccc = value; }
        }
        public string Bcc
        {
            get { return _Bcc; }
            set { _Bcc = value; }
        }
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }
        public string URL
        {
            get { return _Url; }
            set { _Url = value; }
        }
        public string TrainerId
        {
            get { return _TrainerId; }
            set { _TrainerId = value; }
        }
        public string TrainerName
        {
            get { return _TrainerName; }
            set { _TrainerName = value; }
        }
        public string Vendorname
        {
            get { return _Vendorname; }
            set { _Vendorname = value; }
        }
       public string TrainerType
        {
            get { return _TrainerType; }
            set { _TrainerType = value; }
        }
       public string PrefixID
       {
           get { return _PrefixID; }
           set { _PrefixID = value; }
       }
       public string CourseId
       {
           get { return _courseId; }
           set { _courseId = value; }
       }
     
        #endregion
    }
}
