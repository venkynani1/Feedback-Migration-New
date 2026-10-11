use TrainingFeedback
Delete FROM Feedback WHERE session_id ='PPL_8/20/2014To8/21/2014' AND Employee_id in ('27768','28086'
,'28257', '28575') 

Delete FROM Feedback_Comments WHERE sessionid ='PPL_8/20/2014To8/21/2014' AND Employee_id in('27768','28086'
,'28257', '28575')

Delete FROM Nomination_List WHERE session_id ='PPL_8/20/2014To8/21/2014' AND Employee_id in ('27768','28086'
,'28257', '28575')

--select * from dbo.course where course like '%TCDP : Programming Principles Level%'

--select * from C_Session where Courseid='PPL' and startdate ='2014-08-20' and enddate = '2014-08-21'

select * from Session_Trainer where SessionID='PPL_8/20/2014To8/21/2014'

delete from Session_Trainer where SessionID='PPL_8/20/2014To8/21/2014'

insert into Session_Trainer values('PPL_8/20/2014To8/21/2014','Chandrashekhar(100007)')

