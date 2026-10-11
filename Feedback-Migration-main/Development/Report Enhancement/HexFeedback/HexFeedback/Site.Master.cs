#region Header
/// ***********************************************************************************************
/// File name: Site.Master.cs
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
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;

#endregion

#region Class
namespace HexFeedback
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        Hex.Vars.FB.HexFBBLL.SessionBLL _session = new Hex.Vars.FB.HexFBBLL.SessionBLL();
        Hex.Vars.FB.HexFBModule.NominationListInfo nominationlist = new Hex.Vars.FB.HexFBModule.NominationListInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] != null || Session["IsSuperAdmin"] != null)
            {
                AdminMenu.Visible = true;
            }
            else
            {
                AdminMenu.Visible = false;
            }
            if (Session["username"] != null)
            {
                nominationlist.Flag = 1;
                SqlDataReader dr = _session.getindividual(Session["username"].ToString(), null, null, nominationlist.Flag);
                while (dr.Read())
                {
                    lblWelcomeMsg.Text = "Welcome, " + dr["AdminName"].ToString();
                }
            }
        }

        protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
        {

        }

        protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect(FormsAuthentication.LoginUrl);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

    }
}
#endregion