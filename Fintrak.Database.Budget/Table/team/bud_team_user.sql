CREATE TABLE [dbo].[bud_team_user]
(
	[TeamUserId] INT NOT NULL IDENTITY, 
    [LoginID] VARCHAR(50) NOT NULL, 
	[PCDefinitionCode] VARCHAR(50) NULL, 
    [PCDefinitionMisCode] VARCHAR(100) NULL, 
	[CCDefinitionCode] VARCHAR(50) NULL, 
    [CCDefinitionMisCode] VARCHAR(100) NULL, 
	[IsLock] BIT NULL DEFAULT 0, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_team_user] PRIMARY KEY ([TeamUserId]), 
    CONSTRAINT [AK_bud_team_user] UNIQUE ([LoginID]) 
)
