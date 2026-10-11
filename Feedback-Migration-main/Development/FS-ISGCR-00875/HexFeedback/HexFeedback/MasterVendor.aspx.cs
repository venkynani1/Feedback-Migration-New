#region Header
/// ***********************************************************************************************
/// File name: MasterVendor.aspx.cs
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
using Hex.Vars.FB.HexFBBLL;
using System.Data.SqlClient;
using System.Data;

#endregion

#region Class
namespace HexFeedback
{
    public partial class MasterVendor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string errmessage = string.Empty;
                bool IsRecordExist = false;
                if (txtvendor.Text != "" && txtName.Text != "")
                {
                    NominationListBLL nominationbll = new NominationListBLL();
                    Hex.Vars.FB.HexFBModule.MasterInfo MasterMaillist = new Hex.Vars.FB.HexFBModule.MasterInfo();
                    MasterMaillist.Vendorname = txtvendor.Text;
                    MasterMaillist.PrefixID = txtName.Text;
                    nominationbll.insertVendorDet(MasterMaillist, out errmessage, out IsRecordExist);
                    if (IsRecordExist)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Vendor Details are exist.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Vendor Details are successfully Saved.');", true);
                    }
                    txtvendor.Text = "";
                    txtName.Text = "";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Please provide all details.');", true);
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }
        
    }
}


#endregion