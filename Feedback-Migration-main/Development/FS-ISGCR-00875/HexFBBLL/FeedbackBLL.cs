/// ***********************************************************************************************
/// File name: FeedbackBLL.cs
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
using Hex.Vars.FB.HexFBIDAL;
namespace Hex.Vars.FB.HexFBBLL
{
    public class FeedbackBLL
    {
        #region private
        private static readonly IFeedback FeedbackDAL = Hex.Vars.FB.HexFBDALFactory.DataAccess.CreateFeedback();
        #endregion

        #region public methods
        public bool createFeedback(Hex.Vars.FB.HexFBModule.feedbackinfo _feedback, out string errMsg)
        {
            return FeedbackDAL.createFeedback(_feedback,out errMsg);
        }
        public bool createFeedbacks(Hex.Vars.FB.HexFBModule.feedbackinfo _feedback, out string errMsg)
        {
            return FeedbackDAL.createFeedbacks(_feedback, out errMsg);
        }
        #endregion
    }
}
