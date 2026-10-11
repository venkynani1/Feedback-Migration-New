SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: INSERT FEEDBACKS
-- =============================================
--EXEC [usp_InsertfeedbackS] 'SQL2003','SQL2003_4/10/2012To4/16/2012','00001',1,1
CREATE procedure [dbo].[usp_Insertfeedbacks](        
@courseid varchar(50),        
@sessionid varchar(50),        
@employee varchar(7),        
@TrainerId varchar(200),        
@response1 int ,        
@response2 int ,        
@response3 int ,        
@response4 int ,        
@response5 int ,        
@response6 int ,        
@response7 int,        
@response8 int ,        
@response9 int ,        
@response10 int ,        
@response11 int ,        
@response12 int ,        
@response13 int,        
@response14 int,        
@response15 int,        
@response16 int,        
@response17 int,        
@response18 int,
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
declare @na int        
begin try        
        
        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response1=1)        
begin        
set @r1=1        
set @question_no=1        
end        
else if(@response1=2)        
begin        
set @r2=1        
set @question_no=1        
end        
else if(@response1=3)        
begin        
set @r3=1        
set @question_no=1        
end        
else if(@response1=4)        
begin        
set @r4=1        
set @question_no=1        
end        
else if(@response1=5)        
begin        
set @na=1        
set @question_no=1        
end  
else if(@response1=0)        
begin        
set @question_no=1        
end        
      
UPDATE EmpMailstatus SET MailSendFlag=2 WHERE EmplId=@employee AND Courseid=@courseid      
--insert first feedback        
        
        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,        
 TrainerId,        
  
 R1,R2,R3,R4,NA,FeedbackGivenDate,CommonFlag        
 )values(@courseid,@sessionid,@employee,@question_no,@response1,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
        
--start the second feedback        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response2=1)        
begin        
set @r1=1        
set @question_no=2        
end        
else if(@response2=2)        
begin        
set @r2=1        
set @question_no=2        
end        
else if(@response2=3)        
begin        
set @r3=1        
set @question_no=2        
end        
else if(@response2=4)        
begin        
set @r4=1        
set @question_no=2        
end        
else if(@response2=5)        
begin        
set @na=1        
set @question_no=2        
end 
else if(@response2=0)        
begin        
set @question_no=2        
end        
-- insert second        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA,FeedbackGivenDate,CommonFlag        
 )values(@courseid,@sessionid,@employee,@question_no,@response2,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
-- start third        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response3=1)        
begin        
set @r1=1        
set @question_no=3        
end        
else if(@response3=2)        
begin        
set @r2=1        
set @question_no=3        
end        
else if(@response3=3)        
begin        
set @r3=1        
set @question_no=3        
end        
else if(@response3=4)        
begin        
set @r4=1        
set @question_no=3        
end        
else if(@response3=5)        
begin        
set @na=1        
set @question_no=3        
end   
else if(@response3=0)        
begin        
set @question_no=3        
end     
--insert third feedback        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA ,FeedbackGivenDate,CommonFlag      
 )values(@courseid,@sessionid,@employee,@question_no,@response3,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
--start fourth feedback        
        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response4=1)        
begin        
set @r1=1        
set @question_no=4        
end        
else if(@response4=2)        
begin        
set @r2=1        
set @question_no=4      
end        
else if(@response4=3)        
begin        
set @r3=1        
set @question_no=4        
end        
else if(@response4=4)        
begin        
set @r4=1        
set @question_no=4        
end        
else if(@response4=5)        
begin        
set @na=1        
set @question_no=4        
end
else if(@response4=0)        
begin        
set @question_no=4        
end         
--insert fourth feedback        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA ,FeedbackGivenDate,CommonFlag       
 )values(@courseid,@sessionid,@employee,@question_no,@response4,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
--start fifth feedback        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response5=1)        
begin        
set @r1=1        
set @question_no=5        
end        
else if(@response5=2)        
begin        
set @r2=1        
set @question_no=5        
end        
else if(@response5=3)        
begin        
set @r3=1        
set @question_no=5        
end        
else if(@response5=4)        
begin        
set @r4=1        
set @question_no=5        
end        
else if(@response5=5)        
begin        
set @na=1        
set @question_no=5        
end   
else if(@response5=0)        
begin        
set @question_no=5        
end       
--insert fifth feedback        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA ,FeedbackGivenDate,CommonFlag       
 )values(@courseid,@sessionid,@employee,@question_no,@response5,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
         
 --start sixth feedback        
 set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response6=1)        
