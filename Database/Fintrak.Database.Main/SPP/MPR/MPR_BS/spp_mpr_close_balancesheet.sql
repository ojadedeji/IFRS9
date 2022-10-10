
Create proc spp_mpr_close_balancesheet
(
	@Rundate date
)
as

If Exists (Select top 1 * from mpr_balancesheet where RunDate = @Rundate)
Begin
	delete mpr_balancesheet_archive where RunDate = @Rundate;

	Insert into mpr_balancesheet_archive(Id, AccountNo, AccountName, TeamCode, AccountOfficerCode, CaptionName, BranchCode, ProductCode, Category, CurrencyType, Currency, Balancesheettype, ActualBal, AverageBal, Interest, 
							 EffIntRate, Pool, PoolRate, ContractRate, VolumeGL, InterestGL, EntryStatus, MaxRate, PenalCharge, PenalRate, AcctStatus, CreditRating, CompanyCode, RunDate, Active, Deleted, CreatedBy, CreatedOn, 
							 UpdatedBy, UpdatedOn)
	SELECT        BalancesheetId, AccountNo, AccountName, TeamCode, AccountOfficerCode, CaptionName, BranchCode, ProductCode, Category, CurrencyType, Currency, Balancesheettype, ActualBal, AverageBal, Interest, 
							 EffIntRate, Pool, PoolRate, ContractRate, VolumeGL, InterestGL, EntryStatus, MaxRate, PenalCharge, PenalRate, AcctStatus, CreditRating, CompanyCode, RunDate, Active, Deleted, CreatedBy, CreatedOn, 
							 UpdatedBy, UpdatedOn
	FROM            mpr_balancesheet where RunDate = @Rundate

End