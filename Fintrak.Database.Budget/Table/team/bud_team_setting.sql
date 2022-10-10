CREATE TABLE [dbo].[bud_team_setting]
(
	[TeamSettingId] INT NOT NULL IDENTITY, 
    [EnableBudgetToMPRSynch] BIT NOT NULL, 
    [EnableMPRToBudgetSynch] BIT NOT NULL, 
	[Year] VARCHAR(20) NOT NULL, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_team_setting] PRIMARY KEY ([TeamSettingId]), 
    CONSTRAINT [AK_bud_team_setting_year] UNIQUE ([Year]) 
)
