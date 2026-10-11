using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HexFeedback
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ResSessionid = Session["SelecSessionid"].ToString();
           
            System.Text.StringBuilder strBody = new System.Text.StringBuilder("");

            strBody.Append("");


            Response.AppendHeader("Content-Type", "application/msword");
            Response.AppendHeader("Content-disposition", "attachment; filename=myword.doc");
            Response.Write(strBody);
            strBody.Append("");


            Response.AppendHeader("Content-Type", "application/msword");
            Response.AppendHeader("Content-disposition", "attachment; filename=myword.doc");
            Response.Write(strBody);
        }
    }
}