use TrainingFeedback

select a.SESSION_ID,
a.S_EMPLOYEE_ID,LTRIM(rtrim(b.s_employee_fname))+' '+LTRIM(rtrim(b.S_EMPLOYEE_LNAME)) as [EMPLOYEE NAME]
,case b.S_EMPLOYEE_STATUS_ID when 'A' then 'Active' else 'Inactive' end as [EMPLOYEE STATUS],
a.S_SUPERVISOR_ID,LTRIM(rtrim(c.s_employee_fname))+' '+LTRIM(rtrim(c.S_EMPLOYEE_LNAME)) as [SUPERVISOR NAME] 
,case C.S_EMPLOYEE_STATUS_ID when 'A' then 'Active' else 'Inactive' end as [SUPERVISOR STATUS]
from Master_Supervisor_List a 
left outer join HELIXPRD.dbo.M_EMPLOYEE_EXCHANGE b on a.S_EMPLOYEE_ID=b.S_EMPLOYEE_ID
left outer join HELIXPRD.dbo.M_EMPLOYEE_EXCHANGE c on a.S_SUPERVISOR_ID=c.S_EMPLOYEE_ID
where SESSION_ID='FTP_001_5/1/2015To5/31/2015'
ORDER BY S_SUPERVISOR_ID

