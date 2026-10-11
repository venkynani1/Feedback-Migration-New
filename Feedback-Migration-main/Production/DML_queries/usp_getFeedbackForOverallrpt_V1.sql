    
-- =============================================    
-- Author:      VINOTH.G    
-- Create date: 27 OCT 2017    
-- Description: GET FEEDBACK FOR OVERALL REPORT    
-- =============================================    
-- EXEC usp_getFeedbackForOverallrpt 'MO_9/10/2013To9/12/2013','ROMANUS ARUL STEPHEN VIJAY ANTONY MARIA DASS(24040)'    
CREATE procedure [dbo].[usp_getFeedbackForOverallrpt_V1]      
(@sessionid varchar(50),      
 @TrainerId varchar(100) )     
as      
begin      
SET ANSI_NULLS ON      
begin     
 select second.QUESTION_NO as S_No,second.QUESTION,first.A,first.B,first.C,first.D,first.PATTERN      
  from      
  (select  Question_no,SUM(R1)as A ,SUM(R2) as B,SUM(R3) as C,SUM(R4) as D,SUM(R5) as E,      
   (      
       CASE       
       WHEN SUM(R1)>=SUM(R2)and SUM(R1)>=SUM(R3)and SUM(R1)>=SUM(R4)THEN 'E'      
       WHEN SUM(R2)>=SUM(R1)and SUM(R2)>=SUM(R3)and SUM(R2)>=SUM(R4)THEN 'C'       
       WHEN SUM(R3)>=SUM(R1)and SUM(R3)>=SUM(R2)and SUM(R3)>=SUM(R4)THEN 'B'       
       WHEN SUM(R4)>=SUM(R1)and SUM(R4)>=SUM(R2)and SUM(R4)>=SUM(R3)THEN 'A'       
       END)  as PATTERN from [dbo].[Feedback]      
  where session_id=@sessionid and CommonFlag=1      
  group by Question_no) first       
  inner join      
  (select QUESTION,QUESTION_NO from [dbo].[L1_QUESTIONS]) second      
  on first.Question_no=second.QUESTION_NO where second.QUESTION_NO in('1','2','3','4','5','6','7','8','9','10','11','12','21','22')    
  UNION    
  select second.QUESTION_NO as S_No,second.QUESTION,first.A,first.B,first.C,first.D,first.PATTERN      
  from      
  (select  Question_no,SUM(R1)as A ,SUM(R2) as B,SUM(R3) as C,SUM(R4) as D,SUM(R5) as E,      
   (      
      CASE       
       WHEN SUM(R1)>=SUM(R2)and SUM(R1)>=SUM(R3)and SUM(R1)>=SUM(R4)THEN 'E'      
       WHEN SUM(R2)>=SUM(R1)and SUM(R2)>=SUM(R3)and SUM(R2)>=SUM(R4)THEN 'C'       
       WHEN SUM(R3)>=SUM(R1)and SUM(R3)>=SUM(R2)and SUM(R3)>=SUM(R4)THEN 'B'       
       WHEN SUM(R4)>=SUM(R1)and SUM(R4)>=SUM(R2)and SUM(R4)>=SUM(R3)THEN 'A'       
      END)  as PATTERN from [dbo].[Feedback]      
  where session_id=@sessionid and TrainerId=@TrainerId       
  group by Question_no) first       
  inner join      
  (select QUESTION,QUESTION_NO from [dbo].[L1_QUESTIONS]) second      
  on first.Question_no=second.QUESTION_NO where second.QUESTION_NO in('13','14','15','16','17','18','23')    
end      
end    
    
    
    
    