CREATE TABLE [dbo].[cor_processrole]
(
	[ProcessRoleId] INT NOT NULL IDENTITY, 
    [RoleId] INT NOT NULL, 
    [ProcessId] INT NOT NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,
    CONSTRAINT [PK_cor_processrole] PRIMARY KEY ([ProcessRoleId]), 
    CONSTRAINT [AK_cor_processrole_Role] UNIQUE ([RoleId], [ProcessId]), 
    CONSTRAINT [FK_cor_processrole_Role] FOREIGN KEY ([RoleId]) REFERENCES [cor_role]([RoleId]), 
    CONSTRAINT [FK_cor_processrole_Process] FOREIGN KEY ([ProcessId]) REFERENCES [cor_process]([ProcessId])
)
