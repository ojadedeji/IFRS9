CREATE TABLE [dbo].[cdqm_customer_mis]
(
	[CustomerMISId] INT NOT NULL IDENTITY,
	[TargetMarketCode]  VARCHAR(255) NOT NULL,
	[TargetMarketName] varchar(255)  NULL,
	[SegmentCode]  VARCHAR(255)  NULL,
	[SegmentName]  VARCHAR(255)  NULL,
	[GroupCode]  VARCHAR(255)  NULL,
	[GroupName]  VARCHAR(255)  NULL,
	[DivisionCode]  VARCHAR(255)  NULL,
	[DivisionName]  VARCHAR(255)  NULL,
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cdqm_customer_mis] PRIMARY KEY ([CustomerMISId]) 
)

GO
