use TrainingFeedback

select * from C_Session where SessionID='CM_11/12/2014To11/12/2014'

select * from Session_Trainer where SessionID='CM_11/12/2014To11/12/2014'

select * from Feedback where session_id='CM_11/12/2014To11/12/2014'

select * from Feedback_Comments where sessionid='CM_11/12/2014To11/12/2014'

select * from Nomination_List where Session_id='CM_11/12/2014To11/12/2014'

delete from Feedback_Comments where sessionid='CM_11/12/2014To11/12/2014'
delete from Feedback where session_id='CM_11/12/2014To11/12/2014'
delete from Nomination_List where Session_id='CM_11/12/2014To11/12/2014'
delete from Session_Trainer where SessionID='CM_11/12/2014To11/12/2014'
delete from C_Session where sessionid='CM_11/12/2014To11/12/2014'