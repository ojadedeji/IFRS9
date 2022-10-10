CREATE TABLE [dbo].[mpr_expense_raw_basis_temp]
(
	[BasisCode] [varchar](50) NOT NULL,
	[MISCode] [varchar](50) NULL,
	[weight] [float] NULL
)

GO


CREATE INDEX [IX_mpr_expense_raw_basis_temp_MIS] ON [dbo].[mpr_expense_raw_basis_temp] ([MISCode], [BasisCode])
