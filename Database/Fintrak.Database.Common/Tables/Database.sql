CREATE TABLE [dbo].[cor_database]
(
	[DatabaseId] INT NOT NULL IDENTITY,
	[Title] VARCHAR(20) NOT NULL,  
    [DatabaseName] VARCHAR(200) NOT NULL,   
	[ServerName] VARCHAR(200) NOT NULL,   
	[UserName] VARCHAR(200) NULL,  
	[Password] VARCHAR(200) NULL,    
	[IntegratedSecurity] VARCHAR(200) NULL, 
	[CompanyCode] VARCHAR(10) NOT NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_database] PRIMARY KEY ([DatabaseId])
)

GO
