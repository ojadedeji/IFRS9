CREATE TABLE [dbo].[mpr_non_product_rate]
(
	[NonProductRateId] INT NOT NULL IDENTITY,
	[ProductCode] VARCHAR(10) NOT NULL,
	[Year] Varchar(50) NOT NULL,
	[Period] Varchar(50) NOT NULL,
	[Rate]  FLOAT NOT NULL, 
	[CompanyCode] varchar(10) NULL,       
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_non_product_rate] PRIMARY KEY ([NonProductRateId]), 
    CONSTRAINT [FK_mpr_non_product_rate_Product] FOREIGN KEY ([ProductCode]) REFERENCES [cor_product]([Code]) ,
    CONSTRAINT [AK_mpr_non_product_rate_product] UNIQUE ([ProductCode],[Period],[Year])
)

GO
