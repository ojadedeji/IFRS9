/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_totalline_makeup]
(
	[TotalLine_MakeUpId] INT NOT NULL IDENTITY,
	[TotalLineId] INT NOT NULL,
    [CaptionId] INT NOT NULL,
	[CompanyCode] varchar(10) NULL,   
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [AK_ifrs_totalline_makeup_caption] UNIQUE ([TotalLineId],[CaptionId]), 
    CONSTRAINT [PK_ifrs_totalline_makeup] PRIMARY KEY ([TotalLine_MakeUpId]), 
    CONSTRAINT [FK_ifrs_totalline_makeup_totalline] FOREIGN KEY ([TotalLineId]) REFERENCES [ifrs_totalline]([TotalLineId]), 
    CONSTRAINT [FK_ifrs_totalline_makeup_caption] FOREIGN KEY ([CaptionId]) REFERENCES [cor_chartofacct]([ChartOfAccountId])
    
)
