select distinct S_EMPLOYEE_ID,LTRIM(rtrim(s_employee_fname))+ ' '+LTRIM(rtrim(S_EMPLOYEE_LNAME)) as [Employee Name],S_EMPLOYEE_STATUS_ID
from helixprd.dbo.M_EMPLOYEE_EXCHANGE a
right outer join TrainingFeedback.dbo.Supervisor_List b
on a.S_EMPLOYEE_ID=b.Employee_id
where a.S_EMPLOYEE_STATUS_ID='A'
and b.Employee_id not in (select distinct Supervisor_id from TrainingFeedback.dbo.Feedback_supervisor where Session_id=b.Session_id)

