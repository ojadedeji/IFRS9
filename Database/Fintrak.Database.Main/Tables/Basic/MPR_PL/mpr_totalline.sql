CREATE TABLE [dbo].[mpr_totalline]
(
	[totallineId] INT NOT NULL IDENTITY,
	[Name] varchar(200) NOT NULL, 
	[Position] int NOT NULL, 
	[ParentId] int  NULL,
	[Color] Varchar(50) NULL,
	[CompanyCode] Varchar(10) NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,   
	CONSTRAINT [FK_mpr_totalline_company] FOREIGN KEY ([CompanyCode]) REFERENCES [cor_company]([Code]),
    CONSTRAINT [PK_mpr_totalline] PRIMARY KEY ([totallineId]) 
)

GO
