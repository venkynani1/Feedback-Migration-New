#region Header
/// ***********************************************************************************************
/// File name: Session.aspx.cs
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
using System.Data;
#endregion

#region Class
namespace HexFeedback
{
    public partial class _Default : System.Web.UI.Page
    {
        #region declarations
        protected string course = string.Empty;
        protected string session = string.Empty;
        protected DateTime startdate;
        protected DateTime enddate;
        protected string createdby = string.Empty;
        protected DateTime Createddate;
        protected string modifiedby = string.Empty;
        protected DateTime modifieddate;
        protected bool isdeleted = false;
        #endregion declarations
        Hex.Vars.FB.HexFBBLL.SessionBLL _sessionBll = new Hex.Vars.FB.HexFBBLL.SessionBLL();
        Hex.Vars.FB.HexFBBLL.CourseBLL coursebll = new CourseBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack == true)
            {
                try
                {
                    txtSessionID.Enabled = false;
                    tdSessionAuto.Visible = false;
                    BtnTrainmdoe.Enabled = true;
                    LstTrainers.Enabled = true;
                    System.Data.SqlClient.SqlDataReader dr;
                    dr = coursebll.getAllCourse();
                    drpCname.DataSource = dr;
                    drpCname.DataValueField = "courseid";
                    drpCname.DataTextField = "course";
                    drpCname.DataBind();
                    drpCname.Items.Insert(0, "--Select--");
                }
                catch (Exception ex)
                {
                    lblerrMsg.Text = "Error:- " + ex.Message;
                }
            }
        }
        public void GetTrainer()
        {
            System.Data.SqlClient.SqlDataReader drtrainer;
            Hex.Vars.FB.HexFBModule.MasterInfo MasterMaillist = new Hex.Vars.FB.HexFBModule.MasterInfo();
            MasterMaillist.TrainerType = drptype.SelectedValue;
            MasterMaillist.Vendorname = ddlvendor.Text;
            MasterMaillist.CourseId = drpCname.Text;
            drtrainer = coursebll.getAllTrainer(MasterMaillist);
            lSTTraineriD.DataSource = drtrainer;
            lSTTraineriD.DataMember = "TrainerDet";
            lSTTraineriD.DataValueField = "Trainer";
            lSTTraineriD.DataBind();
        }
        public void GetVendorDetais(int flag)
        {
            try
            {
                Hex.Vars.FB.HexFBModule.MasterInfo MasterMaillist = new Hex.Vars.FB.HexFBModule.MasterInfo();
                MasterMaillist.Vendorname = ddlvendor.Text;
                System.Data.SqlClient.SqlDataReader drtrainer;
                drtrainer = coursebll.getVendorDetais(flag, MasterMaillist);
                if (flag == 1)
                {
                    ddlvendor.DataSource = drtrainer;
                    ddlvendor.DataMember = "VendorDet";
                    ddlvendor.DataValueField = "VendorName";
                    ddlvendor.DataBind();
                    ddlvendor.Items.Insert(0, "--Select--");
                }
                else if (flag == 2)
                {
                    lSTTraineriD.DataSource = drtrainer;
                    lSTTraineriD.DataMember = "VendorDet";
                    lSTTraineriD.DataValueField = "Trainers";
                    lSTTraineriD.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        public void checkAvailability()
        {
            try
            {
                String session = drplocation.SelectedValue.Substring(0, 3) + "_" + drpCname.SelectedItem.Value.ToString() + "_" + System.Convert.ToDateTime(txtFrmDt.Text).ToShortDateString() + "To" + System.Convert.ToDateTime(txtToDt.Text).ToShortDateString();
                DataTable dtAvailability = _sessionBll.getSessionAvailability(session);
                if (Convert.ToInt32(dtAvailability.Rows[0]["SessionCount"]) > 0)
                {
                    Session["NewSessionID"] = session + "_" + dtAvailability.Rows[0]["SessionCount"].ToString();
                    hdnConfirm.Value = "1";
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        protected void butSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Convert.ToDateTime(txtFrmDt.Text) > DateTime.Now)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('From Date should be current date or less than current date.');", true);
                }
                else
                {
                    string errMessage = string.Empty;
                    bool IsRecordExist = false;
                    Hex.Vars.FB.HexFBModule.Sessioninfo _Session = new Hex.Vars.FB.HexFBModule.Sessioninfo();
                    _Session.courseid = drpCname.SelectedItem.Value;
                    _Session.courseName =drpCname.SelectedItem.Text;
                    
                    _Session.startdate = System.Convert.ToDateTime(txtFrmDt.Text);
                    _Session.Enddate = System.Convert.ToDateTime(txtToDt.Text);
                    _Session.createdby = Session["username"].ToString();
                    _Session.modifiedby = Session["username"].ToString();
                    _Session.maximumparticipants = 500;// System.Convert.ToInt32(txtmaxpat.Text); //Changed on 27 Apr 2015
                    if (drptype.SelectedItem.ToString() == "Internal")
                    {
                        _Session.Trainername = lSTTraineriD.Text;
                    }
                    else if (drptype.SelectedItem.ToString() == "External")
                    {
                        _Session.Trainername = lSTTraineriD.Text;
                    }
                    _Session.location = drplocation.SelectedItem.ToString();
                    if (hdnCreateNew.Value == "Y")
                    {
                        _Session.session = Session["NewSessionID"].ToString();
                    }
                    else
                    {
                        _Session.session = _Session.location.Substring(0, 3) + "_" + drpCname.SelectedItem.Value.ToString() + "_" + _Session.startdate.ToShortDateString() + "To" + _Session.Enddate.ToShortDateString();
                    }
                        //_Session.session = _Session.location.Substring(0, 3)+ "_" + _Session.session;
                        txtSessionID.Text = _Session.session;
                        hdnSessionID.Value = _Session.session;
                    if (LstTrainers.Items.Count != 0)
                    {
                        _sessionBll.createSession(_Session, out errMessage, out IsRecordExist);
                        if (IsRecordExist)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Same session already Exist.');", true);
                        }
                        else
                        {
                            for (int i = 0; i < LstTrainers.Items.Count; i++)
                            {
                                _Session.Trainerid = LstTrainers.Items[i].Text.Trim();
                                _sessionBll.InsertSessionTrainers(_Session, out errMessage);
                            }
                            ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:CreateSessionMsg();", true);
                            ResetValues();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Minimum one trainers should select for session.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        private void ResetValues()
        {
            drpCname.SelectedIndex = 0;
            drplocation.SelectedIndex = 0;
            drptype.SelectedIndex = 0;
            txtFrmDt.Text = string.Empty;
            txtToDt.Text = string.Empty;
            //txtmaxpat.Text = string.Empty; //Changed on 27 Apr 2015
            txtSessionID.Text = string.Empty;
            lSTTraineriD.Items.Clear();
            LstTrainers.Items.Clear();
            
        }
        protected void BtnTrainmdoe_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = lSTTraineriD.Items.Count - 1; i >= 0; i--)
                {
                    if (lSTTraineriD.Items[i].Selected)
                    {
                        LstTrainers.Items.Add(lSTTraineriD.Items[i]);
                        lSTTraineriD.Items.Remove(lSTTraineriD.Items[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }

        }
        protected void drpCname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlvendor.Items.Count != 0)
                {
                    ddlvendor.SelectedIndex = 0;
                }
                drptype.SelectedIndex = 0;
                lSTTraineriD.Items.Clear();
                LstTrainers.Items.Clear();
                if (!string.IsNullOrEmpty(txtFrmDt.Text) && drpCname.SelectedIndex != 0 && !string.IsNullOrEmpty(txtToDt.Text) && drplocation.SelectedIndex != 0)
                {
                    checkAvailability();
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }

        protected void txtFrmDt_TextChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:FromDateValidation();", true);
            if (!string.IsNullOrEmpty(txtFrmDt.Text) && drpCname.SelectedIndex != 0 && !string.IsNullOrEmpty(txtToDt.Text) && drplocation.SelectedIndex != 0)
            {
                checkAvailability();
            }
        }
        protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetTrainer();

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
                lSTTraineriD.Items.Clear();
                LstTrainers.Items.Clear();
                if (drptype.SelectedIndex == 1)
                {
                    ddlvendor.Enabled = false;
                    ddlvendor.Items.Clear();
                    GetTrainer();
                }
                if (drptype.SelectedIndex == 2)
                {
                    int flag = 1;
                    ddlvendor.Enabled = true;
                    Hex.Vars.FB.HexFBModule.MasterInfo MasterMaillist = new Hex.Vars.FB.HexFBModule.MasterInfo();

                    System.Data.SqlClient.SqlDataReader drtrainer;
                    drtrainer = coursebll.getVendorDetais(flag, MasterMaillist);

                    ddlvendor.DataSource = drtrainer;
                    ddlvendor.DataMember = "VendorDet";
                    ddlvendor.DataValueField = "VendorName";
                    ddlvendor.DataBind();
                    ddlvendor.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }
        protected void BtnRemoveTrainer_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = LstTrainers.Items.Count - 1; i >= 0; i--)
                {
                    if (LstTrainers.Items[i].Selected)
                    {
                        lSTTraineriD.Items.Add(LstTrainers.Items[i]);
                        LstTrainers.Items.Remove(LstTrainers.Items[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        protected void txtToDt_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFrmDt.Text) && drpCname.SelectedIndex != 0 && !string.IsNullOrEmpty(txtToDt.Text) && drplocation.SelectedIndex != 0)
            {
                checkAvailability();
            }
        }

        protected void drplocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFrmDt.Text) && drpCname.SelectedIndex != 0 && !string.IsNullOrEmpty(txtToDt.Text) && drplocation.SelectedIndex != 0)
            {
                checkAvailability();
            }
        }

        
    }
}
#endregion
