CREATE TABLE [dbo].[scd_entry]
(
	[EntryId] INT NOT NULL IDENTITY,
	[StaffCode]  VARCHAR(255)  NULL,
	[MISCode] varchar(50)  NULL,
	[KPICode]  VARCHAR(50)  NULL,
	[Actual]  decimal(18,4) NULL DEFAULT 0,
	[Target]  decimal(18,4) NULL DEFAULT 0,
	[Score]  decimal(18,4) NULL DEFAULT 0,
	[Date]  DATE  NULL, 
	[Period]  INT  NOT NULL,
	[Year]  VARCHAR(50) not NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_scd_entry] PRIMARY KEY ([EntryId]) 
)

GO
