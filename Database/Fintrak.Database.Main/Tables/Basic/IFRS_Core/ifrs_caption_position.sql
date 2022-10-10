/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_captionposition]
(
	[CaptionPositionId] INT NOT NULL IDENTITY,
	[CaptionId] INT NOT NULL, 
    [Position] INT NOT NULL, 
    [RefNote] VARCHAR(50) NULL, 
    [CompanyCode] varchar(10) NOT NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [FK_ifrs_caption_positiont_Caption] FOREIGN KEY ([CaptionId]) REFERENCES [cor_chartofacct]([ChartOfAccountId]), 	
    CONSTRAINT [FK_ifrs_caption_position_Company] FOREIGN KEY ([Companycode]) REFERENCES [cor_company]([Code]), 
    CONSTRAINT [PK_ifrs_caption_position] PRIMARY KEY ([CaptionPositionId])
    
)
