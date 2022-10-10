CREATE TABLE [dbo].[cor_extractionrole]
(
	[ExtractionRoleId] INT NOT NULL IDENTITY, 
    [RoleId] INT NOT NULL, 
    [ExtractionId] INT NOT NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,
    CONSTRAINT [PK_cor_extractionrole] PRIMARY KEY ([ExtractionRoleId]), 
    CONSTRAINT [FK_cor_extractionrole_Role] FOREIGN KEY ([RoleId]) REFERENCES [cor_role]([RoleId]), 
    CONSTRAINT [FK_cor_extractionrole_Extraction] FOREIGN KEY ([ExtractionId]) REFERENCES [cor_extraction]([ExtractionId]), 
    CONSTRAINT [AK_cor_extractionrole_Role] UNIQUE ([RoleId],  [ExtractionId])
)
