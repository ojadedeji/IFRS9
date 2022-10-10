CREATE TABLE [dbo].[cor_ratetype]
(
	[RateTypeId] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(50) NOT NULL,
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,
    CONSTRAINT [PK_cor_ratetype] PRIMARY KEY ([RateTypeId])
)
