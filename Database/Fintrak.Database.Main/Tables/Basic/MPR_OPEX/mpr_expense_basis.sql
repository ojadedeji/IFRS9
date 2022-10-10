CREATE TABLE [dbo].[mpr_expense_basis]
(
	[ExpenseBasisId] INT NOT NULL IDENTITY,
	[Code] varchar(50) NOT NULL,  
	[Name] Varchar(100) NULL,
	[ItemType] [int] NOT NULL,
	[TeamDefinitionCode] [varchar](50) NULL,
	[ValueType] [int] NULL,
	[Category] [int] NULL,
	[CompanyCode] [varchar](10) NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL DEFAULT 'auto', 
    [CreatedOn] DATETIME NULL DEFAULT getdate(), 
    [UpdatedBy] VARCHAR(50) NULL DEFAULT 'auto', 
    [UpdatedOn] DATETIME NULL DEFAULT getdate(), 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_expense_basis] PRIMARY KEY ([Code])   
)

GO
