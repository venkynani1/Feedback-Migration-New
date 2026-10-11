/// ***********************************************************************************************
/// File name: ISession.cs
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
using System.Data.Sql;
using System.Data.SqlClient;

namespace Hex.Vars.FB.HexFBIDAL
{
    public interface ISession
    {
        bool createSession(Hex.Vars.FB.HexFBModule.Sessioninfo _Session, out string errMsg,out bool IsRecordExist);
        bool InsertSessionTrainersDet(Hex.Vars.FB.HexFBModule.Sessioninfo _Session, out string errMsg);
        bool updateSession(Hex.Vars.FB.HexFBModule.Sessioninfo _Session, out string errMsg);
        bool deleteSession(Hex.Vars.FB.HexFBModule.Sessioninfo _Session, out string errMsg);
        //DataTable courseid();
        SqlDataReader getAllCourse();
        DataTable getquestions();
        DataTable GetEmployeeForfeedback(Hex.Vars.FB.HexFBModule.Sessioninfo _Session, out string errmsg);
        DataTable GetFeedbackCount(Hex.Vars.FB.HexFBModule.Sessioninfo _Session);

        SqlDataReader getindividual(string empid, string sessionid, string courseid, int flag);
        SqlDataReader getTrainersDets(string sessionid, string empid);
        SqlDataReader getSessionCourseWise(string _SessionId,int _year);
        DataTable getSessionSessionIdWiseNew1(string _SessionId);
        SqlDataReader getSessionSessionIdWise(string _sessionid);
        SqlDataReader getSessionEmpIdWise(string _SessionId);
        DataTable getYear();
        SqlDataReader getindividual_sup(string empid, string sessionid, string courseid, int flag);
        DataSet getSupFeedbackDetails(string empid, string sessionid);

        int GetmaxnoofParticipants(string _sessionid);

        DataTable GetAllTrainers(HexFBModule.Sessioninfo _Sessionid);
        DataTable getSessionAvailability(string _sessionId);
    }
}
