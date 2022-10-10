CREATE TABLE [dbo].[cdqm_address]
(
	[AddressId] INT NOT NULL IDENTITY,
	[StreetName]  VARCHAR(255)  NULL,
	[City] varchar(255)  NULL,
	[PostalCode]  VARCHAR(255)  NULL,
	[LGA]  VARCHAR(255)  NULL,
	[State] VARCHAR(255)  NULL, 
    [Active] BIT NULL DEFAULT 1, 
    [Deleted] BIT NULL DEFAULT 0, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cdqm_address] PRIMARY KEY ([AddressId]) 
)

GO
