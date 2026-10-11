SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================  
-- Author:      SURESHKUMAR.D  
-- Create date: 18 April 2012  
-- Description: GET EMPLOYEE EMAIL ID  
-- =============================================  
--EXEC usp_GetEmployeeEmailID '22002','SQL2003_4/10/2012To4/16/2012','sql2003'  
  
CREATE procedure [dbo].[usp_GetEmployeeEmailID]    
(  
 @empid varchar(7),    
 @sessionid varchar(50),    
 @courseid varchar(50)    
)    
as    
begin    
 select   
 HV.S_EMPLOYEE_EMAIL  
 from course c inner join C_Session cs on c.courseid=cs.Courseid inner join Nomination_List nl on  
 nl.Session_id=  
 cs.sessionid inner join Hexv_feedb_VW HV on HV.S_EMPLOYEE_ID=nl.Employee_id   where nl.Employee_id=@empid and nl.Session_id = @sessionid and nl.course_id=@courseid       
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


