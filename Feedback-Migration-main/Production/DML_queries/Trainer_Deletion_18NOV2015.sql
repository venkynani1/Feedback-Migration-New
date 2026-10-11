use TrainingFeedback

select * from Session_Trainer where SessionID like '%Q 2_27%'

--begin tran
--delete from Session_Trainer where SessionID='Q 2_27-10-2015To29-10-2015'
--insert into Session_Trainer values ('Q 2_27-10-2015To29-10-2015','All Trainer(ALLTR)')
--commit

--begin tran
--select * from Feedback where session_id='Q 2_27-10-2015To29-10-2015' and employee_id=33155
--update Feedback set CommonFlag=1,Response=4 where session_id='Q 2_27-10-2015To29-10-2015' and employee_id=33155 and TrainerId='SHAILENDRA SRIVASTAVA(27483)'
--delete from Feedback where session_id='Q 2_27-10-2015To29-10-2015' and CommonFlag=0
--update Feedback set TrainerId='All Trainer(ALLTR)' where session_id='Q 2_27-10-2015To29-10-2015'
--select * from Feedback where session_id='Q 2_27-10-2015To29-10-2015'
--commit

--begin tran
--update  Feedback_Comments set CommonFlag=1 where Employee_id=33155 and sessionid='Q 2_27-10-2015To29-10-2015' and TrainerId='SHAILENDRA SRIVASTAVA(27483)'
--select * from Feedback_Comments where sessionid='Q 2_27-10-2015To29-10-2015' and CommonFlag=0
--delete from Feedback_Comments where sessionid='Q 2_27-10-2015To29-10-2015' and CommonFlag=0
--update Feedback_Comments set TrainerId='All Trainer(ALLTR)' where sessionid='Q 2_27-10-2015To29-10-2015'
--select * from Feedback_Comments where sessionid='Q 2_27-10-2015To29-10-2015' order by Employee_id
--commit


