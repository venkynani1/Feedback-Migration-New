using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;

namespace HexFeedback
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string temp = "";   


            //string IsAdmin = Request.QueryString["IsAdmin"].ToString();
            if (Session["Admin"] != null)
            {
                temp = Session["Admin"].ToString();
            }
            else if (Session["IsSuperAdmin"] != null)
            {
                temp = Session["IsSuperAdmin"].ToString();
            }
            string enablehome = Request.QueryString["IsAdmin"];

            if (temp == "True")
            {

                pnlFrSupAdmin.Visible = false;



            }
            else if (temp == "STrue")
            {
                pnlFrSupAdmin.Visible = true;
            }






        }
    }
}