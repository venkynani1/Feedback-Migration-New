SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


create procedure [dbo].[usp_getCoursesession]
as
begin
SET ANSI_NULLS ON
begin
 select course,sessionid from [dbo].course,[dbo].C_Session where dbo.course.courseid=dbo.C_Session.Courseid;
end
end


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


