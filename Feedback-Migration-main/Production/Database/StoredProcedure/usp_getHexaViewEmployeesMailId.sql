SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================  
-- Author:      SURESHKUMAR.D  
-- Create date: 18 April 2012  
-- Description: GET HEXA VIEW EMPLOYEES MAILID  
-- =============================================  
--EXEC usp_getHexaViewEmployeesMailId 'TEST@HEXAWARE.COM'  
  
CREATE procedure [dbo].[usp_getHexaViewEmployeesMailId]  
 (@EmplidMail varchar(1000))   
as  
begin  
SET ANSI_NULLS ON   
begin  
 select S_EMPLOYee_ID as 'EMPLOYEE ID',temp1.EmployeeName AS 'EMPLOYEE NAME', S_EMPLOYEE_EMAIL AS 'EMAIL' from (Select * , Replace(Replace(REPLACE(REPLACE(REPLACE(connectedName,'     ',' '),'    ',' '),'   ',' '),'  ',' '),'  ',' ') as EmployeeName from (
Select *,ISNULL(S_Employee_FName,'')+' '+ISNULL(S_Employee_MName,'')+' '+ISNULL(S_Employee_LName,'') as connectedName from Hexv_feedb_VW) as Temp) as temp1 where temp1.S_EMPLOYEE_ID in ( @EmplidMail) order by temp1.EmployeeName   
 return  
end   
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


