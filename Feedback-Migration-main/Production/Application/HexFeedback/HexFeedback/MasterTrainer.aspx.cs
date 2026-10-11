#region Header
/// ***********************************************************************************************
/// File name: MasterTrainer.aspx.cs
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
    public partial class MasterTrainer : System.Web.UI.Page
    {
        Hex.Vars.FB.HexFBBLL.SessionBLL _sessionBll = new Hex.Vars.FB.HexFBBLL.SessionBLL();
        Hex.Vars.FB.HexFBBLL.CourseBLL coursebll = new CourseBLL();
        Hex.Vars.FB.HexFBModule.NominationListInfo nominationlist = new Hex.Vars.FB.HexFBModule.NominationListInfo();
        Hex.Vars.FB.HexFBBLL.SessionBLL sessionbll = new SessionBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack == true)
            {
                try
                {
                    lblempMsg.Visible = false;
                    DataTable dt = coursebll.SelectCourseAll();
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    drpCname.DataSource = ds.Tables[0];
                    drpCname.DataValueField = "courseid";
                    drpCname.DataTextField = "course";
                    drpCname.DataBind();
                    drpCname.Items.Insert(0, "Select the Course Name");
                    
                    
                }
                catch (Exception ex)
                {
                    lblerrMsg.Text = "Error:- " + ex.Message;
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "" && drptype.SelectedIndex != 0 && drpCname.SelectedIndex != 0)
                {
                    
                    string errmessage = string.Empty;
                    bool IsRecordExist = false;
                    NominationListBLL nominationbll = new NominationListBLL();
                  
                    Hex.Vars.FB.HexFBModule.MasterInfo MasterMaillist = new Hex.Vars.FB.HexFBModule.MasterInfo();
                    MasterMaillist.TrainerId = txtID.Text;
                    MasterMaillist.TrainerName = txtName.Text;
                    MasterMaillist.TrainerType = drptype.SelectedValue;
                    MasterMaillist.CourseId = drpCname.Text;
                   
                    if (drptype.SelectedIndex == 2)
                    {
                        MasterMaillist.Vendorname = ddlvendor.Text;
                        MasterMaillist.TrainerId = null;
                        if (ddlvendor.SelectedIndex == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Please select Vendor Name.');", true);
                        }
                        else
                        {
                            nominationbll.insertTrainerDet(MasterMaillist, out errmessage, out IsRecordExist);
                        }
                    }
                    else
                    {
                        MasterMaillist.Vendorname = null;
                        nominationbll.insertTrainerDet(MasterMaillist, out errmessage, out IsRecordExist);
                    }
                   
                    if (IsRecordExist)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Trainer Details are exist. ');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Trainer Details are successfully Saved.');", true);
                        ResetAllDetails();
                    }
                    if (drptype.SelectedIndex == 1)
                    {
                        txtID.Text = "";
                    }
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
        protected void drptype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int flag = 1;
                if (drptype.SelectedIndex == 2)
                {
                    ddlvendor.Enabled = true;
                    Hex.Vars.FB.HexFBModule.MasterInfo MasterMaillist = new Hex.Vars.FB.HexFBModule.MasterInfo();
                    MasterMaillist.Vendorname = ddlvendor.Text;
                    System.Data.SqlClient.SqlDataReader drtrainer;
                    drtrainer = coursebll.getVendorDetais(flag, MasterMaillist);

                    ddlvendor.DataSource = drtrainer;
                    ddlvendor.DataMember = "VendorDet";
                    ddlvendor.DataValueField = "VendorName";
                    ddlvendor.DataBind();
                    ddlvendor.Items.Insert(0, "Select the Vendor");
                    txtID.Text = "Auto generate";
                    txtID.Enabled = false;
                    txtName.Enabled = true;
                    txtName.Text = string.Empty;
                    lblempMsg.Visible = false;
                }
                if (drptype.SelectedIndex == 1)
                {
                    ddlvendor.Enabled = false;
                    txtID.Text = "";
                    txtID.Enabled = true;
                    txtName.Enabled = false;
                    txtName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }

        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetAllDetails();
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }
        private void ResetAllDetails()
        {
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            if (ddlvendor.SelectedIndex > 0)
            {
                ddlvendor.SelectedIndex = 0;
            }
            drpCname.SelectedIndex = 0;
            drptype.SelectedIndex = 0;
        }

        protected void txtID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                nominationlist.EmployeeId = txtID.Text;
                nominationlist.Flag = 1;
                int Count = 0;
                SqlDataReader DrGetEmpName = sessionbll.getindividual(nominationlist.EmployeeId, null, null, nominationlist.Flag);
                    while (DrGetEmpName.Read())
                    {
                        Count++;
                        txtName.Text = DrGetEmpName["AdminName"].ToString();
                        lblempMsg.Visible = false;
                    }
                    if (Count == 0)
                    {
                        lblempMsg.Visible = true;
                        txtName.Text = string.Empty;
                        lblempMsg.Text = "Please Enter Valid Employee ID.";
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