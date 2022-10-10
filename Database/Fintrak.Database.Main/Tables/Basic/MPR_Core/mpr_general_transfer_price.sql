CREATE TABLE [dbo].[mpr_general_transfer_price](
	[generaltransferpriceid] [int] IDENTITY(1,1) NOT NULL,
	[Category] [int] NOT NULL,
	[CurrencyType] [int] NOT NULL,
	[DefinitionCode] [varchar](50) NOT NULL,
	[MisCode] [varchar](50) NOT NULL,
	[Rate] [float] NOT NULL,
	[Period] int NOT NULL,
	[Year] [varchar](50) NOT NULL,
	[CompanyCode] [varchar](50) NULL,
	[Active] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedOn] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_mpr_general_transfer_price] PRIMARY KEY CLUSTERED 
(
	[generaltransferpriceid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



GO

/****** Object:  Index [IX_mpr_general_transfer_price]    Script Date: 30/09/2015 09:59:33 ******/
CREATE NONCLUSTERED INDEX [IX_mpr_general_transfer_price] ON [dbo].[mpr_general_transfer_price]
(
	[Year] DESC,
	[Period] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


