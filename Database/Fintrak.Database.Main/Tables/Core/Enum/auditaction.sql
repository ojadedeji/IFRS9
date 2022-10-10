CREATE TABLE [dbo].[cor_auditaction]
(
	[id] INT NOT NULL,
	[name] VARCHAR(50) NOT NULL,
	[description]  VARCHAR(50) NOT NULL,
	CONSTRAINT [PK_cor_auditaction] PRIMARY KEY ([id]) 
)

--GO
--Insert into cor_balancesheettype(balancesheettypeid,balancesheettype)
--Select 1, 'ON'
--Union
--Select 2, 'OFF'

--GO

