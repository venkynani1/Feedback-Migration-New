use TrainingFeedback

select * from C_Session where course like '%Leap%'

select * from Nomination_List where Session_id='LRC_7/30/2015To8/1/2015'

delete from Nomination_List where Session_id='LRC_7/30/2015To8/1/2015' and Employee_id between 1 and 20
