/* List of Triggered Extractions */

CREATE TABLE [dbo].[cor_processtrigger]
(
	[ProcessTriggerID] INT NOT NULL IDENTITY, 
	[ProcessJobId] INT NOT NULL,
    [ProcessId] INT NOT NULL,
	[Code] VARCHAR(200) NULL,  
    [Status] INT NULL, 
    [Remark] VARCHAR(MAX) NULL, 
    [UserName] VARCHAR(50) NULL, 
    [StartDate] DATE NOT NULL, 
    [EndDate] DATE NOT NULL, 
    [RunTime] DATETIME NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,
    CONSTRAINT [PK_cor_processtrigger] PRIMARY KEY ([ProcessTriggerID]), 
    CONSTRAINT [FK_Table_Process] FOREIGN KEY ([ProcessId]) REFERENCES [cor_process]([ProcessId]), 
    CONSTRAINT [FK_cor_processtrigger_job] FOREIGN KEY ([ProcessJobId]) REFERENCES [cor_processjob]([ProcessJobId])
)
