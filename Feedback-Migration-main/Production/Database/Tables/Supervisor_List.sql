USE [TrainingFeedback]
GO

/****** Object:  Table [dbo].[Supervisor_List]    Script Date: 10/26/2015 17:27:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Supervisor_List](
	[course_id] [varchar](50) NULL,
	[Session_id] [varchar](50) NULL,
	[Employee_id] [varchar](7) NULL,
	[createdby] [varchar](7) NOT NULL,
	[createddate] [datetime] NULL,
	[modifiedby] [varchar](7) NULL,
	[modifieddate] [datetime] NULL,
	[IsDeleted] [int] NULL,
	[IsExistsAlready] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


