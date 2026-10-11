using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Hex.Vars.FB.HexFBBLL;

namespace HexFeedback
{

    public partial class DispComments : System.Web.UI.Page
    {
        string Tempsessionid = "";
        int Rowcount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Tempsessionid = Session["SelSessionid"].ToString();

            if (!Page.IsPostBack)
            {
                try
                {
                    Getpattern();
                }
                catch (Exception ex)
                {
                    lblerrMsg.Text = "Error:- " + ex.Message;
                }

            }
        }
        public void Getpattern()
        {
            string errMessage = string.Empty;

            Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
            _Sessionid.Sessionid = Tempsessionid;
            Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();

            DataTable dt = reportbll.GetfeedbackComments(_Sessionid, out errMessage);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            GrdComments.DataSource = ds;
            GrdComments.DataBind();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["CommentsValid"].ToString() == "1")
                {
                    CheckBox sample = ((CheckBox)GrdComments.Rows[i].Cells[0].FindControl("ChkDisplay"));
                    sample.Checked = true;
                }
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                GrdComments.Rows[i].Cells[2].HorizontalAlign = HorizontalAlign.Center;
                GrdComments.Rows[i].Cells[1].HorizontalAlign = HorizontalAlign.Center;
                GrdComments.Rows[i].Cells[3].HorizontalAlign = HorizontalAlign.Center;
            }

        }
        protected void GrdComments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                foreach (GridViewRow objects1 in GrdComments.Rows)
                {
                    if (e.CommandName == "Proceed")
                    {
                        string errMessage = string.Empty;

                        Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
                        _Sessionid.Sessionid = Tempsessionid;
                        Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();

                        DataTable dt = reportbll.GetfeedbackComments(_Sessionid, out errMessage);
                        DataSet ds = new DataSet();
                        ds.Tables.Add(dt);
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {

                            string EmployeeId = ((Label)GrdComments.Rows[j].Cells[0].FindControl("LblEmpId")).Text;
                            CheckBox sample = ((CheckBox)GrdComments.Rows[j].Cells[0].FindControl("ChkDisplay"));
                            Hex.Vars.FB.HexFBModule.NominationListInfo nominationlist = new Hex.Vars.FB.HexFBModule.NominationListInfo();
                            NominationListBLL nominationbll = new NominationListBLL();
                            string errmessage = string.Empty;
                            bool IsRecordExist = false;
                            int rowsAffected = 0;
                            nominationlist.EmployeeId = EmployeeId;

                            if (sample.Checked == true)
                            {
                                nominationlist.Comments = "1";
                            }
                            else
                            {
                                nominationlist.Comments = "0";
                            }
                            nominationlist.SessionId = Tempsessionid;
                            nominationbll.UpdateVaildComments(nominationlist, out errmessage, out IsRecordExist, out rowsAffected);
                            ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Valid Comments are updated ');", true);


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }

        }
    }
}
   
     

