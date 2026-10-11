SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[spsSearchCode]   
(  
@Strings   
AS VARCHAR (255)   
)  
AS  
BEGIN  
SET  
NOCOUNT ON   
SET  
TRANSACTION ISOLATION LEVEL READ UNCOMMITTED   
   
DECLARE @tmpString TABLE   
(   
RowNum   
int NOT NULL IDENTITY (1, 1)   
, String varchar(2000)   
)   
DECLARE @tmp TABLE   
(   
RowNum   
int   
, objType varchar(50)   
, objSeq int   
, objName varchar(255)   
, objParentName varchar(255)   
)   
insert into   
@tmpString  
(   
String  
)   
select   
String  
from   
dbo.SplitString(@Strings, ',')   
DECLARE @StringCount int   
SELECT @StringCount = MAX(RowNum) FROM @tmpString   
DECLARE @RowNum int   
DECLARE @String varchar(255)   
DECLARE curString CURSOR FOR   
SELECT   
RowNum  
, String   
FROM   
@tmpString  
OPEN curString   
FETCH NEXT FROM curString INTO @RowNum, @String   
WHILE @@FETCH_STATUS = 0   
BEGIN   
   
insert into   
@tmp  
select distinct   
@RowNum  
,   
CASE sysobjects.type   
when 'P' THEN 'Stored Proc'   
when 'V' THEN 'View'   
when 'FN' THEN 'Function'   
when 'TF' THEN 'Function'   
when 'TR' THEN 'Trigger'   
END AS objType,   
CASE sysobjects.type   
when 'P' THEN 1   
when 'V' THEN 2   
when 'FN' THEN 3   
WHEN 'TF' THEN 3   
when 'TR' THEN 4   
END AS objSeq,   
sysobjects  
.Name as objName,   
NULL as objParentName   
from   
sysobjects   
(nolock)   
inner join syscomments (nolock) on syscomments.ID = sysobjects.ID   
where   
sysobjects  
.type in ('P', 'V', 'FN', 'TR', 'TF') -- stored procs, views, functions, triggers   
and (syscomments.text like '%' + @String + '%')   
-- jobs   
insert into   
@tmp  
select   
@RowNum  
, 'Job' As objType   
, 5 as objSeq   
, 'Step ' + cast(js.step_id as varchar(5)) + ': ' + js.step_name as objName   
, j.name + ' ' + case when j.enabled = 1 then '(Enabled)' else '(Disabled)' end as objParentName   
from   
msdb  
..sysjobsteps js (nolock)   
inner join msdb..sysjobs j (nolock) on js.job_id = j.job_id   
where   
js  
.command like '%' + @String + '%'   
-- table names   
insert into   
@tmp  
select   
@RowNum  
, 'Table' as objType   
, 6 as objSeq   
, syscolumns.name as objName   
, sysobjects.name as objParentName   
from   
syscolumns   
(nolock)   
inner join sysobjects (nolock) on syscolumns.id = sysobjects.id   
where   
syscolumns  
.name like '%' + @String + '%'   
and sysobjects.type = 'U'   
FETCH NEXT FROM curString INTO @RowNum, @String   
END   
CLOSE curString   
DEALLOCATE curString   
delete from   
@tmp  
where   
(objName like '%_tmp%' or objName like '%tmp_%' or objName like '%_bak%' or objName like '%bak_%' or objName like '%_tommy%' or objName like '%tommy_%')   
select   
t1  
.objType as Type   
, t1.objName as Object   
, t1.objParentName as Parent   
from   
@tmp t1  
INNER JOIN   
(   
SELECT   
objType  
, objName   
, objParentName   
FROM   
@tmp  
GROUP BY   
objType  
, objName   
, objParentName   
HAVING   
COUNT(*) = @StringCount   
) as t2 ON t1.objType = t2.objType and t1.objName = t2.ObjName and coalesce(t1.objParentName, '') = coalesce(t2.objParentName, '')   
group by   
t1  
.objType   
, t1.objName   
, t1.objParentName   
order by   
MIN(t1.objSeq)   
, t1.objType   
, t1.objName   
, t1.objParentName   
END  
  
  
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


