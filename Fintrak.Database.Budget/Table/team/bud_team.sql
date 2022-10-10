CREATE TABLE [dbo].[bud_team]
(
	[TeamId] INT NOT NULL IDENTITY, 
    [Code] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
	[ParentCode] VARCHAR(50) NULL, 
	[DefinitionCode] VARCHAR(50) NOT NULL, 
	[ClassificationCode] VARCHAR(50) NULL, 
	[IsDefaultOfficer] BIT NULL DEFAULT 0, 
	[Year] VARCHAR(20) NOT NULL, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_team] PRIMARY KEY ([TeamId]), 
    CONSTRAINT [AK_bud_team_code] UNIQUE ([Code],[DefinitionCode],[Year]), 
    CONSTRAINT [AK_bud_team_name] UNIQUE ([Name],[DefinitionCode],[Year]) 
)
