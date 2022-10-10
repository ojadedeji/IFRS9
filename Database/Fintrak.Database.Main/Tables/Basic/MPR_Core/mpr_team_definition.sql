CREATE TABLE [dbo].[mpr_team_definition]
(
	[TeamDefinitionId] INT NOT NULL IDENTITY,
	[Code] VARCHAR(50) NOT NULL,
	[Name] VARCHAR(100) NOT NULL,
    [Position] int NOT NULL, 
    [Year] VARCHAR(50) NOT NULL, 
	[CanClassified] bit NULL,
	[CompanyCode] VARCHAR(50) NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [AK_mpr_team_definition_name] UNIQUE ([Year] desc,[Name]), 
    CONSTRAINT [AK_mpr_team_definition_position] UNIQUE ([Year] desc,[Position]), 
    CONSTRAINT [PK_mpr_team_definition] PRIMARY KEY ([Year],[Code])
)

GO
