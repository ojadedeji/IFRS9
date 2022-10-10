CREATE TABLE [dbo].[mpr_opex_mis_replacement]
(
	[OpexMISReplacementId] INT NOT NULL IDENTITY,
	[OldMISCode] VARCHAR(20) NOT NULL,
    [MISCode] VARCHAR(50) NOT NULL,
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_opex_mis_replacement] PRIMARY KEY ([OpexMISReplacementId])

)

GO
