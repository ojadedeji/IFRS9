CREATE TABLE [dbo].[cor_product]
(
	[ProductId] INT NOT NULL IDENTITY,
	[Code] VARCHAR(10) NOT NULL,  
    [Name] VARCHAR(200) NOT NULL,  
	[AssetGL] VARCHAR(100) NULL, 
	[LiabilityGL] VARCHAR(100) NULL, 
	[IncomeGL] VARCHAR(100) NULL, 
	[ExpenseGL] VARCHAR(100) NULL, 
	[IsSwitch] BIT NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_product] PRIMARY KEY ([ProductId]) ,
	CONSTRAINT [CK_cor_product_code] UNIQUE (Code) 
)

GO
