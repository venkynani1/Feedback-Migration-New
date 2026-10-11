USE [TrainingFeedback]
GO

/****** Object:  Table [dbo].[Feedback_supervisor]    Script Date: 10/26/2015 17:28:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Feedback_supervisor](
	[Course_id] [varchar](100) NULL,
	[Session_id] [varchar](200) NULL,
	[Supervisor_id] [varchar](20) NULL,
	[Subordinate_id] [varchar](20) NULL,
	[WorkArea] [varchar](max) NULL,
	[PrbSA] [varchar](2) NULL,
	[PrgSkills] [varchar](2) NULL,
	[CommSkills] [varchar](2) NULL,
	[BehSkills] [varchar](2) NULL,
	[Performance] [varchar](2) NULL,
	[OvrAllRating] [varchar](2) NULL,
	[Training_Required] [int] NULL,
	[Overall_Feedback] [varchar](max) NULL,
	[Status] [int] NULL,
	[D_Feedback_Date] [datetime] NULL,
	[PM_ID] [varchar](10) NULL,
	[PM_Remarks] [varchar](max) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


