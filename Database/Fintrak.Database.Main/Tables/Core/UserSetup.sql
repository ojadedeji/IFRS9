CREATE TABLE [dbo].[cor_usersetup]
(
	[UserSetupId] INT NOT NULL IDENTITY, 
    [LoginID] VARCHAR(200) NOT NULL, 
    [Name] VARCHAR(200) NOT NULL, 
	[Email] VARCHAR(200) NOT NULL, 
	[StaffID] VARCHAR(200) NULL, 
	[Photo] [image] NULL,
	[PhotoUrl] [varchar](256) NULL,
	[IsApplicationUser] [bit] NULL CONSTRAINT [DF_cor_usersetup_IsApplicationUser]  DEFAULT ((0)),
	[IsReportUser] [bit] NULL CONSTRAINT [DF_cor_usersetup_IsReportUser]  DEFAULT ((0)),
	[MultiCompanyAccess] BIT NULL,
	[LatestConnection] DATETIME NULL,
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