begin        
set @r1=1        
set @question_no=6        
end        
else if(@response6=2)        
begin        
set @r2=1        
set @question_no=6        
end        
else if(@response6=3)        
begin        
set @r3=1        
set @question_no=6        
end        
else if(@response6=4)        
begin        
set @r4=1        
set @question_no=6        
end        
else if(@response6=5)        
begin        
set @na=1        
set @question_no=6        
end 
else if(@response6=0)        
begin        
set @question_no=6        
end         
--insert sixth feedback        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA,FeedbackGivenDate,CommonFlag        
 )values(@courseid,@sessionid,@employee,@question_no,@response6,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
        
--start seventh feedback        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response7=1)        
begin        
set @r1=1        
set @question_no=7        
end        
else if(@response7=2)        
begin        
set @r2=1        
set @question_no=7        
end        
else if(@response7=3)        
begin        
set @r3=1        
set @question_no=7        
end        
else if(@response7=4)        
begin        
set @r4=1        
set @question_no=7        
end        
else if(@response7=5)        
begin        
set @na=1        
set @question_no=7        
end
else if(@response7=0)        
begin        
set @question_no=7        
end        
        
--insert seventh feedback        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA,FeedbackGivenDate,CommonFlag        
 )values(@courseid,@sessionid,@employee,@question_no,@response7,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
        
--start eigth feedback        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response8=1)        
begin        
set @r1=1        
set @question_no=8        
end        
else if(@response8=2)        
begin        
set @r2=1        
set @question_no=8        
end        
else if(@response8=3)        
begin        
set @r3=1        
set @question_no=8        
end        
else if(@response8=4)        
begin        
set @r4=1        
set @question_no=8        
end        
else if(@response8=5)        
begin        
set @na=1        
set @question_no=8        
end 
else if(@response8=0)        
begin        
set @question_no=8        
end        
        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA ,FeedbackGivenDate,CommonFlag       
 )values(@courseid,@sessionid,@employee,@question_no,@response8,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
 --start ninth feedback        
 set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response9=1)        
begin        
set @r1=1        
set @question_no=9        
end        
else if(@response9=2)        
begin        
set @r2=1        
set @question_no=9        
end        
else if(@response9=3)        
begin        
set @r3=1        
set @question_no=9        
end        
else if(@response9=4)        
begin        
set @r4=1        
set @question_no=9        
end        
else if(@response9=5)        
begin        
set @na=1        
set @question_no=9        
end        
  else if(@response9=0)        
begin        
set @question_no=9        
end        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA,FeedbackGivenDate,CommonFlag        
 )values(@courseid,@sessionid,@employee,@question_no,@response9,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
        
--start tenth feedback        
        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response10=1)        
begin        
set @r1=1        
set @question_no=10        
end        
else if(@response10=2)        
begin        
set @r2=1        
set @question_no=10        
end        
else if(@response10=3)        
begin        
set @r3=1        
set @question_no=10        
end        
else if(@response10=4)        
begin        
set @r4=1        
set @question_no=10        
end        
else if(@response10=5)        
begin        
set @na=1        
set @question_no=10        
end        
   else if(@response10=0)        
begin        
set @question_no=10        
end        
          
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA,FeedbackGivenDate,CommonFlag      
 )values(@courseid,@sessionid,@employee,@question_no,@response10,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
--start eleventh feedback        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response11=1)        
begin        
set @r1=1        
set @question_no=11        
end        
else if(@response11=2)        
begin        
set @r2=1        
set @question_no=11        
end        
else if(@response11=3)        
begin        
set @r3=1        
set @question_no=11        
end        
else if(@response11=4)        
begin        
set @r4=1        
set @question_no=11        
end        
else if(@response11=5)        
begin        
set @na=1        
set @question_no=11        
end        
else if(@response11=0)        
begin        
set @question_no=11        
end          
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA,FeedbackGivenDate,CommonFlag        
 )values(@courseid,@sessionid,@employee,@question_no,@response11,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
        
--start twelveth feedback        
        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response12=1)        
