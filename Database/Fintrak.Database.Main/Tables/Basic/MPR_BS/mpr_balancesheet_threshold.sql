CREATE TABLE [dbo].[mpr_balancesheet_threshold]
(
	[BalancesheetThresholdId] INT NOT NULL IDENTITY,
	[ProductCode]  VARCHAR(10) NOT NULL,
	[CaptionCode] VARCHAR(50) NOT NULL,
	[Rate]  FLOAT  NULL,   
	[CompanyCode] varchar(10) NULL,    
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_balancesheet_threshold] PRIMARY KEY ([BalancesheetThresholdId]) ,
	CONSTRAINT [FK_balancesheet_threshold_ProductCode] FOREIGN KEY ([ProductCode]) REFERENCES [cor_product]([Code]) ,
    CONSTRAINT [AK_mpr_balancesheet_threshold_product] UNIQUE ([ProductCode],[CaptionCode]) 
)

GO
