CREATE TABLE [dbo].[mpr_costcentre_definition]
(
	[CCDefinitionId] INT NOT NULL IDENTITY,
	[Code] varchar(50) NOT NULL,  
	[Name] Varchar(100) NULL,
	[Position] int null,
	[Year] VARCHAR(50) not null,
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,   
    CONSTRAINT [PK_mpr_costcentre_definition] PRIMARY KEY ([CCDefinitionId]) 
)

GO
