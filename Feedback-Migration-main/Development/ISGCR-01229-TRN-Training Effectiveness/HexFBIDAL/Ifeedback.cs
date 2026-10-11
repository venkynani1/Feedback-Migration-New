
/// ***********************************************************************************************
/// File name: Ifeedback.cs
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
/// *************************************************************************************************using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hex.Vars.FB.HexFBModule;

namespace Hex.Vars.FB.HexFBIDAL
{
    public interface IFeedback
    {
        bool createFeedback(Hex.Vars.FB.HexFBModule.feedbackinfo _feedback, out string errMsg);
        bool createFeedbacks(Hex.Vars.FB.HexFBModule.feedbackinfo _feedback, out string errMsg);
        bool createSupFeedbacks(HexFBModule.feedbackSupInfo _supFeedback, out string errMsg);
        bool updatePMRemarks(int status, string remarks, string PM_ID, string sessionId, string supervisorId, out string errMsg);
    }
}
