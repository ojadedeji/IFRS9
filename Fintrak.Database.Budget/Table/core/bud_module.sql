CREATE TABLE [dbo].[bud_module]
(
	[ModuleId] INT NOT NULL IDENTITY, 
    [Code] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_module] PRIMARY KEY ([ModuleId]), 
    CONSTRAINT [AK_bud_module_code] UNIQUE ([Code]), 
    CONSTRAINT [AK_bud_module_name] UNIQUE ([Name]) 
)
