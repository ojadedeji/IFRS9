CREATE TABLE [dbo].[ifrs_bond_computation_result](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RefNo] [varchar](50) NOT NULL,
	[Day] [int] NOT NULL,
	[PaymentDate] [datetime] NULL,
	[Date] [datetime] NULL,
	[OpeningBalance] [money] NULL,
	[AmountPrincInit] [money] NULL,
	[DailyCoupon] [money] NULL,
	[DailyCouponLessFV] [money] NULL,
	[DailyInt] [money] NULL,
	[DailyPrinc] [money] NULL,
	[DailyPrincLessFV] [money] NULL,
	[AmortizedPremiumDisc] [money] NULL,
	[ClosingBalance] [money] NULL,
	[AmountPrincEnd] [money] NULL,
	[AccruedInterest] [money] NULL,
	[AmortizedCost] [money] NULL,
	[DiscountPremium] [money] NULL,
	[UnAmortized] [money] NULL,
	[Amortized] [money] NULL,
	[CouponRate] [decimal](38, 20) NULL,
	[IRR] [decimal](38, 20) NULL,
	[NoOfPeriods] [int] NULL,
	[PostedDate] [date] NULL,
	[LastRunDate] [date] NULL,
	[CompanyCode] [varchar](50) NULL,
 CONSTRAINT [PK_ifrs_bond_computation_result] PRIMARY KEY CLUSTERED 
(
	[Day] ASC,
	[RefNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]