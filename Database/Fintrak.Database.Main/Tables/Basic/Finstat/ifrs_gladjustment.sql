/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_gladjustment]
(
	[GLAdjustmentId] INT NOT NULL IDENTITY, 
	[AdjustmentCode] VARCHAR(50) NULL,
    [GLCode] VARCHAR(50) NOT NULL,    
	[Narration] VARCHAR(150) NOT NULL, 
	[Indicator] INT NOT NULL, --Debit - 1, Credit - 2
	[Amount] DECIMAL(18, 6) NOT NULL,
	[CompanyCode] varchar(10) NOT NULL,
	[Currency] VARCHAR(3) NULL, 
	[RunDate] date NOT NULL,
	[AdjustmentType] INT NOT NULL,--GAAP - 1, IFRS - 2
	[Posted] bit NULL,
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_gladjustment] PRIMARY KEY ([GLAdjustmentId]), 
    CONSTRAINT [FK_ifrs_gladjustment_company] FOREIGN KEY ([CompanyCode]) REFERENCES [cor_company]([Code]) 
    
)
