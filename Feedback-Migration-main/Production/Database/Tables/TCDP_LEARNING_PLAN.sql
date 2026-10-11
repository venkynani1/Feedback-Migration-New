USE [TrainingFeedback_Dev]
GO

/****** Object:  Table [dbo].[TCDP_LEARNING_PLAN]    Script Date: 12/08/2014 18:58:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TCDP_LEARNING_PLAN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](20) NULL,
	[Department] [varchar](500) NULL,
	[Exec Unit Project] [varchar](500) NULL,
	[First Name] [varchar](500) NULL,
	[Last Name] [varchar](500) NULL,
	[Learning Program Title] [varchar](500) NULL,
	[Learning Program ID] [varchar](500) NULL,
	[Learning Program First Access Date] [varchar](500) NULL,
	[Learning Program Last Access Date] [varchar](500) NULL,
	[Enrollment Date] [varchar](500) NULL,
	[Learning Program Completion Date] [varchar](500) NULL,
	[Learning Program Status] [varchar](500) NULL,
	[Percent Complete] [varchar](500) NULL,
	[Owning Unit] [varchar](500) NULL,
	[Role] [varchar](500) NULL,
	[Technology] [varchar](500) NULL,
	[Location] [varchar](500) NULL,
	[Owning Vertical] [varchar](500) NULL,
	[Grade] [varchar](500) NULL,
	[Exec Vertical Project] [varchar](500) NULL,
	[MONTH] [varchar](10) NULL,
	[YEAR] [varchar](10) NULL,
	[CREATEDBY] [varchar](20) NULL,
	[LastAccessedOn] [datetime] NULL,
 CONSTRAINT [PK_TCDP_LEARNING_PLAN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


