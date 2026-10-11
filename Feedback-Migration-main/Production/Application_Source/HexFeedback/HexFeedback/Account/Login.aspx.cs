#region Header
/// ***********************************************************************************************
/// File name: Login.aspx.cs
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
using System.DirectoryServices;
using System.Web.Security;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;
using Hex.Vars.FB.HexFBBLL;
//using HexFeedback.WebReferences;




#endregion

#region class
namespace HexFeedback.Account
{
    public partial class Login : System.Web.UI.Page
    {

        Authentication.StationHAuthentication helixAuthentication = new Authentication.StationHAuthentication();
        //StationHAuthentication helixAuthentication = new StationHAuthentication();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session.RemoveAll();
            }
        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
        }
        protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
        {
            const string domain = "CORP";
            String domainAndUsername = domain + "\\" + LoginUser.UserName;
            DirectoryEntry entry = new DirectoryEntry();
            Session["username"] = LoginUser.UserName;
            entry.Username = domainAndUsername;
            entry.Password = LoginUser.Password;
            //Object obj;
            try
            {

                //int Status = helixAuthentication.IsValidUser(LoginUser.UserName, LoginUser.Password);

                int Status = 1;

                if (Status == 1)
                {
                    //int NoofDays = helixAuthentication.GetDaysRemaining(LoginUser.UserName);
                    e.Authenticated = true;
                }
                else
                {
                    e.Authenticated = false;
                    return;
                }

            }
            catch (Exception ex)
            {
                e.Authenticated = false;
                return;
            }

            if (LoginUser.RememberMeSet)
            {

                AdminBLL adminbll = new AdminBLL();

                bool IsSuperAdmin = true;

                bool IsAdmin = adminbll.IsAdmin(LoginUser.UserName, out IsSuperAdmin);

                if (IsSuperAdmin)
                {
                    Session["IsSuperAdmin"] = "STrue";
                    LoginUser.DestinationPageUrl = "~/Home.aspx?IsSuperAdmin=true";
                    FormsAuthentication.RedirectFromLoginPage(entry.Username, false);
                    FormsAuthentication.SignOut();

                }



                else if (IsAdmin)
                {
                    Session["Admin"] = "True";
                    LoginUser.DestinationPageUrl = "~/Home.aspx?IsAdmin=true";
                    FormsAuthentication.RedirectFromLoginPage(entry.Username, false);
                    FormsAuthentication.SignOut();
                }

                else
                {
                    FormsAuthentication.RedirectFromLoginPage(entry.Username, false);
                    //LoginUser.DestinationPageUrl = "~/TrainingList.aspx?IsAdmin=false";
                    Response.Redirect("~/TrainingList.aspx?IsAdmin=false");
                    FormsAuthentication.SignOut();
                }
            }
            else
            {

                AdminBLL adminbll = new AdminBLL();
                bool IsSuperAdmin = true;
                bool IsAdmin = adminbll.IsAdmin(LoginUser.UserName, out IsSuperAdmin);
                if (IsSuperAdmin)
                {
                    Session["IsSuperAdmin"] = "STrue";
                    LoginUser.DestinationPageUrl = "~/Home.aspx?IsSuperAdmin=true";
                    FormsAuthentication.RedirectFromLoginPage(entry.Username, false);
                    FormsAuthentication.SignOut();
                }



                else if (IsAdmin)
                {
                    Session["Admin"] = "True";
                    LoginUser.DestinationPageUrl = "~/Home.aspx?IsAdmin=true?";
                    FormsAuthentication.RedirectFromLoginPage(entry.Username, false);
                    FormsAuthentication.SignOut();
                }

                else
                {
                    FormsAuthentication.RedirectFromLoginPage(entry.Username, false);
                    //LoginUser.DestinationPageUrl = "~/TrainingList.aspx?IsAdmin=false";
                    Response.Redirect("~/TrainingList.aspx?IsAdmin=false");
                    FormsAuthentication.SignOut();
                }
            }
            FormsAuthentication.SignOut();
        }
    }
}
#endregion


