CREATE TABLE [dbo].[bud_team_classification]
(
	[TeamClassificationId] INT NOT NULL IDENTITY, 
    [Code] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
	[ClassificationTypeCode] VARCHAR(10) NOT NULL, 
	[Year] VARCHAR(20) NOT NULL, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_team_classification] PRIMARY KEY ([TeamClassificationId]), 
    CONSTRAINT [AK_bud_team_classification_code] UNIQUE ([Code],[ClassificationTypeCode],[Year]), 
    CONSTRAINT [AK_bud_team_classification_name] UNIQUE ([Name],[ClassificationTypeCode],[Year]) 
)
