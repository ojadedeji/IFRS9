CREATE TABLE [dbo].[mpr_activity_base]
(
	[ActivityBaseId] INT NOT NULL IDENTITY,
	[ServiceCode] varchar(50) NOT NULL,  
	[ServiceDescription] Varchar(100) NULL,
	[ServiceCategory] VARCHAR(50) null,
	[Weight] FLOAT  null DEFAULT 0,
	[CompanyCode] varchar(10) NULL,   
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,   
    CONSTRAINT [PK_mpr_activity_base] PRIMARY KEY ([ActivityBaseId])
)

GO
