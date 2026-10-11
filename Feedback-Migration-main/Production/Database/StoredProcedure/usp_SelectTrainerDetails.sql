SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: SELECT TRAINER DETAILS
-- =============================================
--EXEC usp_SelectTrainerDetails 'INTERNAL','NIIT','SQL2003'

CREATE procedure [dbo].[usp_SelectTrainerDetails]
 @TrainerType varchar(1)  ,
 @VendorName  varchar(200) = NULL,
 @courseid varchar(50)  

as
declare @createddate date 
begin
SET ANSI_NULLS ON
set @createddate=getdate()

if @TrainerType='I'
begin
    
 select TrainerName+'('+TrainerId +')' as Trainer   from  [dbo].[TrainerDet]
 where TrainerType=@TrainerType and courseid=@courseid
 end
 else
 begin
 select TrainerName+'('+TrainerId +')' as Trainer   from  [dbo].[TrainerDet]
 where VendorName=@VendorName and TrainerType=@TrainerType and courseid=@courseid
 end
           
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


