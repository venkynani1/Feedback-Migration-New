SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================  
-- Author:      SURESHKUMAR.D  
-- Create date: 18 April 2012  
-- Description: GET FEEDBACK BALANCE EMP ID  
-- =============================================  
--EXEC usp_CheckCourse 'SQL2003_4/10/2012To4/16/2012'  
  
CREATE procedure [dbo].[usp_getFeedbackBalanceEmpId]          
(        
@sessionid varchar(50)        
)          
as          
begin    
 SELECT HV.S_EMPLOYEE_FNAME  
     
   FROM Nomination_List NL   
   INNER JOIN Hexv_feedb_VW HV on HV.S_EMPLOYEE_ID=NL.Employee_id  
    
  where  NL.Session_id = @sessionid  
   group by HV.S_EMPLOYEE_FNAME  
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


