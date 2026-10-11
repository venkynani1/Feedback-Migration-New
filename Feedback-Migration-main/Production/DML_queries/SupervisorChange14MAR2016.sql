use TrainingFeedback

select * from Master_Supervisor_List where S_EMPLOYEE_ID in ('31613','31645','31664','31669','31682','31696','31700','31735','31744','31758','31766','31605','31608','31618','31626','31699','31706','31719','31723','31783','30530','31168','31263','31272','30258','30282','31157','31171')

select * from Feedback_supervisor where Subordinate_id in  ('31613','31645','31664','31669','31682','31696','31700','31735','31744','31758','31766','31605','31608','31618','31626','31699','31706','31719','31723','31783','30530','31168','31263','31272','30258','30282','31157','31171')

update Master_Supervisor_List set S_SUPERVISOR_ID='33423' where S_EMPLOYEE_ID in ('31613','31645','31664','31669','31682','31696','31700','31735','31744','31758','31766','31605','31608','31618','31626','31699','31706','31719','31723','31783','30530','31168','31263','31272','30258','30282','31157','31171')

select * from Master_Supervisor_List where S_EMPLOYEE_ID in ('31155','31195','31268','31270')
select * from Feedback_supervisor where Subordinate_id in ('31155','31195','31268','31270')

update Master_Supervisor_List set S_SUPERVISOR_ID='14417' where S_EMPLOYEE_ID in ('31155','31195','31268','31270')

select * from Supervisor_List

insert into Supervisor_List values('FTP_001','FTP_001_11/1/2014To4/30/2015','33423','27381',GETDATE(),null,null,null,0) 
insert into Supervisor_List values('FTP_001','FTP_001_5/1/2015To5/31/2015','33423','27381',GETDATE(),null,null,null,0) 
insert into Supervisor_List values('FTP_001','FTP_001_5/1/2015To5/31/2015','14417','27381',GETDATE(),null,null,null,0) 
update Supervisor_List set Session_id='FTP_001_11/1/2014To4/30/2015' where Employee_id='14417'

-------------------------------------------

select * from Master_Supervisor_List where S_EMPLOYEE_ID in ('29975','30466','30636','30663')
select * from Feedback_supervisor where Subordinate_id in ('29975','30466','30636','30663')

update Master_Supervisor_List set S_SUPERVISOR_ID='14761' where S_EMPLOYEE_ID in ('29975','30466','30636','30663')

insert into Supervisor_List values('FTP_001','FTP_001_11/1/2014To4/30/2015','14761','27381',GETDATE(),null,null,null,0) 

-------------------------------------------

select * from Master_Supervisor_List where S_EMPLOYEE_ID in ('31798','31620')
select * from Feedback_supervisor where Subordinate_id in ('31798','31620')

update Master_Supervisor_List set S_SUPERVISOR_ID='14417' where S_EMPLOYEE_ID in ('31798','31620')

insert into Supervisor_List values('FTP_001','FTP_001_5/1/2015To5/31/2015','14417','27381',GETDATE(),null,null,null,0) 
