/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_collateral_type]
(
	[CollateralTypeId] INT NOT NULL IDENTITY, 
    [Code] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(200) NOT NULL, 
	[CategoryCode] varchar(10) NOT NULL, 
	[CompanyCode] varchar(10) NULL,    
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_collateral_type] PRIMARY KEY ([CollateralTypeId]), 
    CONSTRAINT [AK_ifrs_collateral_type_code] UNIQUE ([Code],[CompanyCode]), 
    CONSTRAINT [AK_ifrs_collateral_type_name] UNIQUE ([Name],[CompanyCode])
    
)
