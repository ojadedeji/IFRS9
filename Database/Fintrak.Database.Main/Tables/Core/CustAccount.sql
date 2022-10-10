CREATE TABLE [dbo].[cor_cust_account]
(
	[CustAccountId] INT NOT NULL IDENTITY,
	[CustNo] VARCHAR(10) NOT NULL,  
	[AccountNo] VARCHAR(50) NOT NULL, 
    [AccountName] VARCHAR(200)  NULL, 
	[Sector] VARCHAR(200)  NULL,
	[SubSector] VARCHAR(200)  NULL,
	[TeamCode] VARCHAR(200)  NULL,
	[AccountOfficerCode] VARCHAR(200)  NULL,
	[ProductCode] VARCHAR(20)  NULL,
	[BranchCode] VARCHAR(20)  NULL,
	[Currency] VARCHAR(20)  NULL,
	[DateOpened] date  NULL,
	[MaturityDate] date  NULL,
	[Status] VARCHAR(50)  NULL,
	[IsDormant] VARCHAR(50)  NULL,
	[IsJoint] VARCHAR(50)  NULL,
	[AccountType] VARCHAR(50)  NULL,
	[SettlementAcct] VARCHAR(50)  NULL,
	[CompanyCode] VARCHAR(200) NULL, 	
	[TeamCodeTemp] VARCHAR(200)  NULL,
	[AccountOfficerCodeTemp] VARCHAR(200)  NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_cust_account] PRIMARY KEY ([AccountNo]) 
)

GO
