CREATE TABLE [dbo].[bud_officer_detail]
(
	[OfficerDetailId] INT NOT NULL IDENTITY, 
    [DefinitionCode] VARCHAR(50) NOT NULL, 
	[MisCode] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
	[StaffID] VARCHAR(50) NOT NULL, 
	[Email] VARCHAR(250) NULL, 
	[Mobile] VARCHAR(50) NULL, 
	[Year] VARCHAR(20) NOT NULL, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_officer_detail] PRIMARY KEY ([OfficerDetailId]), 
    CONSTRAINT [AK_bud_officer_detail_code] UNIQUE ([MisCode],[DefinitionCode],[Year]), 
    CONSTRAINT [AK_bud_officer_detail_staff] UNIQUE ([StaffID],[Year]) 
)
