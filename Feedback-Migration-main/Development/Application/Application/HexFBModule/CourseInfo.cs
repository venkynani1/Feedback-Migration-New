/// ***********************************************************************************************
/// File name: Courseinfo.cs
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
    public class CourseInfo
    {
        #region Constructors
        public CourseInfo() { }
        #endregion Constructors

        #region declarations

        private string _courseId=string.Empty;        
        private string _courseName = string.Empty;
        private string _courseDesc = string.Empty;
        private string _internalExternal = string.Empty;
        private string _courseType = string.Empty;
        private int _max_Participants; 
        private string _createdBy=string.Empty;
        private string _modifiedBy = string.Empty;        
        private string _createdDate;
        private string _modifiedDate;
              

        #endregion declarations

        #region Property
        public string CourseId
        {
            get{return _courseId;}
            set { _courseId = value; }
        }
        public string CourseName
        {
            get { return _courseName; }
            set { _courseName = value; }
        }
        public string CourseDesc
        {
            get { return _courseDesc; }
            set { _courseDesc = value; }
        }
        public string InternalExternal
        {
            get { return _internalExternal; }
            set { _internalExternal = value; }
        }
        public string CourseType
        {
            get { return _courseType; }
            set { _courseType = value; }
        }
        public int Max_Participants
        {
            get { return _max_Participants; }
            set { _max_Participants = value; }
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
        #endregion
    }
}
