/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_credit-risk_rating]
(
	[CreditRiskRatingId] INT NOT NULL IDENTITY, 
    [Code] VARCHAR(50) NOT NULL, 
    [EP] FLOAT NOT NULL,   
	[LGD] FLOAT NOT NULL,
	[PD] FLOAT NOT NULL, 
	[Description] varchar(500) NULL,   
	[CompanyCode] varchar(10) NULL,    
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [AK_ifrs_credit-risk_rating_code] UNIQUE ([Code],[CompanyCode]), 
    CONSTRAINT [PK_ifrs_credit-risk_rating] PRIMARY KEY ([CreditRiskRatingId]) 
    
)
