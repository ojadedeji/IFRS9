CREATE TABLE [dbo].[cor_role]
(
	[RoleId] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(200) NOT NULL, 
    [Description] VARCHAR(300) NULL, 
	[SolutionId] INT NOT NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_role] PRIMARY KEY ([RoleId]), 
    CONSTRAINT [FK_cor_role_solution] FOREIGN KEY ([SolutionId]) REFERENCES [cor_solution]([SolutionId]), 
    CONSTRAINT [CK_cor_role_Name] UNIQUE ([Name],[SolutionId]) 
)

GO
