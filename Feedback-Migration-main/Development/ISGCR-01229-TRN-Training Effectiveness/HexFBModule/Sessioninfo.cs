/// ***********************************************************************************************
/// File name: SessionInfo.cs
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
    public class Sessioninfo
    {
        #region Constructors
        public Sessioninfo()
        {

        }
        #endregion Constructors

        #region declarations
        protected string _course_id;
        protected string _courseName;
        protected string _session;
        protected DateTime _startdate;
        protected DateTime _enddate;
        protected string _createdby = string.Empty;
        protected int _maxparitcipants;
        protected string _trainername;
        protected string _location;
        protected string _empid;
        protected string _Trainerid;
        protected DateTime _createddate;
        protected DateTime _modifieddate;
        protected string _modifiedby = string.Empty;
        protected bool _isdeleted = false;
        private string _;
        #endregion declarations

        #region property
        public string location { 
            get {
                return _location;
        }
            set {
                _location = value;
            }
        }
        
        public string courseid
        {
            get
            {
                return _course_id;
            }
            set
            {
                _course_id = value;
            }
        }
        public string courseName
        {
            get
            {
                return _courseName;
            }
            set
            {
                _courseName = value;
            }
        }
        public string session
        {
            get
            {
                return _session;
            }
            set
            {
                _session = value;
            }
        }
        public DateTime startdate
        {
            get
            {
                return _startdate;
            }
            set
            {
                _startdate = value;
            }
        }

        public DateTime Enddate
        {
            get
            {
                return _enddate;
            }
            set
            {
                _enddate = value;
            }
        }
        public int maximumparticipants
        {
            get {return _maxparitcipants;}
            set { _maxparitcipants = value; }
        }

        public string createdby
        {
            get { return _createdby; }
            set {_createdby=value;  }
        }
        public string modifiedby
        {
            get { return _modifiedby; }
            set { _modifiedby = value; }
        }
        public DateTime createddate
        {
            get
            {
                return _createddate;
            }
            set
            {
                _createddate = value;
            }
        }
        public DateTime modifieddate
        {
            get
            {
                return _modifieddate;
            }
            set
            {
                _modifieddate = value;
            }
        }

        public string Trainername
        {
            get
            {
                return _trainername;
            }
            set
            {
                _trainername = value;
            }
        }
        public string empid
        {
            get
            {
                return _empid;
            }
            set
            {
                _empid = value;
            }
        }
        public string Trainerid
        {
            get
            {
                return _Trainerid;
            }
            set
            {
                _Trainerid = value;
            }
        }
        
        #endregion property

    }
}
