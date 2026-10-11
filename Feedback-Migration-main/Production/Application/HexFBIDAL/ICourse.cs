/// ***********************************************************************************************
/// File name: ICourse.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Hex.Vars.FB.HexFBModule; 
  

namespace Hex.Vars.FB.HexFBIDAL
{
    public interface ICourse
    {
        bool createCourse(Hex.Vars.FB.HexFBModule.CourseInfo _course, out string errMsg, out int IsCourseIdExist,out int IsCourseNameExist,out int RowAffected);
        bool updateCourse(Hex.Vars.FB.HexFBModule.CourseInfo _course, out string errMsg, out int IsCourseNameExist, out int RowAffected);
        bool deleteCourse(Hex.Vars.FB.HexFBModule.CourseInfo _course, out string errMsg, out int RowAffected);
        SqlDataReader getAllCourse();
        DataTable SelectCourseAll();
        SqlDataReader getAllTtrainerDet(Hex.Vars.FB.HexFBModule.MasterInfo MasterMaillist);
        SqlDataReader getAllCourseType();
        SqlDataReader getVendorDet(int flag, Hex.Vars.FB.HexFBModule.MasterInfo _masterinfo);
        /// <summary>
        /// get the specific CourseDetail
        /// </summary>
        /// <param name="_courseId"></param>
        /// <returns></returns>
        DataRow getCourseDetail(string _courseId);
        /// <summary>
        /// get all CourseDetails
        /// </summary>
        /// <returns></returns>
        DataSet getCourseDetail();
    }
}
