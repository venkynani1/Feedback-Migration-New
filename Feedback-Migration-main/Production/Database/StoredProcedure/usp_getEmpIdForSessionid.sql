SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET EMP ID FOR SESSION
-- =============================================
--EXEC usp_getEmpIdForSessionid 'SQL2003'
CREATE procedure [dbo].[usp_getEmpIdForSessionid]
(@sessionid varchar(50)
 )
as
begin
SET ANSI_NULLS ON
begin
	SELECT Employee_id FROM [dbo].[Nomination_List]
    where Session_id=@sessionid
end
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


