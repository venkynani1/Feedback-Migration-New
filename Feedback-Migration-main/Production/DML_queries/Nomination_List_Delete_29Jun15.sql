use TrainingFeedback

--select * from course where course like '%Workday HCM%'

--select * from C_Session where Courseid='125'

select * from Nomination_List where Session_id='125_6/1/2015To6/12/2015'


delete from Nomination_List where Session_id='125_6/1/2015To6/12/2015' and createdby='31822'
