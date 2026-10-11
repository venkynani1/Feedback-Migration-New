use TrainingFeedback

select * from C_Session where course like '%ITIL 2011 FOUNDATION TRAINING & CERTIFICATION%'

select * from Nomination_List where Session_id='ITIL_2011_Foundation_Cerification_2/11/2016To2/13/' and createdby='31400' order by createdby

select * from Session_Trainer where SessionID='ITIL_2011_Foundation_Cerification_11-02-2016To13-0'

select * from Feedback where  Session_id='ITIL_2011_Foundation_Cerification_2/11/2016To2/13/' 
and Employee_id in (select Employee_id from Nomination_List where Session_id='ITIL_2011_Foundation_Cerification_11-02-2016To13-0' 
and createdby='31400')

select * from Feedback_Comments where  Sessionid='ITIL_2011_Foundation_Cerification_2/11/2016To2/13/' 
and Employee_id in (select Employee_id from Nomination_List where Session_id='ITIL_2011_Foundation_Cerification_11-02-2016To13-0' 
and createdby='31400')

UPDATE Nomination_List SET Session_id='ITIL_2011_Foundation_Cerification_11-02-2016To13-0' 
WHERE  Session_id='ITIL_2011_Foundation_Cerification_2/11/2016To2/13/' and createdby='31400' 

update Feedback set session_id='ITIL_2011_Foundation_Cerification_11-02-2016To13-0',TrainerId='Aneej Mitthai(0826)' 
where  Session_id='ITIL_2011_Foundation_Cerification_2/11/2016To2/13/' and Employee_id in (select Employee_id from Nomination_List 
where Session_id='ITIL_2011_Foundation_Cerification_11-02-2016To13-0' and createdby='31400')

update Feedback_Comments set sessionid='ITIL_2011_Foundation_Cerification_11-02-2016To13-0',TrainerId='Aneej Mitthai(0826)' 
where  Sessionid='ITIL_2011_Foundation_Cerification_2/11/2016To2/13/' and Employee_id in (select Employee_id from Nomination_List 
where Session_id='ITIL_2011_Foundation_Cerification_11-02-2016To13-0' and createdby='31400')