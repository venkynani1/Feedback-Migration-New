use TrainingFeedback

--2.
SELECT DISTINCT C.course [COURSE],
	   S.sessionid [SESSION],
	   S.startdate [START DATE],
	   S.enddate [END DTAE],
	   FD.Employee_id [EMPLOYEE ID],
	   ISNULL(LTRIM(RTRIM(EMP.S_EMPLOYEE_FNAME)),'')+' '+ISNULL(LTRIM(RTRIM(EMP.S_EMPLOYEE_LNAME)),'') AS [EMPLOYEE NAME],
	   FD.Question_no [QUE. NO.],
	   Q.QUESTION,
	   FD.Response AS [RESPONSE],
	   FD.TrainerId AS [TRAINER],
	   FDC.Comments AS [COMMENTS],
	   FD.FeedbackGivenDate AS [FEEDBACK GIVEN DATE]
FROM Feedback FD
	 INNER JOIN COURSE C ON FD.Course_id=C.courseid
	 INNER JOIN C_Session S ON FD.session_id=S.sessionid
	 INNER JOIN L1_QUESTIONS Q ON Q.QUESTION_NO=FD.Question_no
	 LEFT OUTER JOIN Feedback_Comments FDC ON FDC.sessionid=FD.session_id AND FDC.Employee_id=FD.Employee_id AND FDC.TrainerId=FD.TrainerId AND FDC.Comments IS NOT NULL AND LTRIM(RTRIM(FDC.Comments))!=''
	 LEFT OUTER JOIN HELIXPRD.DBO.M_EMPLOYEE_EXCHANGE EMP ON EMP.S_EMPLOYEE_ID=FD.Employee_id
WHERE S.startdate BETWEEN CONVERT(DATE,'2015-07-01 00:00:00.000') AND CONVERT(DATE,'2015-12-31 00:00:00.000')
ORDER BY S.sessionid,FD.Employee_id,[START DATE],FD.Question_no