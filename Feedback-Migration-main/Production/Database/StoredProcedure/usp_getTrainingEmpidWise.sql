USE [TrainingFeedback]
GO
/****** Object:  StoredProcedure [dbo].[usp_getTrainingEmpidWise]    Script Date: 10/28/2015 12:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET TRAINING EMPID WISE
-- =============================================
--EXEC usp_getTrainingEmpidWise '1358','false'
ALTER procedure [dbo].[usp_getTrainingEmpidWise]      
(@empid varchar(7)      
,@Isadmin varchar(20))      
as      
begin      
SET ANSI_NULLS ON      
begin      
if(@Isadmin='True')      
begin      
 select NormalQry.Course as Course,NormalQry.Courseid,NormalQry.sessionid,CONVERT(VARCHAR(50),NormalQry.startdate,106) as startdate,CONVERT(VARCHAR(50), NormalQry.enddate,106) as enddate , SplQuery.tot as Tot_Feedbacks,SplQuery.Balance as Bal_Feedbacks from       
(select Train.SessionID,Train.tot,      
(      
   case      
   when feed.bal IS NULL  then  Train.tot      
    when feed.bal is not null then convert(int,Train.tot )  -convert(int,feed.bal)      
           
   end      
   ) as Balance from       
(SELECT [SessionID],count(TrainerID) as tot      
            
  FROM .[dbo].[Session_Trainer]      
  where sessionid in(      
  select SessionID from Nomination_List where Employee_id=@empid)      
  group by SessionID) Train      
        
  left join      
        
  (select count(distinct TrainerId) as bal,session_id FROM .[dbo].[Feedback]      
  WHERE Employee_id=@empid      
  and session_id in(      
  select session_id from Nomination_List where Employee_id=@empid)      
  group BY  session_id) feed      
  on Train.SessionID=feed.session_id)  SplQuery      
  inner join      
        
  (select * from .[dbo].[C_Session]      
  where sessionid in(      
  select Session_id from Nomination_List where Employee_id=@empid)      
) NormalQry      
  on SplQuery.SessionID=NormalQry.sessionid  
  where SplQuery.Balance!=0 and NormalQry.Courseid!='FTP_001'
  order by  CONVERT(VARCHAR(50),NormalQry.startdate,106) asc 
 
 --New addition for training effectiveness/Supervisor feedback
  select NormalQry.Course as Course,
		NormalQry.Courseid,
		NormalQry.sessionid,
		CONVERT(VARCHAR(50),NormalQry.startdate,106) as startdate,
		CONVERT(VARCHAR(50), NormalQry.enddate,106) as enddate ,
		SplQuery.tot as Tot_Feedbacks,
		SplQuery.Balance as Bal_Feedbacks
from       
	(select Train.Session_id,
			Train.tot,      
			(case when feed.bal IS NULL  then  Train.tot      
				  when feed.bal is not null then convert(int,Train.tot )  -convert(int,feed.bal)     
			end) as Balance
	from       
		(SELECT [Session_ID],
				count(Employee_id) as tot      
        FROM .[dbo].Nomination_List a inner join HELIXPRD.dbo.M_EMPLOYEE_EXCHANGE b on a.Employee_id=b.S_EMPLOYEE_ID and S_SUPERVISOR_ID=@empid       
		where Session_id in(select Session_ID from Supervisor_List where Employee_id=@empid)      
		group by Session_id) Train      
        left join      
        (select count(distinct Subordinate_id) as bal,
				session_id
		FROM .[dbo].Feedback_supervisor      
		WHERE Supervisor_id=@empid      
			  and session_id in(select session_id from Supervisor_List where Employee_id=@empid)
			  and Status = 1      
		group BY  session_id) feed      
		on Train.Session_id=feed.session_id)  SplQuery      
	inner join      
	(select * 
	from .[dbo].[C_Session]      
	where sessionid in(select Session_id from Supervisor_List where Employee_id=@empid)      
	) NormalQry      
	on SplQuery.Session_id=NormalQry.sessionid  
where SplQuery.Balance!=0
order by  CONVERT(VARCHAR(50),NormalQry.startdate,106) asc
--end 

end      
else       
begin      
select NormalQry.Course as Course,NormalQry.Courseid,NormalQry.sessionid,CONVERT(VARCHAR(50),NormalQry.startdate,106) as startdate,CONVERT(VARCHAR(50), NormalQry.enddate,106) as enddate , SplQuery.tot as Tot_Feedbacks,SplQuery.Balance as Bal_Feedbacks from       
(select Train.SessionID,Train.tot,      
(      
   case      
   when feed.bal IS NULL  then  Train.tot      
    when feed.bal is not null then convert(int,Train.tot )  -convert(int,feed.bal)      
           
   end      
   ) as Balance from       
(SELECT [SessionID],count(TrainerID) as tot      
            
  FROM .[dbo].[Session_Trainer]      
  where sessionid in(      
  select SessionID from Nomination_List where Employee_id=@empid)      
  group by SessionID) Train      
        
  left join      
        
  (select count(distinct TrainerId) as bal,session_id FROM .[dbo].[Feedback]      
  WHERE Employee_id=@empid      
  and session_id in(      
  select session_id from Nomination_List where Employee_id=@empid)      
  group BY  session_id) feed      
  on Train.SessionID=feed.session_id)  SplQuery      
  inner join      
        
  (select * from .[dbo].[C_Session]      
  where sessionid in(      
  select Session_id from Nomination_List where Employee_id=@empid)      
) NormalQry      
  on SplQuery.SessionID=NormalQry.sessionid      
  where SplQuery.Balance!=0  and NormalQry.Courseid!='FTP_001' order by  CONVERT(VARCHAR(50),NormalQry.startdate,106) asc 
end      

 --New addition for training effectiveness/Supervisor feedback
  select NormalQry.Course as Course,
		NormalQry.Courseid,
		NormalQry.sessionid,
		CONVERT(VARCHAR(50),NormalQry.startdate,106) as startdate,
		CONVERT(VARCHAR(50), NormalQry.enddate,106) as enddate ,
		SplQuery.tot as Tot_Feedbacks,
		SplQuery.Balance as Bal_Feedbacks
from       
	(select Train.Session_id,
			Train.tot,      
			(case when feed.bal IS NULL  then  Train.tot      
				  when feed.bal is not null then convert(int,Train.tot )  -convert(int,feed.bal)     
			end) as Balance
	from       
		(SELECT [Session_ID],
				count(Employee_id) as tot      
        FROM .[dbo].Nomination_List a inner join HELIXPRD.dbo.M_EMPLOYEE_EXCHANGE b on a.Employee_id=b.S_EMPLOYEE_ID and S_SUPERVISOR_ID=@empid       
		where Session_id in(select Session_ID from Supervisor_List where Employee_id=@empid)      
		group by Session_id) Train      
        left join      
        (select count(distinct Subordinate_id) as bal,
				session_id
		FROM .[dbo].Feedback_supervisor      
		WHERE Supervisor_id=@empid      
			  and session_id in(select session_id from Supervisor_List where Employee_id=@empid)
			  and Status = 1      
		group BY  session_id) feed      
		on Train.Session_id=feed.session_id)  SplQuery      
	inner join      
	(select * 
	from .[dbo].[C_Session]      
	where sessionid in(select Session_id from Supervisor_List where Employee_id=@empid)      
	) NormalQry      
	on SplQuery.Session_id=NormalQry.sessionid
where SplQuery.Balance!=0 
order by  CONVERT(VARCHAR(50),NormalQry.startdate,106) asc
--end 
        
end      
end
