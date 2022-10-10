CREATE TABLE [dbo].[cor_menu]
(
	[MenuId] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(200) NOT NULL, 
    [Alias] VARCHAR(200) NULL, 
	[Action] VARCHAR(200) NULL, 
	[ActionUrl] VARCHAR(200) NULL, 
	[Image] IMAGE NULL, 
	[ImageUrl] VARCHAR(200) NULL, 
	[ParentId] INT NULL, 
	[ModuleId] INT NOT NULL, 
	[Position] INT NOT NULL, 
    [Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_cor_menu] PRIMARY KEY ([MenuId]), 
    CONSTRAINT [FK_cor_menu_module] FOREIGN KEY ([ModuleId]) REFERENCES [cor_module]([ModuleId]), 
    CONSTRAINT [CK_cor_menu_Name] UNIQUE ([Name],[ModuleId]), 
    CONSTRAINT [FK_cor_menu_parent] FOREIGN KEY ([ParentId]) REFERENCES [cor_menu]([MenuId]), 
    CONSTRAINT [AK_cor_menu_Alias] UNIQUE ([Alias],[ModuleId]) 
)

GO
