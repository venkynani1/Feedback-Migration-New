USE [TrainingFeedback]
GO
/****** Object:  StoredProcedure [dbo].[usp_getFeedbackBalanceEmpIdForSessionid]    Script Date: 07/30/2014 12:02:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:      SURESHKUMAR.D    
-- Create date: 18 April 2012    
-- Description: GET FEEDBACK BALANCE EMPID FOR SESSION ID    
-- =============================================    
--EXEC usp_getFeedbackBalanceEmpIdForSessionid 'Hex_MUM_379_2/12/2014To3/5/2014'    
ALTER procedure [dbo].[usp_getFeedbackBalanceEmpIdForSessionid]                  
(                
@sessionid varchar(50)                
)                  
as                  
begin                  
SET ANSI_NULLS ON                  
begin                  
     select Total.Employee_id as [Employee ID],LTRIM(RTRIM(Total.S_EMPLOYEE_FNAME))+' '+Total.S_EMPLOYEE_LNAME AS [Employee Name],        
  Total.DEPARTMENT as Department,Total.[Session ID] as [Session ID],Total.TotalFeedbacks AS [Total FeedBack],        
  (                
   case                
    when getcomments.comments  IS NULL  then  Total.TotalFeedbacks                
    when getcomments.comments  is not null then convert(int,Total.TotalFeedbacks)-convert(int,getcomments.comments )                
        
   end                
   ) as [Pending FeedBack]               
  from                 
  (select nominee.course_id,nominee.Session_id as [Session ID],nominee.Employee_id,nominee.S_EMPLOYEE_FNAME,        
   nominee.S_EMPLOYEE_LNAME,nominee.DEPARTMENT,Trainers.TotalFeedbacks from                 
  (SELECT NL.course_id,NL.Session_id,NL.Employee_id, HV.S_EMPLOYEE_FNAME,HV.S_EMPLOYEE_LNAME,HV.DEPARTMENT  FROM        
   [dbo].Nomination_List NL inner join        
   Hexv_feedb_VW HV on NL.Employee_id=HV.S_EMPLOYEE_ID        
                 
   where Session_id=@sessionid) nominee                
               
                   
  inner join                 
                   
  (SELECT SessionID as SessionID  ,count(distinct TrainerID) as TotalFeedbacks  FROM [dbo].[Session_Trainer]                
  where SessionID=@sessionid                
  group by SessionID) Trainers                
  on nominee.Session_id=Trainers.SessionID) Total                
                  
  left join                 
                  
  (SELECT count(distinct TrainerId) as comments,Employee_id as [Employee ID]
   FROM [dbo].[Feedback]                
   where session_id=@sessionid                
   group by Employee_id) getcomments                
   on Total.Employee_id=getcomments.[Employee ID]    
         
end                  
end
