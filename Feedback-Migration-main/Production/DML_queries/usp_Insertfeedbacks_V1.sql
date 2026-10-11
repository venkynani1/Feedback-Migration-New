-- =============================================    
-- Author:      Vinoth.G    
-- Create date: 23 Oct 2017    
-- Description: INSERT FEEDBACKS    
-- =============================================    
--EXEC [usp_InsertfeedbackS] 'SQL2003','SQL2003_4/10/2012To4/16/2012','00001',1,1    
CREATE procedure [dbo].[usp_Insertfeedbacks_V1](            
@courseid varchar(50),            
@sessionid varchar(50),            
@employee varchar(7),            
@TrainerId varchar(200),            
@response1 int ,            
@response2 int ,            
@response3 int ,     
@CommonFlag int,           
@comments varchar(1000)= NULL           
)            
as            
begin            
declare @question_no int            
declare @r1 int            
declare @r2 int            
declare @r3 int            
declare @r4 int    
declare @r5 int             
declare @na int            
begin try            
set @r1=0            
set @r2=0            
set @r3=0            
set @r4=0    
set @r5=0              
set @na=0            
if(@response1=1)            
begin            
set @r1=1            
set @question_no=21            
end            
else if(@response1=2)            
begin            
set @r2=1            
set @question_no=21            
end            
else if(@response1=3)            
begin            
set @r3=1            
set @question_no=21            
end            
else if(@response1=4)            
begin            
set @r4=1            
set @question_no=21            
end            
else if(@response1=5)            
begin            
set @r5=1            
set @question_no=21            
end      
else if(@response1=0)            
begin            
set @question_no=21            
end          
   UPDATE EmpMailstatus SET MailSendFlag=2 WHERE EmplId=@employee AND Courseid=@courseid          
--insert first feedback            
insert into Feedback(Course_id,            
 session_id,            
 Employee_id,            
 question_no,            
 Response,            
 TrainerId,            
 R1,R2,R3,R4,R5,NA,FeedbackGivenDate,CommonFlag            
 )values(@courseid,@sessionid,@employee,@question_no,@response1,@TrainerId,@r1,@r2,@r3,@r4,@r5,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)            
--start the second feedback            
set @r1=0            
set @r2=0            
set @r3=0            
set @r4=0    
set @r5=0             
set @na=0            
if(@response2=1)            
begin            
set @r1=1            
set @question_no=22            
end            
else if(@response2=2)            
begin            
set @r2=1            
set @question_no=22            
end            
else if(@response2=3)            
begin            
set @r3=1            
set @question_no=22            
end            
else if(@response2=4)            
begin            
set @r4=1            
set @question_no=22            
end            
else if(@response2=5)            
begin            
set @r5=1            
set @question_no=22            
end     
else if(@response2=0)            
begin            
set @question_no=22            
end            
-- insert second            
insert into Feedback(Course_id,            
 session_id,            
 Employee_id,            
 question_no,            
 Response,TrainerId,            
 R1,R2,R3,R4,R5,NA,FeedbackGivenDate,CommonFlag            
 )values(@courseid,@sessionid,@employee,@question_no,@response2,@TrainerId,@r1,@r2,@r3,@r4,@r5,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)            
-- start third            
set @r1=0            
set @r2=0            
set @r3=0            
set @r4=0            
set @r5=0            
if(@response3=1)            
begin            
set @r1=1            
set @question_no=23            
end            
else if(@response3=2)            
begin            
set @r2=1            
set @question_no=23            
end            
else if(@response3=3)            
begin            
set @r3=1            
set @question_no=23            
end            
else if(@response3=4)            
begin            
set @r4=1            
set @question_no=23            
end   
else if(@response3=5)            
begin            
set @r5=1            
set @question_no=23            
end       
else if(@response3=0)            
begin            
set @question_no=23            
end         
--insert third feedback            
insert into Feedback(Course_id,session_id,Employee_id,question_no,Response,TrainerId,            
 R1,R2,R3,R4,R5,NA,FeedbackGivenDate,CommonFlag          
 )values(@courseid,@sessionid,@employee,@question_no,@response3,@TrainerId,@r1,@r2,@r3,@r4,@r5,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)      
insert into Feedback_Comments(sessionid,TrainerId,Employee_id,Comments,FeedbackGivenDate,CommonFlag)            
 values(@sessionid,@TrainerId,@employee,@comments,GETDATE(),@CommonFlag)         
end try            
begin catch            
end catch            
end