use TrainingFeedback

select b.course
	   ,a.Session_id
	   ,a.Supervisor_id
	   ,c.S_EMPLOYEE_FNAME+' '+c.S_EMPLOYEE_LNAME as [Supervisor Name]
	   ,a.Subordinate_id
	   ,d.S_EMPLOYEE_FNAME+' '+d.S_EMPLOYEE_LNAME as [Subordinate Name]
	   ,[WorkArea] as [Work Area]
       ,[PrbSA] as [Problem Solving Ability]
       ,[PrgSkills] as [Programming skill / ability or Skill in Automation Tools]
       ,[CommSkills] as [Communication / Writing Skill Ability]
       ,[BehSkills] as [Behavioural Skill]
       ,[Performance]
       ,[OvrAllRating] as [Overall Feedback Rating]
       ,case when [Training_Required]=1 then 'YES' else 'NO' end as [Is retraining required?]
       ,[Overall_Feedback] as [Overall Comments]
       ,Case when [Status]=0 then 'Saved' when Status=1 then 'Submitted for Review' else 'Approved' end as [Status]
       ,[D_Feedback_Date] as [Feedback Date]
       ,[PM_ID]
       ,e.S_EMPLOYEE_FNAME+' '+e.S_EMPLOYEE_LNAME as [PM Name]
       ,[PM_Remarks] as [PM Remarks]
	   from Feedback_supervisor a
inner join course b on a.Course_id=b.courseid
INNER JOIN Hexv_feedb_VW c on a.Supervisor_id=c.S_EMPLOYEE_ID
INNER JOIN Hexv_feedb_VW d on a.Subordinate_id=d.S_EMPLOYEE_ID
INNER JOIN Hexv_feedb_VW e on a.PM_ID=e.S_EMPLOYEE_ID