/* List of Triggered Extractions */

CREATE TABLE [dbo].[cor_extractiontrigger]
(
	[ExtractionTriggerID] INT NOT NULL IDENTITY,
	[ExtractionJobId] INT NOT NULL,  
    [ExtractionId] INT NOT NULL, 
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
    CONSTRAINT [PK_cor_extractiontrigger] PRIMARY KEY ([ExtractionTriggerID]), 
    CONSTRAINT [FK_Table_Extraction] FOREIGN KEY ([ExtractionId]) REFERENCES [cor_extraction]([ExtractionId]), 
    CONSTRAINT [FK_cor_extractiontrigger_job] FOREIGN KEY ([ExtractionJobId]) REFERENCES [cor_extractionjob]([ExtractionJobId])
)
