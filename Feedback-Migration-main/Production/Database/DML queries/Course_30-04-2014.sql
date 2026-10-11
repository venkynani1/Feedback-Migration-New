use trainingfeedback
select * from [Course] where [course] in ('TCDP Requirements Management Level 3','TCDP-Requirements Management Level 3','TCDP - Requirements Management Level 3')
select * from C_Session where Courseid in ('RML3','TCDPRML3','RML3TCDP')
select * from dbo.Nomination_List where Session_id in ('RML3_4/24/2014To4/25/2014','RML3TCDP_4/24/2014To4/25/2014','TCDPRML3_4/24/2014To4/25/2014')
select * from dbo.Session_Trainer where SessionID in ('RML3_4/24/2014To4/25/2014','RML3TCDP_4/24/2014To4/25/2014','TCDPRML3_4/24/2014To4/25/2014')
select * from [dbo].[Feedback] where session_id in ('RML3_4/24/2014To4/25/2014','RML3TCDP_4/24/2014To4/25/2014','TCDPRML3_4/24/2014To4/25/2014')




delete from dbo.Session_Trainer where SessionID in ('RML3_4/24/2014To4/25/2014','RML3TCDP_4/24/2014To4/25/2014','TCDPRML3_4/24/2014To4/25/2014')
delete from C_Session where Courseid in ('RML3','TCDPRML3','RML3TCDP')
delete from [course] where [course] in 
('TCDP Requirements Management Level 3','TCDP-Requirements Management Level 3','TCDP - Requirements Management Level 3')



