/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_instrumenttypeglmap]
(
	[InstrumentTypeGLMapId] INT NOT NULL IDENTITY, 
    [InstrumentTypeId] INT NOT NULL, 
    [GLTypeId] INT NOT NULL, 
    [GLCode] VARCHAR(50) NOT NULL, 
	[CompanyCode] varchar(10) NOT NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_instrumenttype_gl_map] PRIMARY KEY ([InstrumentTypeGLMapId]), 
    CONSTRAINT [FK_ifrs_instrumenttype_gl_map_InstrumentType] FOREIGN KEY ([InstrumentTypeId]) REFERENCES [ifrs_instrumentType]([InstrumentTypeId]), 
	CONSTRAINT [FK_ifrs_instrumenttype_gl_map_GLType] FOREIGN KEY ([GLTypeId]) REFERENCES [ifrs_gltype]([GLTypeId]), 
    CONSTRAINT [FK_ifrs_instrumenttype_gl_map_Company] FOREIGN KEY ([Companycode]) REFERENCES [cor_company]([Code])


	
)
