CREATE TABLE [dbo].[mpr_memo_product_map]
(
	[MemoProductMapId] INT NOT NULL IDENTITY,
	[ProductCode] VARCHAR(50) NOT NULL,
	[Code] VARCHAR(50) NOT NULL,  
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_memo_product] PRIMARY KEY ([MemoProductMapId])
)

GO
