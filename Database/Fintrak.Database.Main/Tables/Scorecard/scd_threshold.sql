CREATE TABLE [dbo].[scd_threshold]
(
	[ThresholdId] INT NOT NULL IDENTITY,
	[KPICode]  VARCHAR(255)  NOT NULL,
	[StaffCode]  VARCHAR(255)  NULL,
	[Minimum]  decimal(18,2) NULL DEFAULT 0,
	[Maximum]  decimal(18,2) NULL DEFAULT 0,
	[Color]  VARCHAR(255)  NULL,
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
    CONSTRAINT [PK_scd_threshold] PRIMARY KEY ([ThresholdId]) 
)

GO
