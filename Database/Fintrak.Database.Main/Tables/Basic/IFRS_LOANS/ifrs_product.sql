/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_product]
(
	[ProductId] INT NOT NULL IDENTITY, 
    [ProductCode] VARCHAR(50) NOT NULL, 
    [ScheduleTypeCode] VARCHAR(50) NOT NULL,   
	[MarketRate] FLOAT NOT NULL,
	[PastDueRate] FLOAT NOT NULL, 
	[CompanyCode] varchar(10) NULL,    
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [AK_ifrs_product_code] UNIQUE ([ProductCode]), 
    CONSTRAINT [PK_ifrs_product] PRIMARY KEY ([ProductId]) 
    
)
