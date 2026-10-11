Update T_TRAVEL_DETAILS set D_RETURN_DATE ='2019-04-06' ,B_RETURN_REQD ='1',S_UPDATED_BY ='36135',D_UPDATED_DATE =GETDate() where S_REQUEST_ID ='M82933'

Update M_TRAVEL_MASTER set S_VISA_TYPE ='B1' ,B_ONEWAY_CHECK =0 ,B_MULTIDEST_CHECK =1 ,S_UPDATED_BY ='36135',D_UPDATED_DATE =GETDate() 
 where S_REQUEST_ID ='M82933'




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
         WHERE A.S_REQUEST_ID='M82933'
         and b.S_TRAVEL_LEG in(1)
 