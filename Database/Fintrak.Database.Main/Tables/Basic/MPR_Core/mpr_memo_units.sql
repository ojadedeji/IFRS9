CREATE TABLE [dbo].[mpr_memo_units]
(
	[MemoUnitId] INT NOT NULL IDENTITY,
	[Code] VARCHAR(50) NOT NULL,
	[Name] VARCHAR(100) NOT NULL,  
	[CompanyCode] [varchar](250) NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_memo_units] PRIMARY KEY ([MemoUnitId])
)

GO


CREATE INDEX [IX_mpr_memo_units_Code] ON [dbo].[mpr_memo_units] ([Code])
