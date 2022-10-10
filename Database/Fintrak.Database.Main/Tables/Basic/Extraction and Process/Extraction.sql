CREATE TABLE [dbo].[cor_extraction]
(
	[ExtractionId] INT NOT NULL IDENTITY, 
    [Title] VARCHAR(50) NOT NULL, 
    [PackageName] VARCHAR(50) NOT NULL, 
    [PackagePath] VARCHAR(250) NULL, 
    [ProcedureName] VARCHAR(50) NOT NULL, 
    [ScriptText] VARCHAR(MAX) NOT NULL, 
    [SolutionId] INT NOT NULL, 
	[Position] INT NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,
    CONSTRAINT [PK_cor_extraction] PRIMARY KEY ([ExtractionId]), 
    CONSTRAINT [AK_cor_extraction_Title] UNIQUE ([Title]), 
    CONSTRAINT [AK_cor_extraction_PackageName] UNIQUE ([PackageName]), 
    CONSTRAINT [FK_cor_extraction_Solution] FOREIGN KEY ([SolutionId]) REFERENCES [cor_solution]([SolutionId])
)
