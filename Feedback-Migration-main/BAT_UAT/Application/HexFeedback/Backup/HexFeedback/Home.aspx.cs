#region Header
/// ***********************************************************************************************
/// File name: Home.aspx.cs
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
#endregion

#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

#region class
namespace HexFeedback
{
    public partial class Home1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] != null)
            {
                string temp = "";
                if (Session["Admin"] != null)
                {
                    temp = Session["Admin"].ToString();
                }
                else if (Session["IsSuperAdmin"] != null)
                {
                    temp = Session["IsSuperAdmin"].ToString();
                }
                string enablehome = Request.QueryString["IsAdmin"];
                string TestID = Request.QueryString["TestID"];
            }

        }
    }
}
#endregion