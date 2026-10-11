use TrainingFeedback

select * from C_Session where course like '%ITIL Foundation awareness Program%'

select * from Nomination_List where Session_id='Che_02122_05-07-2016To08-07-2016' and Employee_id in ('28212','33953')

select * from Feedback where session_id='Che_02122_05-07-2016To08-07-2016' and Employee_id in ('28212','33953')
select * from Feedback_Comments where sessionid='Che_02122_05-07-2016To08-07-2016' and Employee_id in ('28212','33953')

delete from Nomination_List where Session_id='Che_02122_05-07-2016To08-07-2016' and Employee_id in ('28212','33953')