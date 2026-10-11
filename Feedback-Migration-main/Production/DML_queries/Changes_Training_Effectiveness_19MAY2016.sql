USE TrainingFeedback

SELECT * FROM Master_Supervisor_List WHERE S_EMPLOYEE_ID='32636'
SELECT * FROM Master_Supervisor_List WHERE S_EMPLOYEE_ID in ('32476') order by S_SUPERVISOR_ID
SELECT * FROM Master_Supervisor_List WHERE S_EMPLOYEE_ID in ('32556')order by S_SUPERVISOR_ID
select * from Supervisor_List where Employee_id in ('20090','4920')

UPDATE Master_Supervisor_List SET S_SUPERVISOR_ID='20090' WHERE SESSION_ID='Che_FTP_001_7/1/2015To12/31/2015' AND S_EMPLOYEE_ID in ('32636')
UPDATE Master_Supervisor_List SET S_SUPERVISOR_ID='4920' WHERE SESSION_ID='Che_FTP_001_7/1/2015To12/31/2015' AND S_EMPLOYEE_ID in ('32476')

insert into Supervisor_List values('FTP_001','Che_FTP_001_7/1/2015To12/31/2015','4920','27381',GETDATE(),NULL,NULL,NULL,0)
