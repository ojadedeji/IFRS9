CREATE TABLE [dbo].[mpr_opex_basis_mapping]
(
	[Id] INT NOT NULL IDENTITY,
	[GLCODE] [varchar](50) NULL,
	[DESCRIPTION] [varchar](50) NULL,
	[CAPTION] [varchar](100) NULL,
	[LINECAPTION] [varchar](50) NULL,
	
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL DEFAULT 'auto', 
    [CreatedOn] DATETIME NULL DEFAULT getdate(), 
    [UpdatedBy] VARCHAR(50) NULL DEFAULT 'auto',
    [UpdatedOn] DATETIME NULL DEFAULT getdate(), 
    [RowVersion] TIMESTAMP NOT NULL,   
    CONSTRAINT [PK_mpr_opex_basis_mapping] PRIMARY KEY ([Id])
)

GO
