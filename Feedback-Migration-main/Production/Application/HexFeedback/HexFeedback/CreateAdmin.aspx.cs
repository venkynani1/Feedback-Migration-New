using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient; 
using Hex.Vars.FB.HexFBModule;
using Hex.Vars.FB.HexFBBLL;

namespace HexFeedback
{
    public partial class CreateAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AdminBLL adminbll = new AdminBLL(); 
                        
            SqlDataReader dr = adminbll.searchEmployee(txtEmpID.Text, txtFName.Text);

            grvResult.DataSource = dr;
            grvResult.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void grvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}