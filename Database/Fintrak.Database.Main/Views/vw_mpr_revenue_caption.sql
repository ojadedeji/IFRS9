CREATE view [dbo].[vw_mpr_revenue_caption]
as

Select distinct b.Code CaptionCode, b.Name CaptionName, a.GLCode,e.GLDescription,  d.name Category   
from mpr_gl_mapping a 
inner join mpr_pl_caption b on a.CaptionCode = b.Code 
inner join cor_accounttype d on d.id = b.AccountType
left outer join ifrs_glmapping e on a.GLCode = substring(e.GLCode, 13,11)