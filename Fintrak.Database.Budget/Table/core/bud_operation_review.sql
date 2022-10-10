CREATE TABLE [dbo].[bud_operation_review]
(
	[OperationReviewId] INT NOT NULL IDENTITY, 
    [Code] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
	[Description] VARCHAR(500) NULL, 
	[OperationCode] VARCHAR(50) NOT NULL, 
	[Status] INT NOT NULL DEFAULT 1, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_operation_review] PRIMARY Key([OperationReviewId]), 
    CONSTRAINT [AK_bud_operation_review_code] UNIQUE ([Code],[OperationCode]),
	CONSTRAINT [AK_bud_operation_review_name] UNIQUE ([Name],[OperationCode]) 
)
