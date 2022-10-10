CREATE TABLE [dbo].[bud_pay_structure]
(
	[PayStructureId] INT NOT NULL IDENTITY, 
    [GradeCode] VARCHAR(50) NOT NULL, 
    [ClassificationCode] VARCHAR(50) NOT NULL, 
	[GrossPay] decimal(18,4) Not  NULL, 
	[ThirthennMonth] decimal(18,4)  NULL, 
	[Year] VARCHAR(50) NOT NULL, 
	[Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_bud_pay_structure] PRIMARY KEY ([PayStructureId]), 
    CONSTRAINT [AK_bud_pay_structure_code] UNIQUE ([GradeCode],[ClassificationCode],[Year]) 
)
