CREATE TABLE [dbo].[mpr_opex_transaction_detail]
(
	[ACCOUNTNUMBER] VARCHAR(200)  NULL,
	[ACCOUNTTITLE] VARCHAR(200)  NULL,
	[DESCRIPTION] Varchar(200)  NULL, 
    [MISCode] VARCHAR(50)  NULL, 
	[CostCode] VARCHAR(50) NULL, 
	[Amount] decimal(18,6) NULL DEFAULT 0, 
	[EXPAMOUNT] decimal(18,6) NULL DEFAULT 0, 
	[BranchCode] varchar(50) NULL,   
    [Period] int NULL, 
    [Year] int NULL, 
	[TRANSDATE] datetime NULL,
	[TYPE] varchar(50) NULL,
	[ACCOUNTOFFICERCODE] varchar(100) NULL
)

GO


CREATE INDEX [IX_mpr_opex_transaction_detail_IDX] ON [dbo].[mpr_opex_transaction_detail] ([Year] desc, [Period] desc)
