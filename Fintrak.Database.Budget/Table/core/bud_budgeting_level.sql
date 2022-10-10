CREATE TABLE [dbo].[bud_budgeting_level]
(
	[BudgetingLevelId] INT NOT NULL IDENTITY, 
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
    CONSTRAINT [PK_bud_budgeting_level] PRIMARY Key([BudgetingLevelId]), 
    CONSTRAINT [AK_bud_budgeting_level] UNIQUE ([ModuleCode],[DefinitionCode],[Center],[ReviewCode],[Year]) 
)
