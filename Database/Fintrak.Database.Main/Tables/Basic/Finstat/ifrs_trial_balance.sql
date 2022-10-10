/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_trialbalance]
(
	[TrialBalanceId] INT NOT NULL IDENTITY, 
    [BranchCode] varchar(50) NOT NULL, 
    [GLCode] varchar(50) NOT NULL,    
	[Description] VARCHAR(150) NULL, 
	[GLSubHeadCode] VARCHAR(50) NULL, 
	[Currency] VARCHAR(50) NULL, 
	[ExchangeRate] FLOAT NULL, 
	[Debit] DECIMAL(18, 6) NULL, 
	[Credit] DECIMAL(18, 6) NULL, 
	[LCY_Debit] DECIMAL(18, 6) NULL, 
	[LCY_Credit] DECIMAL(18, 6) NULL, 
	[Balance] DECIMAL(18, 6) NULL, 
	[LCY_Balance] DECIMAL(18, 6) NULL, 
	[GLType] VARCHAR(50) NULL,
	[RevaluationDiff] DECIMAL(18, 6) NULL, 
	[TransDate] date NULL,
	[CompanyCode] VARCHAR(50) NULL,
	[SUB_GL] VARCHAR(50) NULL,
	[AdjustmentCode] VARCHAR(50) NULL,
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_trial_balance] PRIMARY KEY ([TrialBalanceId])
    
)
