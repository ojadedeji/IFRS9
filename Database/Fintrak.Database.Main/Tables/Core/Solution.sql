CREATE TABLE [dbo].[cor_solution]
(
	[SolutionId] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(200) NOT NULL, 
    [Alias] VARCHAR(200) NOT NULL, 
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_solution] PRIMARY KEY ([SolutionId]) ,
	CONSTRAINT [CK_cor_solution_Name] UNIQUE (Name), 
    CONSTRAINT [CK_cor_solution_Alias] UNIQUE (Alias) 
)

GO
