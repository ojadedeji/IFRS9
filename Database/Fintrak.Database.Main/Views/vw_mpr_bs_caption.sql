Create View vw_mpr_bs_caption
as

select distinct a.CaptionCode , a.CaptionName , b.name Category,
c.balancesheettype BSType, d.Name CurrencyType from mpr_bs_caption a inner join cor_accounttype b on a.Category = b.id
inner join cor_balancesheettype c on a.BalanceSheetType = c.balancesheettypeid
inner join cor_currencytype d on a.CurrencyType = d.id