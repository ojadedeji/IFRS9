Create VIEW [dbo].[vw_mpr_opex_sbutype]
AS
select distinct a.code, 'PC' [TYPE] from mpr_team a
Union
select distinct a.code, 'CC' [TYPE] from mpr_costcentre a


