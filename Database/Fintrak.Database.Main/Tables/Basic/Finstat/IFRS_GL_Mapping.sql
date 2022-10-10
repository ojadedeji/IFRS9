CREATE TABLE [dbo].[ifrs_glmapping]
(
	[GLMappingId] INT NOT NULL IDENTITY,
	[GLCode] VARCHAR(50) NOT NULL,
    [GLDescription]  VARCHAR(200) NULL,  
	[GLParentCode] VARCHAR(50) NULL,
	[CaptionCode] VARCHAR(100) NOT NULL,
	[SubCaption] VARCHAR(100)  NULL,
	[SubCaption1] VARCHAR(100)  NULL,
	[SubCaption2] VARCHAR(100)  NULL,
	[SubCaption3] VARCHAR(100)  NULL,
	[SubCaption4] VARCHAR(100)  NULL,
	[CompanyCode] varchar(10) NOT NULL, 
	[SubPosition] int NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [FK_ifrs_glmapping_captioncode] FOREIGN KEY ([CaptionCode]) REFERENCES [ifrs_registry]([code]), 
    CONSTRAINT [PK_ifrs_glmapping] PRIMARY KEY ([GLCode]) 	    
    
)