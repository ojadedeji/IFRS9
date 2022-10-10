CREATE TABLE [dbo].[ifrs_bond_computation_result_zero](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RefNo] [varchar](50) NULL,
	[Day] [int] NULL,
	[Date] [datetime] NULL,
	[OpeningBalance] [money] NULL,
	[DailyCoupon] [money] NULL,
	[DailyYield] [money] NULL,
	[AmortizedPremiumDisc] [money] NULL,
	[ClosingBalance] [money] NULL,
	[IRR] FLOAT NULL,
	[Period] [int] NULL,
	[Year] [int] NULL,
	[Rundate] [datetime] NULL,
	[CompanyCode] [varchar](50) NULL, 
    CONSTRAINT [PK_ifrs_bond_computation_result_zero] PRIMARY KEY ([ID])
) ON [PRIMARY]