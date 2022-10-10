﻿CREATE TABLE [dbo].[ifrs_loan_primary_data](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountNo] [varchar](250) NOT NULL,
	[RefNo] [varchar](250) NOT NULL,
	[ProductCategory] [varchar](max) NULL,
	[ProductCode] [varchar](250) NULL,
	[ProductName] [varchar](250) NULL,
	[ProductType] [varchar](250) NULL,
	[Sector] [varchar](50) NULL,
	[BookingDate] [date] NULL,
	[Currency] [varchar](50) NULL,
	[ExchangeRate] [decimal](38, 12) NULL,
	[Amount] [money] NULL,
	[Rate] [decimal](18, 4) NULL,
	[ValueDate] [datetime] NULL,
	[PeriodicRepaymentAmount] [money] NULL,
	[FirstRepaymentdate] [datetime] NULL,
	[MaturityDate] [datetime] NULL,
	[Tenor] [int] NULL,
	[InterestRepayFreq] [int] NULL,
	[PrincipalRepayFreq] [int] NULL,
	[TenorMonth] [int] NULL,
	[LD] [int] NULL,
	[Schedule_Type] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CompanyCode] [varchar](50) NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedOn] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_ifrs_loan_primary_data] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]