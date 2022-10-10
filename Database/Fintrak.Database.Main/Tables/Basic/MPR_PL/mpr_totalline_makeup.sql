CREATE TABLE [dbo].[mpr_totalline_makeup]
(
	[TotalLineMakeUpId] INT NOT NULL IDENTITY,
	[TotalLine] varchar(200) NOT NULL, 
	[CaptionCode] VARCHAR(50) NOT NULL, 
	[CompanyCode] Varchar(10) NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,   
	CONSTRAINT [FK_mpr_totalline_makeup_company] FOREIGN KEY ([CompanyCode]) REFERENCES [cor_company]([Code]),
    CONSTRAINT [PK_mpr_totalline_makeup] PRIMARY KEY ([TotalLineMakeUpId]) 
)

GO
