CREATE TABLE [dbo].[cor_staff]
(
	[StaffId] INT NOT NULL IDENTITY,
	[StaffCode] VARCHAR(20) NOT NULL,  
    [Name] VARCHAR(200) NOT NULL, 
    [Email] VARCHAR(250) NULL, 
	[Phone] varchar(10)  NULL,
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_staff] PRIMARY KEY ([StaffId])
)

GO
