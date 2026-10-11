SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET FEEDBACK FOR AREAS CONCERN
-- =============================================
--EXEC usp_getFeedBackCount 'SQL2003_4/10/2012To4/16/2012','00001'
CREATE procedure [dbo].[usp_getFeedbackForAreasconcern]  
(@sessionid varchar(50),  
 @TrainerId varchar(50))  
as  
begin  
SET ANSI_NULLS ON  
begin  
 select second.QUESTION_NO as S_No,second.QUESTION,first.PATTERN  
  from  
  (select  Question_no,SUM(R1)as A ,SUM(R2) as B,SUM(R3) as C,SUM(R4) as D,  
   (  
      CASE   
       WHEN SUM(R1)>=SUM(R2)and SUM(R1)>=SUM(R3)and SUM(R1)>=SUM(R4)THEN 'E'  
       WHEN SUM(R2)>=SUM(R1)and SUM(R2)>=SUM(R3)and SUM(R2)>=SUM(R4)THEN 'C'   
         WHEN SUM(R3)>=SUM(R1)and SUM(R3)>=SUM(R2)and SUM(R3)>=SUM(R4)THEN 'B'   
          
      WHEN SUM(R4)>=SUM(R1)and SUM(R4)>=SUM(R2)and SUM(R4)>=SUM(R3)THEN 'A'   
             
      END)  as PATTERN from [dbo].[Feedback]  
  where session_id=@sessionid  and commonflag=1
  group by Question_no) first  
  inner join  
  (select QUESTION,QUESTION_NO from [dbo].[L1_QUESTIONS]) second  
  on first.Question_no=second.QUESTION_NO  
  where first.PATTERN in('C','E') and second.QUESTION_NO between 1 AND 12
  UNION
  select second.QUESTION_NO as S_No,second.QUESTION,first.PATTERN  
  from  
  (select  Question_no,SUM(R1)as A ,SUM(R2) as B,SUM(R3) as C,SUM(R4) as D,  
   (  
      CASE   
       WHEN SUM(R1)>=SUM(R2)and SUM(R1)>=SUM(R3)and SUM(R1)>=SUM(R4)THEN 'E'  
       WHEN SUM(R2)>=SUM(R1)and SUM(R2)>=SUM(R3)and SUM(R2)>=SUM(R4)THEN 'C'   
         WHEN SUM(R3)>=SUM(R1)and SUM(R3)>=SUM(R2)and SUM(R3)>=SUM(R4)THEN 'B'   
          
      WHEN SUM(R4)>=SUM(R1)and SUM(R4)>=SUM(R2)and SUM(R4)>=SUM(R3)THEN 'A'   
             
      END)  as PATTERN from [dbo].[Feedback]  
  where session_id=@sessionid  and TrainerId=@TrainerId
  group by Question_no) first  
  inner join  
  (select QUESTION,QUESTION_NO from [dbo].[L1_QUESTIONS]) second  
  on first.Question_no=second.QUESTION_NO  
  where first.PATTERN in('C','E') and second.QUESTION_NO between 13 AND 18
    
end  
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


