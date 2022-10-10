CREATE TABLE [dbo].[mpr_pl_income_report_adjustment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TeamCode] [varchar](50) NULL,
	[AccountOfficerCode] [varchar](50) NULL,
	[Narrative] [varchar](150) NULL,
	[BranchCode] [varchar](50) NULL,
	[GLCode] [varchar](20) NULL,
	[Caption] [varchar](150) NULL,
	[RelatedAccount] [varchar](50) NULL,
	[Amount] [decimal](18, 6) NULL,
	[RunDate] [date] NULL,
	[CompanyCode] [varchar](10) NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_pl_income_report_adjustment] PRIMARY KEY ([id])
)


