CREATE TABLE [dbo].[ifrs_bond_periodic_schedule](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RefNo] [varchar](50) NOT NULL,
	[num_pmt] [int] NULL,
	[date_pmt] [date] NOT NULL,
	[amt_prin_init] [money] NULL,
	[amt_int_pay] [money] NULL,
	[amt_prin_pay] [money] NULL,
	[amt_prin_payLessFV] [money] NULL,
	[amt_cashflow] [money] NULL,
	[coupon_cashflow] [money] NULL,
	[amt_prin_end] [money] NULL,
	[Coupon_Rate] [float] NULL,
	[IRR] [float] NULL,
	[CompanyCode] [varchar](50) NULL,
 CONSTRAINT [PK_ifrs_bond_periodic_schedule] PRIMARY KEY CLUSTERED 
(
	[RefNo] ASC,
	[date_pmt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]