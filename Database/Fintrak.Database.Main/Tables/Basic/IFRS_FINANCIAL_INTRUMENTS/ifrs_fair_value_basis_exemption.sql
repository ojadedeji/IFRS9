/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_fair_value_basis_exemption]
(
	[FairValueBasisExemptionId] INT NOT NULL IDENTITY, 
    [RefNo] VARCHAR(50) NOT NULL, 
    [BasisLevel] INT NOT NULL, 
	[InstrumentType] varchar(50) NOT NULL, 
	[CompanyCode] varchar(10) NULL,    
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_fair_value_basis_exemption] PRIMARY KEY ([FairValueBasisExemptionId]) 
    
)
