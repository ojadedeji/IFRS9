CREATE TABLE [dbo].[bud_team_classification_type]
(
	[TeamClassificationTypeId] INT NOT NULL IDENTITY, 
    [Code] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
	[Position] INT NOT NULL, 
	[Year] VARCHAR(20) NOT NULL, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_team_classification_type] PRIMARY KEY ([TeamClassificationTypeId]), 
    CONSTRAINT [AK_bud_team_classification_type_code] UNIQUE ([Code],[Year]), 
    CONSTRAINT [AK_bud_team_classification_type_name] UNIQUE ([Name],[Year]), 
    CONSTRAINT [AK_bud_team_classification_type_position] UNIQUE ([Position],[Year]) 
)
