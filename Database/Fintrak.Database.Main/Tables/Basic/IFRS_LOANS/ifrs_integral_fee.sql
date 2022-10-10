CREATE TABLE [dbo].[ifrs_integral_fee](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountNo] [varchar](max) NOT NULL,
	[RefNo] [varchar](250) NOT NULL,
	[Date] [date] NULL,
	[FeeAmount] [money] NULL,
	[Description] [varchar](250) NULL,
	[FeeCircle] [int] NULL,
	[ExpiredPeriod] [int] NULL,
	[UnExpiredPeriod] [int] NULL,
	[CompanyCode] [varchar](50) NULL,
	[Period] [int] NULL,
	[Year] [int] NULL,
	[RunDate] [date] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedOn] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL
 CONSTRAINT [PK_ifrs_integral_fee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


GO

CREATE INDEX [IX_ifrs_integral_fee_Column] ON [dbo].[ifrs_integral_fee] ([AccountNo])
