CREATE TABLE [dbo].[bud_operation]
(
	[OperationId] INT NOT NULL IDENTITY, 
    [Code] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
	[Description] VARCHAR(500) NULL, 
	[Status] BIT NOT NULL DEFAULT 0, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_operation] PRIMARY Key([OperationId]), 
    CONSTRAINT [AK_bud_operation_code] UNIQUE ([Code]) ,
	CONSTRAINT [AK_bud_operation_name] UNIQUE ([Name]) 
)
