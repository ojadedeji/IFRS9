CREATE TABLE [dbo].[Mpr_Opex_Item_Dictionary]
(
	[ITEMID] [int] NULL,
	[ITEMDESCRIPTION] [varchar](50) NULL
)

GO


CREATE INDEX [IX_Mpr_Opex_Item_Dictionary_ID] ON [dbo].[Mpr_Opex_Item_Dictionary] ([ITEMID])
