    
-- =============================================    
-- Author:      VINOTH.G    
-- Create date: 25 OCT 2017    
-- Description: RATING FOR OVERALL REPORT    
-- =============================================    
-- EXEC usp_rATINGForOverallrpt 'MO_9/10/2013To9/12/2013','ROMANUS ARUL STEPHEN VIJAY ANTONY MARIA DASS(24040)'    
CREATE procedure [dbo].[usp_rATINGForOverallrpt_V1]        
(    
  @sessionid varchar(50)        
 ,@TrainerId varchar(100)    
     
 )        
as        
begin        
SET ANSI_NULLS ON        
begin      
      SELECT RATINGRES.SECTION as SNo,QUESTYPE.Type AS SECTION,(4.0) AS MAXS,SUBSTRING (CONVERT(varchar, RATINGRES.result),0,5) as SCORE FROM         
   (select second.SECTION,round( ((1.0*sum(first.A)) + (2.0*sum(first.B))+(3.0*sum(first.C))+(4.0*sum(first.D)) +(5.0*sum(first.E)))/(sum(first.A) + sum(first.B)+sum(first.C)+sum(first.D)+sum(first.E)),2) As result from        
   (select  Question_no,SUM(R1)as A ,SUM(R2) as B,SUM(R3) as C,SUM(R4) as D,SUM(R5) as E from [dbo].[Feedback]        
   where  session_id=@sessionid  and CommonFlag=1      
   group by Question_no) first        
   inner join        
   (select QUESTION_NO,SECTION from [dbo].[L1_QUESTIONS]) second        
   on first.Question_no=second.QUESTION_NO        
   group by second.SECTION ) RATINGRES        
   INNER JOIN        
   (SELECT Section,Type,TotMarks FROM [dbo].[SectionType]) QUESTYPE        
   ON RATINGRES.SECTION=QUESTYPE.Section where QUESTYPE.Section=1      
   UNION      
    SELECT RATINGRES.SECTION as SNo,QUESTYPE.Type AS SECTION,(4.0) AS MAXS,SUBSTRING (CONVERT(varchar, RATINGRES.result),0,5) as SCORE FROM         
   (select second.SECTION,round( ((1.0*sum(first.A)) + (2.0*sum(first.B))+(3.0*sum(first.C))+(4.0*sum(first.D)) +(5.0*sum(first.E)))/(sum(first.A) + sum(first.B)+sum(first.C)+sum(first.D)+sum(first.E)),2) As result from        
   (select  Question_no,SUM(R1)as A ,SUM(R2) as B,SUM(R3) as C,SUM(R4) as D,SUM(R5) as E from [dbo].[Feedback]        
   where  session_id=@sessionid  and CommonFlag=1      
   group by Question_no) first        
   inner join        
   (select QUESTION_NO,SECTION from [dbo].[L1_QUESTIONS]) second        
   on first.Question_no=second.QUESTION_NO        
   group by second.SECTION ) RATINGRES        
   INNER JOIN        
   (SELECT Section,Type,TotMarks FROM [dbo].[SectionType]) QUESTYPE        
   ON RATINGRES.SECTION=QUESTYPE.Section where QUESTYPE.Section=2      
    UNION      
      SELECT RATINGRES.SECTION as SNo,QUESTYPE.Type AS SECTION,(4.0) AS MAXS,SUBSTRING (CONVERT(varchar, RATINGRES.result),0,5) as SCORE FROM         
   ( select second.SECTION,round( ((1.0*sum(first.A)) + (2.0*sum(first.B))+(3.0*sum(first.C))+(4.0*sum(first.D)) +(5.0*sum(first.E)))/(sum(first.A) + sum(first.B)+sum(first.C)+sum(first.D)+sum(first.E)),2) As result from        
   (select  Question_no,SUM(R1)as A ,SUM(R2) as B,SUM(R3) as C,SUM(R4) as D,SUM(R5) as E from [dbo].[Feedback]        
   where  session_id=@sessionid      
   group by Question_no) first        
   inner join        
   (select QUESTION_NO,SECTION from [dbo].[L1_QUESTIONS]) second        
   on first.Question_no=second.QUESTION_NO        
   group by second.SECTION ) RATINGRES        
   INNER JOIN        
   (SELECT Section,Type,TotMarks FROM [dbo].[SectionType]) QUESTYPE        
   ON RATINGRES.SECTION=QUESTYPE.Section where QUESTYPE.Section=3      
       
end        
end 