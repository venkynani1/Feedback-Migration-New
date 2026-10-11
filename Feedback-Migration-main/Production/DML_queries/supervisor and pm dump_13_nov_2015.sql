

select distinct S_EMPLOYEE_ID,LTRIM(rtrim(s_employee_fname))+ ' '+LTRIM(rtrim(S_EMPLOYEE_LNAME)) as [Employee Name],S_EMPLOYEE_STATUS_ID
from helixprd.dbo.M_EMPLOYEE_EXCHANGE a
right outer join TrainingFeedback.dbo.Supervisor_List b
on a.S_EMPLOYEE_ID=b.Employee_id
where a.S_EMPLOYEE_STATUS_ID='A'

select distinct S_EMPLOYEE_ID,LTRIM(rtrim(s_employee_fname))+ ' '+LTRIM(rtrim(S_EMPLOYEE_LNAME)) as [Employee Name],S_EMPLOYEE_STATUS_ID
from helixprd.dbo.M_EMPLOYEE_EXCHANGE a
right outer join TrainingFeedback.dbo.Feedback_supervisor b
on a.S_EMPLOYEE_ID=b.PM_ID
where b.Status in ('1','3')
and a.S_EMPLOYEE_STATUS_ID='A'