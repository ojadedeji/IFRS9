CREATE TABLE [dbo].[mpr_balancesheet_archive]
(
	[Id] INT NOT NULL IDENTITY,
	[AccountNo] VARCHAR(50) NOT NULL, 
    [AccountName] VARCHAR(200)  NULL, 
	[TeamCode] VARCHAR(200)  NULL,
	[AccountOfficerCode] VARCHAR(200)  NULL,
	[CaptionName] varchar(200) NULL,
	[BranchCode] varchar(50) NULL,
	[ProductCode] varchar(50) NOT NULL,
	[Category] varchar(50) NOT NULL,
	[CurrencyType] varchar(5) NOT NULL,
	[Currency] varchar(50)  NULL,
	[Balancesheettype] varchar(50)  NULL,
	[ActualBal] decimal(18,6)  NULL,
	[AverageBal] decimal(18,6)  NULL,
	[Interest] decimal(18,6)  NULL,
	[EffIntRate] FLOAT  NULL,
	[Pool] decimal(18,6)  NULL,
	[PoolRate] FLOAT  NULL,
	[ContractRate] FLOAT  NULL,
	[VolumeGL] varchar(50)  NULL, 
	[InterestGL] varchar(50)  NULL,
	[EntryStatus] varchar(200)  NULL DEFAULT 'As Transaction Is',
	[MaxRate] FLOAT  NULL,
	[PenalCharge] decimal(18,6)  NULL,
	[PenalRate] FLOAT  NULL,
	[AcctStatus] varchar(100)  NULL,
	[CreditRating] varchar(100)  NULL,
	[RunDate] date  NULL,
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_balancesheet_archive] PRIMARY KEY ([Id])  
)


GO


CREATE INDEX [IX_mpr_balancesheet_archive_I] ON [dbo].[mpr_balancesheet_archive] ([Rundate] DESC)
