update M_TRAVEL_MASTER
 set  B_RETURN_CHECK=0,B_MULTIDEST_CHECK=1
 where S_REQUEST_ID='D82355'


 update T_TRAVEL_DETAILS
 set B_RETURN_REQD='0',D_RETURN_DATE =NULL
 where S_REQUEST_ID='D82355' and S_TRAVEL_LEG=1




insert into T_TRAVEL_DETAILS values 
 ('D82355',2,'00','HIMANSHU  GUPTA','2019-02-19 00:00:00.000',null,null,null,0,null,'Chennai (Madras) (MAA)','IND','Mumbai (Bombay) - Chhatrapati Shivaji International (MUM)','IND',0,GETDATE(),'36135',null,null,null,null,null,null,null)
 
insert into T_TRAVEL_DETAILS values 
 ('D82355',3,'00','HIMANSHU  GUPTA','2019-02-20 00:00:00.000',null,null,null,0,null,'Mumbai (Bombay) - Chhatrapati Shivaji International (MUM)','IND','Delhi - Indira Gandhi International Airport (DEL)','IND',0,GETDATE(),'36135',null,null,null,null,null,null,null)



 INSERT INTO T_TR_HISTORY 
        SELECT A.S_REQUEST_ID
              ,S_TRAVEL_LEG
              ,S_DEPENDENT_ID
              ,S_SOURCE_COUNTRY
              ,S_SOURCE_CITY
              ,S_DEST_COUNTRY
              ,S_DEST_CITY
              ,D_TRAVEL_DATE
              ,S_TRAVEL_TIME
              ,D_RETURN_DATE
              ,S_RETURN_TIME
              ,S_TRAVEL_PURPOSE
              ,S_PROJECT_ID
              ,'NA'--'2984'
              ,NULL--S_EDIT_APPR_REMARKS
              ,NULL--D_EDIT_APPROVAL_DATE
              ,NULL--S_EDIT_APPROVED_BY
              ,S_UNIT
              ,S_DEPT
              ,B.D_UPDATED_DATE
              ,B.S_UPDATED_BY
              ,S_DURATION_TRAVEL
              ,S_VISA_TYPE
              ,S_VISA_NO
              ,B_RETURN_CHECK
              ,B_ONEWAY_CHECK
              ,B_MULTIDEST_CHECK
              ,S_TRAVEL_MODE
              ,S_MEALPREFERENCES
              ,S_APPROVAL_STATUS
              ,'NA'--'P'
              ,null,null,null,null,null,null,null,null
         FROM M_TRAVEL_MASTER A INNER JOIN T_TRAVEL_DETAILS B ON A.S_REQUEST_ID=B.S_REQUEST_ID
         WHERE A.S_REQUEST_ID='D82355'
         and b.S_TRAVEL_LEG in(1,2,3)
 
 
 Exec [USP_MAIL_TRIGGER] 'D82355','approval_change', '31318'