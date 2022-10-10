/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_instrumentType]
(
	[InstrumentTypeId] INT NOT NULL IDENTITY, 
    [Name] varchar(150) NOT NULL, 
    [Instrument] INT NOT NULL,
    [ParentId] INT  NULL, 
	[CompanyCode] varchar(10) NULL,   
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_instrumentType] PRIMARY KEY ([InstrumentTypeId]),  
    CONSTRAINT [FK_ifrs_instrumentType_parent] FOREIGN KEY ([ParentId]) REFERENCES [ifrs_instrumentType]([InstrumentTypeId])
    
)
