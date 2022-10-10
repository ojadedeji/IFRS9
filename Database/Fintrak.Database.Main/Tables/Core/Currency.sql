CREATE TABLE [dbo].[cor_currency]
(
	[CurrencyId] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Symbol] VARCHAR(3) NOT NULL, 
	[Rate] FLOAT NOT NULL, 
	[IsBase] BIT NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_currency] PRIMARY KEY ([CurrencyId])
)
