CREATE TABLE [dbo].[cdqm_country]
(
	[CountryId] INT NOT NULL IDENTITY,
	[Invalid]  VARCHAR(255)  NULL,
	[Valid] varchar(255)  NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cdqm_country] PRIMARY KEY ([CountryId]) 
)

GO
