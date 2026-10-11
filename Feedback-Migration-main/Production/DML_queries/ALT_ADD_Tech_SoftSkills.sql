
USE [TrainingFeedback]
GO

BEGIN TRANSACTION;  

Alter table Feedback_supervisor add TechSkills varchar (20)
Alter table Feedback_supervisor add SoftSkills varchar (20)


 COMMIT;