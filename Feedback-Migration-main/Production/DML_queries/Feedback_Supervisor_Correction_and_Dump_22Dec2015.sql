use TrainingFeedback

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
       ,case when [Training_Required]=1 then 'YES' else 'NO' end as [Is retraining required?]
       ,[Overall_Feedback] as [Overall Comments]
       ,Case when [Status]=0 then 'Saved' when Status=1 then 'Submitted for Review' else 'Approved' end as [Status]
       ,[D_Feedback_Date] as [Feedback Date]
       ,[PM_ID]
       ,e.S_EMPLOYEE_FNAME+' '+e.S_EMPLOYEE_LNAME as [PM Name]
       ,[PM_Remarks] as [PM Remarks]
	   from Master_Supervisor_List f 
left outer join Feedback_supervisor a on f.S_EMPLOYEE_ID=a.Subordinate_id
left outer join course b on a.Course_id=b.courseid
left outer join Hexv_feedb_VW c on f.S_SUPERVISOR_ID=c.S_EMPLOYEE_ID
left outer join Hexv_feedb_VW d on f.S_EMPLOYEE_ID=d.S_EMPLOYEE_ID
left outer join Hexv_feedb_VW e on a.PM_ID=e.S_EMPLOYEE_ID

select * from Feedback_supervisor

select COUNT(Subordinate_id),Subordinate_id from Feedback_supervisor group by Subordinate_id having COUNT(Subordinate_id)>1
 
 --delete from Feedback_supervisor where Supervisor_id='1800' and Subordinate_id in 
 --(select Subordinate_id from Feedback_supervisor group by Subordinate_id having COUNT(Subordinate_id)>1)