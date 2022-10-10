CREATE TABLE [dbo].[bud_secondary_lock_level]
(
	[LockLevelId] INT NOT NULL IDENTITY, 
    [ModuleCode] VARCHAR(50) NOT NULL, 
    [DefinitionCode] VARCHAR(50) NOT NULL,  
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_secondary_lock_level] PRIMARY KEY ([LockLevelId]), 
    CONSTRAINT [AK_bud_secondary_lock_level] UNIQUE ([ModuleCode],[DefinitionCode]) 
)
