

ALTER procedure [dbo].[usp_SupervisorFeedback_Details]      
(        
 @empid varchar(7),          
 @sessionid varchar(200)=NULL ,
 @pm_id varchar(20)=NULL      
)          
AS          
BEGIN     
	select distinct nl.Employee_id,ltrim(rtrim(emp.s_employee_fname))+' '+ltrim(rtrim(emp.s_employee_lname)) as Employee_Name 
	from Nomination_List nl 
		 inner join HELIXPRD.dbo.M_EMPLOYEE_EXCHANGE emp on nl.Employee_id=emp.S_EMPLOYEE_ID 
		 inner join Master_Supervisor_list b on nl.Employee_id=b.S_employee_id and b.S_SUPERVISOR_ID=@empid and nl.Session_id=b.Session_id
		 --Modified--
		 left join Feedback_supervisor c on c.Subordinate_id = b.S_employee_id and c.Session_id = b.Session_id 
	where nl.Session_id=@sessionid  
	      --Modified--
	      and C.Status is null
	
	Select A.*, ltrim(rtrim(B.s_employee_fname))+' '+ltrim(rtrim(B.s_employee_lname)) as Subordinate_Name
			,case when convert(varchar(10),Training_Required)='1' then 'Yes' when convert(varchar(10),Training_Required)='2' then 'No' else convert(varchar(10),Training_Required) end as [TrnReq]
			,C.Course,C.Location,CONVERT(VARCHAR(50),c.startdate,106) as StartDate ,CONVERT(VARCHAR(50),c.enddate,106) as EndDate
	from Feedback_supervisor A left outer join C_Session c on A.Session_id=C.sessionid
	left outer Join HELIXPRD.dbo.M_EMPLOYEE_EXCHANGE B on A.Subordinate_id=b.S_EMPLOYEE_ID
	where Supervisor_id=@empid and Session_id=@sessionid and isnull(PM_ID,'')=isnull(@pm_id,'')
END