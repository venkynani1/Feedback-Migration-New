USE [TrainingFeedback]
GO

/****** Object:  StoredProcedure [dbo].[usp_InsertSupFeedbacks]    Script Date: 8/22/2018 6:05:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_InsertSupFeedbacks]
@courseid varchar(100),
@sessionid varchar (200),
@SupervisorId varchar (20),
@SubOrdId varchar(20),
@TechSkills varchar(2),
@SoftSkills varchar(2),
@Performance varchar(2),
@OvrAllRating varchar(2),
@OverallFeed varchar(max),
@WorkArea varchar(max),
@status int
AS
BEGIN
		DECLARE @PM_ID VARCHAR(10)=NULL
		IF @status=1
		BEGIN
			SELECT TOP 1 @PM_ID=A.S_EMPLOYEE_ID FROM RESOURCE_MGMT.DBO.M_EMPLOYEE_ASSIGNMENT A 
			LEFT OUTER JOIN HELIXPRD.DBO.M_EMPLOYEE_EXCHANGE B ON A.S_EMPLOYEE_ID=B.S_EMPLOYEE_ID AND B.S_EMPLOYEE_STATUS_ID='A'
			LEFT OUTER JOIN HelixMaster.DBO.M_GRADE_WEIGHTAGE C ON B.S_GRADE_ID=C.S_GRADE
			WHERE S_ROLE_ID ='PM' 
			AND S_PROJECT_ID=(SELECT TOP 1 S_PROJECT_ID FROM RESOURCE_MGMT.DBO.M_EMPLOYEE_ASSIGNMENT 
							  WHERE S_EMPLOYEE_ID=@SubOrdId 
							  AND GETDATE() BETWEEN D_EFFECTIVE_START_DATE AND DATEADD(DAY,1,ISNULL(D_EFFECTIVE_END_DATE,GETDATE()))
							  ORDER BY N_INVOLVEMENT DESC)
			AND GETDATE() BETWEEN D_EFFECTIVE_START_DATE AND DATEADD(DAY,1,ISNULL(D_EFFECTIVE_END_DATE,GETDATE()))
			ORDER BY A.N_INVOLVEMENT DESC,C.N_WEIGHTAGE DESC
		END
		IF EXISTS (SELECT * FROM Feedback_supervisor where Session_id=@sessionid and Supervisor_id=@SupervisorId and Subordinate_id=@SubOrdId and Status=0)
		BEGIN
			IF(@status=0)
			BEGIN
				UPDATE Feedback_supervisor 
				SET TechSkills=TechSkills,
					SoftSkills=@SoftSkills,
					Performance=@Performance,
					OvrAllRating=@OvrAllRating,
					Overall_Feedback=@OverallFeed,
					WorkArea=@WorkArea,
					D_Feedback_Date=GETDATE()
				WHERE Session_id=@sessionid
					  and Supervisor_id=@SupervisorId
					  and Subordinate_id=@SubOrdId
			END
			ELSE
			BEGIN
				UPDATE Feedback_supervisor 
				SET TechSkills=@TechSkills,
					SoftSkills=@SoftSkills,
					Performance=@Performance,
					OvrAllRating=@OvrAllRating,
					Overall_Feedback=@OverallFeed,
					WorkArea=@WorkArea,
					Status=CASE WHEN @PM_ID IS NULL OR @SupervisorId=@PM_ID THEN 3 ELSE @status END,
					D_Feedback_Date=GETDATE(),
					PM_ID=CASE WHEN @PM_ID IS NULL OR @SupervisorId=@PM_ID THEN @SupervisorId ELSE @PM_ID END,
					PM_Remarks=CASE WHEN @PM_ID IS NULL OR @SupervisorId=@PM_ID THEN 'Auto Approved' ELSE NULL END
				WHERE Session_id=@sessionid
					  and Supervisor_id=@SupervisorId
					  and Subordinate_id=@SubOrdId
			END
		END
		ELSE IF NOT EXISTS (SELECT * FROM Feedback_supervisor where Session_id=@sessionid and Supervisor_id=@SupervisorId and Subordinate_id=@SubOrdId)
		BEGIN
			IF(@status=0)
			BEGIN
				insert into Feedback_supervisor (Course_id,Session_id,Supervisor_id,Subordinate_id,WorkArea,Performance,OvrAllRating,Overall_Feedback,[Status],D_Feedback_Date,PM_ID,PM_Remarks,TechSkills,SoftSkills)
				values( @courseid,@sessionid,@SupervisorId,@SubOrdId,@WorkArea,@Performance,@OvrAllRating,@OverallFeed,@status,GETDATE(),NULL,NULL,@TechSkills,@SoftSkills)
			END
			ELSE
			BEGIN
				IF @PM_ID IS NULL OR @PM_ID=@SupervisorId
				BEGIN
					insert into Feedback_supervisor (Course_id,Session_id,Supervisor_id,Subordinate_id,WorkArea,Performance,OvrAllRating,Overall_Feedback,[Status],D_Feedback_Date,PM_ID,PM_Remarks,TechSkills,SoftSkills)
					values( @courseid,@sessionid,@SupervisorId,@SubOrdId,@WorkArea,@Performance,@OvrAllRating,@OverallFeed,3,GETDATE(),@SupervisorId,'Auto Approved',@TechSkills,@SoftSkills)
				END
				ELSE
				BEGIN
					insert into Feedback_supervisor  (Course_id,Session_id,Supervisor_id,Subordinate_id,WorkArea,Performance,OvrAllRating,Overall_Feedback,[Status],D_Feedback_Date,PM_ID,PM_Remarks,TechSkills,SoftSkills)
					values( @courseid,@sessionid,@SupervisorId,@SubOrdId,@WorkArea,@Performance,@OvrAllRating,@OverallFeed,@status,GETDATE(),@PM_ID,NULL,@TechSkills,@SoftSkills)
				END
			END
		END
END
GO


