CREATE TABLE [dbo].[mpr_opex_raw_expense]
(
	[OpexRawExpenseId] INT NOT NULL IDENTITY,
	[GLCode] VARCHAR(100)  NULL,
    [GLName] VARCHAR(100)  NULL,
	[PostDate] datetime  NULL,
	[Amount] DECIMAL(18, 6)  NULL,
	[Description] VARCHAR(350) null,
	[CheckMisCode] VARCHAR(50)  NULL,
	[MisCode] VARCHAR(50)  null,
	[BranchCode] VARCHAR(50)  null,
	[TranID] VARCHAR(50)  NULL,
	[SubGLCode] VARCHAR(50)  NULL,
	[DR] DECIMAL(18, 6)  null,
	[CR] DECIMAL(18, 6)   null,
	[Narrative] VARCHAR(350)  NULL,
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_opex_raw_expense] PRIMARY KEY ([OpexRawExpenseId])

)

GO
