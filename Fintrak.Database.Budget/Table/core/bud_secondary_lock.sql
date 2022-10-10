CREATE TABLE [dbo].[bud_secondary_lock]
(
	[SecondaryLockId] INT NOT NULL IDENTITY, 
    [ModuleCode] VARCHAR(50) NOT NULL, 
    [DefinitionCode] VARCHAR(50) NOT NULL, 
	[MisCode] VARCHAR(50) NOT NULL, 
	[Note] VARCHAR(500) NULL, 
	[Year] VARCHAR(50) NOT NULL, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_secondary_lock] PRIMARY KEY ([SecondaryLockId]), 
    CONSTRAINT [AK_bud_secondary_lock] UNIQUE ([ModuleCode],[DefinitionCode],[MisCode],[Year]) 
)
