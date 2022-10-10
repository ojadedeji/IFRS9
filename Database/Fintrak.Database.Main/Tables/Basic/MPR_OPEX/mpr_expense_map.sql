CREATE TABLE [dbo].[mpr_expense_map]
(
	[ExpenseMappingId] [int] IDENTITY(1,1) NOT NULL,
	[BasisCode] [varchar](50) NOT NULL,
	[ItemCode] [varchar](50) NOT NULL,
	[MISCode] [varchar](50) NULL,
	[Active] [bit] NULL CONSTRAINT [col_active_mpr_expense_map]  DEFAULT ((1)),
	[Deleted] [bit] NULL CONSTRAINT [col_deleted_mpr_expense_map]  DEFAULT ((0)),
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedOn] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CompanyCode] [varchar](10) NULL, 
    CONSTRAINT [PK_mpr_expense_map] PRIMARY KEY ([ExpenseMappingId]),
)

GO
