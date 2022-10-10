CREATE TABLE [dbo].[ifrs_adjustmenttype]
(
	[id] INT NOT NULL,
	[name] VARCHAR(50) NOT NULL,
	[CompanyCode] varchar(10) NULL,   
	CONSTRAINT [PK_ifrs_adjustmenttype] PRIMARY KEY ([id]) 
)

--GO
--Insert into cor_balancesheettype(balancesheettypeid,balancesheettype)
--Select 1, 'ON'
--Union
--Select 2, 'OFF'

--GO

