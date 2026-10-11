using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hex.Vars.FB.HexFBModule
{
    public class feedbackSupInfo
    {

        #region Declaration
        private string _courseid;
        private string _sessionid;
        private string _supervisorId;
        private string _subordinateId;
        private string _trainingRequired;
        private string _overallFeedback;
        private string _status;
        private string _success;
        private string _failure;
        private string _prbSA;
        private string _prgSkills;
        private string _commSkills;
        private string _behSkills;
        private string _performance;
        private string _ovrAllRating;
        private string _workArea;
        
        #endregion

        #region Properties
        public string Courseid
        {
            get { return _courseid; }
            set { _courseid = value; }
        }
        
        public string Sessionid
        {
            get { return _sessionid; }
            set { _sessionid = value; }
        }

        public string SupervisorId
        {
            get { return _supervisorId; }
            set { _supervisorId = value; }
        }

        public string SubordinateId
        {
            get { return _subordinateId; }
            set { _subordinateId = value; }
        }
        
        public string TrainingRequired
        {
            get { return _trainingRequired; }
            set { _trainingRequired = value; }
        }

        public string OverallFeedback
        {
            get { return _overallFeedback; }
            set { _overallFeedback = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string Failure
        {
            get { return _failure; }
            set { _failure = value; }
        }
        public string Success
        {
            get { return _success; }
            set { _success = value; }
        }

        public string OvrAllRating
        {
            get { return _ovrAllRating; }
            set { _ovrAllRating = value; }
        }

        public string Performance
        {
            get { return _performance; }
            set { _performance = value; }
        }

        public string PrbSA
        {
            get { return _prbSA; }
            set { _prbSA = value; }
        }

        public string PrgSkills
        {
            get { return _prgSkills; }
            set { _prgSkills = value; }
        }

        public string CommSkills
        {
            get { return _commSkills; }
            set { _commSkills = value; }
        }

        public string BehSkills
        {
            get { return _behSkills; }
            set { _behSkills = value; }
        }

        public string WorkArea
        {
            get { return _workArea; }
            set { _workArea = value; }
        }
        
        #endregion
    }
}
