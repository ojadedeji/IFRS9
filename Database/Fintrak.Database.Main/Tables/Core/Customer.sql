CREATE TABLE [dbo].[cor_cust]
(
	[CustId] INT NOT NULL IDENTITY,
	[CustNo] VARCHAR(10) NOT NULL,  
    [CustName] VARCHAR(200) NOT NULL, 
    [CustType] VARCHAR(200) NULL, 
	[CreditRating] VARCHAR(200) NULL, 
	[Country] VARCHAR(200) NULL,
	[Gender] VARCHAR(10) NULL,
	[CustCategory] VARCHAR(100) NULL,
	[CompanyCode] VARCHAR(200) NULL, 	
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_cust] PRIMARY KEY ([CustNo])  
)

GO
