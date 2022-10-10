CREATE TABLE [dbo].[mpr_opex_report](
	[ReportId] [int] IDENTITY(1,1) NOT NULL,
	[GLCode] [varchar](50) NOT NULL,
	[BranchCode] [varchar](50) NULL,
	[Amount] [decimal](18, 6) NULL,
	[Currency] [varchar](50) NULL,
	[CompanyCode] [varchar](50) NULL,
	[RunDate] [date] NOT NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedOn] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_mpr_opex_report] PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
