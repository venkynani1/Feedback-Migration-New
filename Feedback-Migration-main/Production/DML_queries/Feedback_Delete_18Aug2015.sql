use TrainingFeedback

select * from Feedback where Employee_id='12811' and session_id='TTT_8/4/2015To8/6/2015'
select * from Feedback_Comments where Employee_id='12811' and sessionid='TTT_8/4/2015To8/6/2015'


delete from Feedback where Employee_id='12811' and session_id='TTT_8/4/2015To8/6/2015'
delete from Feedback_Comments where Employee_id='12811' and sessionid='TTT_8/4/2015To8/6/2015'