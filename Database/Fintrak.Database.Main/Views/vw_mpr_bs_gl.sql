CREATE view [dbo].[vw_mpr_bs_gl]
as

Select b.ProductCode, b.CaptionName, a.glcode , b.Category Category, b.CurrencyType CurrencyType, b.balancesheettype   
from mpr_bs_gl_mapping a 
inner join vw_mpr_product b on a.ProductCode = b.ProductCode 