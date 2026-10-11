SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET FEEDBACK FOR OVERALL PATTERN
-- =============================================
--EXEC usp_getFeedbackForOverallPattern 'SQL2003_4/10/2012To4/16/2012','00001'
CREATE procedure [dbo].[usp_getFeedbackForOverallPattern]  
(@sessionid varchar(50)  
,  
 @TrainerId varchar(50))  
as  
begin  
SET ANSI_NULLS ON  
begin  
  select max(first.PATTERN) as RATING,  
   (  
   case  
   when max(first.PATTERN)='A' then 'Ideal'  
    when max(first.PATTERN)='B'then 'Effective'  
     when max(first.PATTERN)='C' then 'Acceptable'  
      when max(first.PATTERN)='D' then 'Overhauling Required'  
       when max(first.PATTERN)='E' then 'Rejected'  
   end  
   ) as NOMENCLATURE  
  from  
  (select  Question_no,SUM(R1)as A ,SUM(R2) as B,SUM(R3) as C,SUM(R4) as D,  
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
  on first.Question_no=second.QUESTION_NO 
  where second.QUESTION_NO between 13 AND 18
end  
end  
  
----  
/*  
drop table feedback;  
drop table Nomination_List  
drop table C_Session  
drop table course  
drop table L1_Questions  
drop table M_Employee_Exchange  
*/

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


