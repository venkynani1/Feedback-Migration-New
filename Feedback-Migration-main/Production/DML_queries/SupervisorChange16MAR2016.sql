Use TrainingFeedback

select * from Master_Supervisor_List where S_EMPLOYEE_ID in ('31613','31645','31664','31669','31682','31696','31700','31735','31744','31758','31766','31605','31608','31618','31626','31699','31706','31719','31723','31783','30530','31168','31263','31272','30258','30282','31157','31171')
select * from Feedback_supervisor where Subordinate_id in ('31613','31645','31664','31669','31682','31696','31700','31735','31744','31758','31766','31605','31608','31618','31626','31699','31706','31719','31723','31783','30530','31168','31263','31272','30258','30282','31157','31171')

select * from Supervisor_List where Employee_id='5140'

update Master_Supervisor_List set S_SUPERVISOR_ID='5140' where S_EMPLOYEE_ID in ('31613','31645','31664','31669','31682','31696','31700','31735','31744','31758','31766','31605','31608','31618','31626','31699','31706','31719','31723','31783','30530','31168','31263','31272','30258','30282','31157','31171')

insert into Supervisor_List values('FTP_001','FTP_001_11/1/2014To4/30/2015','5140','27381',GETDATE(),null,null,null,0) 
insert into Supervisor_List values('FTP_001','FTP_001_5/1/2015To5/31/2015','5140','27381',GETDATE(),null,null,null,0) 

----------------------------------------------------------------------------
select * from Feedback_supervisor where Subordinate_id in ('31668','31709','31982','31984','31995')

update Feedback_supervisor set PM_ID='2655' where Subordinate_id in ('31668','31709','31982','31984','31995')