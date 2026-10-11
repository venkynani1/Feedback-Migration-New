use TrainingFeedback

select * from Feedback_supervisor where Supervisor_id='2984'

select * from Master_Supervisor_List where S_EMPLOYEE_ID in ('31978','31994','31987','31974','32003')

update Master_Supervisor_List set S_SUPERVISOR_ID=case when S_EMPLOYEE_ID='31978' then '25365'
													   when S_EMPLOYEE_ID='31994' then '28169'
													   when S_EMPLOYEE_ID='31987' then '25365'
													   when S_EMPLOYEE_ID='31974' then '31940'
													   when S_EMPLOYEE_ID='32003' then '30818' end
where S_EMPLOYEE_ID in ('31978','31994','31987','31974','32003')

select * from Supervisor_List

insert into Supervisor_List values('FTP_001','FTP_001_5/1/2015To5/31/2015','25365','27381',GETDATE(),null,null,null,0)
insert into Supervisor_List values('FTP_001','FTP_001_5/1/2015To5/31/2015','28169','27381',GETDATE(),null,null,null,0)
insert into Supervisor_List values('FTP_001','FTP_001_5/1/2015To5/31/2015','31940','27381',GETDATE(),null,null,null,0)
insert into Supervisor_List values('FTP_001','FTP_001_5/1/2015To5/31/2015','30818','27381',GETDATE(),null,null,null,0)

