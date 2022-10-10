CREATE TABLE [dbo].[cor_currencytype]
(
	[id] INT NOT NULL,
	[name] VARCHAR(20) NOT NULL,
	CONSTRAINT [PK_cor_currencytype] PRIMARY KEY ([id]) 
)


--Insert into cor_currencytype(currencytypeid,currencytype)
--Select 1, 'LCY'
--Union
--Select 2, 'FCY'

