SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================  
-- Author:      SURESHKUMAR.D  
-- Create date: 18 April 2012  
-- Description: GET HEXA VIEW EMPLOYEES  
-- =============================================  
--EXEC usp_getHexaViewEmployees '00001','test'  
  
CREATE procedure [dbo].[usp_getHexaViewEmployees]  
 (@empid varchar(7),    
 @EmplName varchar(100))   
as  
begin  
SET ANSI_NULLS ON   
begin  
if (LEN(@EmplName)=0)  
begin  
 select S_EMPLOYee_ID as 'EMPLOYEE ID',temp1.EmployeeName AS 'EMPLOYEE NAME', S_EMPLOYEE_EMAIL AS 'EMAIL' from (Select * , Replace(Replace(REPLACE(REPLACE(REPLACE(connectedName,'     ',' '),'    ',' '),'   ',' '),'  ',' '),'  ',' ') as EmployeeName from (
Select *,ISNULL(S_Employee_FName,'')+' '+ISNULL(S_Employee_MName,'')+' '+ISNULL(S_Employee_LName,'') as connectedName from Hexv_feedb_VW) as Temp) as temp1 where temp1.S_EMPLOYEE_ID=@empid order by temp1.EmployeeName   
 return  
end   
if (LEN(@empid)=0)  
begin  
 select S_EMPLOYEE_ID as 'EMPLOYEE ID',temp1.EmployeeName AS 'EMPLOYEE NAME', S_EMPLOYEE_EMAIL AS 'EMAIL' from (Select * , Replace(Replace(REPLACE(REPLACE(REPLACE(connectedName,'     ',' '),'    ',' '),'   ',' '),'  ',' '),'  ',' ') as EmployeeName from (
Select *,ISNULL(S_Employee_FName,'')+' '+ISNULL(S_Employee_MName,'')+' '+ISNULL(S_Employee_LName,'') as connectedName from Hexv_feedb_VW) as Temp) as temp1 where temp1.EmployeeName like '%'+@EmplName+'%' order by temp1.EmployeeName       
 return  
end   
 select S_EMPLOYee_ID as 'EMPLOYEE ID',temp1.EmployeeName AS 'EMPLOYEE NAME', S_EMPLOYEE_EMAIL AS 'EMAIL'   from (Select * , Replace(Replace(REPLACE(REPLACE(REPLACE(connectedName,'     ',' '),'    ',' '),'   ',' '),'  ',' '),'  ',' ') as EmployeeName from
 (Select *,ISNULL(S_Employee_FName,'')+' '+ISNULL(S_Employee_MName,'')+' '+ISNULL(S_Employee_LName,'') as connectedName from Hexv_feedb_VW) as Temp) as temp1 where temp1.EmployeeName like '%'+@EmplName+'%' or temp1.S_EMPLOYEE_ID=@empid order by temp1.EmployeeName       
end  
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


