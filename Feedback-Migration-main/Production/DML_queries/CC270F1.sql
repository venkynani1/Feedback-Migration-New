USE [HelixPRD]
GO

INSERT INTO [dbo].[M_WORKSTATION_MASTER]
           ([S_LOCATION_ID]
           ,[S_WORK_SITE_NAME]
           ,[S_FLOOR]
           ,[S_WING]
           ,[S_WORKSTATION_ID]
           ,[S_WORKSTATION_TYPE]
           ,[S_STATUS]
           ,[S_EMPLOYEE_ID]
           ,[S_REMARKS]
           ,[S_UPDATE_DT]
           ,[S_UPDATED_BY]
           ,[S_DIRECT_PHONE]
           ,[S_TEL_EXTN]
           ,[S_PURPOSE]
           ,[S_ODC]
           ,[LOCATIONTYPE]
           ,[ISDELETED]
           ,[ERT]
           ,[ODC])
     VALUES
('SIRUSERI','CC','01','-NA-','CC270F1','WRKSTN','02',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'02',0,0,'00')

GO