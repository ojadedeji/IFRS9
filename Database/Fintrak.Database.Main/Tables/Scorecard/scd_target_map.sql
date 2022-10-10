CREATE TABLE [dbo].[scd_target_map]
(
	[MapId] INT NOT NULL IDENTITY,
	[KPICode]  VARCHAR(50)  NULL,
	[Formula]  VARCHAR(250)  NULL,
	[Period]  INT  NOT NULL,
	[Year]  VARCHAR(255)  NOT NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_scd_target_map] PRIMARY KEY ([MapId]) 
)

GO
