/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_postingdetail]
(
	[PostingDetailId] INT NOT NULL IDENTITY, 
    [GLCode] varchar(50) NOT NULL,    
	[TransDescription] VARCHAR(500) NULL, 
	[Indicator] VARCHAR(10) NULL, 
	[GAAPAmount] DECIMAL(18, 6) NULL,
	[IFRSAmount] DECIMAL(18, 6) NULL,
	[CompanyCode] VARCHAR(50) NULL,
	[TransactionId] VARCHAR(50) NULL,
	[RunDate] date not NULL,
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_posting_detail] PRIMARY KEY ([PostingDetailId])
    
)
