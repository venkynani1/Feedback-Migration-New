use TrainingFeedback

select * from course where course like 'Quality Process level 2%'

select * from C_Session where Courseid in ('Q 2','Q2') order by startdate desc

select * from Feedback where session_id='Che_Q2_15-03-2016To17-03-2016'
update Feedback set TrainerId='All Trainer(ALLTR)' where session_id='Che_Q2_15-03-2016To17-03-2016'

select * from Feedback_Comments where sessionid='Che_Q2_15-03-2016To17-03-2016'
update Feedback_Comments set TrainerId='All Trainer(ALLTR)' where sessionid='Che_Q2_15-03-2016To17-03-2016'

select * from Session_Trainer where SessionID='Che_Q2_15-03-2016To17-03-2016'
update Session_Trainer set TrainerID='All Trainer(ALLTR)'  where SessionID='Che_Q2_15-03-2016To17-03-2016'
