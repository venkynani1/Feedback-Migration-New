/// ***********************************************************************************************
/// File name: SessionBLL.cs
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
using System.Data.SqlClient;
using System.Data.Sql;
using Hex.Vars.FB.HexFBIDAL;   


namespace Hex.Vars.FB.HexFBBLL
{
    public class SessionBLL
    {
        #region private
        private static readonly ISession SessionDal = Hex.Vars.FB.HexFBDALFactory.DataAccess.CreateSession();

        //private static readonly ISession SessionDals = Hex.Vars.FB.HexFBDALFactory.DataAccess.courseid();
        
        #endregion //private
               
        #region public methods
        public bool createSession(Hex.Vars.FB.HexFBModule.Sessioninfo _session, out string errMsg, out bool IsRecordExist)
        {
            return SessionDal.createSession(_session, out errMsg, out IsRecordExist);
        }
 public bool InsertSessionTrainers(Hex.Vars.FB.HexFBModule.Sessioninfo _session, out string errMsg)
        {
            return SessionDal.InsertSessionTrainersDet(_session, out errMsg);
        }
        public bool updateSession(Hex.Vars.FB.HexFBModule.Sessioninfo _session, out string errMsg)
        {
            
            return SessionDal.updateSession(_session, out errMsg);
        }
        public bool deleteSession(Hex.Vars.FB.HexFBModule.Sessioninfo _session, out string errMsg)
        {
            return SessionDal.updateSession(_session, out errMsg);
        }
        public DataTable GetFeedbackForEmployeeId(Hex.Vars.FB.HexFBModule.Sessioninfo _Sessionid, out string errMsg)
        {
            return SessionDal.GetEmployeeForfeedback(_Sessionid, out errMsg);
        }
        public DataTable GetFeedbackCounts(Hex.Vars.FB.HexFBModule.Sessioninfo _Sessionid)
        {
            return SessionDal.GetFeedbackCount(_Sessionid);
        }

        public DataTable GetAllTrainers(Hex.Vars.FB.HexFBModule.Sessioninfo _Sessionid)
        {
            return SessionDal.GetAllTrainers(_Sessionid);
        }

        public SqlDataReader getAllCourse()
        {
            return SessionDal.getAllCourse();
        }
        public DataTable getquestions()
        {
            return SessionDal.getquestions();
        }
        public SqlDataReader getindividual(string empid,string sessionid,string courseid,int flag)
        {

            return SessionDal.getindividual(empid, sessionid, courseid, flag);
        }
        public SqlDataReader getTrainers(string sessionid, string empid)
        {

            return SessionDal.getTrainersDets(sessionid, empid);
        }

        public SqlDataReader getSessionCourseWise(string _courseid,int _year)
        {
            return SessionDal.getSessionCourseWise(_courseid,_year);
        }
        public DataTable getSessionSessionIdWiseNew1(string _sessionid)
        {
            return SessionDal.getSessionSessionIdWiseNew1(_sessionid);
        }
        public SqlDataReader getSessionSessionIdWise(string _sessionid)
        {
            return SessionDal.getSessionSessionIdWise(_sessionid);
        }
        public int GetmaxnoofParticipants(string _sessionid)
        {
            return SessionDal.GetmaxnoofParticipants(_sessionid);
        }
        public SqlDataReader getSessionEmployeeIdWise(string _sessionid)
        {
            return SessionDal.getSessionEmpIdWise(_sessionid);
        }

        public DataTable getYear()
        {
            return SessionDal.getYear();
        }

        public SqlDataReader getindividual_sup(string empid, string sessionid, string courseid, int flag)
        {
            return SessionDal.getindividual_sup(empid, sessionid, courseid, flag);
        }

        public DataSet getSupFeedbackDetails(string empid, string sessionid, string pm_id)
        {
            return SessionDal.getSupFeedbackDetails(empid, sessionid, pm_id);
        }
        public DataTable getSessionAvailability(string _sessionId)
        {
            return SessionDal.getSessionAvailability(_sessionId);
        }
        #endregion
        
    }
}
