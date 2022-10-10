/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_derivedcaption]
(
	[DerivedCaptionId] INT NOT NULL IDENTITY,
	[Caption] VARCHAR(100) NOT NULL,
    [DependencyCaption] VARCHAR(100) NOT NULL,
    [Factor] FLOAT NOT NULL, 
	[CompanyCode] varchar(10) NULL,   
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 

    CONSTRAINT [PK_ifrs_derived_caption] PRIMARY KEY ([DerivedCaptionId])
    
)
