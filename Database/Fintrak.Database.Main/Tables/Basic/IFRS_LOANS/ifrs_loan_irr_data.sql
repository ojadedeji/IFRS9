CREATE TABLE [dbo].[ifrs_loan_irr_data](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RefNo] [varchar](50) NOT NULL,
	[IRR] [float] NULL,
	[NominalRate] [float] NULL,
	[FirstPaymentDate] [date] NULL,
	[StartDate] [date] NULL,
	[MaturityDate] [date] NULL,
	[DateCreated] [date] NULL,
	[InitialIRR] [float] NULL,
	[NoOfPeriods] [int] NULL,
	[CompanyCode] [varchar](50) NULL,
	[PLR] [float] NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedOn] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL
 CONSTRAINT [PK_ifrs_loan_irr_data] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


GO

CREATE INDEX [IX_ifrs_loan_irr_data_refno] ON [dbo].[ifrs_loan_irr_data] ([RefNo])
