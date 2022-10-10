CREATE TABLE [dbo].[cor_balancesheettype]
(
	[balancesheettypeid] INT NOT NULL,
	[balancesheettype] VARCHAR(20) NOT NULL,
	CONSTRAINT [PK_cor_balancesheettype] PRIMARY KEY ([balancesheettypeid]) 
)

--GO
--Insert into cor_balancesheettype(balancesheettypeid,balancesheettype)
--Select 1, 'ON'
--Union
--Select 2, 'OFF'

--GO

