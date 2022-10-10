CREATE TABLE [dbo].[cdqm_gender_group]
(
	[GenderGroupId] INT NOT NULL IDENTITY,
	[Title]  VARCHAR(255)  NULL,
	[GroupGender] varchar(255)  NULL,	
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cdqm_gender_group] PRIMARY KEY ([GenderGroupId]) 
)

GO
