CREATE TABLE [dbo].[mpr_setup]
(
	[SetupId] INT NOT NULL IDENTITY,
	[ExcoDefinitionCode] VARCHAR(50) NOT NULL,
	[ExcoTeamCode] VARCHAR(50) NOT NULL,
    [AccountLenght] int NOT NULL,
	[Year] VARCHAR(50) NOT NULL, 
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_setup] PRIMARY KEY ([SetupId]) 
)

GO
