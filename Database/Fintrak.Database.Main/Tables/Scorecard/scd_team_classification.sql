CREATE TABLE [dbo].[scd_team_classification]
(
	[TeamClassificationId] INT NOT NULL IDENTITY,
	[Code]  VARCHAR(50)  NOT NULL,
	[Name]  VARCHAR(255)  NOT NULL,
	[Period]  INT  NOT NULL,
	[Year]  VARCHAR(255)  NOT NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_scd_classification] PRIMARY KEY ([TeamClassificationId]) 
)

GO
