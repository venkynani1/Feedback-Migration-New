SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE procedure [dbo].[usp_getTrainingEmpidWiseTemp]    
(@empid varchar(7)    
,@Isadmin varchar(20))    
as    
begin    
SET ANSI_NULLS ON    
begin    
if(@Isadmin='True')    
begin    
 select NormalQry.Courseid,NormalQry.sessionid,CONVERT(VARCHAR(50),NormalQry.startdate,106) as startdate,CONVERT(VARCHAR(50), NormalQry.enddate,106) as enddate , SplQuery.tot as Tot_Feedbacks,SplQuery.Balance as Bal_Feedbacks from     
(select Train.SessionID,Train.tot,    
(    
   case    
   when feed.bal IS NULL  then  Train.tot    
    when feed.bal is not null then convert(int,Train.tot )  -convert(int,feed.bal)    
         
   end    
   ) as Balance from     
(SELECT [SessionID],count(TrainerID) as tot    
          
  FROM [dbo].[Session_Trainer]    
  where sessionid in(    
  select SessionID from Nomination_List where Employee_id=@empid)    
  group by SessionID) Train    
      
  left join    
      
  (select count(distinct TrainerId) as bal,session_id FROM [dbo].[Feedback]    
  WHERE Employee_id=@empid    
  and session_id in(    
  select session_id from Nomination_List where Employee_id=@empid)    
  group BY  session_id) feed    
  on Train.SessionID=feed.session_id)  SplQuery    
  inner join    
      
  (select * from [dbo].[C_Session]    
  where sessionid in(    
  select Session_id from Nomination_List where Employee_id=@empid)    
) NormalQry    
  on SplQuery.SessionID=NormalQry.sessionid    
  where SplQuery.Balance!=0    
end    
else     
begin    
select NormalQry.Courseid,NormalQry.sessionid,CONVERT(VARCHAR(50),NormalQry.startdate,106) as startdate,CONVERT(VARCHAR(50), NormalQry.enddate,106) as enddate , SplQuery.tot as Tot_Feedbacks,SplQuery.Balance as Bal_Feedbacks from     
(select Train.SessionID,Train.tot,    
(    
   case    
   when feed.bal IS NULL  then  Train.tot    
    when feed.bal is not null then convert(int,Train.tot )  -convert(int,feed.bal)    
         
   end    
   ) as Balance from     
(SELECT [SessionID],count(TrainerID) as tot    
          
  FROM [dbo].[Session_Trainer]    
  where sessionid in(    
  select SessionID from Nomination_List where Employee_id=@empid)    
  group by SessionID) Train    
      
  left join    
      
  (select count(distinct TrainerId) as bal,session_id FROM [dbo].[Feedback]    
  WHERE Employee_id=@empid    
  and session_id in(    
  select session_id from Nomination_List where Employee_id=@empid)    
  group BY  session_id) feed    
  on Train.SessionID=feed.session_id)  SplQuery    
  inner join    
      
  (select * from [dbo].[C_Session]    
  where sessionid in(    
  select Session_id from Nomination_List where Employee_id=@empid)    
) NormalQry    
  on SplQuery.SessionID=NormalQry.sessionid    
  where SplQuery.Balance!=0    
end    
      
end    
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


