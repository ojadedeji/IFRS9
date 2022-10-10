CREATE TABLE [dbo].[cor_packagesetup]
(
	[PackageSetupId] INT NOT NULL IDENTITY, 
    [ExtractionPath] VARCHAR(250) NULL, 
    [ProcessPath] VARCHAR(250) NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,
    CONSTRAINT [PK_cor_packagesetup] PRIMARY KEY ([PackageSetupId])
)
