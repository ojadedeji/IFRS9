CREATE TABLE [dbo].[cdqm_product]
(
	[ProductId] INT NOT NULL IDENTITY,
	[ProductCode]  VARCHAR(255) NOT NULL,
	[ProductName] varchar(255)  NULL,
	[IsCardable]  bit  NULL DEFAULT 0,
	[CustomerType]  VARCHAR(255)  NULL,
	[MinimumAge] Int  NULL DEFAULT 0, 
	[MaximumAge] Int  NULL DEFAULT 0, 
	[ProductSegment]  VARCHAR(255)  NULL,
	[CustomerMIS]  VARCHAR(255)  NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cdqm_product] PRIMARY KEY ([ProductId]) 
)

GO
