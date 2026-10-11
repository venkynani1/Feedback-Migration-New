USE TrainingFeedback
GO

Update C_Session set enddate = '2026-09-22' where Courseid like '%DS - Negotiation Fundamentals - Level 1 - Batch 2%'

INSERT INTO  RESOURCE_MGMT.dbo.TMP_DEPT_MANUAL_PUSH
select 'F9376','2000171735'
  
GO
