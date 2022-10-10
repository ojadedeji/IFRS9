CREATE TABLE [dbo].[cor_currencyrate]
(
	[CurrencyRateId] INT NOT NULL IDENTITY,
	[CurrencyId] INT NOT NULL, 
    [RateTypeId] INT NOT NULL, 
	[Rate] FLOAT NOT NULL, 
	[Date] DATE NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_currency_rate] PRIMARY KEY ([CurrencyRateId]), 
    CONSTRAINT [FK_cor_currency_rate_cor_currency] FOREIGN KEY ([Currencyid]) REFERENCES [cor_currency]([Currencyid]), 
    CONSTRAINT [FK_cor_currency_rate_cor_rate_type] FOREIGN KEY ([RateTypeId]) REFERENCES [cor_ratetype]([RateTypeId]) 
)
