SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: SELECT VENDOR TRAINER DETAILS
-- =============================================
--EXEC usp_SelectVendorTrainerDetails 'NIIT',0

CREATE procedure [dbo].[usp_SelectVendorTrainerDetails]
@vendorName varchar(50) = null,
@flag int 

as
declare @createddate date 
begin
SET ANSI_NULLS ON
set @createddate=getdate()


    
 SELECT VendorName
  FROM [dbo].[VendorDet]
 
           
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


