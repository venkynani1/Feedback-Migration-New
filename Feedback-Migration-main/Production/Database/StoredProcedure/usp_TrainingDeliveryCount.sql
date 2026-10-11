SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: TRAINING DELIVERY COUNT
-- =============================================
--EXEC usp_TrainingDeliveryCount 'SQL2003_4/10/2012To4/16/2012'
CREATE procedure [dbo].[usp_TrainingDeliveryCount] 
(
	@sessionid varchar(50) 
)  
AS  
BEGIN
	  SELECT SessionID,TrainerID
	  FROM  Session_Trainer
	  WHERE SessionID=@sessionid
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


