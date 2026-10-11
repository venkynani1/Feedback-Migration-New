use trainingfeedback

select * from C_Session where sessionid like 'che_ajs%'

--update C_Session set IsDeleted=1 where sessionid like 'Che_AJS_28-03-2016To29-03-2016'

select * from Nomination_List where Session_id='Che_AJS_28-03-2016To29-03-2016'

--update Nomination_List set Session_id='Che_AJS_28-03-2016To30-03-2016' where Session_id='Che_AJS_28-03-2016To29-03-2016'

select * from Feedback  where Session_id='Che_AJS_28-03-2016To29-03-2016'
--update Feedback set Session_id='Che_AJS_28-03-2016To30-03-2016' where Session_id='Che_AJS_28-03-2016To29-03-2016'

select * from Session_Trainer where SessionID='Che_AJS_28-03-2016To29-03-2016'
--update Session_Trainer set SessionID='Che_AJS_28-03-2016To30-03-2016' where SessionID='Che_AJS_28-03-2016To29-03-2016'

select * from Feedback_Comments where Sessionid='Che_AJS_28-03-2016To29-03-2016'
--update Feedback_Comments set SessionID='Che_AJS_28-03-2016To30-03-2016' where SessionID='Che_AJS_28-03-2016To29-03-2016'

delete from C_Session where sessionid like 'Che_AJS_28-03-2016To29-03-2016'