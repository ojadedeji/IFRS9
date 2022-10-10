CREATE TABLE [dbo].[bud_general_setting]
(
	[GeneralSettingId] INT NOT NULL IDENTITY, 
    [Year] VARCHAR(50) NOT NULL, 
	[ReviewCode] VARCHAR(50) NOT NULL, 
    [StartMonth] INT NOT NULL, 
	[EndMonth] INT NOT NULL, 
	[CurrentMonth] INT NOT NULL, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_general_setting] PRIMARY KEY ([GeneralSettingId]), 
    CONSTRAINT [AK_bud_general_setting_year] UNIQUE ([ReviewCode],[Year]) 
)
