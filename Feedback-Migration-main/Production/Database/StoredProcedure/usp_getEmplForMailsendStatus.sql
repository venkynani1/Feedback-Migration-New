SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET EMPL FOR MAIL SEND STATUS
-- =============================================
--EXEC usp_getEmplForMailsendStatus 'SQL2003_12/03/2012TO13/03/2012','sql2003'
CREATE procedure [dbo].[usp_getEmplForMailsendStatus]  
(
	@session_id varchar(50),  
	@course_Id varchar(50))  
as  
begin  
SET ANSI_NULLS ON  
begin  
  select   EmplId as [Employee ID] ,CONVERT(VARCHAR(50),Nominateddate,106) as [Nominated Date] ,Status from [dbo].[EmpMailstatus]  
  where Courseid=@course_Id  
  and sessionid=@session_id  
end  
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


