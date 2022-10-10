CREATE TABLE [dbo].[mpr_pl_caption]
(
	[PLCaptionId] INT NOT NULL IDENTITY,
	[Code] varchar(50) NOT NULL,
	[Name] varchar(200) NOT NULL,
    [AccountType] int NOT NULL, --Asset = 1, Liability =2
	[Color] varchar(50) NULL,
	[Position] int NOT NULL,	
	[CompanyCode] Varchar(10) NULL,
	[ParentCode] VARCHAR(50) NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
  
	--CONSTRAINT [FK_mpr_pl_caption_parent] FOREIGN KEY ([ParentId]) REFERENCES [mpr_pl_caption]([PLCaptionId]), 
	CONSTRAINT [FK_mpr_pl_caption_accounttype] FOREIGN KEY ([AccountType]) REFERENCES [cor_accounttype]([id]),
	CONSTRAINT [FK_mpr_pl_caption_company] FOREIGN KEY ([CompanyCode]) REFERENCES [cor_company]([Code]), 
    CONSTRAINT [PK_mpr_pl_caption] PRIMARY KEY ([PLCaptionId])	

)

GO
