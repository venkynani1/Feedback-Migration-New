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
                    fill(User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1), Request.QueryString["s_id"], Request.QueryString["c_id"], Request.QueryString["c_Name"]);
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
                        Row11.Visible = true;
                        Row12.Visible = true;
                        Row13.Visible = true;

                        RowSecondHearer.Visible = true;
                        Row21.Visible = true;
                        Row22.Visible = true;
                        Row23.Visible = true;
                        Row24.Visible = true;
                        Row25.Visible = true;
                        Row26.Visible = true;
                        Row27.Visible = true;
                        Row28.Visible = true;
                        Row29.Visible = true;

                        RowThirdHeader.Visible = true;
                        RB_ans3_1.Enabled = true;
                        RB_ans3_2.Enabled = true;
                        RB_ans3_3.Enabled = true;
                        RB_ans3_4.Enabled = true;
                        RB_ans3_5.Enabled = true;
                        RB_ans3_6.Enabled = true;

                        Lb_traineecomments.Visible = true;
                        Tb_traineecomments.Visible = true;
                    }
                    else
                    {
                        RowFirstHeader.Visible = false;
                        Row11.Visible = false;
                        Row12.Visible = false;
                        Row13.Visible = false;

                        RowSecondHearer.Visible = false;
                        Row21.Visible = false;
                        Row22.Visible = false;
                        Row23.Visible = false;
                        Row24.Visible = false;
                        Row25.Visible = false;
                        Row26.Visible = false;
                        Row27.Visible = false;
                        Row28.Visible = false;
                        Row29.Visible = false;

                        RowThirdHeader.Visible = true;
                        RB_ans3_1.Enabled = true;
                        RB_ans3_2.Enabled = true;
                        RB_ans3_3.Enabled = true;
                        RB_ans3_4.Enabled = true;
                        RB_ans3_5.Enabled = true;
                        RB_ans3_6.Enabled = true;

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
                        Lb_traineecomments.Visible = false;
                        Tb_traineecomments.Visible = false;
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
                Lb_ques1_1.Text = dt.Rows[0][0].ToString();
                Lb_ques1_2.Text = dt.Rows[1][0].ToString();
                Lb_ques1_3.Text = dt.Rows[2][0].ToString();
                Lb_qus2_1.Text = dt.Rows[3][0].ToString();
                Lb_qus2_2.Text = dt.Rows[4][0].ToString();
                Lb_qus2_3.Text = dt.Rows[5][0].ToString();
                Lb_qus2_4.Text = dt.Rows[6][0].ToString();
                Lb_qus2_5.Text = dt.Rows[7][0].ToString();
                Lb_qus2_6.Text = dt.Rows[8][0].ToString();
                Lb_qus2_7.Text = dt.Rows[9][0].ToString();
                Lb_qus2_8.Text = dt.Rows[10][0].ToString();
                Lb_qus2_9.Text = dt.Rows[11][0].ToString();
                Lb_qus3_1.Text = dt.Rows[12][0].ToString();
                Lb_qus3_2.Text = dt.Rows[13][0].ToString();
                Lb_qus3_3.Text = dt.Rows[14][0].ToString();
                Lb_qus3_4.Text = dt.Rows[15][0].ToString();
                Lb_qus3_5.Text = dt.Rows[16][0].ToString();
                Lb_qus3_6.Text = dt.Rows[17][0].ToString();
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

                        if (status() == true)
                        {
                            if (Tb_traineecomments.Text != "")
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
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Please enter your comments');", true);
                            }
                        }
                    }
                    else
                    {
                        if (status() == true)
                        {
                            if (Tb_traineecomments.Text != "")
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
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Please enter your comments');", true);
                            }
                        }
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
                    _feedbackinfo.response2 = RB_ans1_2.SelectedValue.ToString();
                    _feedbackinfo.response3 = RB_ans1_3.SelectedValue.ToString();
                    _feedbackinfo.response4 = RB_ans2_1.SelectedValue.ToString();
                    _feedbackinfo.response5 = RB_ans2_2.SelectedValue.ToString();
                    _feedbackinfo.response6 = RB_ans2_3.SelectedValue.ToString();
                    _feedbackinfo.response7 = RB_ans2_4.SelectedValue.ToString();
                    _feedbackinfo.response8 = RB_ans2_5.SelectedValue.ToString();
                    _feedbackinfo.response9 = RB_ans2_6.SelectedValue.ToString();
                    _feedbackinfo.response10 = RB_ans2_7.SelectedValue.ToString();
                    _feedbackinfo.response11 = RB_ans2_8.SelectedValue.ToString();
                    _feedbackinfo.response12 = RB_ans2_9.SelectedValue.ToString();
                    _feedbackinfo.response13 = RB_ans3_1.SelectedValue.ToString();
                    _feedbackinfo.response14 = RB_ans3_2.SelectedValue.ToString();
                    _feedbackinfo.response15 = RB_ans3_3.SelectedValue.ToString();
                    _feedbackinfo.response16 = RB_ans3_4.SelectedValue.ToString();
                    _feedbackinfo.response17 = RB_ans3_5.SelectedValue.ToString();
                    _feedbackinfo.response18 = RB_ans3_6.SelectedValue.ToString();
                    _feedbackinfo.CommonFlag = 1;

                }
                else
                {
                    _feedbackinfo.response1 = Convert.ToString(0);
                    _feedbackinfo.response2 = Convert.ToString(0);
                    _feedbackinfo.response3 = Convert.ToString(0);
                    _feedbackinfo.response4 = Convert.ToString(0);
                    _feedbackinfo.response5 = Convert.ToString(0);
                    _feedbackinfo.response6 = Convert.ToString(0);
                    _feedbackinfo.response7 = Convert.ToString(0);
                    _feedbackinfo.response8 = Convert.ToString(0);
                    _feedbackinfo.response9 = Convert.ToString(0);
                    _feedbackinfo.response10 = Convert.ToString(0);
                    _feedbackinfo.response11 = Convert.ToString(0);
                    _feedbackinfo.response12 = Convert.ToString(0);

                    _feedbackinfo.response13 = RB_ans3_1.SelectedValue.ToString();
                    _feedbackinfo.response14 = RB_ans3_2.SelectedValue.ToString();
                    _feedbackinfo.response15 = RB_ans3_3.SelectedValue.ToString();
                    _feedbackinfo.response16 = RB_ans3_4.SelectedValue.ToString();
                    _feedbackinfo.response17 = RB_ans3_5.SelectedValue.ToString();
                    _feedbackinfo.response18 = RB_ans3_6.SelectedValue.ToString();
                    _feedbackinfo.CommonFlag = 0;

                }
                _feedbackinfo.comments = Tb_traineecomments.Text;
                _feedbackinfo.TrainerId = ddlTrainer.Text;
                _feedbackBLL.createFeedbacks(_feedbackinfo, out errMessage);
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }
        public bool status()
        {
            bool status13 = false;
            bool status14 = false;
            bool status15 = false;
            bool status16 = false;
            bool status17 = false;
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
                for (int j = 0; j <= RB_ans1_2.Items.Count - 1; j++)
                {


                    if (RB_ans1_2.Items[j].Selected == true)
                    {
                        status1 = true;
                        break;
                    }




                }

                for (int k = 0; k <= RB_ans1_3.Items.Count - 1; k++)
                {


                    if (RB_ans1_3.Items[k].Selected == true)
                    {
                        status2 = true;
                        break;
                    }

                }
                for (int i = 0; i <= RB_ans2_1.Items.Count - 1; i++)
                {
                    if (RB_ans2_1.Items[i].Selected == true)
                    {
                        status3 = true;
                        break;
                    }
                }
                for (int i = 0; i <= RB_ans2_2.Items.Count - 1; i++)
                {
                    if (RB_ans2_2.Items[i].Selected == true)
                    {
                        status4 = true;
                        break;
                    }
                }
                for (int i = 0; i <= RB_ans2_3.Items.Count - 1; i++)
                {
                    if (RB_ans2_3.Items[i].Selected == true)
                    {
                        status5 = true;
                        break;
                    }
                }
                for (int i = 0; i <= RB_ans2_4.Items.Count - 1; i++)
                {
                    if (RB_ans2_4.Items[i].Selected == true)
                    {
                        status6 = true;
                        break;
                    }
                }
                for (int i = 0; i <= RB_ans2_5.Items.Count - 1; i++)
                {
                    if (RB_ans2_5.Items[i].Selected == true)
                    {
                        status7 = true;
                        break;
                    }
                }
                for (int i = 0; i <= RB_ans2_6.Items.Count - 1; i++)
                {
                    if (RB_ans2_6.Items[i].Selected == true)
                    {
                        status8 = true;
                        break;
                    }
                }
                for (int i = 0; i <= RB_ans2_7.Items.Count - 1; i++)
                {
                    if (RB_ans2_7.Items[i].Selected == true)
                    {
                        status9 = true;
                        break;
                    }
                }
                for (int i = 0; i <= RB_ans2_8.Items.Count - 1; i++)
                {
                    if (RB_ans2_8.Items[i].Selected == true)
                    {
                        status10 = true;
                        break;
                    }
                }
                for (int i = 0; i <= RB_ans2_9.Items.Count - 1; i++)
                {
                    if (RB_ans2_9.Items[i].Selected == true)
                    {
                        status11 = true;
                        break;
                    }
                }
            }
            else
            {
                status0 = true;
                status1 = true;
                status2 = true;
                status3 = true;
                status4 = true;
                status5 = true;
                status6 = true;
                status7 = true;
                status8 = true;
                status9 = true;
                status10 = true;
                status11 = true;

            }

            for (int i = 0; i <= RB_ans3_1.Items.Count - 1; i++)
            {
                if (RB_ans3_1.Items[i].Selected == true)
                {
                    status12 = true;
                    break;
                }
            }
            for (int i = 0; i <= RB_ans3_2.Items.Count - 1; i++)
            {
                if (RB_ans3_2.Items[i].Selected == true)
                {
                    status13 = true;
                    break;
                }
            }
            for (int i = 0; i <= RB_ans3_3.Items.Count - 1; i++)
            {
                if (RB_ans3_3.Items[i].Selected == true)
                {
                    status14 = true;
                    break;
                }
            }
            for (int i = 0; i <= RB_ans3_4.Items.Count - 1; i++)
            {
                if (RB_ans3_4.Items[i].Selected == true)
                {
                    status15 = true;
                    break;
                }
            }
            for (int i = 0; i <= RB_ans3_5.Items.Count - 1; i++)
            {
                if (RB_ans3_5.Items[i].Selected == true)
                {
                    status16 = true;
                    break;
                }
            }
            for (int i = 0; i <= RB_ans3_6.Items.Count - 1; i++)
            {
                if (RB_ans3_6.Items[i].Selected == true)
                {
                    status17 = true;
                    break;
                }
            }


            if (status0 == true && status1 == true && status2 == true && status3 == true && status4 == true &&
                status5 == true && status6 == true && status7 == true && status8 == true && status9 == true &&
                status10 == true && status11 == true && status12 == true && status13 == true && status14 == true
                && status15 == true && status16 == true && status17 == true)
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
                    RB_ans1_2.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans1_2.ForeColor = System.Drawing.Color.Green;
                }
                if (status2 == false)
                {
                    RB_ans1_3.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans1_3.ForeColor = System.Drawing.Color.Green;
                }
                if (status3 == false)
                {
                    RB_ans2_1.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans2_1.ForeColor = System.Drawing.Color.Green;
                }
                if (status4 == false)
                {
                    RB_ans2_2.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans2_2.ForeColor = System.Drawing.Color.Green;
                }
                if (status5 == false)
                {
                    RB_ans2_3.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans2_3.ForeColor = System.Drawing.Color.Green;
                }
                if (status6 == false)
                {
                    RB_ans2_4.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans2_4.ForeColor = System.Drawing.Color.Green;
                }
                if (status7 == false)
                {
                    RB_ans2_5.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans2_5.ForeColor = System.Drawing.Color.Green;
                }
                if (status8 == false)
                {
                    RB_ans2_6.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans2_6.ForeColor = System.Drawing.Color.Green;
                }
                if (status9 == false)
                {
                    RB_ans2_7.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans2_7.ForeColor = System.Drawing.Color.Green;
                }
                if (status10 == false)
                {
                    RB_ans2_8.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans2_8.ForeColor = System.Drawing.Color.Green;
                }
                if (status11 == false)
                {
                    RB_ans2_9.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans2_9.ForeColor = System.Drawing.Color.Green;
                }
                if (status12 == false)
                {
                    RB_ans3_1.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans3_1.ForeColor = System.Drawing.Color.Green;
                }
                if (status13 == false)
                {
                    RB_ans3_2.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans3_2.ForeColor = System.Drawing.Color.Green;
                }
                if (status14 == false)
                {
                    RB_ans3_3.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans3_3.ForeColor = System.Drawing.Color.Green;
                }

                if (status15 == false)
                {
                    RB_ans3_4.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans3_4.ForeColor = System.Drawing.Color.Green;
                }
                if (status16 == false)
                {
                    RB_ans3_5.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans3_5.ForeColor = System.Drawing.Color.Green;
                }
                if (status17 == false)
                {
                    RB_ans3_6.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    RB_ans3_6.ForeColor = System.Drawing.Color.Green;
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
                    if (pendingFeedbackCount>=1)
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
            for (int i = 0; i <= RB_ans3_1.Items.Count - 1; i++)
            {
                if (RB_ans3_1.Items[i].Selected == true)
                {
                    RB_ans3_1.Items[i].Selected = false;
                }
            }
            for (int i = 0; i <= RB_ans3_2.Items.Count - 1; i++)
            {
                if (RB_ans3_2.Items[i].Selected == true)
                {
                    RB_ans3_2.Items[i].Selected = false;
                }
            }
            for (int i = 0; i <= RB_ans3_3.Items.Count - 1; i++)
            {
                if (RB_ans3_3.Items[i].Selected == true)
                {
                    RB_ans3_3.Items[i].Selected = false;
                }
            }
            for (int i = 0; i <= RB_ans3_4.Items.Count - 1; i++)
            {
                if (RB_ans3_4.Items[i].Selected == true)
                {
                    RB_ans3_4.Items[i].Selected = false;
                }
            }
            for (int i = 0; i <= RB_ans3_5.Items.Count - 1; i++)
            {
                if (RB_ans3_5.Items[i].Selected == true)
                {
                    RB_ans3_5.Items[i].Selected = false;
                }
            }
            for (int i = 0; i <= RB_ans3_6.Items.Count - 1; i++)
            {
                if (RB_ans3_6.Items[i].Selected == true)
                {
                    RB_ans3_6.Items[i].Selected = false;
                }
            }

            //End-For deselect the radio button

            //Start-Hide SEC 1.1 to 2.9
            RowFirstHeader.Visible = false;
            Row11.Visible = false;
            Row12.Visible = false;
            Row13.Visible = false;

            RowSecondHearer.Visible = false;
            Row21.Visible = false;
            Row22.Visible = false;
            Row23.Visible = false;
            Row24.Visible = false;
            Row25.Visible = false;
            Row26.Visible = false;
            Row27.Visible = false;
            Row28.Visible = false;
            Row29.Visible = false;

            RowThirdHeader.Visible = true;
            RB_ans3_1.Enabled = true;
            RB_ans3_2.Enabled = true;
            RB_ans3_3.Enabled = true;
            RB_ans3_4.Enabled = true;
            RB_ans3_5.Enabled = true;
            RB_ans3_6.Enabled = true;

            Lb_traineecomments.Visible = false;
            Tb_traineecomments.Visible = false;
            //End-Hide SEC 1.1 to 2.9

            //Start-Change Default colour
            if (RB_ans3_1.ForeColor == System.Drawing.Color.Red || RB_ans3_1.ForeColor == System.Drawing.Color.Green)
            {
                RB_ans3_1.ForeColor = System.Drawing.Color.Black;
            }

            if (RB_ans3_2.ForeColor == System.Drawing.Color.Red || RB_ans3_2.ForeColor == System.Drawing.Color.Green)
            {
                RB_ans3_2.ForeColor = System.Drawing.Color.Black;
            }

            if (RB_ans3_3.ForeColor == System.Drawing.Color.Red || RB_ans3_3.ForeColor == System.Drawing.Color.Green)
            {
                RB_ans3_3.ForeColor = System.Drawing.Color.Black;
            }

            if (RB_ans3_4.ForeColor == System.Drawing.Color.Red || RB_ans3_4.ForeColor == System.Drawing.Color.Green)
            {
                RB_ans3_4.ForeColor = System.Drawing.Color.Black;
            }

            if (RB_ans3_5.ForeColor == System.Drawing.Color.Red || RB_ans3_5.ForeColor == System.Drawing.Color.Green)
            {
                RB_ans3_5.ForeColor = System.Drawing.Color.Black;
            }

            if (RB_ans3_6.ForeColor == System.Drawing.Color.Red || RB_ans3_6.ForeColor == System.Drawing.Color.Green)
            {
                RB_ans3_6.ForeColor = System.Drawing.Color.Black;
            }

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
                Lb_traineecomments.Visible = false;
                Tb_traineecomments.Visible = false;
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