SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET THE CURRENT TRAINING DETAILS
-- =============================================
--EXEC usp_CurrentTrainings 'SQL2003','00001'

CREATE procedure [dbo].[usp_CurrentTrainings]
(@courseid  varchar(50),
@empid varchar(7))
as
begin
SET ANSI_NULLS ON
begin
select Employee_id,Session_id from [dbo].[Nomination_List]
  where course_id=@courseid AND Employee_id=@empid

  
end
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


