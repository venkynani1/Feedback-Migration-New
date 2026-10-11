using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;


namespace HexFeedback
{
    public partial class HomeMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string connStr = "Data Source =EBIII184F3; Initial Catalog =HexawareTraining; User Id =sa; Password =Password123;";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "Select MenuID, Text,Description, ParentID,URL from Menu";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds);
                da.Dispose();
            }

            ds.DataSetName = "Menus";
            ds.Tables[0].TableName = "Menu";
            DataRelation relation = new DataRelation("ParentChild",
             ds.Tables["Menu"].Columns["MenuID"],
             ds.Tables["Menu"].Columns["ParentID"], true);

            relation.Nested = true;
            ds.Relations.Add(relation);

            xmlDataSource.Data = ds.GetXml();

            if (Request.Params["Sel"] != null)
                Response.Redirect(Request.Params["Sel"]);

                //Page.Controls.Add(new System.Web.UI.LiteralControl("You selected " +
                //  Request.Params["Sel"]));
        }

    }
}
