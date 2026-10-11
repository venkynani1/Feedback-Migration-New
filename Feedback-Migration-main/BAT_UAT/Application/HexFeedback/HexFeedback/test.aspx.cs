using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

using iTextSharp.text.html;


using System.Collections;


namespace HexFeedback
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String htmlText = "<font  " +
       " color=\"#0000FF\"><b><i>Title One</i></b></font><font   " +
       " color=\"black\"><br><br>Some text here<br><br><br><font   " +
       " color=\"#0000FF\"><b><i>Another title here   " +
       " </i></b></font><font   " +
       " color=\"black\"><br><br>Text1<br>Text2<br><OL><LI><DIV Style='color:green'>Pham Duy Hoa</DIV></LI><LI>how are u</LI></OL><br/>" +
       "<table border='1'><tr><td style='color:red;text-align:right;width:20%'>123426</td><td style='color:green;width:60%'>78910</td><td style='color:red;width:20%'>ASFAFA</td></tr><tr><td style='color:red;text-align:right'>123426</td><td style='color:green;width:60%'>78910</td><td style='color:red;width:20%'>DAFSDGAFW</td></tr></table><br/>" +
       "<div><ol><li>123426</li><li>123426</li><li>123426</li><li>123426</li></ol></div>";
            //         StringBuilder template = new StringBuilder();
            //         String htmlText = "<div>" +
            //    " <fieldset class=\"ForFieldset\"> " +
            //        " <legend class=\"ForLegend\">Training Feedback Analysis</legend>" +
            //         "<table style=\"width: 100%; text-align: center;\">" +
            //             "<tr>" +
            //                " <td style=\"background-color: #33CCFF; color: #000000; font-weight: bold; font-style: italic; font-family: Verdana; font-size: 20px\">" +
            //                 "test" +
            //                 "</td>" +
            //            " </tr>" +

            //        "</table>" +
            //     "</fieldset>" +
            //"</div>)";


            //         String htmlText = "<table style=\"width: 100%; font-family: Verdana; font-size: 12px\"> " +
            //     "<tr>" +
            //     "<td>"+
            //    " <img src=\"Images/arrow1.gif\" width=\"318px\" height=\"103\"/>"+
            //    " </td>"+
            //         "<td style=\"text-align: center; font-weight: bold;\">" +
            //             "Training Feedback Analysis" +
            //         "</td>" +
            //    " </tr>" +
            //    " <tr>" +
            //        " <td style=\"height: 14px\">" +
            //         "</td>" +
            //    " </tr>" +
            //     "<tr style=\"text-align: center; font-weight: bold;\">" +
            //         "<td>" +
            //            " Level 1 Reactionaries Feedback Analysis" +
            //        " </td>" +
            //    " </tr>" +
            //    " <tr>" +
            //         "<td style=\"height: 14px\">" +
            //         "</td>" +
            //     "</tr>" +
            //     "<tr>" +
            //       "<td>" +
            //            " <table style=\"border-color: #000000; width: 100%; font-family: Verdana;\">" +
            //                 "<tr style=\"width: 100%;\">" +
            //                    "<td colspan=\"4\" style=\"border-color: #000000; border-width: 1px; text-align: center;font-weight: bold;\">" +
            //                         "Course Details" +
            //                    " </td>" +
            //                 "</tr>" +
            //                "<tr >" +
            //                     "<td style=\"width: 25%;text-align:right\">" +
            //                     "Course Title:" +
            //                    " </td>" +
            //                    " <td style=\"width: 25%;text-align:left\">" +
            //                     "<CourseTitle>" +
            //                     "</td>" +
            //                    "<td style=\"width: 25%;text-align:right\">" +
            //                     "Course Date(s): " +
            //                     "</td>" +
            //                     "<td style=\"width: 25%;text-align:left\">" +
            //                     "<CourseDate>" +
            //                     "</td>" +
            //                 "</tr>" +
            //                " <tr >" +
            //                     "<td style=\"width: 25%;text-align:right\">" +
            //                     "Trainer’s Name:   " +
            //                     "</td>" +
            //                     "<td style=\"width: 25%;text-align:left\">" +
            //                     "<TrainerName> " +
            //                     "</td>" +
            //                     "<td style=\"width: 25%;text-align:right\">" +
            //                     "No. of Participants:" +
            //                    " </td>" +
            //                     "<td style=\"width: 25%;text-align:left\">" +
            //                     "<NOP>" +
            //                    " </td>" +
            //                 "</tr>" +
            //            " </table>" +
            //         "</td>" +
            //     "</tr>" +
            //     " <tr>" +
            //        " <td style=\"height: 14px\">" +
            //         "</td>" +
            //     "</tr>" +
            //    " <tr>" +
            //         "<td>" +
            //        " <table style=\"border-color: #000000; width: 100%; font-family: Verdana;\">" +
            //                " <tr style=\"font-weight: bold;\">" +
            //                     "<td style=\"width: 25%;text-align:right\">" +
            //                     " </td>" +
            //                    " <td style=\"width: 25%;text-align:center\">" +
            //                    " Maximum Score " +
            //                    " </td>" +
            //                    " <td style=\"width: 25%;text-align:center\">" +
            //                    "Score" +
            //                     "</td>" +
            //                    " <td style=\"width: 25%;text-align:center\">" +
            //                     "Percentage" +
            //                    " </td>" +
            //                 "</tr>" +
            //                 "<tr >" +
            //                    " <td style=\"width: 25%;text-align:right\">" +
            //                   " Training Administration:   " +
            //                    " </td>" +
            //                     "<td style=\"width: 25%;text-align:center\">" +
            //                    " 4.0" +
            //                     "</td>" +
            //                     "<td style=\"width: 25%;text-align:center\">" +
            //                    "<S1>" +
            //                     "</td>" +
            //                     "<td style=\"width: 25%;text-align:center\">" +
            //                     "<P1>" +
            //                     "</td>" +
            //                " </tr>" +
            //                 "<tr >" +
            //                     "<td style=\"width: 25%;text-align:right\">" +
            //                   " Training Design & Methodology:   " +
            //                    " </td>" +
            //                     "<td style=\"width: 25%;text-align:center\">" +
            //                    " 4.0" +
            //                     "</td>" +
            //                     "<td style=\"width: 25%;text-align:center\">" +
            //                    "<S1>" +
            //                     "</td>" +
            //                     "<td style=\"width: 25%;text-align:center\">" +
            //                     "<P1>" +
            //                     "</td>" +
            //                " </tr>" +
            //                 "<tr >" +
            //                    "<td style=\"width: 25%;text-align:right\">" +
            //                   " Trainer Delivery:   " +
            //                    " </td>" +
            //                     "<td style=\"width: 25%;text-align:center\">" +
            //                    " 4.0" +
            //                     "</td>" +
            //                     "<td style=\"width: 25%;text-align:center\">" +
            //                    "<S1>" +
            //                     "</td>" +
            //                     "<td style=\"width: 25%;text-align:center\">" +
            //                     "<P1>" +
            //                     "</td>" +
            //                " </tr>" +
            //            " </table>" +
            //        "</td>" +
            //   " </tr>" +
            //     " <tr>" +
            //        " <td style=\"height: 14px\">" +
            //         "</td>" +
            //     "</tr>" +
            //     "<tr style=\"text-align:left;height:60px;\">" +
            //         "<td style=\"vertical-align:top\">" +
            //            " Based on overall feedback received from participants, record areas of improvements if any:<br />" +
            //           " <comments>" +
            //        "</td>" +
            //     "</tr>" +
            //      " <tr>" +
            //        " <td style=\"height: 14px\">" +
            //         "</td>" +
            //     "</tr>" +
            //   " <tr>" +
            //        " <td>" +
            //        "  <table style=\"border-color: #000000; width: 100%; font-family: Verdana;\">" +
            //                "<tr>" +
            //                    " <td style=\"width: 16%;text-align:right\">" +
            //                     " Prepared by: " +
            //                     "</td>" +
            //                     "<td style=\"width: 16%;text-align:left\">" +
            //                    "____________" +
            //                     "</td>" +
            //                     "<td style=\"width: 16%;text-align:right\">" +
            //                 " Signature:" +
            //                     "</td>" +
            //                    " <td style=\"width: 16%;text-align:left\">" +
            //                   "____________ "+
            //                     "</td>" +
            //                    " <td style=\"width: 16%;text-align:right\">" +
            //                   "Date:" +
            //                     "</td>" +
            //                     "<td style=\"width: 16%;text-align:left\">" +
            //                  "____________" +
            //                    " </td>" +
            //                 "</tr>" +
            //                     "<tr>" +
            //                    " <td style=\"width: 16%;text-align:right\">" +
            //                     " Reviewed by: " +
            //                     "</td>" +
            //                     "<td style=\"width: 16%;text-align:left\">" +
            //                    "____________ "+
            //                     "</td>" +
            //                     "<td style=\"width: 16%;text-align:right\">" +
            //                 " Signature:" +
            //                     "</td>" +
            //                    " <td style=\"width: 16%;text-align:left\">" +
            //                   "____________ "+
            //                     "</td>" +
            //                    " <td style=\"width: 16%;text-align:right\">" +
            //                   "Date:" +
            //                     "</td>" +
            //                     "<td style=\"width: 16%;text-align:left\">" +
            //                  "____________ "+
            //                    " </td>" +
            //                 "</tr>" +
            //                " </table>" +
            //               "  </td>" +
            //                 "</tr>" +
            //" </table>";
            HTMLToPdf(htmlText, "PDFfile.pdf");
        }

        public void HTMLToPdf(string HTML, string FilePath)
        {
            Document document = new Document();

            PdfWriter.GetInstance(document, new FileStream(Request.PhysicalApplicationPath + "\\Chap0101.pdf", FileMode.Create));
            document.Open();


            iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(Server.MapPath(ResolveUrl("~/Images/HexawareLogo.jpg")));
            pdfImage.ScaleToFit(100, 50);
            pdfImage.Alignment = iTextSharp.text.Image.UNDERLYING; pdfImage.SetAbsolutePosition(10, 750);
            document.Add(pdfImage);
            //Image pdfImage = System.Web.UI.WebControls.Image.GetInstance(Server.MapPath("logo.png"));

            //pdfImage.ScaleToFit(100, 50);

            //pdfImage.Alignment = iTextSharp.text.Image.UNDERLYING; pdfImage.SetAbsolutePosition(180, 760);

            //document.Add(pdfImage);

            iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();

            ArrayList objects;
            styles.LoadTagStyle("body", "font-family", "times new roman");
            styles.LoadTagStyle("body", "font-size", "5px");
            styles.LoadTagStyle("body", "color", "Black");
            iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
            //hw.Parse(new StringReader(HTML));

            objects = iTextSharp.text.html.simpleparser.
                       HTMLWorker.ParseToList(new StringReader(HTML), styles);
            for (int k = 0; k < objects.Count; k++)
            {
                document.Add((IElement)objects[k]);
            }

            document.Close();
            ShowPdf("Chap0101.pdf");
        }
        private void ShowPdf(string s)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            //Response.AppendHeader("Content-Disposition", "inline;filename=" + s);
            //Response.ContentType = "application/pdf";
            //Response.WriteFile(s);
            //Response.Flush();
            //Response.Clear();

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + s);
            Response.WriteFile(s);
            Response.Flush();
            Response.Clear();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
        }
        AjaxControlToolkit.ModalPopupExtender modalPop = new AjaxControlToolkit.ModalPopupExtender();
        protected void Button2_Click1(object sender, EventArgs e)
        {
            modalPop.ID = "popUp";
            modalPop.PopupControlID = "ModalPanel";
            modalPop.TargetControlID = "Button2";
            modalPop.DropShadow = false;
            modalPop.BackgroundCssClass = "modalBackground";
            this.Panel1.Controls.Add(modalPop);
            modalPop.Show();
            Timer1.Enabled = true;
        }
        protected void timer1_Ontick(object sender, EventArgs e)
        {
            modalPop.Hide();
            Timer1.Enabled = false;
        }
    }

}
