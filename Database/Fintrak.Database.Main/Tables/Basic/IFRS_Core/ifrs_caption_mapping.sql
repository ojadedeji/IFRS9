/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_captionmapping]
(
	[CaptionMappingId] INT NOT NULL IDENTITY,
	 [CaptionId] INT NOT NULL,
    [ParentId] INT NULL, 
    [CompanyCode] varchar(10) NULL,   
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [FK_ifrs_caption_mapping_maincaption] FOREIGN KEY ([CaptionId]) REFERENCES [cor_chartofacct]([ChartOfAccountId]), 	
    CONSTRAINT [FK_ifrs_caption_mapping_subcaption] FOREIGN KEY ([ParentId]) REFERENCES [ifrs_captionmapping]([CaptionMappingId]), 		
    CONSTRAINT [PK_ifrs_caption_mapping] PRIMARY KEY ([CaptionMappingId])
    
)
