CREATE TABLE [dbo].[cor_general]
(
	[GeneralId] INT NOT NULL IDENTITY,
	[Host] VARCHAR(256) NULL,  
    [Email] VARCHAR(250) NULL, 
    [Password] VARCHAR(250) NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_general] PRIMARY KEY ([GeneralId]) 
)

GO
