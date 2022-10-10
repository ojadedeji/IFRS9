GO

CREATE TABLE [dbo].[ifrs_budget](
	[IFRSBudgetId] [int] IDENTITY(1,1) NOT NULL,
	[CaptionName] [varchar](200) NOT NULL,
	[ReportDate] [date] NOT NULL,
	[StretchBudget] [decimal](18, 6) NOT NULL,
	[BoardBudget] [decimal](18, 6) NOT NULL,
	[CompanyCode] varchar(10) NULL,   
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedOn] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_ifrs_budget] PRIMARY KEY CLUSTERED 
(
	[IFRSBudgetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ifrs_budget_CaptionDate] UNIQUE NONCLUSTERED 
(
	[CaptionName] ASC,
	[ReportDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



