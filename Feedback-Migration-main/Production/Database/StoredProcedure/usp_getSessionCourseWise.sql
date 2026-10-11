USE [TrainingFeedback]
GO
/****** Object:  StoredProcedure [dbo].[usp_getSessionCourseWise]    Script Date: 09/23/2014 18:05:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: GET SESSION COURSE WISE
-- =============================================
--EXEC usp_getSessionCourseWise '00001'
ALTER procedure [dbo].[usp_getSessionCourseWise]    
(@courseid varchar(100),@year int)    
as    
begin    
SET ANSI_NULLS ON    
begin    
 select TrainerId,sessionid,startdate,enddate,max_participants,createdby,createddate,modifiedby,modifieddate  
  from [dbo].C_Session where IsDeleted=0 and Courseid=@courseid and YEAR(startdate)=@year;      
end    
end
