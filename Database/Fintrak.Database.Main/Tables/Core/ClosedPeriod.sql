CREATE TABLE [dbo].[cor_closedperiod]
(
	[ClosedPeriodId] INT NOT NULL IDENTITY, 
	[SolutionId] INT NOT NULL, 
	[Date] date not null,
	[Status] BIT NULL DEFAULT 1, 
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_closedperiod] PRIMARY KEY ([ClosedPeriodId]), 
    CONSTRAINT [FK_cor_closedperiod_solution] FOREIGN KEY ([SolutionId]) REFERENCES [cor_solution]([SolutionId]), 
    CONSTRAINT [CK_cor_closedperiod_date] UNIQUE ([Date],[SolutionId]) 
)