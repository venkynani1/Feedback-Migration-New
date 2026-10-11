SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: INSERT COURSE DETAILS
-- =============================================
--EXEC usp_InsertCourse 'SQL2003','SQLSERVER 2003','SQLSERVER 2003',
--'INTERNAL','TECHNICAL','00001'
CREATE procedure [dbo].[usp_InsertCourse]  
@courseid varchar(50),  
@course varchar(500),  
@Description varchar(100),  
@InternalExternal char(1),  
@course_type Nvarchar(100),  
@createdby varchar(7),  
@rowaffected int out,  
@IsCourseIdExist int out,  
@IsCourseNameExist int out  
as  
declare @createddate date   
begin  
SET ANSI_NULLS ON  
set @createddate=getdate()  
if exists(select courseid from course where courseid = @courseid)  
begin    
  set @IsCourseIdExist=1;    
end   
if exists(select course from dbo.course where course = @course)  
begin    
  set @IsCourseNameExist=1;        
end   
if((@IsCourseIdExist<=1) or(@IsCourseNameExist=1))   
begin  
 set @rowaffected=0  
 return  
end  
else  
begin  
 INSERT INTO [dbo].[course]  
           ([courseid]  
           ,[course]  
           ,[Description]  
           ,[InternalExternal]  
           ,[course_type]             
           ,[createdby]  
           ,[createddate])             
     VALUES  
           (@courseid,  
   @course,  
   @Description,  
   @InternalExternal,  
   @course_type,     
   @createdby,  
   @createddate  
)  
end  
set @rowaffected=@@rowcount;  

Insert into dbo.TrainerDet values('I','ALLTR','All Trainer',null,@courseid)
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


