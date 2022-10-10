Create view vw_mpr_productmis
as

SELECT        ProductMISId, ProductCode, d.CaptionName, 
a.TeamCode, a.AccountOfficerCode 
FROM            mpr_productmis A  left outer join mpr_bs_caption d
on a.CaptionCode = d.CaptionCode 