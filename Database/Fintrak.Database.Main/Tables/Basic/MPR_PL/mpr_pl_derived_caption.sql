/*** GL Mapping table ***/

CREATE TABLE [dbo].[mpr_pl_derived_caption]
(
	[PLDerivedCaptionId] INT NOT NULL IDENTITY,
	[CaptionCode] VARCHAR(100) NOT NULL,
    [DependencyCaptionCode] VARCHAR(100) NOT NULL,
    [Factor] FLOAT NOT NULL,
	[Period] INT NOT NULL,
	[Year] varchar(20) NOT NULL,
	[CompanyCode] varchar(10) NULL,   
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 

    CONSTRAINT [PK_mpr_pl_derived_caption] PRIMARY KEY ([PLDerivedCaptionId])
    
)
