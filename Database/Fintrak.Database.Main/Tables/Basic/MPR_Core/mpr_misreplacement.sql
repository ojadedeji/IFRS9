CREATE TABLE [dbo].[mpr_misreplacement]
(
	[MISReplacementId] INT NOT NULL IDENTITY,
	[OldMISCode] VARCHAR(20) NOT NULL,
    [MISCode] VARCHAR(50) NOT NULL,
	[DefinitionCode] Varchar(50) NOT NULL,  
	[CompanyCode] VARCHAR(10)  NULL,
	[Year] VARCHAR(50) NOT NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_misreplacement] PRIMARY KEY ([MISReplacementId]), 
    CONSTRAINT [AK_mpr_misreplacement_new] UNIQUE ([MISCode],[DefinitionCode],[Year])

)

GO
