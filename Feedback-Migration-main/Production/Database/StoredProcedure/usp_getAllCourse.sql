SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET ALL  COURSE DETAILS
-- =============================================
--EXEC usp_getAllCourse
CREATE procedure [dbo].[usp_getAllCourse]  
as  
begin  
SET ANSI_NULLS ON  
begin  
 select courseid,course from [dbo].course order by course asc    
end  
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


