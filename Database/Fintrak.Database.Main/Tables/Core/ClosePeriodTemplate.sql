CREATE TABLE [dbo].[cor_closeperiod_template]
(
	[ClosePeriodTemplateId] INT NOT NULL IDENTITY, 
	[SolutionId] INT NOT NULL, 
	[Action] varchar(250) not null,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_closeperiod_template] PRIMARY KEY ([ClosePeriodTemplateId]), 
    CONSTRAINT [FK_cor_closeperiod_template_solution] FOREIGN KEY ([SolutionId]) REFERENCES [cor_solution]([SolutionId])
)