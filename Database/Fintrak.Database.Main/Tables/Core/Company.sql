CREATE TABLE [dbo].[cor_company]
(
	[CompanyId] INT NOT NULL IDENTITY,
	[Code] VARCHAR(10) NOT NULL,  
    [Name] VARCHAR(200) NOT NULL, 
    [Email] VARCHAR(250) NOT NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_company] PRIMARY KEY ([Code]) ,
	CONSTRAINT [CK_cor_company_Name] UNIQUE (Name), 
    CONSTRAINT [CK_cor_company_Email] UNIQUE (Email) 
)

GO
