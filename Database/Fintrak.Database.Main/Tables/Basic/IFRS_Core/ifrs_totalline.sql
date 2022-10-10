/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_totalline]
(
	[TotalLineId] INT NOT NULL IDENTITY,
	[Name] VARCHAR(200) NOT NULL,
    [Position] INT NOT NULL, 
	[ParentId] INT NULL, 
	[CompanyId] INT NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [AK_ifrs_totalline_name] UNIQUE ([Name]), 
    CONSTRAINT [PK_ifrs_totalline] PRIMARY KEY ([TotalLineId]), 
    CONSTRAINT [FK_ifrs_totalline_parent] FOREIGN KEY ([ParentId]) REFERENCES [ifrs_totalline]([totalLineId])
    
)
