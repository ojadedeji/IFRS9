CREATE TABLE [dbo].[mpr_managementtree]
(
	[ManagementTreeId] INT NOT NULL IDENTITY,
	[AccountNo] VARCHAR(50) NOT NULL,
	[TeamDefinitionCode] Varchar(50) NOT NULL, 
    [TeamCode] VARCHAR(50) NOT NULL, 
	[AccountOfficerDefinitionCode] Varchar(50) NULL, 
	[AccountOfficerCode] VARCHAR(50) NULL, 
	[Rate] float NULL, 
	[Year] VARCHAR(50) NULL, 
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_managementtree] PRIMARY KEY ([ManagementTreeId]) 
)

GO
