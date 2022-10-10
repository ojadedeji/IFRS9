CREATE TABLE [dbo].[ifrs_registry]
(
	[RegistryId] INT NOT NULL IDENTITY,
	[Code] VARCHAR(100) NOT NULL,
	[Caption] VARCHAR(100) NOT NULL,
    [Position] INT NOT NULL, 
    [RefNote] VARCHAR(50) NULL,  
	[FinType]   VARCHAR(20) NULL,
	[FinSubType]   VARCHAR(20) NULL,
	[ParentId] int null,
	[IsTotalLine] BIT NULL, 
	[Color] VARCHAR(50) NULL, 
	[Class] int NULL,
	[CompanyCode] varchar(10) NULL,   
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [FK_ifrs_registry_fintype] FOREIGN KEY ([FinType]) REFERENCES [cor_financial_type]([code]), 	
    CONSTRAINT [FK_ifrs_registry_finsubtype] FOREIGN KEY ([FinSubType]) REFERENCES [cor_financial_type]([code]), 
    CONSTRAINT [PK_ifrs_registry] PRIMARY KEY ([Code]) 
    
)
