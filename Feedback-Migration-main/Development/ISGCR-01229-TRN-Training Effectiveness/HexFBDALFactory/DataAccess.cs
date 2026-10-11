/// ***********************************************************************************************
/// File name: DataAccess.cs
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
using Hex.Vars.FB.HexFBIDAL;

namespace Hex.Vars.FB.HexFBDALFactory
{
    public sealed class DataAccess
    {
        #region private
        private DataAccess() { }
        #endregion

        #region public Methods
        public static Hex.Vars.FB.HexFBIDAL.ISession CreateSession()
        {
            return (Hex.Vars.FB.HexFBIDAL.ISession)new Hex.Vars.FB.HexFBDAL.sessionDAL();
        }
        public static Hex.Vars.FB.HexFBIDAL.ISession courseid()
        {
            return(Hex.Vars.FB.HexFBIDAL.ISession)new Hex.Vars.FB.HexFBDAL.sessionDAL();
        }
        public static Hex.Vars.FB.HexFBIDAL.ICourse CreateCourse()
        {
            return (Hex.Vars.FB.HexFBIDAL.ICourse)new Hex.Vars.FB.HexFBDAL.CourseDAL();
        }
        public static Hex.Vars.FB.HexFBIDAL.IFeedback CreateFeedback()
        {
            return (Hex.Vars.FB.HexFBIDAL.IFeedback)new Hex.Vars.FB.HexFBDAL.FeedbackDAL();
        }
        public static Hex.Vars.FB.HexFBIDAL.ITrainingList CreateTrainingList()
        {
            return (Hex.Vars.FB.HexFBIDAL.ITrainingList)new Hex.Vars.FB.HexFBDAL.TrainingListDAL();

        }
        public static Hex.Vars.FB.HexFBIDAL.INominationList CreateNominationList()
        {
            return (Hex.Vars.FB.HexFBIDAL.INominationList)new Hex.Vars.FB.HexFBDAL.NominationListDAL();
        }
        public static Hex.Vars.FB.HexFBIDAL.IAdmin CreateAdmin()
        {
            return (Hex.Vars.FB.HexFBIDAL.IAdmin)new Hex.Vars.FB.HexFBDAL.AdminDAL();
        }
        public static Hex.Vars.FB.HexFBIDAL.IReport GetoverallRpt()
        {
            return (Hex.Vars.FB.HexFBIDAL.IReport)new Hex.Vars.FB.HexFBDAL.ReportDAL();
        }
        #endregion

    }
}
