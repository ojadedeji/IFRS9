

CREATE TABLE [dbo].[mpr_opex_template_all](
	[MISCODE] [varchar](max) NULL,
	[AMOUNT] [decimal](36, 16) NULL,
	[TEMPLATE] [varchar](max) NULL,
	[SOURCE] [varchar](max) NULL,
	[SN] [int] NULL,
	[TRANSTYPE] [varchar](50) NULL,
	[GLCODE] [varchar](50) NULL,
	[Period] [int] NULL,
	[Year] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[mpr_opex_apr_basis](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[miscode] [varchar](50) NULL,
	[segmentcode] [varchar](50) NULL,
	[accountofficercode] [varchar](50) NULL,
	[accountno] [varchar](50) NULL,
	[totalcount] [int] NULL,
	[totalactualbalance] [money] NULL,
	[actualbalance] [money] NULL
) ON [PRIMARY]


GO

CREATE TABLE [dbo].[mpr_opex_detail_share](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accountnumber] [varchar](50) NULL,
	[accountofficercode] [varchar](50) NULL,
	[teamcode] [varchar](50) NULL,
	[amount] [money] NULL,
	[costcode] [varchar](50) NULL,
	[source] [varchar](50) NULL,
	[period] [int] NULL,
	[year] [varchar](50) NULL,
	[description] [varchar](150) NULL,
	[productcode] [varchar](50) NULL,
	[glcode] [varchar](50) NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[mpr_opex_apr_basis_target](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[miscode] [varchar](50) NULL,
	[segmentcode] [varchar](50) NULL,
	[accountofficercode] [varchar](50) NULL,
	[accountno] [varchar](50) NULL,
	[totalcount] [int] NULL,
	[totalactualbalance] [money] NULL,
	[actualbalance] [money] NULL
) ON [PRIMARY]


GO


CREATE TABLE [dbo].[mpr_opex_team_share](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[miscode] [varchar](50) NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[mpr_activity_based_transaction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[miscode] [varchar](50) NULL,
	[accountofficercode] [varchar](50) NULL,
	[accountnumber] [varchar](50) NULL,
	[accounttitle] [varchar](150) NULL,
	[description] [varchar](150) NULL,
	[postdate] [date] NULL,
	[amount_fcy] [money] NULL,
	[amount_lcy] [money] NULL,
	[indicator] [varchar](50) NULL,
	[currency] [varchar](50) NULL,
	[newmiscode] [varchar](50) NULL,
	[expenseamount] [money] NULL,
	[branchtype] [varchar](50) NULL,
	[branchcode] [varchar](50) NULL
) ON [PRIMARY]


GO


CREATE TABLE [dbo].[mpr_opex_allocated_cost](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[EmpID] [varchar](50) NULL,
	[AccountOfficerCode] [varchar](50) NULL,
	[TeamCode] [varchar](50) NULL,
	[AccountNumber] [varchar](50) NULL,
	[AccountTitle] [varchar](150) NULL,
	[Amount] [money] NULL,
	[Description] [varchar](150) NULL,
	[ProductCode] [varchar](50) NULL,
	[TransDate] [date] NULL,
	[TransType] [varchar](50) NULL,
	[GLCode] [varchar](50) NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[mpr_opex_expense_basis_detail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[caption] [varchar](100) NULL,
	[segmentcode] [varchar](50) NULL,
	[accountofficercode] [varchar](50) NULL,
	[accountnumber] [varchar](50) NULL,
	[customername] [varchar](150) NULL,
	[amount] [money] NULL,
	[productcode] [varchar](50) NULL,
	[description] [varchar](150) NULL,
	[balance] [money] NULL,
	[costcode] [varchar](50) NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[mpr_opex_mis](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[source] [varchar](50) NULL,
	[sn] [int] NULL,
	[template] [varchar](100) NULL,
	[total] [varchar](150) NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[mpr_opex_transaction](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountNumber] [varchar](50) NULL,
	[AccountTitle] [varchar](100) NULL,
	[Description] [varchar](100) NULL,
	[MisCode] [varchar](50) NULL,
	[AccountOfficerCode] [varchar](50) NULL,
	[CostCode] [varchar](50) NULL,
	[Amount] [money] NULL,
	[Period] [int] NULL,
	[Year] [varchar](50) NULL,
	[BranchCode] [varchar](50) NULL,
	[ExpenseAmount] [money] NULL,
	[TransDate] [date] NULL,
	[Type] [varchar](50) NULL
) ON [PRIMARY]


GO
CREATE TABLE [dbo].[mpr_opex_share](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[AccountNumber] [varchar](50) NULL,
	[AccountOfficerCode] [varchar](50) NULL,
	[TeamCode] [varchar](50) NULL,
	[Amount] [money] NULL,
	[CostCode] [varchar](50) NULL,
	[Source] [varchar](50) NULL,
	[Period] [int] NULL,
	[Year] [varchar](50) NULL,
	[Description] [varchar](50) NULL,
	[ProductCode] [varchar](50) NULL,
	[SubCaption] [varchar](50) NULL
) ON [PRIMARY]


GO
/****** Object:  Index [IX_mpr_opex_transaction]    Script Date: 16/09/2015 11:45:34 ******/
CREATE NONCLUSTERED INDEX [IX_mpr_opex_transaction] ON [dbo].[mpr_opex_transaction]
(
	[Year] DESC,
	[Period] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO



/****** Object:  Index [IX_mpr_opex_share]    Script Date: 16/09/2015 11:45:34 ******/
CREATE NONCLUSTERED INDEX [IX_mpr_opex_share] ON [dbo].[mpr_opex_share]
(
	[Year] DESC,
	[Period] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[mpr_opex_expense_basis_detail] ADD  CONSTRAINT [DF_mpr_opex_expense_basis_detail_amount]  DEFAULT ((0)) FOR [amount]
GO

ALTER TABLE [dbo].[mpr_opex_expense_basis_detail] ADD  CONSTRAINT [DF_mpr_opex_expense_basis_detail_balance]  DEFAULT ((0)) FOR [balance]
GO


