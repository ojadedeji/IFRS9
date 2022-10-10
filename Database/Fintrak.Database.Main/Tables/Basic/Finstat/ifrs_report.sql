/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_report]
(
	[ReportId] INT NOT NULL IDENTITY, 
    [GLCode] varchar(50) NOT NULL,    
	[BranchCode] VARCHAR(50) NULL, 
	[Amount] DECIMAL(18, 6) NULL,
	[Currency] VARCHAR(50) NULL,
	[CompanyCode] VARCHAR(50) NULL,
	[RunDate] date not NULL,
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_report] PRIMARY KEY ([ReportId])
    
)
