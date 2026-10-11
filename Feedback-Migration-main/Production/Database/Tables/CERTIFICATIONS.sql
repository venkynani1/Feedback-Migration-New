USE [TrainingFeedback_Dev]
GO

/****** Object:  Table [dbo].[CERTIFICATIONS]    Script Date: 12/08/2014 18:56:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CERTIFICATIONS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [varchar](500) NULL,
	[Employee id] [varchar](500) NULL,
	[Category] [varchar](500) NULL,
	[Type] [varchar](500) NULL,
	[Certification Name] [varchar](500) NULL,
	[DEPT] [varchar](500) NULL,
	[OWNING UNIT] [varchar](500) NULL,
	[OWNING VERTICAL] [varchar](500) NULL,
	[Execution Unit] [varchar](500) NULL,
	[Execution  Vertical] [varchar](500) NULL,
	[CREATEDBY] [varchar](100) NULL,
	[FileLoadedDate] [datetime] NULL,
	[MONTH] [varchar](10) NULL,
	[YEAR] [varchar](10) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


