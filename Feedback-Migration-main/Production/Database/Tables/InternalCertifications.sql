USE [TrainingFeedback_Dev]
GO

/****** Object:  Table [dbo].[InternalCertifications]    Script Date: 12/08/2014 18:57:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[InternalCertifications](
	[S_NO] [int] IDENTITY(1,1) NOT NULL,
	[emp id] [varchar](10) NULL,
	[employee name] [varchar](max) NULL,
	[quiz name] [varchar](500) NULL,
	[time start] [varchar](500) NULL,
	[grade\100] [varchar](500) NULL,
	[quiz status] [varchar](500) NULL,
	[Department] [varchar](500) NULL,
	[Executing Unit] [varchar](500) NULL,
	[Owning Vertical] [varchar](500) NULL,
	[LPAccessReportdate] [datetime] NULL,
	[MONTH] [varchar](10) NULL,
	[YEAR] [varchar](10) NULL,
	[CREATEDBY] [varchar](50) NULL,
 CONSTRAINT [PK_InternalCertifications] PRIMARY KEY CLUSTERED 
(
	[S_NO] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


