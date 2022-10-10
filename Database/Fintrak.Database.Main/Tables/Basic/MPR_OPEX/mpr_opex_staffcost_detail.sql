CREATE TABLE [dbo].[mpr_opex_staffcost_detail]
(
	[StaffCostDetId] INT NOT NULL IDENTITY,
	[EmployeeCode] varchar(50) NOT NULL,  
	[EmployeeName] Varchar(100) NOT NULL,
	[Amount] DECIMAL(18, 6) null,
	[MISCode] VARCHAR(50) not null,
	[CompanyCode] varchar(10) NULL,  
	[Period]  INT null,
	[Year]  INT null,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,   
    CONSTRAINT [PK_mpr_staffcost_detail] PRIMARY KEY ([StaffCostDetId])
)

GO


CREATE INDEX [IX_mpr_opex_staffcost_detail_Indx1] ON [dbo].[mpr_opex_staffcost_detail] ([Year] desc, [Period] desc,[MISCode],[EmployeeCode])
