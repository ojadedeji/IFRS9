CREATE TABLE [dbo].[mpr_team]
(
	[TeamId] INT NOT NULL IDENTITY,
	[Code] VARCHAR(50) NOT NULL,  
    [Name] VARCHAR(200) NOT NULL, 
    [ParentCode] VARCHAR(50) NULL, 
	[DefinitionCode] VARCHAR(50) not NULL,
	[CompanyCode] VARCHAR(50) not NULL,
	[Year] VARCHAR(50) not NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_team] PRIMARY KEY ([TeamId]), 
    CONSTRAINT [AK_mpr_team_code] UNIQUE ([Code],[DefinitionCode],[Year])
)

GO
