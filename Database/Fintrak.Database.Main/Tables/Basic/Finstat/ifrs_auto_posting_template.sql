/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_autopostingtemplate]
(
	[AutoPostingTemplateId] INT NOT NULL IDENTITY, 
    [Title] varchar(150) NOT NULL, 
    [Action] varchar(150) NOT NULL,  
	[CompanyCode] varchar(10) NULL,   
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_autopostingtemplate] PRIMARY KEY ([AutoPostingTemplateId])
    
)
