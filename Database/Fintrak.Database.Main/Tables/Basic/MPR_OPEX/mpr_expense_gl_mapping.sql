CREATE TABLE [dbo].[mpr_expense_gl_mapping]
(
	[ExpenseGLId] INT NOT NULL IDENTITY,
	[BasisCode] varchar(50) NOT NULL,  
	[GLCode] Varchar(10) NULL,	
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,   
    CONSTRAINT [PK_mpr_expense_gl_mapping] PRIMARY KEY ([ExpenseGLId]), 	
    CONSTRAINT [FK_mpr_expense_gl_mapping_basis] FOREIGN KEY ([BasisCode]) REFERENCES [mpr_expense_basis]([Code]) 
)

GO
