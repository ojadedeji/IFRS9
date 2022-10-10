CREATE TABLE [dbo].[mpr_expense_product_mapping]
(
	[ExpenseProductId] INT NOT NULL IDENTITY,
	[BasisCode] varchar(50) NOT NULL,  
	[ProductCode] Varchar(10) NULL,	
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,   
    CONSTRAINT [PK_mpr_expense_product_mapping] PRIMARY KEY ([ExpenseProductId]), 
	CONSTRAINT [FK_mpr_expense_product_mapping_Product] FOREIGN KEY ([ProductCode]) REFERENCES [cor_product]([Code]) ,
    CONSTRAINT [FK_mpr_expense_product_mapping_basis] FOREIGN KEY ([BasisCode]) REFERENCES [mpr_expense_basis]([Code]) 
)

GO
