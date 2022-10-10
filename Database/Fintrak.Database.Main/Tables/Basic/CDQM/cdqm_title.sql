CREATE TABLE [dbo].[cdqm_title]
(
	[TitleId] INT NOT NULL IDENTITY,
	[Invalid]  VARCHAR(255)  NULL,
	[Valid] varchar(255)  NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cdqm_title] PRIMARY KEY ([TitleId]) 
)

GO
