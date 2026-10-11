USE [TrainingFeedback_Dev]
GO

/****** Object:  Table [dbo].[TRAININGHOURS]    Script Date: 12/08/2014 18:58:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TRAININGHOURS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [varchar](500) NULL,
	[Category] [varchar](500) NULL,
	[Type] [varchar](500) NULL,
	[Sub Type] [varchar](500) NULL,
	[Emp No] [varchar](500) NULL,
	[Owning Unit] [varchar](500) NULL,
	[Department] [varchar](500) NULL,
	[Owning Vertical] [varchar](500) NULL,
	[Execution Unit] [varchar](500) NULL,
	[Execution Vertical] [varchar](500) NULL,
	[Attended Yes/No] [varchar](500) NULL,
	[No of  Training Hours] [varchar](500) NULL,
	[CREATEDBY] [varchar](100) NULL,
	[EmployeeID] [varchar](100) NULL,
	[FileLoadedDate] [datetime] NULL,
	[MONTH] [varchar](10) NULL,
	[YEAR] [varchar](10) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


