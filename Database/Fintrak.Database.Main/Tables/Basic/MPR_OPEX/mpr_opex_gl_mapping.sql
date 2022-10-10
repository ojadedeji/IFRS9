CREATE TABLE [dbo].[mpr_opex_gl_mapping]
(
	[GLMappingId] [int] IDENTITY(1,1) NOT NULL,
	[GLCode] [varchar](50) NOT NULL,
	[GLDescription] [varchar](200) NULL,
	[Caption] [varchar](100) NULL,
	[SubCaption] [varchar](100) NULL,
	[SubCaption1] [varchar](100) NULL,
	[SubCaption2] [varchar](100) NULL,
	[SubCaption3] [varchar](100) NULL,
	[SubCaption4] [varchar](100) NULL,
	[CompanyCode] [varchar](10) NOT NULL,
	[SubPosition] [int] NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_opex_gl_mapping] PRIMARY KEY ([GLMappingId])

)

GO
