

BEGIN TRAN

DELETE FROM Feedback WHERE session_id ='PPL_7/10/2014To7/10/2014' AND Employee_id in ('17320','17997'
,'19269', '19270', '19273', '19748', '21001', '21357', '21833', '24601') 

DELETE FROM Feedback_Comments WHERE sessionid ='PPL_7/10/2014To7/10/2014' AND Employee_id in ('17320','17997'
,'19269', '19270', '19273', '19748', '21001', '21357', '21833', '24601')

DELETE FROM Nomination_List WHERE session_id ='PPL_7/10/2014To7/10/2014' AND Employee_id in ('17320','17997'
,'19269', '19270', '19273', '19748', '21001', '21357', '21833', '24601')

COMMIT

--ROLLBACK






