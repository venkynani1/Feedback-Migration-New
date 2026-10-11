SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: COMMENTS VAILD DETAILS
-- =============================================
--EXEC usp_UpdateCommentsvaildDetails '00002,'SQL2003_4/10/2012To4/16/2012',1
CREATE procedure [dbo].[usp_UpdateCommentsvaildDetails]
(@Employee_id varchar(50),
@session_id varchar(50),
@Commentsvalid int)

as
declare @createddate date 
begin
SET ANSI_NULLS ON


    
 update [dbo].[Feedback_Comments]
          set CommentsValid=@Commentsvalid
          where Employee_id=@Employee_id
          and sessionid=@session_id
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


