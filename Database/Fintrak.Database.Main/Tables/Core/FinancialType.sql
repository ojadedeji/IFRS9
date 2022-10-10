CREATE TABLE [dbo].[cor_financial_type]
(
	[FinancialTypeId] INT NOT NULL IDENTITY,
	[Code] VARCHAR(20) NOT NULL,  
    [Name] VARCHAR(200) NOT NULL,
	[ParentId]    INT null,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,      
    CONSTRAINT [PK_cor_financial_type] PRIMARY KEY ([Code])
)

GO
