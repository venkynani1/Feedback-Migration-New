#region Header
/// ***********************************************************************************************
/// File name: MasterMail.aspx.cs
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
    public partial class MasterMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!Page.IsPostBack)
            {
                try
                {
                    BindTxtboxs();
                    TxtFrom.Enabled = false;
                    txtbody.Enabled = false;
                    
                }
                catch (Exception ex)
                {
                    lblerrMsg.Text = "Error:- " + ex.Message;
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try{
            UpdateAllMailDetails();
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        private void UpdateAllMailDetails()
        {
            string errmessage = string.Empty;

            NominationListBLL nominationbll = new NominationListBLL();
            Hex.Vars.FB.HexFBModule.MasterInfo MasterMaillist = new Hex.Vars.FB.HexFBModule.MasterInfo();

            MasterMaillist.FromMail = null;
            MasterMaillist.Bcc =null ;
            MasterMaillist.Ccc = txtCcc.Text;
            MasterMaillist.Body = txtbody.Text;
            MasterMaillist.Subject = txtSubj.Text;
            MasterMaillist.URL = null;
            if (rdType.SelectedValue == "0")
                MasterMaillist.Type = 1;
            else
                MasterMaillist.Type = 2;
            nominationbll.insertMasterDet(MasterMaillist, out errmessage);
            BindTxtboxs();
            ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Mail Details are successfully updated');", true);
        }
        public void BindTxtboxs()
        {
            
            string errmessage = string.Empty;
            NominationListBLL nominationbll = new NominationListBLL();
            Hex.Vars.FB.HexFBModule.MasterInfo MasterMaillist = new Hex.Vars.FB.HexFBModule.MasterInfo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            if (rdType.SelectedValue == "0")
                MasterMaillist.Type = 1;
            else if (rdType.SelectedValue == "1")
                MasterMaillist.Type = 2;
            else if (rdType.SelectedValue == "2")
                MasterMaillist.Type = 3;
            else
                MasterMaillist.Type = 4;
            dt = nominationbll.SelcMailMaster(MasterMaillist, out errmessage);
            ds.Tables.Add(dt);
            txtCcc.Text = ds.Tables[0].Rows[0]["CccMail"].ToString();
            txtSubj.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
            txtbody.Text = ds.Tables[0].Rows[0]["Body"].ToString();

            //Get From Mail ID
            Hex.Vars.FB.HexFBBLL.SessionBLL sessionbll = new SessionBLL();
            Hex.Vars.FB.HexFBModule.NominationListInfo nominationlist = new Hex.Vars.FB.HexFBModule.NominationListInfo();
            nominationlist.EmployeeId = Session["username"].ToString();
            nominationlist.Flag = 1;
            nominationlist.SessionId = null;
            nominationlist.CourseId = null;
            SqlDataReader FromMailID = sessionbll.getindividual(nominationlist.EmployeeId, nominationlist.SessionId, nominationlist.CourseId, nominationlist.Flag);
            if (FromMailID != null)
            {
                if (FromMailID.Read())
                {
                    TxtFrom.Text = FromMailID["ReplyMailID"].ToString();
                }
            }

        }
        public void GetMailMaster()
        {
            string errmessage = string.Empty;
            NominationListBLL nominationbll = new NominationListBLL();
            Hex.Vars.FB.HexFBModule.MasterInfo MasterMaillist = new Hex.Vars.FB.HexFBModule.MasterInfo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = nominationbll.SelcMailMaster(MasterMaillist, out errmessage);
            ds.Tables.Add(dt);
        }

        protected void btnEditUpdatebody_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnEditUpdatebody.Text == "Edit")
                {
                    btnEditUpdatebody.Text = "Update";
                    txtbody.Enabled = true;
                }
                else
                {
                    btnEditUpdatebody.Text = "Edit";
                    txtbody.Enabled = false;
                    UpdateAllMailDetails();
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        protected void rdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTxtboxs();
        }
    }
}
#endregion