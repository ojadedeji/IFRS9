CREATE TABLE [dbo].[cor_branch]
(
	[BranchId] INT NOT NULL IDENTITY,
	[Code] VARCHAR(20) NOT NULL,  
    [Name] VARCHAR(200) NOT NULL, 
    [Address] VARCHAR(250) NULL, 
	[CompanyCode] varchar(10) NOT NULL, 
	[Email] VARCHAR(250) NULL,
	[Contact] VARCHAR(250) NULL,
	[Phone] VARCHAR(250) NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_branch] PRIMARY KEY ([Code]), 
    CONSTRAINT [FK_cor_branch_company] FOREIGN KEY ([CompanyCode]) REFERENCES [cor_company]([Code])
)

GO
