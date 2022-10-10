CREATE TABLE [dbo].[bud_primary_lock]
(
	[PrimaryLockId] INT NOT NULL IDENTITY, 
    [DefinitionCode] VARCHAR(50) NOT NULL, 
	[MisCode] VARCHAR(50) NOT NULL, 
	[Note] VARCHAR(500) NULL, 
	[Lock] BIT NULL DEFAULT 0, 
	[CanOverride] BIT NULL DEFAULT 0, 
	[Year] VARCHAR(50) NOT NULL, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_primary_lock] PRIMARY KEY ([PrimaryLockId]), 
    CONSTRAINT [AK_bud_primary_lock] UNIQUE ([DefinitionCode],[MisCode],[Year]) 
)
