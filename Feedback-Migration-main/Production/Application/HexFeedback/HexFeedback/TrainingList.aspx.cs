#region Header
/// ***********************************************************************************************
/// File name: TrainingList.aspx.cs
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
using System.Data;
using System.Data.SqlClient;
using Hex.Vars.FB.HexFBBLL;
#endregion

#region Class
namespace HexFeedback
{
    public partial class TrainingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack == true)
            {
                try
                {
                    TrainingListBLL traininglistbll = new TrainingListBLL();
                    Hex.Vars.FB.HexFBModule.TrainingListInfo tl = new Hex.Vars.FB.HexFBModule.TrainingListInfo();
                    string user = Request.QueryString["IsAdmin"].ToString();
                    tl.IsAdmin = user;
                    string EmpID;
                    if (Session["username"] != null)
                    {
                        EmpID = Session["username"].ToString();
                    }
                    else
                    {
                        EmpID = User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1);
                    }
                    DataTable DtGetDetailsEmployeeID = traininglistbll.getTrainingsEmpIdWiseNew(EmpID, tl.IsAdmin);
                    DataSet dsGetDetailswithEmployeeID = new DataSet();
                    dsGetDetailswithEmployeeID.Tables.Add(DtGetDetailsEmployeeID);
                    if (dsGetDetailswithEmployeeID.Tables[0].Rows.Count != 0)
                    {
                        grvTraining.DataSource = dsGetDetailswithEmployeeID.Tables[0];
                        grvTraining.DataBind();
                        for (int i = 0; i < dsGetDetailswithEmployeeID.Tables[0].Rows.Count; i++)
                        {
                            grvTraining.Rows[i].Cells[0].HorizontalAlign = HorizontalAlign.Left;
                            grvTraining.Rows[i].Cells[1].HorizontalAlign = HorizontalAlign.Center;
                            grvTraining.Rows[i].Cells[2].HorizontalAlign = HorizontalAlign.Center;
                            grvTraining.Rows[i].Cells[3].HorizontalAlign = HorizontalAlign.Center;
                            grvTraining.Rows[i].Cells[4].HorizontalAlign = HorizontalAlign.Center;
                            grvTraining.Rows[i].Cells[5].HorizontalAlign = HorizontalAlign.Center;
                        }
                    }
                    else
                    {
                        lblerrMsg.Text = "Note:Only those trainings which are not yet closed and feedback pending by you will be shown below.";
                    }

                    DataTable DtGetDetailsSupID = traininglistbll.getTrainingsSupWiseNew(EmpID, tl.IsAdmin);
                    DataSet DtGetDetailsSup = new DataSet();
                    DtGetDetailsSup.Tables.Add(DtGetDetailsSupID);
                    if (DtGetDetailsSup.Tables[0].Rows.Count != 0)
                    {
                        gvTrainingEffteciveness.DataSource = DtGetDetailsSup.Tables[0];
                        gvTrainingEffteciveness.DataBind();
                        for (int i = 0; i < DtGetDetailsSup.Tables[0].Rows.Count; i++)
                        {
                            gvTrainingEffteciveness.Rows[i].Cells[0].HorizontalAlign = HorizontalAlign.Left;
                            gvTrainingEffteciveness.Rows[i].Cells[1].HorizontalAlign = HorizontalAlign.Center;
                            gvTrainingEffteciveness.Rows[i].Cells[2].HorizontalAlign = HorizontalAlign.Center;
                            gvTrainingEffteciveness.Rows[i].Cells[3].HorizontalAlign = HorizontalAlign.Center;
                            gvTrainingEffteciveness.Rows[i].Cells[4].HorizontalAlign = HorizontalAlign.Center;
                            gvTrainingEffteciveness.Rows[i].Cells[5].HorizontalAlign = HorizontalAlign.Center;
                        }
                    }
                    else
                    {
                        lblErrMsg1.Text = "Note:Only those trainings which are not yet closed and feedback pending by you will be shown below.";
                    }
                    DataTable dtReviews = traininglistbll.getReviews(EmpID);
                    if (dtReviews.Rows.Count != 0)
                    {
                        gvReviews.DataSource = dtReviews;
                        gvReviews.DataBind();
                        for (int i = 0; i < dtReviews.Rows.Count; i++)
                        {
                            gvReviews.Rows[i].Cells[0].HorizontalAlign = HorizontalAlign.Left;
                            gvReviews.Rows[i].Cells[1].HorizontalAlign = HorizontalAlign.Left;
                            gvReviews.Rows[i].Cells[2].HorizontalAlign = HorizontalAlign.Center;
                            gvReviews.Rows[i].Cells[3].HorizontalAlign = HorizontalAlign.Center;
                            gvReviews.Rows[i].Cells[4].HorizontalAlign = HorizontalAlign.Center;
                        }   
                    }
                    else
                    {
                        lblErrMsg2.Text = "Note:Only those Reviews which are pending for your approval will be shown below.";
                    }
                }
                catch (Exception ex)
                {
                    lblerrMsg.Text = "Error:- " + ex.Message;
                }
            }
        }

        protected void grvTraining_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void grvTraining_SelectedIndexChanged1(object sender, EventArgs e)
        {
        }
   }
}
#endregion