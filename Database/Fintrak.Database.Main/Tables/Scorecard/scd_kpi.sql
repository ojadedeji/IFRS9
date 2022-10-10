CREATE TABLE [dbo].[scd_kpi]
(
	[KPIId] INT NOT NULL IDENTITY,
	[Code]  VARCHAR(255)  NULL,
	[Name] varchar(50)  NULL,
	[Description]  VARCHAR(255)  NULL,
	[PeriodType]  INT  NOT NULL,
	[Direction]  INT  NOT NULL,
	[CategoryCode]  VARCHAR(255)  NULL,
	[IsKPICalculated] BIT NULL DEFAULT 0,
	[Formula]  VARCHAR(255)  NULL, 
	[AggregateMethod]  INT  NOT NULL,
	[IsTargetCalculated] BIT NULL DEFAULT 0,
	[ScoreFormula]  VARCHAR(255)  NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_scd_kpi] PRIMARY KEY ([KPIId]) 
)

GO
