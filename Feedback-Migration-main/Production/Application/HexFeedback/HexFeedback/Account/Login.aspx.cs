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

        NEWAUTHENTICATION.StationHAuthentication ObjHelix = new NEWAUTHENTICATION.StationHAuthentication();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session.RemoveAll();
                lblErrorMsg.Visible = false;
                lblErrorMsg.Text = string.Empty;
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
            string FiveDigitNum = string.Empty;
            const string domain = "CORP";
            lblErrorMsg.Text = string.Empty;

            //String domainAndUsername = domain + "\\" + LoginUser.UserName;
            //DirectoryEntry entry = new DirectoryEntry();
            //Session["username"] = LoginUser.UserName;
            //entry.Username = domainAndUsername;
            //entry.Password = LoginUser.Password;
            //Object obj;
            try
            {
              
                int Status = ObjHelix.IsValidUser(LoginUser.UserName, LoginUser.Password);

             // int Status = 1;

                if (Status == 1)
                {
                    //int NoofDays = helixAuthentication.GetDaysRemaining(LoginUser.UserName);
                    AdminBLL adminbll = new AdminBLL();
                    string num = adminbll.FiveDigitID(LoginUser.UserName, out FiveDigitNum);
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
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
                e.Authenticated = false;
                return;
            }

            if (FiveDigitNum != string.Empty)
            {
                String domainAndUsername = domain + "\\" + FiveDigitNum;// LoginUser.UserName;
                DirectoryEntry entry = new DirectoryEntry();
                Session["username"] = FiveDigitNum;// LoginUser.UserName;
                entry.Username = domainAndUsername;
                entry.Password = LoginUser.Password;

                if (LoginUser.RememberMeSet)
                {

                    AdminBLL adminbll = new AdminBLL();

                    bool IsSuperAdmin = true;

                   // bool IsAdmin = adminbll.IsAdmin(LoginUser.UserName, out IsSuperAdmin);
                    bool IsAdmin = adminbll.IsAdmin(FiveDigitNum, out IsSuperAdmin);

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
                   // bool IsAdmin = adminbll.IsAdmin(LoginUser.UserName, out IsSuperAdmin);
                    bool IsAdmin = adminbll.IsAdmin(FiveDigitNum, out IsSuperAdmin);
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
            else
            {
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = "Five digit id not available for this employee";
            }
        }
    }
}
#endregion


