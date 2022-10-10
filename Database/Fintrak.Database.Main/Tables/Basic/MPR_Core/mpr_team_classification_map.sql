CREATE TABLE [dbo].[mpr_team_classification_map]
(
	[TeamClassificationMapId] INT NOT NULL IDENTITY,
	[DefinitionCode] VARCHAR(50) NOT NULL,
	[MisCode] VARCHAR(50) NOT NULL,
	[ClassificationCode] VARCHAR(50) NOT NULL,	
	[Year] VARCHAR(50) NOT NULL,	
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_team_classification_map] PRIMARY KEY ([TeamClassificationMapId]), 
    CONSTRAINT [AK_mpr_team_classification_map_team_classification] UNIQUE ([MisCode],[DefinitionCode],[ClassificationCode],[Year])
)

GO
