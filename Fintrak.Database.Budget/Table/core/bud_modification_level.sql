CREATE TABLE [dbo].[bud_modification_level]
(
	[ModificationLevelId] INT NOT NULL IDENTITY, 
    [ModuleCode] VARCHAR(50) NOT NULL, 
    [DefinitionCode] VARCHAR(50) NOT NULL, 
	[Status] BIT NULL DEFAULT 1, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_modification_level] PRIMARY KEY ([ModificationLevelId]), 
    CONSTRAINT [AK_bud_modification_level] UNIQUE ([ModuleCode],[DefinitionCode]) 
)
