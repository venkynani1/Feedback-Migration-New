/// ***********************************************************************************************
/// File name:NominationListInfo.cs
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
    public class NominationListInfo
    {
        #region Constructors
        public NominationListInfo() { }
        #endregion Constructors

        #region declarations

        private string _courseId=string.Empty;        
        private string _sessionid = string.Empty;
        private string _employeeid = string.Empty;
        private string _comments = string.Empty;                
        private string _createdBy=string.Empty;
        private string _modifiedBy = string.Empty;
        private int _Dispalcomments = 0;   
        private string _createdDate=string.Empty;
        private string _Status=string.Empty;
        private string _EmpMailID = string.Empty;
        private string _modifiedDate;

        private string _FromMailID = string.Empty;
        private string _ToMailID = string.Empty;
        private string _BccMailID = string.Empty;
        private string _CcMailID = string.Empty;
        private string _SubJect = string.Empty;
        private string _Body = string.Empty;
        private int _Flag = 0;   

        #endregion declarations

        #region Property

        public string FromMailID
        {
            get { return _FromMailID; }
            set { _FromMailID = value; }
        }

        public string ToMailID
        {
            get { return _ToMailID; }
            set { _ToMailID = value; }
        }

        public string BccMailID
        {
            get { return _BccMailID; }
            set { _BccMailID = value; }
        }

        public string CcMailID
        {
            get { return _CcMailID; }
            set { _CcMailID = value; }
        }

        public string SubJect
        {
            get { return _SubJect; }
            set { _SubJect = value; }
        }

        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }

        public string CourseId
        {
            get{return _courseId;}
            set { _courseId = value; }
        }
        public string SessionId
        {
            get { return _sessionid; }
            set { _sessionid = value; }
        }
        public string EmployeeId
        {
            get { return _employeeid; }
            set { _employeeid = value; }
        }
        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
        
        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }
        public string CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
        public string ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public string EmpMailID
        {
            get { return _EmpMailID; }
            set { _EmpMailID = value; }
        }
        public int Dispalcomments
        {
            get { return _Dispalcomments; }
            set { _Dispalcomments = value; }
        }

        public int Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }
        
        #endregion
    }
}
