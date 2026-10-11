-- =============================================      
-- Author:  VINOTH G      
-- Create date: 14/06/2018      
-- Description: To check whether employee exist or not in LMS     
-- =============================================      
CREATE PROCEDURE [dbo].[USP_CHECK_MOCHA_EMP_RECORD]      
 -- Add the parameters for the stored procedure here      
 @EMPID varchar(50),@DOMAIN varchar(100)      
AS      
BEGIN      
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
      
   
    -- Insert statements for procedure here      
    if(@DOMAIN='1')      
    BEGIN      
      SELECT   
         (SELECT (B.S_EMPLOYEE_FNAME + ' ' + B.S_EMPLOYEE_LNAME ) FROM dbo.[UF_GetEmployeeExchangeDetails](@EMPID) B)    
          AS [S_EMPLOYEE_FNAME],   
         S_EMPLOYEE_EMAIL    
         FROM M_EMPLOYEE_EXCHANGE WHERE S_EMPLOYEE_ID=@EMPID      
    END      
      
END   
  
  
