CREATE TABLE [dbo].[bud_pay_classification]
(
	[PayClassificationId] INT NOT NULL IDENTITY, 
    [Code] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
	[Description] VARCHAR(300) NULL, 
	[Year] VARCHAR(50) NOT NULL, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_pay_classification] PRIMARY KEY ([PayClassificationId]), 
    CONSTRAINT [AK_bud_pay_classification_code] UNIQUE ([Code],[Year]), 
    CONSTRAINT [AK_bud_pay_classification_name] UNIQUE ([Name],[Year]) 
)
