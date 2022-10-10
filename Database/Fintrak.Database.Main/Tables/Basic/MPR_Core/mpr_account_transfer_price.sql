CREATE TABLE [dbo].[mpr_account_transfer_price]
(
	[accounttransferpriceId] INT NOT NULL IDENTITY,
	[AccountNo]  VARCHAR(50) NOT NULL,
	[Category] int NOT NULL, --Asset = 1, Liability =2
	[Rate]  DECIMAL(18, 6) not NULL,
	[Year] VARCHAR(50) NOT NULL, 
	[Period] VARCHAR(50) NOT NULL, 	
	[SolutionId] INT NOT NULL,   
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_account_transfer_price] PRIMARY KEY ([accounttransferpriceId]) ,
	CONSTRAINT [FK_mpr_account_transfer_price_Solution] FOREIGN KEY ([SolutionId]) REFERENCES [cor_solution]([SolutionId])
)

GO
