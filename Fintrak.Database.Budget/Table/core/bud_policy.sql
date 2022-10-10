CREATE TABLE [dbo].[bud_policy]
(
	[PolicyId] INT NOT NULL IDENTITY, 
    [Code] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
	[ModuleCode] VARCHAR(50) NOT NULL, 
	[Status] BIT NOT NULL DEFAULT 0, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_policy] PRIMARY Key([PolicyId]), 
    CONSTRAINT [AK_bud_policy_code] UNIQUE ([Code],[ModuleCode]) ,
	CONSTRAINT [AK_bud_policy_name] UNIQUE ([Name],[ModuleCode]) 
)
