/// ***********************************************************************************************
/// File name: ReportInfo.cs
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
    public class Reportinfo
    {
        #region Constructors
        public Reportinfo()
        {

        }
        #endregion Constructors

        #region declarations
        protected string _Sessionid;
        protected string _TrainerID;
        protected DateTime _SessionDte;
        protected int _Flag;
      
        #endregion declarations

        #region property

        public int Flag
        {
            get
            {
                return _Flag;
            }
            set
            {
                _Flag = value;
            }
        }

        public string Sessionid
        {
            get
            {
                return _Sessionid;
            }
            set
            {
                _Sessionid = value;
            }
        }
        public string TrainerID
        {
            get
            {
                return _TrainerID;
            }
            set
            {
                _TrainerID = value;
            }
        }
        public DateTime SessionDte
        {
            get
            {
                return _SessionDte;
            }
            set
            {
                _SessionDte = value;
            }
        }
     

        #endregion property
    }
}
