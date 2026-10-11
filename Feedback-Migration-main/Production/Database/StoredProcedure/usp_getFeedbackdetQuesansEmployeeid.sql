USE [TrainingFeedback]
GO
/****** Object:  StoredProcedure [dbo].[usp_getFeedbackdetQuesansEmployeeid]    Script Date: 07/30/2014 18:36:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =================================================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET FEEDBACK DETAILS QUESTION AND ANSWER EMPLOYEE ID
-- =================================================================
--EXEC usp_getFeedbackdetQuesansEmployeeid 'EBW_7/21/2014To7/22/2014','All','ANANTH(0156)'
ALTER procedure [dbo].[usp_getFeedbackdetQuesansEmployeeid]    
(@sessionid varchar(100),    
 @empid varchar(50),    
@TrainerId varchar(100)    
 )    
    
as    
begin    
SET ANSI_NULLS ON    
begin    
IF @empid <> 'All'
BEGIN

SELECT
	Feedbcak.session_id,
	Feedbcak.Employee_id [Employee_id],
	Feedbcak.[EMPPLOYEE NAME],
	Question.QUESTION_NO as [Question No],
	Question.QUESTION,
	Feedbcak.Response
--	Feedbcak.Comments 
 FROM(
		SELECT (  
				LTRIM(RTRIM(E.S_EMPLOYEE_FNAME))
				+' '+LTRIM(RTRIM(ISNULL(S_EMPLOYEE_MNAME , '')))
				+' '+ LTRIM(RTRIM(S_EMPLOYEE_LNAME))
				) "EMPPLOYEE NAME"
				,Question_no,Response
				,session_id
				,F.Employee_id
				--,ISNULL(comments,'')as [Comments] 
				FROM [Feedback] F  
				--LEFT OUTER JOIN Feedback_Comments FC
				--	 ON F.session_id=FC.sessionid  AND F.Employee_id=FC.Employee_id 
				LEFT OUTER JOIN HELIXPRD.DBO.M_EMPLOYEE_EXCHANGE E ON F.Employee_id=E.S_EMPLOYEE_ID 
				where Session_id= @sessionid 
				and Employee_id=@empid 
				and CommonFlag=1
				--	and CommentsValid=1	
				--	and  fc.Comments is not null  
				--	and  fc.Comments != ''
					and F.CommonFlag=1
					and f.TrainerId=@TrainerId
				) Feedbcak      
		INNER JOIN(
					SELECT QUESTION ,QUESTION_NO  
					FROM [dbo].[L1_QUESTIONS]
				  ) Question    
			ON Feedbcak.Question_no=Question.QUESTION_NO 
	WHERE Question.QUESTION_NO between 1 AND 12      

UNION
  
SELECT 
	 Feedbcak.session_id,
	Feedbcak.Employee_id [Employee_id],
	Feedbcak.[EMPPLOYEE NAME],
	Question.QUESTION_NO as [Question No],
	Question.QUESTION,
	Feedbcak.Response
--	  ,Feedbcak.Comments 
FROM(
	 SELECT (  
			  LTRIM(RTRIM(S_EMPLOYEE_FNAME))
			  +' '+LTRIM(RTRIM(ISNULL(S_EMPLOYEE_MNAME , '')))
			  +' '+ LTRIM(RTRIM(S_EMPLOYEE_LNAME))
			) "EMPPLOYEE NAME" 
			, Question_no,Response
			,session_id
			,F.Employee_id 
--			,ISNULL(comments,'')as [Comments] 
	FROM [Feedback] F 
--	LEFT OUTER JOIN Feedback_Comments FC 
--		ON F.session_id=FC.sessionid and F.Employee_id=FC.Employee_id   
	LEFT OUTER JOIN HELIXPRD.DBO.M_EMPLOYEE_EXCHANGE E 
		ON F.Employee_id=E.S_EMPLOYEE_ID
	WHERE Session_id=@sessionid 
		  and F.TrainerId=@TrainerId 
		  and Employee_id=@empid
--		  and CommentsValid=1 
--		  and  fc.Comments is not null  
--		  and  fc.Comments != ''
		  and f.TrainerId=@TrainerId
	) Feedbcak     
	INNER JOIN (
				SELECT QUESTION,QUESTION_NO  
				FROM [dbo].[L1_QUESTIONS]) Question    
		ON Feedbcak.Question_no=Question.QUESTION_NO
 WHERE Question.QUESTION_NO between 13 AND 18 
 ORDER BY Employee_id,Question.QUESTION_NO


--    select Question.QUESTION_NO as [Question No],Question.QUESTION,Feedbcak.Response from    
--(SELECT Question_no,Response FROM [dbo].[Feedback]    
--  where Session_id=@sessionid 
--  and Employee_id=@empid and CommonFlag=1) Feedbcak      
--  inner join    
--  (SELECT QUESTION,QUESTION_NO  FROM [dbo].[L1_QUESTIONS]) Question    
--  on Feedbcak.Question_no=Question.QUESTION_NO where Question.QUESTION_NO between 1 AND 12      
--  UNION
--  select Question.QUESTION_NO as [Question No],Question.QUESTION,Feedbcak.Response from    
--(SELECT Question_no,Response FROM [dbo].[Feedback]    
--  where Session_id=@sessionid and TrainerId=@TrainerId    
--  and Employee_id=@empid) Feedbcak     
--  inner join    
--  (SELECT QUESTION,QUESTION_NO  FROM [dbo].[L1_QUESTIONS]) Question    
--  on Feedbcak.Question_no=Question.QUESTION_NO where Question.QUESTION_NO between 13 AND 18 
END
ELSE
BEGIN
SELECT
	Question.QUESTION_NO as [Question No],
	Question.QUESTION,
	Feedbcak.Response,
	Feedbcak.session_id,
	Feedbcak.Employee_id [Employee_id],
	Feedbcak.[EMPPLOYEE NAME]
--	Feedbcak.Comments 
 FROM(
		SELECT (  
				LTRIM(RTRIM(E.S_EMPLOYEE_FNAME))
				+' '+LTRIM(RTRIM(ISNULL(S_EMPLOYEE_MNAME , '')))
				+' '+ LTRIM(RTRIM(S_EMPLOYEE_LNAME))
				) "EMPPLOYEE NAME"
				,Question_no,Response
				,F.session_id
				,F.Employee_id
				--,ISNULL(comments,'')as [Comments] 
				FROM  [Feedback] F
				LEFT OUTER JOIN HELIXPRD.DBO.M_EMPLOYEE_EXCHANGE E ON F.Employee_id=E.S_EMPLOYEE_ID 
				where F.Session_id= @sessionid 
				--	and CommentsValid=1	
				--	and  fc.Comments is not null  
				--	and  fc.Comments != ''
					and F.CommonFlag=1
					and f.TrainerId=@TrainerId
				) Feedbcak      
		INNER JOIN(
					SELECT QUESTION ,QUESTION_NO  
					FROM [dbo].[L1_QUESTIONS]
				  ) Question    
			ON Feedbcak.Question_no=Question.QUESTION_NO 
	WHERE Question.QUESTION_NO between 1 AND 12      

UNION
  
SELECT 
	  Question.QUESTION_NO as [Question No]
	  ,Question.QUESTION
	  ,Feedbcak.Response
	  ,Feedbcak.session_id
	  ,Feedbcak.Employee_id [Employee_id]
	  ,Feedbcak.[EMPPLOYEE NAME]
--	  ,Feedbcak.Comments 
FROM(
	 SELECT (  
			  LTRIM(RTRIM(S_EMPLOYEE_FNAME))
			  +' '+LTRIM(RTRIM(ISNULL(S_EMPLOYEE_MNAME , '')))
			  +' '+ LTRIM(RTRIM(S_EMPLOYEE_LNAME))
			) "EMPPLOYEE NAME" 
			, Question_no,Response
			,F.session_id
			,F.Employee_id 
--			,ISNULL(comments,'')as [Comments] 
	FROM  [Feedback] F 	 
	LEFT OUTER JOIN HELIXPRD.DBO.M_EMPLOYEE_EXCHANGE E 
		ON F.Employee_id=E.S_EMPLOYEE_ID
	WHERE F.Session_id=@sessionid 
		  and F.TrainerId=@TrainerId 
--		  and CommentsValid=1 
--		  and  fc.Comments is not null  
--		  and  fc.Comments != ''
		  and f.TrainerId=@TrainerId
	) Feedbcak     
	INNER JOIN (
				SELECT QUESTION,QUESTION_NO  
				FROM [dbo].[L1_QUESTIONS]) Question    
		ON Feedbcak.Question_no=Question.QUESTION_NO
 WHERE Question.QUESTION_NO between 13 AND 18 
 ORDER BY Employee_id,Question.QUESTION_NO
END
end    
end
