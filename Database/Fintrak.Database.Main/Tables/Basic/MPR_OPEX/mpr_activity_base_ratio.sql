CREATE TABLE [dbo].[mpr_activity_base_ratio]
(
	[ActivityBaseRatioId] INT NOT NULL IDENTITY,
	[ServiceClass] varchar(50) NOT NULL,  
	[Ratio] FLOAT  null DEFAULT 0,
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,   
    CONSTRAINT [PK_mpr_activity_base_ratio] PRIMARY KEY ([ActivityBaseRatioId])
)

GO
