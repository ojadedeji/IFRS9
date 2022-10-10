/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_loan_setup]
(
	[LoanSetupId] INT NOT NULL IDENTITY, 
    [SignificantLoanMarkUp] DECIMAL(18, 6) NOT NULL, 
    [PastDueRate] FLOAT NOT NULL,   
	[RatingType] INT NOT NULL,--1-Individual,2-Product,3-Sector  
	[EPOption] BIT NULL,  
	[EPDefault] INT NULL, 
	[CompanyCode] varchar(10) NULL,    
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_loan_setup] PRIMARY KEY ([LoanSetupId]), 
    CONSTRAINT [AK_ifrs_loan_setup_company] UNIQUE ([CompanyCode])
    
)
