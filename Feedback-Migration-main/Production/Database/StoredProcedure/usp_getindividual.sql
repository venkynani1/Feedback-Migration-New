SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE procedure [dbo].[usp_getindividual]          
(        
 @empid varchar(7),          
 @sessionid varchar(50)=NULL,          
 @courseid varchar(50)=NULL,    
 @Flag Int=0       
)          
AS          
BEGIN     
IF  @Flag=0    
 BEGIN        
   select LTRIM(RTRIM(HV.S_EMPLOYEE_FNAME))+' '+LTRIM(RTRIM(S_EMPLOYEE_LNAME)) AS TRAINEENAME,        
   c.courseid,cs.TrainerID,cs.location,nl.Employee_id,CONVERT(VARCHAR(50),cs.startdate,106) as StartDate ,CONVERT(VARCHAR(50),cs.enddate,106) as EndDate,        
   HV.S_EMPLOYEE_EMAIL AS TOEMAILID        
   from course c inner join C_Session cs on c.courseid=cs.Courseid         
   inner join Nomination_List nl on nl.Session_id=cs.sessionid inner join        
   HELIXPRD.dbo.Hexv_feedb_VW HV on HV.S_EMPLOYEE_ID=nl.Employee_id         
    where nl.Employee_id=@empid and nl.Session_id = @sessionid and nl.course_id=@courseid           
 END    
 ELSE    
 BEGIN    
  select  LTRIM(RTRIM(S_EMPLOYEE_FNAME))+' '+LTRIM(RTRIM(S_EMPLOYEE_LNAME)) AS AdminName,    
       S_EMPLOYEE_EMAIL AS ReplyMailID  from HELIXPRD.dbo.HEXV_FEEDB_VW where S_EMPLOYEE_ID=@empid    
 END    
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


