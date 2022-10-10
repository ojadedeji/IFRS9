CREATE TABLE [dbo].[cor_gl_definition]
(
	[GLDefinitionid] INT NOT NULL IDENTITY,
	[GL_Code] [varchar](50) NULL,
	[Description] [varchar](200) NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_gl_definition] PRIMARY KEY ([GLDefinitionid]) 
)

GO
