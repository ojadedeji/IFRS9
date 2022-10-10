CREATE TABLE [dbo].[cdqm_cust_duplicates](
	[CUST_DUPLICATES_ID] INT NOT NULL IDENTITY,
	[COD_CUST_ID] [numeric](10, 0) NOT NULL,
	[COD_GROUP_ID] [numeric](10, 0) NOT NULL,
	[NAM_GROUP_NAME] [varchar](120) NULL,
	[NAM_CUST_FULL] [varchar](120) NULL,
	[DAT_BIRTH_CUST] [varchar](10) NULL,
	[TXT_CUST_SEX] [varchar](1) NULL,
	[Score] [float] NULL,
	[NotDuplicate] [bit] NULL DEFAULT 0,
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cdqm_cust_duplicates] PRIMARY KEY ([CUST_DUPLICATES_ID]) 
)

GO
