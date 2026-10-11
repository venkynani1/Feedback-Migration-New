/*                      
============================================================================                      
Description : Function to GetEmployee Details             
References :                      
                      
Ver  Date   Author   Description                      
---------------------------------------------------------------------------                      
1.0  17-July-2012 Karmegaraja M                       
============================================================================            
*/          
-- SELECT * FROM dbo.[UF_GetEmployeeExchangeDetails]('28518')          
CREATE FUNCTION [dbo].[UF_GetEmployeeExchangeDetails]      
(      
 @EmpID VARCHAR(25) = ''      
)                 
RETURNS @temptable TABLE         
(      
 S_EMPLOYEE_ID  VARCHAR(25),      
 S_EMPLOYEE_FNAME VARCHAR(100),      
 S_EMPLOYEE_LNAME VARCHAR(100),      
 S_EMPLOYEE_EMAIL VARCHAR(100),      
 S_EMPLOYEE_FROM  VARCHAR(10),      
 S_EMPLOYEE_STATUS_ID CHAR(1),      
 S_LOCATION_ID  VARCHAR(25)      
)        
AS                 
BEGIN                 
          
    INSERT INTO @temptable       
 (S_EMPLOYEE_ID,S_EMPLOYEE_FNAME,S_EMPLOYEE_LNAME,S_EMPLOYEE_EMAIL,S_EMPLOYEE_FROM,S_EMPLOYEE_STATUS_ID,S_LOCATION_ID)      
    SELECT S_EMPLOYEE_ID,S_EMPLOYEE_FNAME,S_EMPLOYEE_LNAME,S_EMPLOYEE_EMAIL,S_EMPLOYEE_FROM,S_EMPLOYEE_STATUS_ID,S_LOCATION_ID      
    FROM      
  (      
   SELECT S_EMPLOYEE_ID,S_EMPLOYEE_FNAME,S_EMPLOYEE_LNAME,S_EMPLOYEE_EMAIL,'HEX' AS [S_EMPLOYEE_FROM],      
     S_EMPLOYEE_STATUS_ID,S_LOCATION_ID      
   FROM HELIXPRD.dbo.M_EMPLOYEE_EXCHANGE      
   WHERE ((@EmpID = '') OR (S_EMPLOYEE_ID = @EmpID))      
         
    
  ) A       
RETURN           
END 