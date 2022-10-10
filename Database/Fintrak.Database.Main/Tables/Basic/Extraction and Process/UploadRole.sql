CREATE TABLE [dbo].[cor_uploadrole]
(
	[UploadRoleId] INT NOT NULL IDENTITY, 
    [RoleId] INT NOT NULL, 
    [UploadId] INT NOT NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,
    CONSTRAINT [PK_cor_uploadrole] PRIMARY KEY ([UploadRoleId]), 
    CONSTRAINT [FK_cor_uploadrole_Role] FOREIGN KEY ([RoleId]) REFERENCES [cor_role]([RoleId]), 
    CONSTRAINT [FK_cor_uploadrole_Upload] FOREIGN KEY ([UploadId]) REFERENCES [cor_upload]([UploadId]), 
    CONSTRAINT [AK_cor_uploadrole_Role] UNIQUE ([RoleId],  [UploadId])
)
