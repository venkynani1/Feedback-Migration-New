SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET COUNT EMPLOYEE FOR SESSION
-- =============================================
--EXEC usp_getCountEmployeeForSession 'SQL2003'
CREATE procedure [dbo].[usp_getCountEmployeeForSession]
(
@courseid varchar(50)
)
as
begin
UPDATE [dbo].[C_Session] SET [max_participants]=
(select count([course_id]) from [dbo].[Nomination_List] where course_id='@courseid'and  [Employee_id]<>'&nbsp;')
WHERE [Courseid]=@courseid
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


