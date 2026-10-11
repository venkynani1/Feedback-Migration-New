SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: INSERT TRAINER DETAILS
-- =============================================
--EXEC usp_InsertTrainerDetails 'INTERNAL',''00002,'TEST','NIIT','SQL2003',''
CREATE procedure [dbo].[usp_InsertTrainerDetails]
 @TrainerType varchar(1)  ,
 @TrainerId varchar(200) =null ,
 @TrainerName varchar(50),
 @VendorName  varchar(200) = NULL,
 @course_Id varchar(50)  ,
 @IsExistsAlready int out 

as
declare @createddate date 
begin
SET ANSI_NULLS ON
set @createddate=getdate()
if exists(select * from [dbo].[TrainerDet] where TrainerId = @TrainerId and TrainerName=@TrainerName and TrainerType=@TrainerType and courseid=@course_Id )
begin  
  set @IsExistsAlready=1;  
end 
else
BEGIN


if @VendorName is null
begin
    
 INSERT INTO [dbo].[TrainerDet]
           ( [TrainerType],
	[TrainerId] ,
	[TrainerName] ,
	[VendorName] ,
	[courseid] 
      )
     VALUES
           (@TrainerType  ,
@TrainerId ,
	@TrainerName ,
	@VendorName ,
	@course_Id
)
end
else
begin
INSERT INTO [dbo].[TrainerDet]
           ( [TrainerType],
	[TrainerId] ,
	[TrainerName] ,
	[VendorName],
	[courseid]   
      )
     VALUES
           (@TrainerType  ,
(select PrefixID  from [dbo].[VendorDet] where VendorName=@VendorName)+convert (varchar(10),(convert(int,(select COUNT(TrainerID) FROM [dbo].[TrainerDet]
	where VendorName=@VendorName))+1)),
	@TrainerName ,
	@VendorName ,
	@course_Id
	)
end
end
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


