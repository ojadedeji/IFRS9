CREATE TABLE [dbo].[cdqm_etl_configuration]
(
	[ETLConfigurationId] INT NOT NULL IDENTITY,
	[ConfigurationFilter]  VARCHAR(255)  NULL,
	[ConfigurationValue] varchar(255)  NULL,
	[PackagePath] varchar(255)  NULL,
	[ConfiguredValueType] varchar(255)  NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cdqm_etl_configuration] PRIMARY KEY ([ETLConfigurationId]) 
)

GO
