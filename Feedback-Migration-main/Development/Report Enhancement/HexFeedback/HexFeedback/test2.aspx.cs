using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HexFeedback
{
    public partial class test2 : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection("Data Source =EBIII122F3; Initial Catalog =HexawareTraining; User Id =sa; Password =Password123;");
        protected void Page_Load(object sender, EventArgs e)
        {
            elm1.Attributes.Add("onkeydown", "textboxMultilineMinNumber(this,10)");
            if (!IsPostBack)
            {
                
                BindGridData();
            }

        }

        private void BindGridData()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Employee_Details", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvdetails.DataSource = dt;
            gvdetails.DataBind();
            

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update Employee_Details set FirstName=@FirstName,LastName=@LastName, City=@City,Designation=@Designation where UserId=@UserId", con);
            cmd.Parameters.AddWithValue("@FirstName", txtfname.Text);
            cmd.Parameters.AddWithValue("@LastName", txtlname.Text);
            cmd.Parameters.AddWithValue("@City", txtCity.Text);
            cmd.Parameters.AddWithValue("@Designation", txtDesg.Text);
            cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(lblID.Text));
            cmd.ExecuteNonQuery();
            con.Close();
            lblresult.Text = lblusername.Text + " Details Updated Successfully";
            lblresult.ForeColor =System.Drawing.Color.Green;
            BindGridData();
        }
        protected void imgbtn_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            lblID.Text = gvdetails.DataKeys[gvrow.RowIndex].Value.ToString();
            lblusername.Text = gvrow.Cells[1].Text;
            txtfname.Text = gvrow.Cells[2].Text;
            txtlname.Text = gvrow.Cells[3].Text;
            txtCity.Text = gvrow.Cells[4].Text;
            txtDesg.Text = gvrow.Cells[5].Text;
            this.ModalPopupExtender1.Show();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String HTMLText = elm1.Value;
            Label1.Text = elm1.Value.ToString(); 
        }

       

    }
}