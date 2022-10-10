CREATE TABLE [dbo].[cor_userrole]
(
	[UserRoleId] INT NOT NULL IDENTITY, 
    [UserSetupId] INT NOT NULL, 
	[RoleId] INT NOT NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_userrole] PRIMARY KEY ([UserRoleId]), 
    CONSTRAINT [FK_cor_userrole_usersetup] FOREIGN KEY ([UserSetupId]) REFERENCES [cor_usersetup]([UserSetupId]), 
    CONSTRAINT [CK_cor_userrole_usersetup_role] UNIQUE (UserSetupId,RoleId), 
    CONSTRAINT [FK_cor_userrole_role] FOREIGN KEY ([RoleId]) REFERENCES [cor_role]([RoleId]) 
)

GO
