/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_collateral_realization_period]
(
	[CollateralRealizationPeriodId] INT NOT NULL IDENTITY, 
    [TypeCode] VARCHAR(50) NOT NULL, 
    [Duration] INT NOT NULL, 
	[CompanyCode] varchar(10) NULL,    
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_collateral_realization_period] PRIMARY KEY ([CollateralRealizationPeriodId]), 
    CONSTRAINT [AK_ifrs_collateral_realization_period_code] UNIQUE ([TypeCode],[CompanyCode]) 
    
)
