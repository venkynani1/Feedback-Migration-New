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
        private string _TechSkills;
        private string _SoftSkills;
        private string _commSkills;
        private string _behSkills;
        private string _performance;
        private string _ovrAllRating;
        private string _workArea;

        public string Courseid
        {
            get => this._courseid;
            set => this._courseid = value;
        }

        public string Sessionid
        {
            get => this._sessionid;
            set => this._sessionid = value;
        }

        public string SupervisorId
        {
            get => this._supervisorId;
            set => this._supervisorId = value;
        }

        public string SubordinateId
        {
            get => this._subordinateId;
            set => this._subordinateId = value;
        }

        public string TrainingRequired
        {
            get => this._trainingRequired;
            set => this._trainingRequired = value;
        }

        public string OverallFeedback
        {
            get => this._overallFeedback;
            set => this._overallFeedback = value;
        }

        public string Status
        {
            get => this._status;
            set => this._status = value;
        }

        public string Failure
        {
            get => this._failure;
            set => this._failure = value;
        }

        public string Success
        {
            get => this._success;
            set => this._success = value;
        }

        public string OvrAllRating
        {
            get => this._ovrAllRating;
            set => this._ovrAllRating = value;
        }

        public string Performance
        {
            get => this._performance;
            set => this._performance = value;
        }

        public string TechSkills
        {
            get => this._TechSkills;
            set => this._TechSkills = value;
        }

        public string SoftSkills
        {
            get => this._SoftSkills;
            set => this._SoftSkills = value;
        }

        public string CommSkills
        {
            get => this._commSkills;
            set => this._commSkills = value;
        }

        public string BehSkills
        {
            get => this._behSkills;
            set => this._behSkills = value;
        }

        public string WorkArea
        {
            get => this._workArea;
            set => this._workArea = value;
        }

        #endregion
    }
}
