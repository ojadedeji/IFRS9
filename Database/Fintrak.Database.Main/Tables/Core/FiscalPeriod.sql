CREATE TABLE [dbo].[cor_fiscalperiod]
(
	[FiscalPeriodId] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(200) NOT NULL, 
    [StartDate] DATE NOT NULL, 
	[EndDate] DATE NOT NULL, 
	[FiscalYearId] INT NOT NULL, 
	[Closed] BIT NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_fiscalperiod] PRIMARY KEY ([FiscalPeriodId]), 
    CONSTRAINT [AK_cor_fiscalperiod_name] UNIQUE ([Name],[FiscalYearId]) 
)

GO
