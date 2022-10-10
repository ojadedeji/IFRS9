/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_gltype]
(
	[GLTypeId] INT NOT NULL IDENTITY, 
    [Name] varchar(150) NOT NULL, 
    [Description] varchar(150) NULL,   
	[CompanyCode] varchar(10) NULL,    
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_gltype] PRIMARY KEY ([GLTypeId])
    
)
