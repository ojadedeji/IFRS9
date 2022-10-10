CREATE TABLE [dbo].[cor_fiscalyear]
(
	[FiscalYearId] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(200) NOT NULL, 
    [StartDate] DATE NOT NULL, 
	[EndDate] DATE NOT NULL, 
	[Closed] BIT NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_fiscalyear] PRIMARY KEY ([FiscalYearId]), 
    CONSTRAINT [AK_cor_fiscalyear_name] UNIQUE ([Name]) 
)

GO
