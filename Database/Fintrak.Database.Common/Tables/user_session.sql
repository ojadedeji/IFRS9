CREATE TABLE [dbo].[cor_user_session]
(
	[UserSessionId] INT NOT NULL IDENTITY,
	[UserId] VARCHAR(20) NOT NULL,  
    [CompanyCode] VARCHAR(10) NOT NULL,    
	[DatabaseId] int NOT NULL, 
	[CanExpire] bit NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_user_session] PRIMARY KEY ([UserSessionId])
)

GO
