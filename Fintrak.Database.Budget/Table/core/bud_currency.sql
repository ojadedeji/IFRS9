CREATE TABLE [dbo].[bud_currency]
(
	[CurrencyId] INT NOT NULL IDENTITY, 
    [Code] VARCHAR(10) NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
	[Symbol] VARCHAR(10) NOT NULL,
	[Rate] FLOAT NOT NULL,
	[IsBase] BIT NULL DEFAULT 0,  
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_currency] PRIMARY KEY ([CurrencyId]), 
    CONSTRAINT [AK_bud_currency_code] UNIQUE ([Code]), 
    CONSTRAINT [AK_bud_currency_name] UNIQUE ([Name]) 
)
