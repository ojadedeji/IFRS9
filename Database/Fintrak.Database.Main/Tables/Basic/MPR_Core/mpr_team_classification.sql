CREATE TABLE [dbo].[mpr_team_classification]
(
	[TeamClassificationId] INT NOT NULL IDENTITY,
	[Code] VARCHAR(50) NULL, 
    [Name] VARCHAR(200) NULL, 
	[ClassificationTypeCode] VARCHAR(50) NOT NULL,
	[Year] VARCHAR(50) NOT NULL,
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_team_classification] PRIMARY KEY ([TeamClassificationId]), 
    CONSTRAINT [AK_mpr_team_classification_code] UNIQUE ([Code],[ClassificationTypeCode]), 
    CONSTRAINT [AK_mpr_team_classification_name] UNIQUE ([Name],[ClassificationTypeCode])
)

GO
