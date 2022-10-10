Create proc spp_mpr_balancesheet_adjustment_push
as

/* Push manual Adjustments when final table is refreshed*/

Declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR')


Insert into mpr_balancesheet(AccountNo, AccountName, TeamCode, AccountOfficerCode, ProductCode, Category, CurrencyType, ActualBal, AverageBal, Interest, RunDate, CaptionName,
VolumeGL , InterestGL, AcctStatus,CreatedBy, CreatedOn, UpdatedBy, UpdatedOn  , EntryStatus, Currency   )


SELECT        a.AccountNo,isnull(c.AccountName, a.AccountName), a.TeamCode, a.AccountOfficerCode, a.ProductCode, a.Category, a.CurrencyType, ActualBal, AverageBal, Interest, RunDate, b.CaptionName,
b.VolumeGL , b.InterestGL, c.Status, a.CreatedBy, a.CreatedOn, a.UpdatedBy, a.UpdatedOn , 'manual entry' EntryStatus, c.Currency
FROM            dbo.mpr_balancesheet_adjustment a inner join dbo.vw_mpr_product b on a.ProductCode = b.ProductCode and a.CurrencyType = b.CurrencyType
and a.Category = b.Category left outer join cor_cust_account c on a.AccountNo = c.AccountNo where a.RunDate = @Rundate