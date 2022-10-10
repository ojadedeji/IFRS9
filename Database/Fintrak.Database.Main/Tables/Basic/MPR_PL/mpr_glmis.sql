CREATE TABLE [dbo].[mpr_glmis]
(
	[GlMisId] INT NOT NULL IDENTITY,
	[GLAccount] varchar(50) NOT NULL,
	[TeamDefinitionCode] Varchar(50) NOT NULL, 
	[AccountOfficerDefinitionCode] Varchar(50) NULL,  
    [TeamCode] VARCHAR(50) NOT NULL, 
	[AccountOfficerCode] VARCHAR(50) NULL,     
	[CompanyCode] Varchar(10) NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,   	
	CONSTRAINT [FK_mpr_glmis_company] FOREIGN KEY ([CompanyCode]) REFERENCES [cor_company]([Code]),
    CONSTRAINT [PK_mpr_glmis] PRIMARY KEY ([GlMisId]) 
)

GO
