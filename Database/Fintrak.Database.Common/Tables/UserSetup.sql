CREATE TABLE [dbo].[cor_usersetup]
(
	[UserSetupId] INT NOT NULL IDENTITY, 
    [LoginID] VARCHAR(200) NOT NULL, 
    [Name] VARCHAR(200) NOT NULL, 
	[Email] VARCHAR(200) NOT NULL, 
	[StaffID] VARCHAR(200) NULL, 
	[MultiCompanyAccess] BIT NULL,
	[LatestConnection] DATETIME NULL,
	[CompanyCode] VARCHAR(10) NOT NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_usersetup] PRIMARY KEY ([UserSetupId]) ,
	CONSTRAINT [CK_cor_usersetup_LoginID] UNIQUE (LoginID), 
    CONSTRAINT [CK_cor_usersetup_Email] UNIQUE (Email) 
)

GO
