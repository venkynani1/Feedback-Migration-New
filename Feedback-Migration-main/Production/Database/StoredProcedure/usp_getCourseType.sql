SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET COURSE TYPE
-- =============================================
--EXEC usp_getCourseType 
CREATE procedure [dbo].[usp_getCourseType]
AS
BEGIN
	BEGIN
		SELECT
		 CourseTYpeID
		,CourceTypeName
		FROM
		 CourseTypeMaster
	END
END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


