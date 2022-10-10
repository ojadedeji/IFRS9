CREATE TABLE [dbo].[bud_currency_rate]
(
	[CurrencyRateId] INT NOT NULL IDENTITY, 
    [CurrencyCode] VARCHAR(10) NOT NULL, 
    [RateType] INT NOT NULL,--Budget,Forecast 
	[Month1] FLOAT NOT NULL,
	[Month2] FLOAT NOT NULL,
	[Month3] FLOAT NOT NULL,
	[Month4] FLOAT NOT NULL,
	[Month5] FLOAT NOT NULL,
	[Month6] FLOAT NOT NULL,
	[Month7] FLOAT NOT NULL,
	[Month8] FLOAT NOT NULL,
	[Month9] FLOAT NOT NULL,
	[Month10] FLOAT NOT NULL,
	[Month11] FLOAT NOT NULL,
	[Month12] FLOAT NOT NULL,
	[ReviewCode] VARCHAR(50) NOT NULL, 
	[Year] VARCHAR(20) NOT NULL,  
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_currency_rate] PRIMARY KEY ([CurrencyRateId]), 
    CONSTRAINT [AK_bud_currency_rate_code] UNIQUE ([CurrencyCode],[RateType],[ReviewCode],[Year]) 
)
