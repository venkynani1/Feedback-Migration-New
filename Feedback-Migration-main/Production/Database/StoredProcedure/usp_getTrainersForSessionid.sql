SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET TRAINERS FOR SESSION
-- =============================================
--EXEC usp_getTrainersForSessionid 'SQL2003_4/10/2012To4/16/2012','00001'
CREATE procedure [dbo].[usp_getTrainersForSessionid]
(@sessionid varchar(50),
@empid varchar(7) = null
)

as
begin
SET ANSI_NULLS ON
begin
if @empid=null
begin
SELECT TrainerID
  FROM [dbo].[Session_Trainer]
  where SessionID=@sessionid
  
end
else
begin 
 SELECT TrainerID
  FROM [dbo].[Session_Trainer]
  where SessionID=@sessionid
  AND TrainerID NOT IN (SELECT TrainerId
  FROM [dbo].[Feedback_Comments]
  WHERE sessionid=@sessionid
  AND Employee_id=@empid)
  
  end
end
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


