CREATE TABLE [dbo].[cor_module]
(
	[ModuleId] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(200) NOT NULL, 
    [Alias] VARCHAR(200) NOT NULL, 
	[SolutionId] INT NOT NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_module] PRIMARY KEY ([ModuleId]), 
    CONSTRAINT [FK_cor_module_solution] FOREIGN KEY ([SolutionId]) REFERENCES [cor_solution]([SolutionId]), 
    CONSTRAINT [CK_cor_module_Name] UNIQUE (Name), 
    CONSTRAINT [CK_cor_module_Alias] UNIQUE (Alias) 
)

GO
