use TrainingFeedback

select distinct S_EMPLOYEE_ID,LTRIM(rtrim(s_employee_fname))+ ' '+LTRIM(rtrim(S_EMPLOYEE_LNAME)) as [Employee Name],S_EMPLOYEE_STATUS_ID
from helixprd.dbo.M_EMPLOYEE_EXCHANGE a
right outer join TrainingFeedback.dbo.Supervisor_List b
on a.S_EMPLOYEE_ID=b.Employee_id
where b.Employee_id not in (select distinct Supervisor_id from TrainingFeedback.dbo.Feedback_supervisor where Session_id=b.Session_id)
and Session_id='FTP_001_11/1/2014To4/30/2015'

select distinct S_EMPLOYEE_ID,LTRIM(rtrim(s_employee_fname))+ ' '+LTRIM(rtrim(S_EMPLOYEE_LNAME)) as [Employee Name],S_EMPLOYEE_STATUS_ID
from Feedback_supervisor b left outer join
helixprd.dbo.M_EMPLOYEE_EXCHANGE a on b.PM_ID=a.S_EMPLOYEE_ID 
where b.Status=1 and Session_id='FTP_001_11/1/2014To4/30/2015'


---TO CHECK
select distinct PM_ID from  Feedback_supervisor b left outer join
helixprd.dbo.M_EMPLOYEE_EXCHANGE a on b.PM_ID=a.S_EMPLOYEE_ID 
where b.Status=1 

select * from  Feedback_supervisor b left outer join
helixprd.dbo.M_EMPLOYEE_EXCHANGE a on b.PM_ID=a.S_EMPLOYEE_ID 
where b.Status=1 

select distinct Supervisor_id from TrainingFeedback.dbo.Feedback_supervisor where Session_id='FTP_001_11/1/2014To4/30/2015'
select * from Supervisor_List where Session_id='FTP_001_11/1/2014To4/30/2015' and Employee_id not in (
select distinct S_SUPERVISOR_ID from Master_Supervisor_List)

