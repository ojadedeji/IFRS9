CREATE TABLE [dbo].[mpr_transfer_price]
(
	[transferpriceId] INT NOT NULL IDENTITY,
	[ProductCode]  VARCHAR(10) NOT NULL,
	[CaptionCode] Varchar(50) NOT NULL,
	[Rate]  float not NULL,
	[Year] Varchar(50) NOT NULL, 
	[Period] Varchar(50) NOT NULL, 
	[DefinitionCode] Varchar(50) NOT NULL,
	[MisCode] Varchar(50) NOT NULL, 
	[CompanyCode] Varchar(50) NULL,
	[SolutionId] INT NOT NULL,   
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_mpr_transfer_price] PRIMARY KEY ([transferpriceId]) ,
	CONSTRAINT [FK_mpr_transfer_price_ProductCode] FOREIGN KEY ([ProductCode]) REFERENCES [cor_product]([Code]) ,
	CONSTRAINT [FK_mpr_transfer_price_Solution] FOREIGN KEY ([SolutionId]) REFERENCES [cor_solution]([SolutionId]) 
)

GO
