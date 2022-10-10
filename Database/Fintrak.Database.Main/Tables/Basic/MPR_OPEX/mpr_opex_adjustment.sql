CREATE TABLE [dbo].[mpr_opex_adjustment]
(
	[Gl_Code] [varchar](100) NULL,
	[Trans_DT] [datetime] NULL,
	[Amount] [money] NULL,
	[Description] [varchar](300) NULL,
	[SBU] [varchar](100) NULL,
	[Orig_Branch] [nvarchar](100) NULL,
	[Currency] [varchar](100) NULL,
	[Period] [int] NULL,
	[Year] [int] NULL,   
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL 
)

GO


CREATE INDEX [IX_mpr_opex_adjustment_IDX1] ON [dbo].[mpr_opex_adjustment] ([Year] desc, [Period] desc,[GL_Code])
