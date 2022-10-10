/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_collateral_category]
(
	[CollateralCategoryId] INT NOT NULL IDENTITY, 
    [Code] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(200) NOT NULL, 
	[CompanyCode] varchar(10) NULL,    
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_collateral_category] PRIMARY KEY ([CollateralCategoryId]), 
    CONSTRAINT [AK_ifrs_collateral_category_code] UNIQUE ([Code],[CompanyCode]), 
    CONSTRAINT [AK_ifrs_collateral_category_name] UNIQUE ([Name],[CompanyCode])
    
)
