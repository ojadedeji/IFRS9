CREATE TABLE [dbo].[mpr_branchdefaultmis]
(
	[BranchDefaultMISId] INT NOT NULL IDENTITY,
	[BranchCode] varchar(20) NOT NULL,
	[DefinitionCode] VARCHAR(50) NOT NULL, 
    [MisCode] VARCHAR(50) NOT NULL, 
	[Year] VARCHAR(50) NOT NULL, 
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_branchdefaultmis] PRIMARY KEY ([BranchDefaultMISId]), 
    CONSTRAINT [AK_mpr_branchdefaultmis_team] UNIQUE ([BranchCode],[DefinitionCode],[MISCode],[Year]) 
)

GO
