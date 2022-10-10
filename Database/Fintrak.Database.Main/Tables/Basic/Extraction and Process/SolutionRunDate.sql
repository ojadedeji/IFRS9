CREATE TABLE [dbo].[cor_solutionrundate]
(
	[SolutionRunDateId] INT NOT NULL IDENTITY, 
    [SolutionId] INT NOT NULL, 
    [RunDate] DATE NOT NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,
    CONSTRAINT [PK_cor_solutionrundate] PRIMARY KEY ([SolutionRunDateId]), 
    CONSTRAINT [FK_cor_solution] FOREIGN KEY ([SolutionId]) REFERENCES [cor_solution]([SolutionId]), 
    CONSTRAINT [AK_cor_solutionrundate_solution] UNIQUE ([SolutionId])
)
