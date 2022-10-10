CREATE TABLE [dbo].[mpr_bs_gl_mapping]
(
	
	[BSGLMappingId] INT NOT NULL IDENTITY,
	[GLCode] varchar(50) NOT NULL,
	[ProductCode] varchar(50) NOT NULL,    
	[CompanyCode] Varchar(10) NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,   
	--CONSTRAINT [FK_mpr_gl_mapping_caption] FOREIGN KEY ([CaptionCode]) REFERENCES [mpr_pl_caption]([Code]),
	--CONSTRAINT [FK_mpr_gl_mapping_company] FOREIGN KEY ([CompanyCode]) REFERENCES [cor_company]([Code]),
    CONSTRAINT [PK_mpr_bs_gl_mapping] PRIMARY KEY ([BSGLMappingId]) 
)

GO

CREATE UNIQUE INDEX [IX_mpr_bs_gl_mapping_Column] ON [dbo].[mpr_bs_gl_mapping] ([GLCode])
