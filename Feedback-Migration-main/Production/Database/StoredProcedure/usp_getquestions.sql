SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET QUESTIONS
-- =============================================
--EXEC usp_getquestions 

CREATE procedure [dbo].[usp_getquestions]
as
begin
SET ANSI_NULLS ON
begin
 select * from L1_QUESTIONS 
end
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


