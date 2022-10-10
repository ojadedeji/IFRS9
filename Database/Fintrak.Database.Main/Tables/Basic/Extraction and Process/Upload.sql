CREATE TABLE [dbo].[cor_upload]
(
	[UploadId] INT NOT NULL IDENTITY, 
    [Title] VARCHAR(200) NOT NULL, 
    [Code] VARCHAR(200) NULL, 
    [SolutionId] INT NOT NULL, 
	[Action] VARCHAR(200) NULL, 
	[TruncateAction] VARCHAR(200) NULL, 
	[Position] INT NULL, 
	[Template] VARCHAR(200) NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,
    CONSTRAINT [PK_cor_upload] PRIMARY KEY ([UploadId]), 
    CONSTRAINT [AK_cor_upload_Title] UNIQUE ([Title]), 
    CONSTRAINT [FK_cor_upload_Solution] FOREIGN KEY ([SolutionId]) REFERENCES [cor_solution]([SolutionId])
)
