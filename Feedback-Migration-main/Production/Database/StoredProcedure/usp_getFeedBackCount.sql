SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET FEEDBACK COUNT
-- =============================================
--EXEC usp_getFeedBackCount 'SQL2003_4/10/2012To4/16/2012','00001'
CREATE procedure [dbo].[usp_getFeedBackCount]    
(  
 @sessionid varchar(50),    
 @empid varchar(7) = null    
)    
    
AS    
BEGIN    
  SELECT COUNT(*)   
  FROM Feedback_Comments    
  WHERE sessionid=@sessionid    
  AND Employee_id=@empid  
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


