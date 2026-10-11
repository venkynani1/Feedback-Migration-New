SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: SEARCH COURSE
-- =============================================
--EXEC [usp_SearchCourse] 'SQL2003'

CREATE procedure [dbo].[usp_SearchCourse]
(  @courseid varchar(50))

as
begin
SET ANSI_NULLS ON
begin
select [courseid] from [dbo].course
  where [courseid] like '%'+@courseid+'%'
end
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


