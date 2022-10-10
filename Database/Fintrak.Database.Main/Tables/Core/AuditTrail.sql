CREATE TABLE [dbo].[cor_audittrail]
(
	[AuditTrailId] INT NOT NULL IDENTITY, 
    [RevisionStamp] DATETIME NOT NULL, 
    [TableName] VARCHAR(200) NOT NULL, 
	[IPAddress] VARCHAR(200)  NULL, 
	[UserName] VARCHAR(200) NOT NULL, 
	[Actions] TINYINT NOT NULL, -- C - 1, U - 2, D - 3, E - 4
	[OldData] XML NULL, 
	[NewData] XML NULL, 
	[ChangedColumns] VARCHAR(MAX) NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_audittrail] PRIMARY KEY ([AuditTrailId]) 
)

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'-- C - 1, U - 2, D - 3, E - 4',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'cor_audittrail',
    @level2type = N'COLUMN',
    @level2name = N'Actions'