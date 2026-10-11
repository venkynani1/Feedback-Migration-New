SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: TRAINING DELIVERY
-- =============================================
--EXEC usp_TRAININGDELIVERY 'SQL2003_4/10/2012To4/16/2012','00002,
CREATE procedure [dbo].[usp_TRAININGDELIVERY]  
(
	@sessionid varchar(50),  
	@TrainerId varchar(50)
)  
AS  
BEGIN
	  DECLARE @SESSIONIDCOUNT varchar(100)
	  DECLARE @TOTALSCORE INT
	  DECLARE @CURRENTSCORE INT
	  SELECT @SESSIONIDCOUNT=COUNT(sessionid)
	  FROM  dbo.Session_Trainer
	  WHERE SessionID=@sessionid 
	  Print @SESSIONIDCOUNT
	  DECLARE @MainintFlag INT  
	  SET @MainintFlag = 1  
      WHILE (@MainintFlag <=@SESSIONIDCOUNT)
			BEGIN
				   SELECT RATINGRES.SECTION as SNo,QUESTYPE.Type AS SECTION,(4.0) AS MAXS,
				   SUBSTRING (CONVERT(varchar, RATINGRES.result),0,5) as SCORE
				   FROM  (SELECT SECOND.SECTION,ROUND( ((1.0*SUM(FIRST.A)) + 
				  (2.0*SUM(FIRST.B))+(3.0*SUM(FIRST.C))+(4.0*SUM(FIRST.D)))/(SUM(FIRST.A)
				   + SUM(FIRST.B)+SUM(FIRST.C)+SUM(FIRST.D)),2) As result
				   FROM (SELECT  Question_no,SUM(R1)as A ,SUM(R2) as B,SUM(R3) as C,SUM(R4) as D 
				   FROM [dbo].[Feedback]  
				   WHERE session_id=@sessionid 
				   AND TrainerId=@TrainerId  
				   GROUP BY Question_no) FIRST  
				   INNER JOIN  
				  (SELECT QUESTION_NO,SECTION 
				   FROM [dbo].[L1_QUESTIONS]) SECOND  
				   ON FIRST.Question_no=second.QUESTION_NO  
				   GROUP BY SECOND.SECTION ) RATINGRES  
				   INNER JOIN  
				  (SELECT Section,Type,TotMarks FROM [dbo].[SectionType]) QUESTYPE  
				   ON RATINGRES.SECTION=QUESTYPE.Section WHERE QUESTYPE.Section=3
				   SET @TOTALSCORE=@TOTALSCORE+@CURRENTSCORE
				   print @TOTALSCORE
				   SET @MainintFlag = @MainintFlag + 1  
			 END
	 
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


