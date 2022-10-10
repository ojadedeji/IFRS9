CREATE TABLE [dbo].[scd_actual]
(
	[ActualId] INT NOT NULL IDENTITY,
	[MisCode]  VARCHAR(50)  NULL,
	[Caption]  VARCHAR(255)  NULL,
	[Amount] DECIMAL(18, 4)  NULL DEFAULT 0,
	[Date]  DATE  NULL,
	[Period]  INT  NOT NULL,
	[Year]  VARCHAR(255)  NOT NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_scd_actual] PRIMARY KEY ([ActualId]) 
)

GO
