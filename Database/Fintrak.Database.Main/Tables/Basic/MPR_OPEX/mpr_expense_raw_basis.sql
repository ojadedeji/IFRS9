CREATE TABLE [dbo].[mpr_expense_raw_basis]
(
	[ExpenseRawBasisId] INT NOT NULL IDENTITY,
	[BasisCode] varchar(50) NOT NULL,  
	[MISCode] Varchar(50) NULL,	
	[Weight] FLOAT NULL,	
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,   
    CONSTRAINT [PK_mpr_expense_raw_basis] PRIMARY KEY ([ExpenseRawBasisId])
)

GO
