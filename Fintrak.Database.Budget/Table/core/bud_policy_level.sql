CREATE TABLE [dbo].[bud_policy_level]
(
	[PolicyLevelId] INT NOT NULL IDENTITY, 
	[PolicyCode] VARCHAR(50) NOT NULL, 
    [ModuleCode] VARCHAR(50) NOT NULL, 
    [DefinitionCode] VARCHAR(50) NOT NULL, 
	[Center] INT NOT NULL, 
	[ReviewCode] VARCHAR(50) NOT NULL, 
	[Year] VARCHAR(50) NOT NULL, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_policy_level] PRIMARY Key([PolicyLevelId]), 
    CONSTRAINT [AK_bud_policy_level] UNIQUE ([ModuleCode],[PolicyCode],[DefinitionCode],[Center],[ReviewCode],[Year]) 
)
