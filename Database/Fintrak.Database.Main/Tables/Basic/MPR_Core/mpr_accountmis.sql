CREATE TABLE [dbo].[mpr_accountmis]
(
	[AccountMISId] INT NOT NULL IDENTITY,
	[AccountNo] VARCHAR(50) NOT NULL,
	[TeamDefinitionCode] VARCHAR(50) NOT NULL, 
    [TeamCode] VARCHAR(50) NOT NULL, 
	[AccountOfficerDefinitionCode] VARCHAR(50) NULL,  
	[AccountOfficerCode] VARCHAR(50) NULL,  
	[Year] VARCHAR(50) NOT NULL,  
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_accountmis] PRIMARY KEY ([AccountMISId])
)

GO
