SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================  
-- Author:      SURESHKUMAR.D  
-- Create date: 18 April 2012  
-- Description: GET SESSION DETAILS  
-- =============================================  
--EXEC [usp_getSessionSessionIdWise] 'SQL2003_4/10/2012To4/16/2012','00001'  
CREATE procedure [dbo].[usp_getSessionSessionIdWise]        
(@sessionid varchar(50))        
as        
begin        
SET ANSI_NULLS ON        
begin        
select CONVERT(VARCHAR(50),GETDATE(),106)as PreparedDate    
 ,CS.sessionid      
 ,CONVERT(VARCHAR(50),CS.startdate,106)as startdate      
 ,CONVERT(VARCHAR(50),CS.enddate,106)as enddate      
 ,count(NL.Session_id) as max_participants   
 ,ST.TrainerID AS [Trainer Name]   
 from [dbo].C_Session CS 
 INNER JOIN Session_Trainer ST ON CS.sessionid=ST.SessionID  
 INNER JOIN Nomination_List NL ON NL.Session_id=CS.sessionid
 where CS.IsDeleted=0 and CS.sessionid=@sessionid 
 group by CS.sessionid
 ,CONVERT(VARCHAR(50),CS.startdate,106)
 ,CONVERT(VARCHAR(50),CS.enddate,106)
 ,ST.TrainerID
          
end        
end  
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


