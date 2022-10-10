CREATE TABLE [dbo].[mpr_non_product_map]
(
	[NonProductMapId] INT NOT NULL IDENTITY,
	[NonProductCode] VARCHAR(10) NOT NULL,
	[ProductCode]  VARCHAR(10) NULL,
	[CaptionCode] VARCHAR(50) NOT NULL,	   
	[CompanyCode] varchar(10) NULL,     
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL DEFAULT 'auto', 
    [CreatedOn] DATETIME NULL DEFAULT getdate(), 
    [UpdatedBy] VARCHAR(50) NULL DEFAULT 'auto', 
    [UpdatedOn] DATETIME NULL DEFAULT getdate(), 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_non_product_map] PRIMARY KEY ([NonProductMapId]), 
    CONSTRAINT [FK_mpr_non_product_map_nonproduct] FOREIGN KEY ([NonProductCode]) REFERENCES [cor_product]([Code]) ,
    CONSTRAINT [AK_mpr_non_product_map_product] UNIQUE ([NonProductCode],[CaptionCode]) 
)

GO