begin        
set @r1=1        
set @question_no=12        
end        
else if(@response12=2)        
begin        
set @r2=1        
set @question_no=12        
end        
else if(@response12=3)        
begin        
set @r3=1        
set @question_no=12        
end        
else if(@response12=4)        
begin        
set @r4=1        
set @question_no=12        
end        
else if(@response12=5)        
begin        
set @na=1        
set @question_no=12        
end        
else if(@response12=0)        
begin        
set @question_no=12        
end          
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA,FeedbackGivenDate,CommonFlag        
 )values(@courseid,@sessionid,@employee,@question_no,@response12,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
        
--start thirdteen feedback        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response13=1)        
begin        
set @r1=1        
set @question_no=13        
end        
else if(@response13=2)        
begin        
set @r2=1        
set @question_no=13        
end        
else if(@response13=3)        
begin        
set @r3=1        
set @question_no=13        
end        
else if(@response13=4)        
begin        
set @r4=1        
set @question_no=13        
end        
else if(@response13=5)        
begin        
set @na=1        
set @question_no=13        
end        
        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA,FeedbackGivenDate,CommonFlag        
 )values(@courseid,@sessionid,@employee,@question_no,@response13,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
        
--start fourteen feedback        
        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response14=1)        
begin        
set @r1=1        
set @question_no=14        
end        
else if(@response14=2)        
begin        
set @r2=1        
set @question_no=14        
end        
else if(@response14=3)        
begin        
set @r3=1        
set @question_no=14        
end        
else if(@response14=4)        
begin        
set @r4=1        
set @question_no=14        
end        
else if(@response14=5)        
begin        
set @na=1        
set @question_no=14        
end        
        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA,FeedbackGivenDate,CommonFlag        
 )values(@courseid,@sessionid,@employee,@question_no,@response14,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
--start fifteen feedback        
        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response15=1)        
begin        
set @r1=1        
set @question_no=15        
end        
else if(@response15=2)        
begin        
set @r2=1        
set @question_no=15        
end        
else if(@response15=3)        
begin        
set @r3=1        
set @question_no=15        
end        
else if(@response15=4)        
begin        
set @r4=1        
set @question_no=15        
end        
else if(@response15=5)        
begin        
set @na=1        
set @question_no=15        
end        
        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA,FeedbackGivenDate,CommonFlag        
 )values(@courseid,@sessionid,@employee,@question_no,@response15,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
         
 --start sixteen feedback        
 set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response16=1)        
begin        
set @r1=1        
set @question_no=16        
end        
else if(@response16=2)        
begin        
set @r2=1        
set @question_no=16        
end        
else if(@response16=3)        
begin        
set @r3=1        
set @question_no=16        
end        
else if(@response16=4)        
begin        
set @r4=1        
set @question_no=16        
end        
else if(@response16=5)        
begin        
set @na=1        
set @question_no=16        
end        
        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA,FeedbackGivenDate,CommonFlag        
 )values(@courseid,@sessionid,@employee,@question_no,@response16,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
        
--start seventeen feedback        
        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response17=1)        
begin        
set @r1=1        
set @question_no=17        
end        
else if(@response17=2)        
begin        
set @r2=1        
set @question_no=17        
end        
else if(@response17=3)        
begin        
set @r3=1        
set @question_no=17        
end        
else if(@response17=4)        
begin        
set @r4=1        
set @question_no=17        
end        
else if(@response17=5)        
begin        
set @na=1        
set @question_no=17        
end        
        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA,FeedbackGivenDate,CommonFlag        
 )values(@courseid,@sessionid,@employee,@question_no,@response17,@TrainerId,@r1,@r2,@r3,@r4,@na,convert(Varchar(50), GETDATE(), 101),@CommonFlag)        
--start eighteen feedback        
        
set @r1=0        
set @r2=0        
set @r3=0        
set @r4=0        
set @na=0        
if(@response18=1)        
begin        
set @r1=1        
set @question_no=18        
end        
else if(@response18=2)        
begin        
set @r2=1        
set @question_no=18        
end        
else if(@response18=3)        
begin        
set @r3=1        
set @question_no=18        
end        
else if(@response18=4)        
begin        
set @r4=1        
set @question_no=18        
end        
else if(@response18=5)        
begin        
set @na=1        
set @question_no=18        
end        
        
        
insert into Feedback(Course_id,        
 session_id,        
 Employee_id,        
 question_no,        
 Response,TrainerId,        
 R1,R2,R3,R4,NA,FeedbackGivenDate,CommonFlag        
 )values(@courseid,@sessionid,@employee,@question_no,@response18,@TrainerId,@r1,@r2,@r3,@r4,@na,GETDATE(),@CommonFlag)        
        
insert into Feedback_Comments(sessionid,TrainerId,Employee_id,Comments,FeedbackGivenDate,CommonFlag)        
  values(@sessionid,@TrainerId,@employee,@comments,GETDATE(),@CommonFlag)       
        
        
end try        
begin catch        
       
end catch        
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


