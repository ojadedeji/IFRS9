CREATE TABLE [dbo].[mpr_opex_value_map]
(
	[VALUEID] [int] NULL,
	[VALUEDESCRIPTION] [varchar](50) NULL
)

GO

CREATE INDEX [IX_mpr_opex_value_map_IDX] ON [dbo].[mpr_opex_value_map] ([VALUEID])
