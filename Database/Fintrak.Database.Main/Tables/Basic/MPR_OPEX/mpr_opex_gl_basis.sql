CREATE TABLE [dbo].[mpr_opex_gl_basis]
(
	[Id] INT NOT NULL IDENTITY,
	[CAPTION] [varchar](50) NULL,
	[BRANCHCODE] [varchar](50) NULL,
	[MIS_CODE] [varchar](50) NULL,
	[BASIS] [float] NULL,
	[NARRATION] [varchar](100) NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL DEFAULT 'auto', 
    [CreatedOn] DATETIME NULL DEFAULT getdate(), 
    [UpdatedBy] VARCHAR(50) NULL DEFAULT 'auto',
    [UpdatedOn] DATETIME NULL DEFAULT getdate(), 
    [RowVersion] TIMESTAMP NOT NULL,   
    CONSTRAINT [PK_mpr_mpr_opex_gl_basis] PRIMARY KEY ([Id])
)

GO
