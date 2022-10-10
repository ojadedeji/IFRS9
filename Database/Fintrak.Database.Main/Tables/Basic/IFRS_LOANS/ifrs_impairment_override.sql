/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_impairment_override]
(
	[ImpairmentOverrideId] INT NOT NULL IDENTITY, 
    [RefNo] VARCHAR(50) NOT NULL, 
    [AccountNo] VARCHAR(200) NOT NULL, 
	[Classification] varchar(200) NOT NULL, 
	[Reason] varchar(500)  NULL, 
	[CompanyCode] varchar(10) NULL,    
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_impairment_override] PRIMARY KEY ([ImpairmentOverrideId]) 
    
)
