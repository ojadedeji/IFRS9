Create view [dbo].[vw_mpr_product]
as

Select b.CaptionName, a.ProductCode , d.name Category, c.name CurrencyType, e.balancesheettype, a.VolumeGL, a.InterestGL   
from mpr_product a 
inner join mpr_bs_caption b on a.CaptionCode = b.CaptionCode inner join cor_currencytype c on c.id = b.CurrencyType
inner join cor_accounttype d on d.id = b.Category inner join cor_balancesheettype e on e.balancesheettypeid = 
b.BalanceSheetType
