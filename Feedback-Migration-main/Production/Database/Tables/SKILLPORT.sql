USE [TrainingFeedback_Dev]
GO

/****** Object:  Table [dbo].[SKILLPORT]    Script Date: 12/08/2014 18:58:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SKILLPORT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Learning Program Title] [varchar](500) NULL,
	[Learning Program ID] [varchar](500) NULL,
	[Learning Program Status] [varchar](500) NULL,
	[Group Name] [varchar](500) NULL,
	[Group Org Code] [varchar](500) NULL,
	[Username] [varchar](500) NULL,
	[First Name] [varchar](500) NULL,
	[Last Name] [varchar](500) NULL,
	[Learning Program First Access Date] [varchar](500) NULL,
	[Learning Program Last Access Date] [varchar](500) NULL,
	[Learning Program Completion Date] [varchar](500) NULL,
	[Learning Program Enrollment Date] [varchar](500) NULL,
	[Learning Program Percent Complete] [varchar](500) NULL,
	[Container Title] [varchar](500) NULL,
	[Asset Title] [varchar](500) NULL,
	[Asset ID] [varchar](500) NULL,
	[Asset Category] [varchar](500) NULL,
	[Times Restarted] [varchar](500) NULL,
	[First Access Date] [varchar](500) NULL,
	[Last Access Date] [varchar](500) NULL,
	[Completion Status] [varchar](500) NULL,
	[Completion Date] [varchar](500) NULL,
	[Times Accessed] [varchar](500) NULL,
	[Pre-test] [varchar](500) NULL,
	[High Score] [varchar](500) NULL,
	[Last Score] [varchar](500) NULL,
	[Expected Duration] [varchar](500) NULL,
	[Actual Duration] [varchar](500) NULL,
	[Approval Manager ID] [varchar](500) NULL,
	[Approval Manager First Name] [varchar](500) NULL,
	[Approval Manager Last Name] [varchar](500) NULL,
	[Email Address] [varchar](500) NULL,
	[Owning Unit] [varchar](500) NULL,
	[Role] [varchar](500) NULL,
	[Technology] [varchar](500) NULL,
	[Department] [varchar](500) NULL,
	[PM Name] [varchar](500) NULL,
	[Location] [varchar](500) NULL,
	[Owning Vertical] [varchar](500) NULL,
	[Birthdate] [varchar](500) NULL,
	[Grade] [varchar](500) NULL,
	[Exec Vertical Project] [varchar](500) NULL,
	[Exec Unit Project] [varchar](500) NULL,
	[CC Expr] [varchar](500) NULL,
	[free1] [varchar](500) NULL,
	[EmployeeID] [varchar](100) NULL,
	[LPAccessReportdate] [datetime] NULL,
	[MONTH] [varchar](10) NULL,
	[YEAR] [varchar](10) NULL,
	[CREATEDBY] [varchar](100) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


