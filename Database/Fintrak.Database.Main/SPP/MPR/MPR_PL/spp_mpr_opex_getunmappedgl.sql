Create proc [dbo].[spp_mpr_opex_getunmappedgl]
as

IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'mpr_unmapped_gls') DROP TABLE mpr_unmapped_gls;

--Run distinct Revenue GLs with transactions into mpr_gls
Select distinct cast(null as varchar(50)) glcode, cast(null as varchar(150)) description into mpr_unmapped_gls
--from transaction_table glcode like '%%'


--select distinct a.GLCode,b.description from mpr_unmapped_gls a inner join cor_gl_definition b
--on a.GLCode = b.gl_code where a.GLCode not in 
--(Select c.GLCode from vw_mpr_revenue_caption c)