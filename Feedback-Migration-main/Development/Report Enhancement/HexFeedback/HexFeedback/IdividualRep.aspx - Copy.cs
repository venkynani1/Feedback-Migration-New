#region Header
/// ***********************************************************************************************
/// File name: IndividualRep.aspx.cs
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
using System.Data.OleDb;
using System.Data.SqlClient;
using Hex.Vars.FB.HexFBBLL;
using System.Text;
using System.Text.RegularExpressions;
using Novacode;
using System.IO;
using System.Drawing;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

#endregion

#region Class

namespace HexFeedback
{
    public partial class IdividualRep : System.Web.UI.Page
    {
        string urlparm = "";
        string urlparm1 = "";
        decimal score1;
        decimal score2;
        decimal score3;
        decimal score4;
        decimal TrainerAvg;
        int Perc1;
        int Perc2;
        int Perc3;
        Hex.Vars.FB.HexFBBLL.SessionBLL _session = new Hex.Vars.FB.HexFBBLL.SessionBLL();
        Hex.Vars.FB.HexFBModule.NominationListInfo nominationlist = new Hex.Vars.FB.HexFBModule.NominationListInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    urlparm = "toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,height=300,width=300,left=250,top=250";
                    urlparm1 = "toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,height=650,width=850,left=250,top=250";
                    fldRating.Visible = false;
                    legRating.Visible = false;
                    Hex.Vars.FB.HexFBBLL.CourseBLL coursebll = new CourseBLL();
                    SqlDataReader dr = coursebll.getAllCourse();
                    drpCourse.DataSource = dr;
                    drpCourse.DataValueField = "courseid";
                    drpCourse.DataTextField = "course";
                    drpCourse.DataBind();
                    drpCourse.Items.Insert(0, "--Select--");
                    drpCourse.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    lblerrMsg.Text = "Error:- " + ex.Message;
                }
            }
        }
        protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Hex.Vars.FB.HexFBBLL.SessionBLL sessionbll = new SessionBLL();
                SqlDataReader dr1 = sessionbll.getSessionCourseWise(drpCourse.Text);
                ddlSession.DataSource = dr1;
                ddlSession.DataMember = "session";
                ddlSession.DataValueField = "sessionid";
                ddlSession.DataBind();
                ddlSession.Items.Insert(0, "--Select--");
                ddlSession.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }


        protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string tempEmpid = null;
                Hex.Vars.FB.HexFBBLL.SessionBLL _session = new Hex.Vars.FB.HexFBBLL.SessionBLL();
                SqlDataReader drtrainer = _session.getTrainers(ddlSession.Text, tempEmpid);
                ddltrainers.DataSource = drtrainer;
                ddltrainers.DataMember = "Session_Trainer";
                ddltrainers.DataValueField = "TrainerID";
                ddltrainers.DataBind();
                ddltrainers.Items.Insert(0, "--Select--");
                ddltrainers.Items.Insert(1, "OverAll");
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }

        }
        public void Getpattern()
        {
            try
            {
                string errMessage = string.Empty;
                Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
                _Sessionid.Sessionid = ddlSession.Text;
                _Sessionid.TrainerID = ddltrainers.Text;
                Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();

                DataTable dt = reportbll.GetFeedback(_Sessionid, out errMessage);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                GrvOverallViews.DataSource = ds;
                GrvOverallViews.DataBind();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    GrvOverallViews.Rows[i].Cells[6].HorizontalAlign = HorizontalAlign.Center;
                    if (ds.Tables[0].Rows[i][6].ToString() == "C" || ds.Tables[0].Rows[i][6].ToString() == "E")
                    {
                        string temp = ds.Tables[0].Rows[i][6].ToString();
                        GrvOverallViews.Rows[i].Cells[6].ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }

        }
        public void GetRating()
        {
            string errMessage = string.Empty;
            Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
            _Sessionid.Sessionid = ddlSession.Text;
            _Sessionid.TrainerID = ddltrainers.Text;
            Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();
            int count = 0;
            if (ddltrainers.SelectedIndex == 1)
            {
                fldRating.Visible = true;
                legRating.Visible = true;
                int ddlCount = ddltrainers.Items.Count - 1;
                for (; ddlCount > 0; ddlCount--)
                {
                    _Sessionid.TrainerID = ddltrainers.Items[ddlCount].Text;
                    DataSet dt1 = new DataSet();
                    dt1 = null;
                    dt1 = reportbll.Getfeedbackrating(_Sessionid, out errMessage);
                    if (errMessage == null || errMessage == "")
                    {
                        lblerrMsg.Text = string.Empty;
                        GrvRating.Visible = true;
                        fldRating.Visible = true;
                        legRating.Visible = true;
                        if (dt1.Tables[0] != null && dt1.Tables[0].Rows.Count == 3)
                        {
                            count++;
                            GrvRating.DataSource = dt1.Tables[0];
                            GrvRating.DataBind();
                            score1 = Convert.ToDecimal(dt1.Tables[0].Rows[0][3].ToString());
                            score2 = Convert.ToDecimal(dt1.Tables[0].Rows[1][3].ToString());
                            score3 = Convert.ToDecimal(dt1.Tables[0].Rows[2][3].ToString());
                            TrainerAvg = TrainerAvg + score3;
                        }
                    }
                    else
                    {
                        GrvRating.Visible = false;
                        fldRating.Visible = false;
                        legRating.Visible = false;
                        lblerrMsg.Text = "Unable to run report.Participant have selected the option 5 (Not Applicable).";
                        
                    }
                }
                if (count == ddltrainers.Items.Count - 2)
                {
                    TrainerAvg = TrainerAvg / count;
                    GrvRating.Rows[2].Cells[2].Text = Convert.ToString(System.Math.Round(TrainerAvg, 2));
                }
                else
                {
                    GrvRating.Visible = false;
                    fldRating.Visible = false;
                    legRating.Visible = false;
                    lblerrMsg.Text = "Unable to run report.Participant have selected the option 5 (Not Applicable).";
                }
            }
            else
            {
                fldRating.Visible = true;
                legRating.Visible = true;
                DataSet dt1 = new DataSet();
                dt1 = null;
                dt1 = reportbll.Getfeedbackrating(_Sessionid, out errMessage);
                if (errMessage == null || errMessage == "")
                {
                    if (dt1.Tables[0] != null)
                    {
                        lblerrMsg.Text = string.Empty;
                        GrvRating.Visible = true;
                        fldRating.Visible = true;
                        legRating.Visible = true;
                        GrvRating.DataSource = dt1.Tables[0];
                        GrvRating.DataBind();
                        score1 = Convert.ToDecimal(dt1.Tables[0].Rows[0][3].ToString());
                        score2 = Convert.ToDecimal(dt1.Tables[0].Rows[1][3].ToString());
                        score3 = Convert.ToDecimal(dt1.Tables[0].Rows[2][3].ToString());
                    }
                }
                else
                {
                    GrvRating.Visible = false;
                    fldRating.Visible = false;
                    legRating.Visible = false;
                    lblerrMsg.Text = "Unable to run report.Participant have selected the option 5 (Not Applicable).";
                }
            }
        }
        public void GetOverPattern()
        {
            string errMessage = string.Empty;
            Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
            _Sessionid.Sessionid = ddlSession.Text;
            _Sessionid.TrainerID = ddltrainers.Text;
            Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();
            DataTable dt2 = reportbll.GetfeedbackPattern(_Sessionid, out errMessage);
            DataSet ds2 = new DataSet();
            ds2.Tables.Add(dt2);
            GrvPattern.DataSource = ds2;
            GrvPattern.DataBind();
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                GrvPattern.Rows[i].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                GrvPattern.Rows[i].Cells[1].HorizontalAlign = HorizontalAlign.Center;
            }
        }
        public void GetAreasConcern()
        {
            string errMessage = string.Empty;
            Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
            _Sessionid.Sessionid = ddlSession.Text;
            _Sessionid.TrainerID = ddltrainers.Text;
            Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();
            DataTable dt3 = reportbll.GetfeedbackConcern(_Sessionid, out errMessage);
            DataSet ds3 = new DataSet();
            ds3.Tables.Add(dt3);
            GrvConcern.DataSource = ds3;
            GrvConcern.DataBind();
        }
        protected void LnkRating_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds1 = new DataSet();
                string errMessage = string.Empty;
                Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
                _Sessionid.Sessionid = ddlSession.Text;
                _Sessionid.TrainerID = ddltrainers.Text;
                Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();
                DataSet dt1 = reportbll.Getfeedbackrating(_Sessionid, out errMessage);
                ExportToPDF("Sample", dt1);
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }
        public void ExportToPDF(string filename, DataSet dsInput)
        {
            openTemplateandsave();
        }
        public void openTemplateandsave()
        {
            try
            {
                string htmlText = ConverPDFReport();
                HTMLToPdf(htmlText, "PDFfile.pdf");
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }

        private string ConverPDFReport()
        {
            string strdte = string.Empty;
            string enddate = string.Empty;
            string Noofpart = string.Empty;
            string Trainername = string.Empty;
            string FinalTrainerName = string.Empty;
            string TempTrainerName = string.Empty;
            string Comments = string.Empty;
            
            Hex.Vars.FB.HexFBBLL.SessionBLL sessionbll = new SessionBLL();
            DataTable dtTrainerDetails = sessionbll.getSessionSessionIdWiseNew1(ddlSession.SelectedValue);
            DataSet dsTrainerDetails = new DataSet();
            dsTrainerDetails.Tables.Add(dtTrainerDetails);
            strdte = dsTrainerDetails.Tables[0].Rows[0]["startdate"].ToString();
            enddate = dsTrainerDetails.Tables[0].Rows[0]["enddate"].ToString();
            if (strdte == enddate)
            {
                strdte = dsTrainerDetails.Tables[0].Rows[0]["startdate"].ToString();
            }
            else
            {
                strdte = dsTrainerDetails.Tables[0].Rows[0]["startdate"].ToString() + " To " + dsTrainerDetails.Tables[0].Rows[0]["enddate"].ToString();
            }
            Noofpart = dsTrainerDetails.Tables[0].Rows[0]["max_participants"].ToString();
            if (ddltrainers.SelectedIndex == 1)
            {
                if (dsTrainerDetails.Tables[0] != null)
                {
                    int Count = 0;
                    Count = dsTrainerDetails.Tables[0].Rows.Count;
                    Trainername = string.Empty;
                    if (Count == 1)
                    {
                        Trainername = dsTrainerDetails.Tables[0].Rows[0]["Trainer Name"].ToString().ToUpper();
                    }
                    else
                    {
                        for (int TrainerCount = 0; TrainerCount < dsTrainerDetails.Tables[0].Rows.Count; TrainerCount++)
                        {
                            if (Count == 1)
                                Trainername = Trainername + dsTrainerDetails.Tables[0].Rows[TrainerCount]["Trainer Name"].ToString().ToUpper();
                            else if (Count == 2)
                                Trainername = Trainername + dsTrainerDetails.Tables[0].Rows[TrainerCount]["Trainer Name"].ToString().ToUpper() + " and ";
                            else
                                Trainername = Trainername + dsTrainerDetails.Tables[0].Rows[TrainerCount]["Trainer Name"].ToString().ToUpper() + ",";
                            Count--;
                        }
                    }
                }
                TempTrainerName = Regex.Replace(Trainername, @"[()]", String.Empty);
                FinalTrainerName = Regex.Replace(TempTrainerName, @"[0-9]", String.Empty);
            }
            else
            {
                TempTrainerName = Regex.Replace(ddltrainers.SelectedValue, @"[()]", String.Empty);
                FinalTrainerName = Regex.Replace(TempTrainerName, @"[0-9]", String.Empty);
            }
            string errMessage = string.Empty;
            Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
            _Sessionid.Sessionid = ddlSession.Text;
            Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();
            GetRating();
            DataTable dt = reportbll.GetfeedbackComments(_Sessionid, out errMessage);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            StringBuilder builder = new StringBuilder();
            int sno = 0;
            builder.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
               "<tr>" +
           "<td>" +
              "<b> Based on overall feedback received from participants, record areas of improvements if any:</b><br />" +
          "</td> </tr><tr><td><ul>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
               
                
                if (ds.Tables[0].Rows[i]["CommentsValid"].ToString() == "1")
                {
                    
                    string tempComments = ds.Tables[0].Rows[i]["Comments"].ToString().Trim();
                    sno++;
                    builder.Append("&nbsp;<li>");
                    builder.Append(tempComments);
                    builder.Append("</li>");
                    //builder.Append(Environment.NewLine);
                }
               
            }
            builder.Append("</ul></td></tr></table>");
            Comments = builder.ToString();
            StringBuilder template = new StringBuilder();
            template.AppendFormat("<table style=\"width: 100%; font-family: Verdana; font-size:7px\"> " +
                "<tr>" +
            "<td style='text-align: right; font-weight: bold;'>" +
                "Hex 2557" +
            "</td>" +
       " </tr>" +
        "<tr>" +
            "<td style='text-align: center; font-weight: bold;'>" +
                "Training Feedback Analysis" +
            "</td>" +
       " </tr>" +
       " <tr>" +
           " <td style=\"height: 14px\">" +
            "</td>" +
       " </tr>" +
        "<tr  border='0.5' style=\"text-align: center; font-weight: bold;color:#006BD7;background-color:#faf8f8;\">" +
            "<td>" +
               " Training Feedback Analysis" +
           " </td>" +
       " </tr>" +
       " <tr>" +
            "<td style=\"height: 14px\">" +
            "</td>" +
        "</tr>" +
        "<tr>" +
          "<td>" +
               " <table  border='0.5' style=\"border-color: #000000; width: 100%; font-family: Verdana;\">" +
                    "<tr style=\"width: 100%;\">" +
                       "<td colspan=\"4\" style=\"border-color: #000000; border-width: 1px; text-align: center;font-weight: bold;\">" +
                            "Course Details" +
                       " </td>" +
                    "</tr>" +
                   "<tr >" +
                        "<td style=\"width: 25%;text-align:right;font-weight: bold;\">" +
                        "Course Title:" +
                       " </td>" +
                       " <td style=\"width: 25%;text-align:left;color:#006BD7\">" +
                        "<CourseTitle>" +
                        "</td>" +
                       "<td style=\"width: 25%;text-align:right;font-weight: bold;\">" +
                        "Course Date(s): " +
                        "</td>" +
                        "<td style=\"width: 25%;text-align:left;color:#006BD7\">" +
                        "<CourseDate>" +
                        "</td>" +
                    "</tr>" +
                   " <tr >" +
                        "<td style=\"width: 25%;text-align:right;font-weight: bold;\">" +
                        "Trainer’s Name:   " +
                        "</td>" +
                        "<td style=\"width: 25%;text-align:left;color:#006BD7\">" +
                        "<TrainerName> " +
                        "</td>" +
                        "<td style=\"width: 25%;text-align:right;font-weight: bold;\">" +
                        "No. of Participants:" +
                       " </td>" +
                        "<td style=\"width: 25%;text-align:left;color:#006BD7\">" +
                        "<NOP>" +
                       " </td>" +
                    "</tr>" +
               " </table>" +
            "</td>" +
        "</tr>" +
        " <tr>" +
           " <td style=\"height: 14px\">" +
            "</td>" +
        "</tr>" +
       " <tr>" +
            "<td>" +
           " <table  border='0.5' style=\"border-color: #000000; width: 100%; font-family: Verdana;\">" +
                   " <tr style=\"font-weight: bold;\">" +
                        "<td style=\"width: 33%;text-align:right\">" +
                        " </td>" +
                       " <td style=\"width: 33%;text-align:center;font-weight: bold;\">" +
                       " Maximum Score " +
                       " </td>" +
                       " <td style=\"width: 33%;text-align:center;font-weight: bold;\">" +
                       "Score" +
                        "</td>" +

                    "</tr>" +
                    "<tr >" +
                       " <td style=\"width: 33%;text-align:right;font-weight: bold;\">" +
                      " Training Administration:   " +
                       " </td>" +
                        "<td style=\"width: 33%;text-align:center;font-weight: bold;\">" +
                       " 4.0" +
                        "</td>" +
                        "<td style=\"width: 33%;text-align:center;color:#006BD7\">" +
                       "<S1>" +
                        "</td>" +

                   " </tr>" +
                    "<tr >" +
                        "<td style=\"width: 33%;text-align:right;font-weight: bold;\">" +
                      " Training Design & Methodology:   " +
                       " </td>" +
                        "<td style=\"width: 33%;text-align:center;font-weight: bold;\">" +
                       " 4.0" +
                        "</td>" +
                        "<td style=\"width: 33%;text-align:center;color:#006BD7\">" +
                       "<S2>" +
                        "</td>" +

                   " </tr>" +
                    "<tr >" +
                       "<td style=\"width: 33%;text-align:right;font-weight: bold;\">" +
                      " Trainer Delivery:   " +
                       " </td>" +
                        "<td style=\"width: 33%;text-align:center;font-weight: bold;\">" +
                       " 4.0" +
                        "</td>" +
                        "<td style=\"width: 33%;text-align:center;color:#006BD7\">" +
                       "<S3>" +
                        "</td>" +

                   " </tr>" +
               " </table>" +
           "</td>" +
      " </tr>" +
        " <tr>" +
           " <td style=\"height: 14px\">" +
            "</td>" +
        "</tr>" +

         " <tr>" +
           " <td>" +
             "<table  border='0.5' style=\"border-color: #000000; width: 100%; font-family: Verdana;\">" +
                   "  <tr >" +
                        "<td style=\"width: 50%;text-align:right;font-weight: bold;\">" +
                         "Overall Rating: " +
                        "</td>" +
                        "<td style=\"width: 50%;text-align:left;color:#006BD7\">" +
                       "<S4>" +
                        "</td>" +

                    "</tr>" +
                    "</table>" +

                   " </td>" +
                   " </tr>" +

                            " <tr>" +
           " <td style=\"height: 14px\">" +
            "</td>" +
        "</tr>" +
        "<tr><td><table>" +
               "<tr  border='0.5' style=\"text-align:left;height:100px;\">" +
           "<td style=\"vertical-align:top\"><comments></td></tr></table></td></tr>" +
        "</tr>" +
         " <tr>" +
           " <td style=\"height: 14px\">" +
            "</td>" +
        "</tr>" +
      " <tr>" +
           " <td>" +
           "  <table  border='0.5' style=\"border-color: #000000; width: 100%; font-family: Verdana;\">" +
                   "<tr>" +
                       " <td style=\"width: 25%;text-align:right;font-weight: bold;\">" +
                        " Prepared by: " +
                        "</td>" +
                       "<td style=\"width: 25%;text-align:left;color:#006BD7\">" +
                     "<Preparedby>" +
                       " </td>" +
                       "<td style=\"width: 25%;text-align:right;font-weight: bold;\">" +
                      "Date:" +
                        "</td>" +
                        "<td style=\"width: 25%;text-align:left;color:#006BD7\">" +
                     "<PreparedDate> " +
                       " </td>" +
                    "</tr>" +
                   " </table>" +
                  "  </td>" +
                    "</tr>" +
   " </table>");

            template.Replace("<CourseTitle>", drpCourse.SelectedItem.Text);
            template.Replace("<CourseDate>", strdte);
            template.Replace("<TrainerName>", FinalTrainerName);
            template.Replace("<NOP>", Noofpart);
            template.Replace("<S1>", Convert.ToString(score1).Substring(0, 4));
            template.Replace("<S2>", Convert.ToString(score2).Substring(0, 4));

            if (ddltrainers.SelectedIndex == 1)
            {
                template.Replace("<S3>", Convert.ToString(TrainerAvg).Substring(0, 4));
                score4 = (score1 + score2 + TrainerAvg) / 3;
            }
            else
            {
                template.Replace("<S3>", Convert.ToString(score3).Substring(0, 4));
                score4 = (score1 + score2 + score3) / 3;
            }
            template.Replace("<S4>", Convert.ToString(score4).Substring(0, 4));
            string[] Commentline = Regex.Split(Comments, "\n");

           


            template.Replace("<comments>",Comments);
            if (Session["username"] != null)
            {
                nominationlist.Flag = 1;
                SqlDataReader DrPreparedByUserName = _session.getindividual(Session["username"].ToString(), null, null, nominationlist.Flag);
                while (DrPreparedByUserName.Read())
                {
                    template.Replace("<Preparedby>", DrPreparedByUserName["AdminName"].ToString());
                }
            }

            template.Replace("<PreparedDate>", dsTrainerDetails.Tables[0].Rows[0]["PreparedDate"].ToString());
            return Convert.ToString(template);
        }

        public void HTMLToPdf(string HTML, string FilePath)
        {
            Document document = new Document();

            PdfWriter.GetInstance(document, new FileStream(Request.PhysicalApplicationPath + "\\Chap0101.pdf", FileMode.Create));
            document.Open();
            iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(Server.MapPath(ResolveUrl("~/Images/HexawareLogo.jpg")));
            pdfImage.ScaleToFit(100, 50);
            pdfImage.Alignment = iTextSharp.text.Image.UNDERLYING; pdfImage.SetAbsolutePosition(35, 780);
            document.Add(pdfImage);
            iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
            iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
            hw.Parse(new StringReader(HTML));
            document.Close();
            ShowPdf("Chap0101.pdf");
        }
        private void ShowPdf(string s)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + s);
            Response.WriteFile(s);
            Response.Flush();
            Response.Clear();
        }

        protected void ddltrainers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddltrainers.Text == "--Select--")
                {

                }
                else
                {
                    string errMessage = string.Empty;
                    Hex.Vars.FB.HexFBModule.Reportinfo _Sessionid = new Hex.Vars.FB.HexFBModule.Reportinfo();
                    _Sessionid.Sessionid = ddlSession.Text;
                    _Sessionid.TrainerID = ddltrainers.Text;
                    Hex.Vars.FB.HexFBBLL.ReportBLL reportbll = new ReportBLL();
                    DataTable dt = reportbll.GetFeedback(_Sessionid, out errMessage);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    if (ds.Tables[0].Rows.Count > 12 || ddltrainers.Text == "OverAll")
                    {
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('No feedbacks are given by nominated staff');", true);
                        }
                        else
                        {
                            Getpattern();
                            GetRating();
                            GetOverPattern();
                            GetAreasConcern();
                        }
                    }
                    else
                    {
                        fldRating.Visible = false;
                        legRating.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('No feedbacks are given by nominated staff');", true);
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

#endregion