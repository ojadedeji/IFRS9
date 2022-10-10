CREATE TABLE [dbo].[cor_chartofacct]
(
	[ChartOfAccountId] INT NOT NULL IDENTITY, 
    [AccountType] INT NOT NULL, 
    [AccountCode] VARCHAR(50) NOT NULL, 
	[AccountName] VARCHAR(150) NOT NULL, 
	[FinancialTypeId] INT NOT NULL, 
    [ParentId] INT NULL, 
    [IFRS] VARCHAR(100) NULL, 
	[Position] INT NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_chartofacct] PRIMARY KEY ([ChartOfAccountId]), 
   -- CONSTRAINT [FK_cor_chartofacct_financialtype] FOREIGN KEY ([FinancialTypeId]) REFERENCES [cor_financial_type]([FinancialTypeId])
)
