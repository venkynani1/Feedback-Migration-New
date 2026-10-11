SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET NOMIATED EMPL FOR MAIL SEND
-- =============================================
--EXEC usp_getNomiatedEmplForMailsend 'SQL2003'

CREATE procedure [dbo].[usp_getNomiatedEmplForMailsend]
(@session_id varchar(50),
@course_Id varchar(50))
as
begin
SET ANSI_NULLS ON
begin
  select Employee_id from [dbo].[Nomination_List]
  where course_id=@course_Id
  and
  Session_id=@session_id
end
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


