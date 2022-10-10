CREATE TABLE [dbo].[mpr_pl_income_report](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TransId] [varchar](50) NULL,
	[transdate] [date] NULL,
	[Narrative] [varchar](150) NULL,
	[TeamCode] [varchar](50) NULL,
	[AccountOfficerCode] [varchar](50) NULL,
	[BranchCode] [varchar](50) NULL,
	[GLCode] [varchar](20) NULL,
	[GLAccount] [varchar](50) NULL,
	[Caption] [varchar](150) NULL,
	[RelatedAccount] [varchar](50) NULL,
	[AccountTitle] [varchar](100) NULL,
	[CustCode] [varchar](50) NULL,
	[ProductCode] [varchar](50) NULL,
	[Amount] [decimal](18, 6) NULL,
	[Period] [int] NULL,
	[Year] [varchar](20) NULL,
	[EntryStatus] [varchar](100) NULL DEFAULT 'As Transaction Is',
	[RunDate] [date] NULL,
	[MartDate] [date] NULL,
	[StaffID] [varchar](50) NULL,
	[CompanyCode] [varchar](10) NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedOn] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL, 
    CONSTRAINT [PK_mpr_pl_income_report] PRIMARY KEY ([id])
)

GO


CREATE INDEX [IX_mpr_pl_income_report_I] ON [dbo].[mpr_pl_income_report] ([RunDate] DESC)
