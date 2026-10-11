use TrainingFeedback

--select * from C_Session where course='Effective Business Communication'


--select * from Nomination_List where session_id='023_8/7/2015To8/7/2015' and Employee_id in ('32689','32690','32691','32692','32693','32694','32695',
--'32696','32697','32698','32699','32700','32701','32702','32703','32704','32705','32706','32708','32725')

--select * from Feedback where session_id='023_8/7/2015To8/7/2015' and Employee_id in ('32689','32690','32691','32692','32693','32694','32695',
--'32696','32697','32698','32699','32700','32701','32702','32703','32704','32705','32706','32708','32725')

--select * from Feedback_Comments where sessionid='023_8/7/2015To8/7/2015' and Employee_id in ('32689','32690','32691','32692','32693','32694','32695',
--'32696','32697','32698','32699','32700','32701','32702','32703','32704','32705','32706','32708','32725')


--delete from Nomination_List where session_id='023_8/7/2015To8/7/2015' and Employee_id in ('32689','32690','32691','32692','32693','32694','32695',
--'32696','32697','32698','32699','32700','32701','32702','32703','32704','32705','32706','32708','32725')

--delete from Feedback where session_id='023_8/7/2015To8/7/2015' and Employee_id in ('32689','32690','32691','32692','32693','32694','32695',
--'32696','32697','32698','32699','32700','32701','32702','32703','32704','32705','32706','32708','32725')

--delete from Feedback_Comments where sessionid='023_8/7/2015To8/7/2015' and Employee_id in ('32689','32690','32691','32692','32693','32694','32695',
--'32696','32697','32698','32699','32700','32701','32702','32703','32704','32705','32706','32708','32725')


select * from Feedback where session_id='023_8/7/2015To8/7/2015' and TrainerId='Shifa(0abc8)'

select * from Feedback_Comments where sessionid='023_8/7/2015To8/7/2015' and TrainerId='Shifa(0abc8)'

select * from Session_Trainer where sessionid='023_8/7/2015To8/7/2015' and TrainerId='Shifa(0abc8)'


delete from Feedback where session_id='023_8/7/2015To8/7/2015' and TrainerId='Shifa(0abc8)'

delete from Feedback_Comments where sessionid='023_8/7/2015To8/7/2015' and TrainerId='Shifa(0abc8)'

delete from Session_Trainer where sessionid='023_8/7/2015To8/7/2015' and TrainerId='Shifa(0abc8)'