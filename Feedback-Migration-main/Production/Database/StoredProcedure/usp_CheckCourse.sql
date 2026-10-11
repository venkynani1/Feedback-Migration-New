SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: COURSE IS EXIST OR NOT
-- =============================================
--EXEC usp_CheckCourse 'SQL2003'

CREATE procedure [dbo].[usp_CheckCourse]
(  @courseid varchar(50),@IsCourseIdExist int out)

as
begin
SET ANSI_NULLS ON
begin
if exists(select courseid from course where courseid = @courseid)
begin  
  set @IsCourseIdExist=1;
 end

  
end
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


