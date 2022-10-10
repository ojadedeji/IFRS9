CREATE TABLE [dbo].[cor_company_security]
(
	[CompanySecurityId] INT NOT NULL IDENTITY,
	[Root] VARCHAR(200) NOT NULL,   
	[Filter] VARCHAR(200) not NULL,  
	[Attributes] VARCHAR(200) not NULL,    
	[Scope] VARCHAR(200) not NULL, 
	[CompanyCode] VARCHAR(10) NOT NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_company_security] PRIMARY KEY ([CompanySecurityId])
)

GO
