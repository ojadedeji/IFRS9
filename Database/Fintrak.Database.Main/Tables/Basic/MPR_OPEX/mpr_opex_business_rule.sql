CREATE TABLE [dbo].[mpr_opex_business_rule]
(
	[OpexBusinessRuleId] INT NOT NULL IDENTITY,
	[Source] VARCHAR(100) NOT NULL,
    [BasisCaption] VARCHAR(100) NOT NULL,
	[Target] VARCHAR(100)  NULL,
	[Description] VARCHAR(100)  NULL,
	[Ratio] float null DEFAULT 0,
	[Template] VARCHAR(100)  NULL,
	[Position] int not null,
	[Total] VARCHAR(MAX) not null,
	[Type] VARCHAR(50)  NULL,
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_opex_business_rule] PRIMARY KEY ([OpexBusinessRuleId])

)

GO
