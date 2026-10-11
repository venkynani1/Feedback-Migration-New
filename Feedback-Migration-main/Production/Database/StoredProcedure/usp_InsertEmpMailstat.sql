SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================  
-- Author:      SURESHKUMAR.D  
-- Create date: 18 April 2012  
-- Description: INSERT EMP MAIL STATUS  
-- =============================================  
--EXEC usp_InsertEmpMailstat 'SQL2003','SQL2003_4/10/2012To4/16/2012','12/03/2012',  
--'00001','0','TEST@HEXAWARE.COM','TEST@HEXAWARE.COM','','','TEST','TEST'  
CREATE procedure [dbo].[usp_InsertEmpMailstat]        
 @course_Id varchar(50)        
,@session_id varchar(50)        
,@Nominatedate date        
,@Employee_id varchar(50)        
,@Status varchar(50)      
,@FromMail nvarchar(50)      
,@ToMail nvarchar(50)      
,@BccMail nvarchar(1000)=NULL       
,@CcMail nvarchar(1000)=NULL      
,@Subject nvarchar(300)      
,@Body nvarchar(max)      
AS        
BEGIN        
IF @Status='Failed'     
    BEGIN    
     UPDATE dbo.Nomination_List SET IsExistsAlready=1    
     WHERE     
     course_id = @course_id and     
     Session_id=@session_id and    
     Employee_id = @Employee_id      
    END    
        
   INSERT INTO [dbo].[EmpMailstatus]        
    (      
     [Courseid] ,        
     [sessionid] ,        
     [EmplId] ,        
     [Nominateddate] ,        
     [Status],      
     FeedbackSendDate,      
     FromMail,      
     ToMail,      
     BccMail,      
     CcMail,      
     Subject,      
     Body,      
     MailSendFlag      
    )          
     VALUES    
      (        
       @course_Id,        
       @session_id,        
       @Employee_id,        
       @Nominatedate,        
       @Status ,      
       CONVERT(varchar(50), GETDATE(), 101),      
       @FromMail,      
       @ToMail,      
       @BccMail,      
       @CcMail,      
       'Reminder' + '-'+ @Subject,      
       @Body,      
       0      
      )     
     
END  
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


