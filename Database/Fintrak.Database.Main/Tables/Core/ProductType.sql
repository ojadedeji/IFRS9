CREATE TABLE [dbo].[cor_producttype]
(
	[ProductTypeId] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(200) NOT NULL, 
    [Description] VARCHAR(300) NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_producttype] PRIMARY KEY ([ProductTypeId]), 
    CONSTRAINT [CK_cor_producttype_Name] UNIQUE ([Name]
	) 
)

GO
