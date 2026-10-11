#region Header
/// ***********************************************************************************************
/// File name: Feedback.aspx.cs
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
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using Hex.Vars.FB.HexFBBLL;

#endregion

#region class
namespace HexFeedback
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string[] rb_s1 = new string[18];
        bool status0 = false;
        bool status1 = false;
        bool status2 = false;
        bool status3 = false;
        bool status4 = false;
        bool status5 = false;
        bool status6 = false;
        bool status7 = false;
        bool status8 = false;
        bool status9 = false;
        bool status10 = false;
        bool status11 = false;
        bool status12 = false;

        static int FeedbackCount = 0;
        static string SessionID = string.Empty;
        static string EmployeeID = string.Empty;

        static int TotalFeedbackCount = 0;
        static int pendingFeedbackCount = 0;
        Hex.Vars.FB.HexFBModule.Sessioninfo _session = new Hex.Vars.FB.HexFBModule.Sessioninfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack == true)
            {
                try                                     
                {
                    Tb_traineecomments.Attributes.Add("onkeydown", "textboxMultilineMinNumber(this,1000)");
                    ddlTrainer.Attributes.Add("onclick", "CheckTrainerSelection()");
                    ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:CheckTrainerSelection();", true);
                    string EmpID;
                    if (Session["username"] != null)
                    {
                        EmpID = Session["username"].ToString();
                    }
                    else
                    {
                        EmpID = User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1);
                    }

                    fill(EmpID, Request.QueryString["s_id"], Request.QueryString["c_id"], Request.QueryString["c_Name"]);
                    GetTrainers();
                    TotalFeedbackCount = Convert.ToInt32(Request.QueryString["TotalFeedbacks"].ToString());
                    pendingFeedbackCount = Convert.ToInt32(Request.QueryString["PendingFeedbacks"].ToString());
                    SessionID = Request.QueryString["s_id"].ToString();
                    EmployeeID = lblTraineeID.Text;
                    Hex.Vars.FB.HexFBBLL.SessionBLL _sessionFeedBackCount = new Hex.Vars.FB.HexFBBLL.SessionBLL();
                    _session.session = SessionID;
                    _session.empid = EmployeeID;
                    DataTable DtFeedBackCount = _sessionFeedBackCount.GetFeedbackCounts(_session);
                    DataSet DataSetFeedBackCount = new DataSet();
                    DataSetFeedBackCount.Tables.Add(DtFeedBackCount);
                    FeedbackCount = Convert.ToInt32(DataSetFeedBackCount.Tables[0].Rows[0][0].ToString());
                    if (pendingFeedbackCount == TotalFeedbackCount)
                    {
                        RowFirstHeader.Visible = true;
                        RowSecondHearer.Visible = true;
                        RowThirdHeader.Visible = true;
                        Lb_traineecomments.Visible = true;
                        Tb_traineecomments.Visible = true;
                    }
                    else
                    {
                        RowFirstHeader.Visible = false;
                        

                        RowSecondHearer.Visible = false;
                        

                        RowThirdHeader.Visible = true;
                       

                        Lb_traineecomments.Visible = false;
                        Tb_traineecomments.Visible = false;
                    }

                    //Enable and Disable the button
                    if (pendingFeedbackCount == 1)
                    {
                        //for button
                        Btn_submit.Visible = true;
                        BtnNextTrainer.Visible = false;
                        //For Text Box
                        Lb_traineecomments.Visible = true;
                        Tb_traineecomments.Visible = true;
                    }
                    else
                    {
                        //for button
                        Btn_submit.Visible = false;
                        BtnNextTrainer.Visible = true;
                        //For Text Box
                        Lb_traineecomments.Visible = true;
                        Tb_traineecomments.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblerrMsg.Text = "Error:- " + ex.Message;
                }

            }

        }

        protected void MV_Reactionaire_ActiveViewChanged(object sender, EventArgs e)
        {


        }

        protected void RB_ans1_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void RB_ans1_2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void RB_ans1_3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Btn_next_Click(object sender, EventArgs e)
        {
            try
            {
                status();
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        protected void RB_ans2_1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Btn_next3_Click(object sender, EventArgs e)
        {
           
        }
        protected void Btn_previous1_Click(object sender, EventArgs e)
        {

           

        }

        protected void Btn_Previous2_Click(object sender, EventArgs e)
        {

            

        }

        public void fill(string emp_id, string sessionid, string courseid, string courseName)
        {
            try
            {
                Hex.Vars.FB.HexFBBLL.SessionBLL _session = new Hex.Vars.FB.HexFBBLL.SessionBLL();
                DataTable dt = _session.getquestions();
                Hex.Vars.FB.HexFBModule.NominationListInfo nominationlist = new Hex.Vars.FB.HexFBModule.NominationListInfo();
                nominationlist.Flag = 0;
                SqlDataReader dr = _session.getindividual(emp_id, sessionid, courseid, nominationlist.Flag);


                while (dr.Read())
                {
                    lblCourseName.Text = courseName.ToString();
                    lblTraineeID.Text = dr["Employee_id"].ToString();
                    lblTraineeName.Text = dr["TRAINEENAME"].ToString();
                    lblTrainingDate.Text = dr["startdate"].ToString() + " To " + dr["enddate"].ToString();
                    string date = lblTrainingDate.Text;
                    lblTrainingLocation.Text = dr["location"].ToString();
                    break;
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }
        public void GetTrainers()
        {
            try
            {
                Hex.Vars.FB.HexFBBLL.SessionBLL _session = new Hex.Vars.FB.HexFBBLL.SessionBLL();

                SqlDataReader drtrainer = _session.getTrainers(Request.QueryString["s_id"], lblTraineeID.Text);

                ddlTrainer.DataSource = drtrainer;
                ddlTrainer.DataMember = "Session_Trainer";
                ddlTrainer.DataValueField = "TrainerID";
                ddlTrainer.DataBind();
                ddlTrainer.Items.Insert(0, "Select the Trainer");
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        protected void Btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTrainer.SelectedIndex != 0)
                {
                    if (pendingFeedbackCount == 1)
                    {

                        //if (status() == true)
                        //{
                            if (Tb_traineecomments.Text.Trim() != "")
                            {
                                if (status() == true)
                                {
                                    if (Session["Admin"] == null)
                                    {
                                        Response.Redirect("TrainingList.aspx?IsAdmin=false",false);
                                    }
                                    else
                                    {
                                        Response.Redirect("TrainingList.aspx?IsAdmin=true",false);
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Please enter your comments');", true);
                            }
                       // }
                    }
                    else
                    {
                        //if (status() == true)
                        //{
                            if (Tb_traineecomments.Text.Trim() != "")
                            {
                                if (status() == true)
                                {
                                    if (Session["Admin"] == null)
                                    {
                                        Response.Redirect("TrainingList.aspx?IsAdmin=false",false);
                                    }
                                    else
                                    {
                                        Response.Redirect("TrainingList.aspx?IsAdmin=true",false);
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Please enter your comments');", true);
                            }
                        //}
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Please Select Trainers');", true);
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        public void insertfeedback()
        {
            try
            {
                string errMessage = string.Empty;

                Hex.Vars.FB.HexFBBLL.FeedbackBLL _feedbackBLL = new Hex.Vars.FB.HexFBBLL.FeedbackBLL();
                Hex.Vars.FB.HexFBModule.feedbackinfo _feedbackinfo = new Hex.Vars.FB.HexFBModule.feedbackinfo();

                _feedbackinfo.courseid = Request.QueryString["c_id"].ToString();
                _feedbackinfo.sessionid = Request.QueryString["s_id"].ToString();
                if (Session["Admin"] != null)
                {
                    _feedbackinfo.IsAdmin = "true";
                }
                else
                {
                    _feedbackinfo.IsAdmin = "false";
                }
                _feedbackinfo.traineeid = lblTraineeID.Text;
                if (pendingFeedbackCount == TotalFeedbackCount)
                {
                    _feedbackinfo.response1 = RB_ans1_1.SelectedValue.ToString();
                    _feedbackinfo.response2 = RB_ans2_1.SelectedValue.ToString();
                    _feedbackinfo.response3 = RB_ans3_1.SelectedValue.ToString();
                 
                   
                    _feedbackinfo.CommonFlag = 1;

                }
                else
                {
                    _feedbackinfo.response1 = Convert.ToString(0);
                    _feedbackinfo.response2 = Convert.ToString(0);
                    _feedbackinfo.response3 = RB_ans3_1.SelectedValue.ToString();
                    
                    _feedbackinfo.CommonFlag = 0;

                }
                _feedbackinfo.comments = Tb_traineecomments.Text;
                _feedbackinfo.TrainerId = ddlTrainer.Text;
                _feedbackBLL.createFeedbacks(_feedbackinfo, out errMessage);
                Tb_traineecomments.Text = "";
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }
        public bool status()
        {
          
            if (FeedbackCount == 0)
            {
                for (int i = 0; i <= RB_ans1_1.Items.Count - 1; i++)
                {
                    if (RB_ans1_1.Items[i].Selected == true)
                    {
                        status0 = true;
                        break;
                    }
                }

                for (int i = 0; i <= RB_ans2_1.Items.Count - 1; i++)
                {
                    if (RB_ans2_1.Items[i].Selected == true)
                    {
                        status1 = true;
                        break;
                    }
                }

                for (int i = 0; i <= RB_ans3_1.Items.Count - 1; i++)
                {
                    if (RB_ans3_1.Items[i].Selected == true)
                    {
                        status2 = true;
                        break;
                    }
                }

            }
            else
            {
                status0 = true;
                status1 = true;

                if (!String.IsNullOrEmpty(RB_ans3_1.SelectedValue.ToString()))
                      status2 = true;
                else
                     status2 = false; 
            }

            if (status0 == true && status1 == true && status2 == true )
            {
                insertfeedback();
                return true;
            }
            else
            {
                if (status0 == false)
                {
                    RB_ans1_1.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans1_1.ForeColor = System.Drawing.Color.Green;
                }
               
                if (status1 == false)
                {
                    RB_ans2_1.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans2_1.ForeColor = System.Drawing.Color.Green;
                }


                if (status2 == false)
                {
                    RB_ans3_1.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans3_1.ForeColor = System.Drawing.Color.Green;
                }
               
                return false;
            }


        }



        public static void showMessageBox(string message)
        {
            Page page = HttpContext.Current.Handler as Page;
            page.RegisterStartupScript("key2", "<script>alert(" + message + ");</script>");

        }
        public void commented()
        {
          
        }

        protected void BtnNextTrainer_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTrainer.SelectedIndex != 0)
                {
                    if (Tb_traineecomments.Text.Trim() != "")
                    {
                        if (pendingFeedbackCount >= 1)
                        {

                            if (status() == true)
                            {
                                HideForSecondTrainer();
                            }

                        }
                        else
                        {
                            if (status() == true)
                            {
                                HideForSecondTrainer();
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Please enter your comments');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Please Select Trainers');", true);
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        private void HideForSecondTrainer()
        {
            //Start-for deselect the radio button
           
            //End-For deselect the radio button

            //Start-Hide SEC 1.1 to 2.9
            RowFirstHeader.Visible = false;
           

            RowSecondHearer.Visible = false;
           

            RowThirdHeader.Visible = true;
           

            Lb_traineecomments.Visible = false;
            Tb_traineecomments.Visible = false;
            //End-Hide SEC 1.1 to 2.9

            //Start-Change Default colour
           

            //End -Change Default colour

            //Start-Get Remaining Trainer
            ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:ClearTrainer();", true);

            //End-Get Remaining Trainer

            pendingFeedbackCount = pendingFeedbackCount - 1;
            //Enable and Disable the button
            if (pendingFeedbackCount == 1)
            {
                //for button
                Btn_submit.Visible = true;
                BtnNextTrainer.Visible = false;
                //For Text Box
                Lb_traineecomments.Visible = true;
                Tb_traineecomments.Visible = true;
            }
            else
            {
                //for button
                Btn_submit.Visible = false;
                BtnNextTrainer.Visible = true;
                //For Text Box
                Lb_traineecomments.Visible = true;
                Tb_traineecomments.Visible = true;
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Response.Redirect("TrainingList.aspx?IsAdmin=false");
            }
            else
            {
                Response.Redirect("TrainingList.aspx?IsAdmin=true");
            }
        }

        protected void ddlTrainer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}

#endregion