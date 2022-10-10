/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_watchlisted_loan]
(
	[WatchListedLoanId] INT NOT NULL IDENTITY, 
    [RefNo] VARCHAR(50) NOT NULL, 
	[CompanyCode] varchar(10) NULL,    
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_watchlisted_loan] PRIMARY KEY ([WatchListedLoanId]), 
    CONSTRAINT [AK_ifrs_watchlisted_loan_code] UNIQUE ([RefNo],[CompanyCode]) 
    
)
