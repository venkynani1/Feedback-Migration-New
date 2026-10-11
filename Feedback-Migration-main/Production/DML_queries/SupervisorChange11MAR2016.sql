use TrainingFeedback

select * from Feedback_supervisor where Supervisor_id='14724'

select * from Master_Supervisor_List where S_EMPLOYEE_ID in ('31665','31727','31666','31714','31748')

update Master_Supervisor_List set S_SUPERVISOR_ID='14724' where S_EMPLOYEE_ID in ('31665','31727','31666','31714','31748')

select * from Supervisor_List

insert into Supervisor_List values('FTP_001','FTP_001_5/1/2015To5/31/2015','14724','27381',GETDATE(),null,null,null,0)
