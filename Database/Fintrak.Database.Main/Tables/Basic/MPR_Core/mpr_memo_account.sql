CREATE TABLE [dbo].[mpr_memo_account_map]
(
	[MemoAccountMapId] INT NOT NULL IDENTITY,
	[AccountNo] VARCHAR(50) NOT NULL,
	[Code] VARCHAR(50) NOT NULL,  
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_memo_account] PRIMARY KEY ([MemoAccountMapId])
)

GO
