CREATE TABLE [dbo].[mpr_balancesheet_budget_officer]
(
	[BalancesheetBudgetOffId] INT NOT NULL IDENTITY,
	[CompanyCode] varchar(10) NULL,  
	[AccountOfficerCode] VARCHAR(200)  NULL,
	[Year] VARCHAR(20)  NULL,
	[CaptionName] varchar(200) NULL,
	[Mth1] [decimal](18, 2) NOT NULL DEFAULT 0,
	[Mth2] [decimal](18, 2) NOT NULL DEFAULT 0,
	[Mth3] [decimal](18, 2) NOT NULL DEFAULT 0,
	[Mth4] [decimal](18, 2) NOT NULL DEFAULT 0,
	[Mth5] [decimal](18, 2) NOT NULL DEFAULT 0,
	[Mth6] [decimal](18, 2) NOT NULL DEFAULT 0,
	[Mth7] [decimal](18, 2) NOT NULL DEFAULT 0,
	[Mth8] [decimal](18, 2) NOT NULL DEFAULT 0,
	[Mth9] [decimal](18, 2) NOT NULL DEFAULT 0,
	[Mth10] [decimal](18, 2) NOT NULL DEFAULT 0,
	[Mth11] [decimal](18, 2) NOT NULL DEFAULT 0,
	[Mth12] [decimal](18, 2) NOT NULL DEFAULT 0, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_balancesheet_budget_officer] PRIMARY KEY ([BalancesheetBudgetOffId])  
)


GO
