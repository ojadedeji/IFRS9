/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_collateral_information]
(
	[CollateralInformationId] INT NOT NULL IDENTITY, 
    [RefNo] VARCHAR(50) NOT NULL, 
    [AccountNo] VARCHAR(100) NOT NULL, 
	[Category] varchar(100) NOT NULL, 
	[Type] varchar(100) NOT NULL, 
	[CustomerName] varchar(150) NOT NULL, 
	[Amount] DECIMAL(18,6) NOT NULL, 
	[CompanyCode] varchar(10) NULL,    
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_collateral_information] PRIMARY KEY ([CollateralInformationId]) 
    
)
