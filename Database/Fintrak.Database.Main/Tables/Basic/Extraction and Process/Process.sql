CREATE TABLE [dbo].[cor_process]
(
	[ProcessId] INT NOT NULL IDENTITY, 
    [Title] VARCHAR(50) NOT NULL, 
    [PackageName] VARCHAR(50) NOT NULL, 
    [PackagePath] VARCHAR(250) NULL, 
    [ModuleId] INT NOT NULL, 
	[Position] INT NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,
    CONSTRAINT [PK_cor_process] PRIMARY KEY ([ProcessId]), 
    CONSTRAINT [AK_cor_process_Title] UNIQUE ([Title]), 
    CONSTRAINT [AK_cor_process_Packagename] UNIQUE ([PackageName]), 
    CONSTRAINT [FK_cor_process_Module] FOREIGN KEY ([ModuleId]) REFERENCES [cor_module]([ModuleId])
)
