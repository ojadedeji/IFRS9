CREATE TABLE [dbo].[scd_configuration](
	[ConfigurationId] [int] IDENTITY(1,1) NOT NULL,
	[Mode] [int] NULL,
	[PeriodType] [int] NULL,
	[CompanyCode] [varchar](255) NULL,
	[TeamClassificationType] [varchar](255) NULL,
	[Active] [bit] NULL DEFAULT 1,
	[Deleted] [bit] NULL DEFAULT 0,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedOn] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL
    CONSTRAINT [PK_scd_configuration] PRIMARY KEY ([ConfigurationId]) 
)

GO
