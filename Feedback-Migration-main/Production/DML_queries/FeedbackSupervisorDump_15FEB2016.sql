use TrainingFeedback

DECLARE @session_ID varchar(500)='FTP_001_11/1/2014To4/30/2015'--'FTP_001_5/1/2015To5/31/2015'

SELECT * INTO #TEMP FROM (
SELECT A.S_EMPLOYEE_ID,A.S_PROJECT_ID FROM RESOURCE_MGMT.DBO.M_EMPLOYEE_ASSIGNMENT A INNER JOIN Master_Supervisor_List B ON A.S_EMPLOYEE_ID=B.S_EMPLOYEE_ID
WHERE N_INVOLVEMENT=(SELECT MAX(N_INVOLVEMENT) FROM RESOURCE_MGMT.DBO.M_EMPLOYEE_ASSIGNMENT WHERE S_EMPLOYEE_ID=A.S_EMPLOYEE_ID 
 AND GETDATE() BETWEEN D_EFFECTIVE_START_DATE AND DATEADD(DAY,1,ISNULL(D_EFFECTIVE_END_DATE,GETDATE())))
 AND GETDATE() BETWEEN D_EFFECTIVE_START_DATE AND DATEADD(DAY,1,ISNULL(D_EFFECTIVE_END_DATE,GETDATE()))
 AND B.Session_id=@session_ID)T
 
 
select * into #TEMP1 from(
select f.S_SUPERVISOR_ID
	   ,c.S_EMPLOYEE_FNAME+' '+c.S_EMPLOYEE_LNAME as [Supervisor Name]
	   ,f.S_EMPLOYEE_ID [Subordinate_id]
	   ,d.S_EMPLOYEE_FNAME+' '+d.S_EMPLOYEE_LNAME as [Subordinate Name]
	   ,b.course
	   ,a.Session_id
	   ,[WorkArea] as [Work Area]
       ,[PrbSA] as [Problem Solving Ability]
       ,[PrgSkills] as [Programming skill / ability or Skill in Automation Tools]
       ,[CommSkills] as [Communication / Writing Skill Ability]
       ,[BehSkills] as [Behavioural Skill]
       ,[Performance]
       ,[OvrAllRating] as [Overall Feedback Rating]
       ,case when [Training_Required]=1 then 'YES' when [Training_Required]=2 then 'NO' else NULL end as [Is retraining required?]
       ,[Overall_Feedback] as [Overall Comments]
       ,Case when [Status]=0 then 'Saved' when Status=1 then 'Submitted for Review'  when Status=3 then 'Approved' else convert(varchar(50),[Status]) end as [Status]
       ,[D_Feedback_Date] as [Feedback Date]
       ,ISNULL(a.[PM_ID],(SELECT TOP 1 X.S_EMPLOYEE_ID FROM RESOURCE_MGMT.DBO.M_EMPLOYEE_ASSIGNMENT X 
			INNER JOIN #TEMP T ON T.S_PROJECT_ID=X.S_PROJECT_ID AND T.S_EMPLOYEE_ID=F.S_EMPLOYEE_ID
			LEFT OUTER JOIN HELIXPRD.DBO.M_EMPLOYEE_EXCHANGE Y ON X.S_EMPLOYEE_ID=Y.S_EMPLOYEE_ID --AND Y.S_EMPLOYEE_STATUS_ID='A'
			LEFT OUTER JOIN HelixMaster.DBO.M_GRADE_WEIGHTAGE Z ON Y.S_GRADE_ID=Z.S_GRADE
			WHERE S_ROLE_ID ='PM' 
			AND GETDATE() BETWEEN D_EFFECTIVE_START_DATE AND DATEADD(DAY,1,ISNULL(D_EFFECTIVE_END_DATE,GETDATE()))
			ORDER BY X.N_INVOLVEMENT DESC,Z.N_WEIGHTAGE DESC)) AS [PM_ID]
       ,CONVERT(VARCHAR(MAX),'') as [PM Name]
       ,[PM_Remarks] as [PM Remarks]
	   from Master_Supervisor_List f 
left outer join Feedback_supervisor a on f.S_EMPLOYEE_ID=a.Subordinate_id and a.Session_id=f.SESSION_ID 
left outer join course b on a.Course_id=b.courseid
left outer join Hexv_feedb_VW c on f.S_SUPERVISOR_ID=c.S_EMPLOYEE_ID
left outer join Hexv_feedb_VW d on f.S_EMPLOYEE_ID=d.S_EMPLOYEE_ID
where f.Session_id=@session_ID)tt 

update a set [PM Name] = e.S_EMPLOYEE_FNAME+' '+e.S_EMPLOYEE_LNAME from #temp1 a left outer join Hexv_feedb_VW e on a.PM_ID=e.S_EMPLOYEE_ID

select * from #TEMP1 order by course desc

DROP TABLE #TEMP
DROP TABLE #TEMP1