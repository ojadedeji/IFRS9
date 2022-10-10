CREATE TABLE [dbo].[mpr_bs_caption]
(
	[CaptionId] INT NOT NULL IDENTITY,
	[CaptionCode] varchar(50) NOT NULL,
	[CaptionName] varchar(200) NOT NULL,
    [Category] int NOT NULL, --Asset = 1, Liability =2
	[CurrencyType] int NOT NULL,--LCY = 1, FCY = 2
	[BalanceSheetType] int NOT NULL,--ON = 1,OFF =2
	[Position] int NOT NULL,
	[PLCaption] varchar(50) NULL,
	[ParentId] int NULL,
	[CompanyCode] Varchar(10) NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
  
	--CONSTRAINT [FK_mpr_bs_caption_parent] FOREIGN KEY ([ParentId]) REFERENCES [mpr_bs_caption]([CaptionId]), 
	CONSTRAINT [FK_mpr_bs_caption_company] FOREIGN KEY ([CompanyCode]) REFERENCES [cor_company]([Code]), 
    CONSTRAINT [AK_mpr_bs_caption_caption] UNIQUE ([CaptionName],[Category],[CurrencyType]), 
    CONSTRAINT [PK_mpr_bs_caption] PRIMARY KEY ([CaptionCode]) 
)

GO
