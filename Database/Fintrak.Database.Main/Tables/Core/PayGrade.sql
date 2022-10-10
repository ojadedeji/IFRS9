CREATE TABLE [dbo].[cor_paygrade]
(
	[PayGradeId] INT NOT NULL IDENTITY,
	[Code] VARCHAR(20) NOT NULL,  
    [Name] VARCHAR(200) NOT NULL, 
    [GrossPay] DECIMAL NOT NULL, 
	[NetPay] DECIMAL NOT NULL, 
	[ThirteenthMonth] DECIMAL NOT NULL, 
	[Year] VARCHAR(20) NOT NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_paygrade] PRIMARY KEY ([PayGradeId])
)

GO
