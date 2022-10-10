CREATE TABLE [dbo].[scd_category]
(
	[CategoryId] INT NOT NULL IDENTITY,
	[Code]  VARCHAR(255)  NULL,
	[Name] varchar(50)  NULL,
	[ParentCode]  VARCHAR(255)  NULL,
	[Period]  INT  NOT NULL,
	[Year]  VARCHAR(255)  NOT NULL,
	[TeamClassificationCode]  VARCHAR(255)  NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_scd_category] PRIMARY KEY ([CategoryId]) 
)

GO
