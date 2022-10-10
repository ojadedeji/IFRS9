Create proc spp_mpr_bs_getunmappedproduct
as
--Get Base Curency
declare @BaseCur varchar(10) = ( select c.Symbol from cor_currency c)

IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'mpr_unmapped_product') DROP TABLE mpr_unmapped_product;

--Run distinct Products with transactions into mpr_unmapped_product also considering currency type
Select distinct cast(null as varchar(50)) productcode, cast(null as varchar(150)) description,
case when cast(null as varchar(50)) = @BaseCur then 'LCY'else 'FCY' end CurrencyType into mpr_unmapped_product
--from transaction_table glcode like '%%';


select distinct a.productcode,b.Name, a.CurrencyType from mpr_unmapped_product a inner join cor_product b
on a.productcode = b.Code where a.productcode + a.CurrencyType not in 
(Select c.ProductCode + c.CurrencyType from dbo.vw_mpr_product c)