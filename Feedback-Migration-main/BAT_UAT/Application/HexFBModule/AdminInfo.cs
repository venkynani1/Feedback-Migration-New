/// ***********************************************************************************************
/// File name: AdminInfo.cs
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

namespace Hex.Vars.FB.HexFBModule
{
    public class AdminInfo
    {
         #region Constructors
        public AdminInfo()
            {

            }
            #endregion Constructors

            #region declarations
            protected string _EmpId;
            protected string _EmpName;
            protected bool _IsSuperAdmin;
            protected string _createdby;
            protected string _createddate;
            #endregion declarations

            #region property

            public string EmpId
            {
                get
                {
                    return _EmpId;
                }
                set
                {
                    _EmpId = value;
                }
            }
            public string EmpName
            {
                get
                {
                    return _EmpName;
                }
                set
                {
                    _EmpName = value;
                }
            }
            public bool IsSuperAdmin
            {
                get
                {
                    return _IsSuperAdmin;
                }
                set
                {
                    _IsSuperAdmin = value;
                }
            }
            public string createdby
            {
                get
                {
                    return _createdby;
                }
                set
                {
                    _createdby = value;
                }
            }
            public string createddate
            {
                get
                {
                    return _createddate;
                }
                set
                {
                    _createddate = value;
                }
            }
            
            #endregion property
    }
}
