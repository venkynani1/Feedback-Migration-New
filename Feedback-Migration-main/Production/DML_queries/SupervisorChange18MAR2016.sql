use TrainingFeedback
select * from Master_Supervisor_List where S_EMPLOYEE_ID in ('30332','29966')
select * from Feedback_supervisor where Subordinate_id in ('30332','29966')

select * from Supervisor_List where Employee_id in ('3398','10245')

update Master_Supervisor_List set S_SUPERVISOR_ID='10245' where S_EMPLOYEE_ID ='30332'
update Master_Supervisor_List set S_SUPERVISOR_ID='3398' where S_EMPLOYEE_ID ='29966'



insert into Supervisor_List values('FTP_001','FTP_001_11/1/2014To4/30/2015','3398','27381',GETDATE(),null,null,null,0) 
insert into Supervisor_List values('FTP_001','FTP_001_11/1/2014To4/30/2015','10245','27381',GETDATE(),null,null,null,0) 