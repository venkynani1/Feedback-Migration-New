
USE [TrainingFeedback]
GO

BEGIN TRANSACTION; 

Delete from [dbo].[Feedback_supervisor] where Supervisor_id ='12811'


 COMMIT; 