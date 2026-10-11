#region "References"
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
#endregion
/// <summary>
/// Summary description for Procedures
/// </summary>
public class Procedures
{
	public Procedures()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #region "ConnString"
     public static string SkillPortConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
    #endregion

    #region "Procedures"
     
     //Employee ID
     public const string SP_GETEMPLOYEEROLEBYID = "GetEmployeeRoleByID";
     
    

    //Behavioral Summary
     public const string SP_IMPORTBEHAVIORALSUMMARYDETAILS = "InsertBehaviorSummaryData";
     public const string SP_CHECKBEHAVIORALRECORDDUPLICATE = "CheckBehavioralRecordDuplicate";
     public const string SP_DELETEBEHAVIORALDUPLICATERECORDS = "DeleteBehavioralDuplicateRecords";

     //TCDP
     public const string SP_IMPORTTCDPDETAILS = "InsertTCDPData";
     public const string SP_CHECKTCDPRECORDDUPLICATE = "CheckTCDPRecordDuplicate";
     public const string SP_DELETETCDPDUPLICATERECORDS = "DeleteTCDPDuplicateRecords";

     //Certifications
     public const string SP_IMPORTCERTIFICATIONSDETAILS = "InsertCertificationsData";
     public const string SP_CHECKCERTIFICATIONSRECORDDUPLICATE = "CheckCertificationsRecordDuplicate";
     public const string SP_DELETECERTIFICATIONSDUPLICATERECORDS = "DeleteCertificationsDuplicateRecords";

     //Contributions
     public const string SP_IMPORTCONTRIBUTIONSDETAILS = "InsertContributionsData";
     public const string SP_CHECKCONTRIBUTIONSRECORDDUPLICATE = "CheckContributionsRecordDuplicate";
     public const string SP_DELETECONTRIBUTIONSDUPLICATERECORDS = "DeleteContributionsDuplicateRecords";

     //SKillPort
     public const string SP_VIEW_HVLEARNINGREPORT = "GetSkillportAssessmentReport";
     public const string SP_INSERTSKILLPORTDETAILS = "InsertSkillPortData";
     public const string SP_CHECKRECORDDUPLICATE = "CheckRecordDuplicate";
     public const string SP_DELETEDUPLICATERECORDS = "DeleteDuplicateRecords";
    
    //Training Hours
     public const string SP_TRAININGHOURSDETAILS = "InsertTrainingHoursData";
     public const string SP_CHECKTRAININGHOURSRECORDDUPLICATE = "CheckTrainingHoursRecordDuplicate";
     public const string SP_DELETETRAININGHOURSDUPLICATERECORDS = "DeleteTrainingHoursDuplicateRecords";

    

    #endregion 
}
