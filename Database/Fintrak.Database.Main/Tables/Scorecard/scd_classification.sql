CREATE TABLE [dbo].[scd_kpi_classification]
(
	[KPIClassificationId] INT NOT NULL IDENTITY,
	[KPICode]  VARCHAR(255)  NOT NULL,
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
    CONSTRAINT [PK_scd_kpi_classification] PRIMARY KEY ([KPIClassificationId]) 
)

GO
