CREATE TABLE [dbo].[mpr_opex_incomeacctbasis](
	[id] INT NOT NULL IDENTITY,
	[AcccountNumber] [varchar](50) NOT NULL,
	[SBU_Code] [varchar](50) NULL,
	[MIS_Code] [varchar](50) NULL,
	[AccountOfficer_Code] [varchar](50) NULL,
	[CustomerName] [varchar](100) NULL,
	[ProductCode] [varchar](10) NULL,
	[BrokerStaffCode] [varchar](50) NULL,
	[DateOpened] [datetime] NULL,
	[Cust_ID] [varchar](50) NULL,
	[Acct_Status] [varchar](50) NULL,
	[SBU_CodeTemp] [varchar](50) NULL,
	[MIS_CodeTemp] [varchar](50) NULL,
	[AccountOfficer_CodeTemp] [varchar](50) NULL,
	[ClosedDate] [date] NULL, 
	[CompanyCode] varchar(10) NULL
    CONSTRAINT [PK_mpr_opex_incomeacctbasis] PRIMARY KEY ([id])
) ON [PRIMARY]




