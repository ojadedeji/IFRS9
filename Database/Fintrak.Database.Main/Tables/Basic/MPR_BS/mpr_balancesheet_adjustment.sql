CREATE TABLE [dbo].[mpr_balancesheet_adjustment]
(
	[BalancesheetAdjustmentId] INT NOT NULL IDENTITY,
	[AccountNo] VARCHAR(50) NOT NULL, 
    [AccountName] VARCHAR(200)  NULL, 
	[TeamCode] VARCHAR(200)  NULL,
	[AccountOfficerCode] VARCHAR(200)  NULL,
	[ProductCode] varchar(50) NOT NULL,
	[Category] varchar(50) NOT NULL,
	[CurrencyType] varchar(5) NOT NULL,
	[ActualBal] decimal(18,6)  NULL,
	[AverageBal] decimal(18,6)  NULL,
	[Interest] decimal(18,6)  NULL,
	[RunDate] date  NULL,
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_balancesheet_adjustment] PRIMARY KEY ([BalancesheetAdjustmentId])  
)


GO
