CREATE TABLE [dbo].[cor_producttypemapping](
	[ProductTypeMappingId] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [varchar](10) NOT NULL,
	[ProductType] [varchar](50) NOT NULL,
	[Active] [bit] NULL DEFAULT 1,
	[Deleted] [bit] NULL DEFAULT 0,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedOn] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL,
    CONSTRAINT [PK_cor_producttypemapping] PRIMARY KEY ([ProductTypeMappingId])
)

GO
