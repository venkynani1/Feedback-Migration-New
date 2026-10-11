SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: INSERT SESSION TRAINER DETAILS
-- =============================================
--EXEC usp_InsertSessionTrainerDetails 'SQL2003','SQL2003_4/10/2012To4/16/2012','0001'

ALTER procedure [dbo].[usp_InsertSessionTrainerDetails]
@sessionid varchar(50),
	@TrainerID varchar(200) 

as
declare @createddate date 
begin
SET ANSI_NULLS ON
set @createddate=getdate()
IF NOT EXISTS (SELECT * FROM Session_Trainer WHERE SessionID=@sessionid AND TrainerID =@TrainerID)
BEGIN
 INSERT INTO  [dbo].[Session_Trainer]
           ( [SessionID] ,
	[TrainerID] 
      )
     VALUES
           (@sessionid,
	@TrainerID 
)
END
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


