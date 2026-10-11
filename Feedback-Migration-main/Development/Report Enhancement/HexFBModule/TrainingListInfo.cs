
/// ***********************************************************************************************
/// File name: TrainingListInfo.cs
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
        public class TrainingListInfo
        {
            #region Constructors
            public TrainingListInfo()
            {

            }
            #endregion Constructors

            #region declarations
            protected string _course_id;
            protected string _courseName;
            protected string _session_id;
            protected string _sessionName;
            protected DateTime _startdate;
            protected DateTime _enddate;                                                            
            
            #endregion declarations

            #region property

            /*iyass    public string strName { get; set; }*/

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
                    _course_id = value;
                }
            }
            public string sessionid
            {
                get
                {
                    return _session_id;
                }
                set
                {
                    _session_id = value;
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
            public string IsAdmin
            {
                get;
                set;
            }
            #endregion property

        }
}
