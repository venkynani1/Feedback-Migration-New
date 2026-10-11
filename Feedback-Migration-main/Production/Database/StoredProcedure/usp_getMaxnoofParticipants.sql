USE [TrainingFeedback]
GO

GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET MAX NO OF PARTICIPANTS
-- =============================================
--EXEC [usp_getMaxnoofParticipants] 'SQL2003'
ALTER procedure [dbo].[usp_getMaxnoofParticipants]    
(@courseid varchar(100))    
as    
begin    
SET ANSI_NULLS ON    
begin    
 select max(max_participants) as max_participants
  from [dbo].C_Session where IsDeleted=0 and  Courseid=@courseid;     
end    
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


