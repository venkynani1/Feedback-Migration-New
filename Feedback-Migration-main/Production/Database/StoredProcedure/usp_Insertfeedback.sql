SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
-- Author:      SURESHKUMAR.D
-- Create date: 18 April 2012
-- Description: INSERT FEEDBACK
-- =============================================
--EXEC [usp_Insertfeedback] 'SQL2003','SQL2003_4/10/2012To4/16/2012','00001',1,1
CREATE procedure [dbo].[usp_Insertfeedback](
@courseid varchar(50),
@sessionid varchar(50),
@employee varchar(7),
@question_no int,
@response int
)
as
begin
declare @r1 int
declare @r2 int
declare @r3 int
declare @r4 int
declare @na int
if(@response=1)
begin
set @r1=1
set @r2=0
set @r3=0
set @r4=0
set @na=0
insert into Feedback
(Course_id,
 session_id,
 Employee_id,
 Question_no,
 Response,
 R1,
 R2,
 R3,
 R4,
 NA)values(@courseid,@sessionid,@employee,@question_no,@response,@r1,@r2,@r3,@r4,@na)
 end
else if(@response=2)
begin
set @r1=0
set @r2=1
set @r3=0
set @r4=0
set @na=0
insert into Feedback
(Course_id,
 session_id,
 Employee_id,
 Question_no,
 Response,
 R1,
 R2,
 R3,
 R4,
 NA)values(@courseid,@sessionid,@employee,@question_no,@response,@r1,@r2,@r3,@r4,@na)
 end
 else if(@response=3)
begin
set @r2=0
set @r1=0
set @r4=0
set @r3=1
set @na=0
insert into Feedback
(Course_id,
 session_id,
 Employee_id,
 Question_no,
 Response,
 R1,
 R2,
 R3,
 R4,
 NA)values(@courseid,@sessionid,@employee,@question_no,@response,@r1,@r2,@r3,@r4,@na)
 end
 if(@response=4)
begin
set @r2=0
set @r3=0
set @r1=0
set @r4=1
set @na=0
insert into Feedback
(Course_id,
 session_id,
 Employee_id,
 Question_no,
 Response,
 R1,
 R2,
 R3,
 R4,
 NA)values(@courseid,@sessionid,@employee,@question_no,@response,@r1,@r2,@r3,@r4,@na)
 end
 if(@response=5)
begin
set @r2=0
set @r3=0
set @r4=0
set @r1=0
set @na=1
insert into Feedback
(Course_id,
 session_id,
 Employee_id,
 Question_no,
 Response,
 R1,
 R2,
 R3,
 R4,
 NA)values(@courseid,@sessionid,@employee,@question_no,@response,@r1,@r2,@r3,@r4,@na)
 end
 end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


