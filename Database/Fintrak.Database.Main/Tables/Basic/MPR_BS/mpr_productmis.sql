CREATE TABLE [dbo].[mpr_productmis]
(
	[ProductMISId] INT NOT NULL IDENTITY,
	[ProductCode] VARCHAR(10) NOT NULL,
	[CaptionCode] Varchar(50) NOT NULL, 
	[TeamDefinitionCode] Varchar(50) NOT NULL, 
	[AccountOfficerDefinitionCode] Varchar(50) NULL,  
    [TeamCode] VARCHAR(50) NOT NULL, 
	[AccountOfficerCode] VARCHAR(50) NULL, 
	[Year] Varchar(50) NOT NULL, 
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_productmis] PRIMARY KEY ([ProductMISId]),
	CONSTRAINT [FK_mpr_productmis_Product] FOREIGN KEY ([ProductCode]) REFERENCES [cor_product]([Code])
)

GO
