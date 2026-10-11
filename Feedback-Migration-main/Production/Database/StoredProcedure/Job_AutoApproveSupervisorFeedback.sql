use TrainingFeedback
go

create procedure dbo.Job_AutoApproveSupervisorFeedback
as
Begin

--select * from Feedback_supervisor where Datediff(day,COnvert(date,D_Feedback_Date),CONVERT(date,getdate()))>=7 and Status=1

update Feedback_supervisor set Status=3, PM_Remarks='Auto Approved', D_Feedback_Date=GETDATE()
where Datediff(day,COnvert(date,D_Feedback_Date),CONVERT(date,getdate()))>=7 and Status=1 and D_Feedback_Date>= '2016-04-26 00:00:00.000'

End