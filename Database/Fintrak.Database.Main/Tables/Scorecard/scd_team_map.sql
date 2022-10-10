CREATE TABLE [dbo].[scd_team_map]
(
	[TeamMapId] INT NOT NULL IDENTITY,
	[Centre]  VARCHAR(255)  NULL,
	[TeamDefinitionCode] varchar(50)  NULL,
	[MISCode]  VARCHAR(255)  NULL,
	[MISName]  VARCHAR(255)  NULL,
	[ParentCode]  VARCHAR(50)  NULL,
	[StaffCode]  VARCHAR(50)  NULL,
	[Grade]  VARCHAR(255)  NULL,
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
    CONSTRAINT [PK_scd_team_map] PRIMARY KEY ([TeamMapId]) 
)

GO
