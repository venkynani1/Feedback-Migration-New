/// ***********************************************************************************************
/// File name: CourseBLL.cs
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
using Hex.Vars.FB.HexFBIDAL;
using System.Data;
using System.Data.SqlClient;

namespace Hex.Vars.FB.HexFBBLL
{
    public class CourseBLL
    {

        #region private
        private static readonly ICourse CourseDal = Hex.Vars.FB.HexFBDALFactory.DataAccess.CreateCourse();
        #endregion //private


        #region public methods
        public bool createCourse(Hex.Vars.FB.HexFBModule.CourseInfo _course, out string errMsg,out int IsCourseIdExist,out int IsCourseNameExist,out int RowAffected)
        {
            return CourseDal.createCourse(_course, out errMsg,out IsCourseIdExist,out IsCourseNameExist,out RowAffected);
        }
        public bool updateCourse(Hex.Vars.FB.HexFBModule.CourseInfo _course, out string errMsg,out int IsCourseNameExist,out int RowAffected)
        {
            return CourseDal.updateCourse(_course, out errMsg,out IsCourseNameExist,out RowAffected);
        }

        public bool deleteCourse(Hex.Vars.FB.HexFBModule.CourseInfo _course, out string errMsg,out int RowAffected)
        {
            return CourseDal.deleteCourse(_course, out errMsg,out RowAffected);
        }
        public SqlDataReader getAllCourse()
        {
            return CourseDal.getAllCourse();
        }
        public DataTable SelectCourseAll()
        {
            return CourseDal.SelectCourseAll();
        }
        public SqlDataReader getAllTrainer(Hex.Vars.FB.HexFBModule.MasterInfo MasterMaillist)
        {
            return CourseDal.getAllTtrainerDet(MasterMaillist);
        }
        public SqlDataReader getVendorDetais(int flag,Hex.Vars.FB.HexFBModule.MasterInfo _masterinfo)
        {
            return CourseDal.getVendorDet(flag, _masterinfo);
        }

        public SqlDataReader getAllCourseType()
        {
            return CourseDal.getAllCourseType();
        }
        #endregion

    }
}
