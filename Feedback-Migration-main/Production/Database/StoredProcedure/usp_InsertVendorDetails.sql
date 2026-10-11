SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: INSERT VENDOR DETAILS
-- =============================================
--EXEC usp_InsertVendorDetails 'NIIT',N,''

CREATE procedure [dbo].[usp_InsertVendorDetails]
@VendorName varchar(200) ,
@PrefixID varchar(50),
@IsExistsAlready int out 

as
declare @createddate date 
begin
SET ANSI_NULLS ON
set @createddate=getdate()

if exists(select * from [dbo].[VendorDet] where VendorName = @VendorName OR PrefixID=@PrefixID )
begin  
  set @IsExistsAlready=1;  
end 
else
BEGIN
    
 INSERT INTO [dbo].[VendorDet]
           ( [VendorName]
      ,[PrefixID]
      )
     VALUES
           (@VendorName ,
	@PrefixID
	
)
END

end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


