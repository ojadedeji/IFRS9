CREATE TABLE [dbo].[ifrs_loan_periodic_schedule](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RefNo] [varchar](50) NOT NULL,
	[num_pmt] [int] NULL,
	[date_pmt] [date] NOT NULL,
	[amt_prin_init] [money] NULL,
	[amt_pmt] [money] NULL,
	[amt_int_pay] [money] NULL,
	[amt_prin_pay] [money] NULL,
	[amt_prin_end] [money] NULL,
	[Rate] [float] NULL,
	[AMRefNo] [varchar](50) NULL,
	[AMnum_pmt] [int] NULL,
	[AMdate_pmt] [date] NULL,
	[AMamt_prin_init] [money] NULL,
	[AMamt_pmt] [money] NULL,
	[AMamt_int_pay] [money] NULL,
	[AMamt_prin_pay] [money] NULL,
	[AMamt_prin_end] [money] NULL,
	[IRR] [float] NULL,
	[CompanyCode] [varchar](50) NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedOn] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_ifrs_loan_periodic_schedule] PRIMARY KEY CLUSTERED 
(
	[RefNo] ASC,
	[date_pmt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]