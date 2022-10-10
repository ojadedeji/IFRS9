CREATE TABLE [dbo].[cor_menurole]
(
	[MenuRoleId] INT NOT NULL IDENTITY, 
    [MenuId] INT NOT NULL, 
	[RoleId] INT NOT NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_menurole] PRIMARY KEY ([MenuRoleId]), 
    CONSTRAINT [FK_cor_menurole_menusetup] FOREIGN KEY ([MenuId]) REFERENCES [cor_menu]([MenuId]), 
    CONSTRAINT [CK_cor_menurole_menusetup_role] UNIQUE (MenuId,RoleId), 
    CONSTRAINT [FK_cor_menurole_role] FOREIGN KEY ([RoleId]) REFERENCES [cor_role]([RoleId]) 
)

GO
