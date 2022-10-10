CREATE TABLE [dbo].[ifrs_instrument]
(
	[id] INT NOT NULL,
	[name] VARCHAR(20) NOT NULL,
	[CompanyCode] varchar(10) NULL,   
	CONSTRAINT [PK_ifrs_instrument] PRIMARY KEY ([id]) 
)


--Insert into cor_currencytype(currencytypeid,currencytype)
--Select 1, 'LCY'
--Union
--Select 2, 'FCY'

