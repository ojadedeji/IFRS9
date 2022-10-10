CREATE TABLE [dbo].[mpr_opex_abc_exemption]
(
	[OpexAbcExemptionId] INT NOT NULL IDENTITY,
	[MisCode] varchar(50) NOT NULL,  
	[CompanyCode] varchar(50) NOT NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,   
    CONSTRAINT [PK_mpr_opex_abc_exemption] PRIMARY KEY ([OpexAbcExemptionId])
)

GO
