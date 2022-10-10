CREATE TABLE [dbo].[mpr_gl_revenue_share]
(
	[GLRevenueShareId] INT NOT NULL IDENTITY,
	[TeamDefinitionCode] VARCHAR(50) NOT NULL, 
    [TeamCode] VARCHAR(50) NOT NULL, 
	[AccountOfficerDefinitionCode] VARCHAR(50) NULL,  
	[AccountOfficerCode] VARCHAR(50) NULL,  
	[Ratio] decimal(18,2) NOT NULL DEFAULT 0,  
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_gl_revenue_share] PRIMARY KEY ([GLRevenueShareId])
)

GO
