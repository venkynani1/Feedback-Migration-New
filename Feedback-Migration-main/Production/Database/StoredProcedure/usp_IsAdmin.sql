SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: CHECK IS ADMIN
-- =============================================
--EXEC usp_IsAdmin '00002,
CREATE procedure [dbo].[usp_IsAdmin]
 (@empid varchar(7))  
as
begin
SET ANSI_NULLS ON 
select Admin.ISSuperAdmin  from Admin where Admin.EMP_ID=@empid  
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


