CREATE TABLE [dbo].[mpr_opex_checklist]
(
	[ChecklistId] INT NOT NULL IDENTITY,
	[Caption] varchar(150) NOT NULL,  
	[Type] Varchar(100) NULL,
	[Source] [varchar](50) NULL,
	[Actual] decimal(18,6) NULL DEFAULT 0,	
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL DEFAULT 'auto', 
    [CreatedOn] DATETIME NULL DEFAULT getdate(), 
    [UpdatedBy] VARCHAR(50) NULL DEFAULT 'auto', 
    [UpdatedOn] DATETIME NULL DEFAULT getdate(), 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_opex_checklist] PRIMARY KEY ([ChecklistId])   
)

GO
