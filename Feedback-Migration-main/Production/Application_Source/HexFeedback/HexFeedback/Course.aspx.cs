#region Header
/// ***********************************************************************************************
/// File name: Course.aspx.cs
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
using Hex.Vars.FB.HexFBModule;
using Hex.Vars.FB.HexFBBLL;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
#endregion

#region class
namespace HexFeedback
{
    public partial class _Course : System.Web.UI.Page
    {
        Hex.Vars.FB.HexFBBLL.CourseBLL coursebll = new CourseBLL();
        Hex.Vars.FB.HexFBModule.CourseInfo courseinfo = new CourseInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCourseType();
            }
        }

        private void LoadCourseType()
        {
            try
            {
                SqlDataReader DrGetCourseType = coursebll.getAllCourseType();

                drpCtype.DataSource = DrGetCourseType;
                drpCtype.DataMember = "CourseTYpeID";
                drpCtype.DataValueField = "CourceTypeName";
                drpCtype.DataBind();
                drpCtype.Items.Insert(0, "Select a Type");
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCourseID.Text != "" && txtCourseName.Text != "" && txtDesc.Text != "" && drpCtype.SelectedIndex != 0 && drpIntExt.SelectedIndex != 0)
                {

                    string errMessage = string.Empty;
                    int IsCourseIdExist = 1;
                    int IsCourseNameExist = 1;
                    int RowAffected = 1;

                    courseinfo.CourseId = txtCourseID.Text;
                    courseinfo.CourseName = txtCourseName.Text;
                    courseinfo.CourseDesc = txtDesc.Text;
                    courseinfo.InternalExternal = drpIntExt.Text;
                    courseinfo.CourseType = drpCtype.Text;
                    courseinfo.CreatedBy = User.Identity.Name.ToString();
                    coursebll.createCourse(courseinfo, out errMessage, out IsCourseIdExist, out IsCourseNameExist, out RowAffected);

                    if (IsCourseIdExist == 1 && IsCourseNameExist == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Course Already exist ');", true);
                    }

                    else
                    {
                        CancelDetails();

                        ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Course successfully created');", true);

                        Response.Redirect("~/Session.aspx");
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "info", "javascript:alert('Please provide all information');", true);

                }
            }
            catch (Exception ex)
            {
                lblerrMsg.Text = "Error:- " + ex.Message;
            }

        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            CancelDetails();
        }

        private void CancelDetails()
        {
            txtCourseID.Text = string.Empty;
            txtCourseName.Text = string.Empty;
            txtDesc.Text = string.Empty;
            drpIntExt.SelectedIndex = 0;
            drpCtype.SelectedIndex = 0;
        }
    }
}
#endregion