CREATE TABLE [dbo].[mpr_team_classification_type]
(
	[TeamClassificationTypeId] INT NOT NULL IDENTITY,
	[Code] VARCHAR(50) NULL, 
	[Name] VARCHAR(200) NULL, 
    [Description] VARCHAR(200) NULL, 
	[Year] VARCHAR(50) NOT NULL,
	[CompanyCode] VARCHAR(50) NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_team_classification_type] PRIMARY KEY ([TeamClassificationTypeId]), 
    CONSTRAINT [AK_mpr_team_classification_type_name] UNIQUE ([Name],[Year]), 
    CONSTRAINT [AK_mpr_team_classification_type_code] UNIQUE ([Code],[Year])
)

GO
