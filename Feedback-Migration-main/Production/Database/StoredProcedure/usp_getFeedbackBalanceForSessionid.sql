USE [TrainingFeedback]
GO
/****** Object:  StoredProcedure [dbo].[usp_getFeedbackBalanceForSessionid]    Script Date: 09/10/2014 16:28:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET FEEDBACK BALANCE FOR SESSION ID
-- =============================================
--EXEC usp_getFeedbackBalanceForSessionid 'SQL2003_4/10/2012To4/16/2012'

ALTER procedure [dbo].[usp_getFeedbackBalanceForSessionid]
(@sessionid varchar(50) )

as
begin
SET ANSI_NULLS ON
begin

if @sessionid ='Temp'
begin
  
  select Absents.course_id,Absents.Session_id,overall.Total ,Absents.Balance,((overall.Total-Absents.Balance)*100/ overall.Total) as Percentage from
  (select  course_id,count(distinct Employee_id)as Balance ,Session_id from [dbo].[Nomination_List]
  
  group by Session_id,course_id) Absents
  inner join
  (select  count(distinct Employee_id) as Total,Session_id from [dbo].[Nomination_List]
 
  group by Session_id,course_id ) overall
  on Absents.Session_id=overall.Session_id
  end
  else
  begin
  Select feedbback.sessionid,Feedbackdet.EMPLOYEES,Feedbackdet.Trainers,Feedbackdet.Tot_Feedbacks,feedbback.Comp_Feedback from 
(SELECT count(distinct Employee_id) as Comp_Feedback,sessionid     
  FROM [dbo].[Feedback_Comments]
  where sessionid=@sessionid
  group by sessionid) feedbback
  
  
 
  inner join 
   
   (Select Nomintbl.Session_id,Nomintbl.EMPLOYEES,Traintbl.Trainers,(Nomintbl.EMPLOYEES*Traintbl.Trainers) as Tot_Feedbacks from 
   (SELECT Session_id,COUNT(distinct Employee_id) AS EMPLOYEES FROM [dbo].[Nomination_List]
   WHERE Session_id=@sessionid
   group BY Session_id) Nomintbl
  
  inner join
  (SELECT [SessionID]
      ,Count(distinct [TrainerID]) as TRAINERS
  FROM [dbo].[Session_Trainer]
  where sessionid=@sessionid
  group by sessionid) Traintbl
  on Nomintbl.Session_id=Traintbl.SessionID) Feedbackdet
  on feedbback.sessionid=Feedbackdet.Session_id
  
  end
  
  
  
end
end
