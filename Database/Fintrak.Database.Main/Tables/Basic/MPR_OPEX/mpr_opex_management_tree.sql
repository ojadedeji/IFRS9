CREATE TABLE [dbo].[mpr_opex_management_tree]
(
	[OpexMgtTreeId] INT NOT NULL IDENTITY,
	[CostCentreMISCode] VARCHAR(50) NOT NULL,
	[TeamDefinitionCode] Varchar(50) NOT NULL, 
    [TeamCode] VARCHAR(50) NOT NULL, 
	[AccountOfficerDefinitionCode] Varchar(50) NULL, 
	[AccountOfficerCode] VARCHAR(50) NULL, 
	[Ratio] float NULL DEFAULT 0, 
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_opex_management_tree] PRIMARY KEY ([OpexMgtTreeId]) 
)

GO
