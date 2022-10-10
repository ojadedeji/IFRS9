CREATE TABLE [dbo].[mpr_usermis]
(
	[UserMisId] INT NOT NULL IDENTITY,
	[LoginID] VARCHAR(200) NOT NULL,  
    [ProfitCenterDefinitionCode] VARCHAR(50) NULL, 
    [ProfitCenterMisCode] VARCHAR(50) NULL, 
	[CostCenterDefinitionCode] VARCHAR(50) NULL,
	[CostCenterMisCode] VARCHAR(50) NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_usermis] PRIMARY KEY ([UserMisId]), 
    CONSTRAINT [AK_mpr_usermis_code] UNIQUE ([LoginID])
)