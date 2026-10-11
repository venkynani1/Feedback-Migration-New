/// ***********************************************************************************************
/// File name: feedbackinfo.cs
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
    public class feedbackinfo
    {
        #region Constructors
        public feedbackinfo()
        {

        }
        #endregion Constructors
        #region declaration
        protected string _empname;
        protected string _empid;
        protected string _trainername;
        protected string _courseid;
        protected string _sessionid;
        protected string _date;
        protected string _location;
        protected string _ques1_1;
        protected string _ques1_2;
        protected string _ques1_3;
        protected string _ques1_4;
        protected string _ques1_5; 
        protected string _ques2_3;
        protected string _ques2_4;
        protected string _ques2_5;
        protected string _ques2_6;
        protected string _ques2_7;
        protected string _ques2_8;
        protected string _ques2_9;
        protected string _ques3_1;
        protected string _ques3_2;
        protected string _ques3_3;
        protected string _ques3_4;
        protected string _ques3_5;
        protected string _ques3_6;
        protected string _ans1_1;
        protected string _ans1_2;
        protected string _ans1_3;
        protected string _ans1_4;
        protected string _ans1_5;
        protected string _ans2_3;
        protected string _ans2_4;
        protected string _ans2_5;
        protected string _ans2_6;
        protected string _ans2_7;
        protected string _ans2_8;
        protected string _ans2_9;
        protected string _ans3_1;
        protected string _ans3_2;
        protected string _ans3_3;
        protected string _ans3_4;
        protected string _ans3_5;
        protected string _ans3_6;
        protected string _comments;
        protected string _success;
        protected string _TrainerId;
        protected string _failure;
        protected int _CommonFlag;
        #endregion declaration
        #region property
        public string response1
        {
            get
            {
                return _ques1_1;
            }
            set
            {
                _ques1_1 = value;
            }
        }
        public string response2
        {
            get
            {
                return _ques1_2;
            }
            set
            {
                _ques1_2 = value;
            }
        }
        public string response3
        {
            get
            {
                return _ques1_3;
            }
            set
            {
                _ques1_3 = value;
            }
        }
        public string response4
        {
            get
            {
                return _ques1_4;
            }
            set
            {
                _ques1_4 = value;
            }
        }
        public string response5
        {
            get
            {
                return _ques1_5;
            }
            set
            {
                _ques1_5 = value;
            }
        }
        public string response6
        {
            get
            {
                return _ques2_3;
            }
            set
            {
                _ques2_3 = value;
            }
        }
        public string response7
        {
            get
            {
                return _ques2_4;
            }
            set
            {
                _ques2_4 = value;
            }
        }
        public string response8
        {
            get
            {
                return _ques2_5;
            }
            set
            {
                _ques2_5 = value;
            }
        }
        public string response9
        {
            get
            {
                return _ques2_6;
            }
            set
            {
                _ques2_6 = value;
            }
        }
        public string response10
        {
            get
            {
                return _ques2_7;
            }
            set
            {
                _ques2_7= value;
            }
        }
        public string response11
        {
            get
            {
                return _ques2_8;
            }
            set
            {
                _ques2_8= value;
            }
        }
        public string response12
        {
            get
            {
                return _ques2_9;
            }
            set
            {
                _ques2_9= value;
            }
        }
        public string response13
        {
            get
            {
                return _ques3_1;
            }
            set
            {
                _ques3_1 = value;
            }
        }
        public string response14
        {
            get
            {
                return _ques3_2;
            }
            set
            {
                _ques3_2 = value;
            }
        }
        public string response15
        {
            get
            {
                return _ques3_3;
            }
            set
            {
                _ques3_3 = value;
            }
        }
        public string response16
        {
            get
            {
                return _ques3_4;
            }
            set
            {
                _ques3_4 = value;
            }
        }
        public string response17
        {
            get
            {
                return _ques3_5;
            }
            set
            {
                _ques3_5 = value;
            }
        }
        public string response18
        {
            get
            {
                return _ques3_6;
            }
            set
            {
                _ques3_6 = value;
            }
        }
      
        public string traineeid
        {
            get
            {
                return _empid;
            }
            set
            {
                _empid= value;
            }
        }
        public string courseid
        {
            get
            {
                return _courseid;
            }
            set
            {
                _courseid = value;
            }
        }
        public string sessionid
        {
            get
            {
                return _sessionid;
            }
            set
            {
                _sessionid = value;
            }
        }
        public string question1
        {
            get
            {
                return _ans1_1;
            }
            set
            {
                _ans1_1 = value;
            }
        }
        public string question2
        {
            get
            {
                return _ans1_2;
            }
            set
            {
                _ans1_2 = value;
            }
        }
        public string question3
        {
            get
            {
                return _ans1_3;
            }
            set
            {
                _ans1_3 = value;
            }
        }
        public string question4
        {
            get
            {
                return _ans1_4;
            }
            set
            {
                _ans1_4 = value;
            }
        }
        public string question5
        {
            get
            {
                return _ans1_5;
            }
            set
            {
                _ans1_5 = value;
            }
        }
        public string question6
        {
            get
            {
                return _ans2_3;
            }
            set
            {
                _ans2_3 = value;
            }
        }
        public string question7
        {
            get
            {
                return _ans2_4;
            }
            set
            {
                _ans2_4 = value;
            }
        }
        public string question8
        {
            get
            {
                return _ans2_5;
            }
            set
            {
                _ans2_5 = value;
            }
        }
        public string question9
        {
            get
            {
                return _ans2_6;
            }
            set
            {
                _ans2_6 = value;
            }
        }
        public string question10
        {
            get
            {
                return _ans2_7;
            }
            set
            {
                _ans2_7 = value;
            }
        }
        public string question11
        {
            get
            {
                return _ans2_8;
            }
            set
            {
                _ans2_8 = value;
            }
        }
        public string question12
        {
            get
            {
                return _ans2_9;
            }
            set
            {
                _ans2_9 = value;
            }
        }
        public string question13
        {
            get
            {
                return _ans3_1;
            }
            set
            {
                _ans3_1 = value;
            }
        }
        public string question14
        {
            get
            {
                return _ans3_2;
            }
            set
            {
                _ans3_2 = value;
            }
        }
        public string question15
        {
            get
            {
                return _ans3_3;
            }
            set
            {
                _ans3_3 = value;
            }
        }
        public string question16
        {
            get
            {
                return _ans3_4;
            }
            set
            {
                _ans3_4 = value;
            }
        }
        public string question17
        {
            get
            {
                return _ans3_5;
            }
            set
            {
                _ans3_5 = value;
            }
        }
        public string question18
        {
            get
            {
                return _ans3_6;
            }
            set
            {
                _ans3_6 = value;
            }
        }

        public string comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
            }
        }
        public string success
        {
            get
            {
                return _success;
            }
            set
            {
                _success = value;
            }
        }
        public string TrainerId
        {
            get
            {
                return _TrainerId;
            }
            set
            {
                _TrainerId = value;
            }
        }
        public string failure
        {
            get
            {
                return _failure;
            }
            set
            {
                _failure=value;
            }
        }

        public int CommonFlag
        {
            get
            {
                return _CommonFlag;
            }
            set
            {
                _CommonFlag = value;
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
