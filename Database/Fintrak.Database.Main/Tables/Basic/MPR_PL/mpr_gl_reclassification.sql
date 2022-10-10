CREATE TABLE [dbo].[mpr_gl_reclassification]
(
	[GLReclassificationId] INT NOT NULL IDENTITY,
	[GLAccount] varchar(50) NOT NULL,
	[CaptionCode] varchar(50) NOT NULL,    
	[CompanyCode] Varchar(10) NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,   
	--CONSTRAINT [FK_mpr_gl_reclassification_caption] FOREIGN KEY ([CaptionCode]) REFERENCES [mpr_pl_caption]([Code]),
	--CONSTRAINT [FK_mpr_gl_reclassification_company] FOREIGN KEY ([CompanyCode]) REFERENCES [cor_company]([Code]),
    CONSTRAINT [PK_mpr_gl_reclassification] PRIMARY KEY ([GLReclassificationId]) 
)


GO
