CREATE TABLE [dbo].[mpr_staffcost]
(
	[StaffCostId] INT NOT NULL IDENTITY,
	[EmployeeCode] varchar(50) NOT NULL,  
	[EmployeeName] Varchar(100) NOT NULL,
	[Level] VARCHAR(50) null,
	[Amount] DECIMAL(18, 6) null,
	[BranchCode] VARCHAR(50) null,
	[MISCode] VARCHAR(50) not null,
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,   
    CONSTRAINT [PK_mpr_staffcost] PRIMARY KEY ([StaffCostId])
)

GO
