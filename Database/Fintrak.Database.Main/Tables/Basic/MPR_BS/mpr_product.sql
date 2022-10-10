CREATE TABLE [dbo].[mpr_product]
(
	[ProductId] INT NOT NULL IDENTITY,
	[ProductCode]  VARCHAR(10) NOT NULL,
	[CaptionCode] varchar(50) NOT NULL,
	[VolumeGL]  VARCHAR(100)  NULL,
	[InterestGL]  VARCHAR(100)  NULL,
	[Budgetable] BIT NOT NULL, 
	[IsNotional] BIT NOT NULL, 
	[CompanyCode] varchar(10) NULL,       
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    [Rate] FLOAT NULL, 
    CONSTRAINT [PK_mpr_product] PRIMARY KEY ([ProductId]) ,
	CONSTRAINT [FK_mpr_Product_ProductCode] FOREIGN KEY ([ProductCode]) REFERENCES [cor_product]([Code]) ,
    CONSTRAINT [AK_mpr_product_product] UNIQUE ([ProductCode],[CaptionCode]) 
)

GO
