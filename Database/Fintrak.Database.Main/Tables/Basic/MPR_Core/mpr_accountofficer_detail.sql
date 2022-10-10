CREATE TABLE [dbo].[mpr_accountofficer_detail]
(
	[AccountofficerDetailId] INT NOT NULL IDENTITY,
	[MisCode] VARCHAR(50) NOT NULL, 
	[StaffID] VARCHAR(50) NULL, 
	[Email] VARCHAR(50) NULL, 
	[Phone] VARCHAR(50) NULL, 
	[Year] VARCHAR(50) NULL, 
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_accountofficer_detail] PRIMARY KEY ([AccountofficerDetailId]),
    CONSTRAINT [AK_mpr_accountofficer_detail_accountofficer] UNIQUE ([MisCode],[Year])
)

GO
