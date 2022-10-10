Create proc spp_mpr_balancesheet_mgt_tree_apply
as
IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'mpr_balancesheet_managementtree') DROP TABLE mpr_balancesheet_managementtree;

SELECT        BalancesheetId, a.AccountNo, AccountName, b.TeamCode, b.AccountOfficerCode, CaptionName, BranchCode, ProductCode, Category, CurrencyType, Currency,ActualBal * b.Rate/100 ActualBal, AverageBal * b.Rate/100 AverageBal, Interest * b.Rate/100 Interest, EffIntRate, [Pool] * b.Rate/100 Pool, 
                         PoolRate, ContractRate, VolumeGL, InterestGL, 'Management Tree ' + cast(b.Rate as varchar(20)) + '% share' EntryStatus, MaxRate, PenalCharge * b.Rate/100 PenalCharge, PenalRate, AcctStatus, CreditRating, RunDate
						 --, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn, RowVersion
into mpr_balancesheet_managementtree
FROM            mpr_balancesheet AS a inner join mpr_managementtree b on a.AccountNo = b.AccountNo
and b.Active = 1 and b.Deleted = 0;

Delete mpr_balancesheet where AccountNo in (Select  b.AccountNo from mpr_managementtree b where b.Active = 1 and b.Deleted = 0);


Insert into mpr_balancesheet(AccountNo, AccountName, TeamCode, AccountOfficerCode, CaptionName, BranchCode, ProductCode, Category, CurrencyType, Currency, ActualBal, AverageBal, Interest, EffIntRate, Pool, 
                         PoolRate, ContractRate, VolumeGL, InterestGL, EntryStatus, MaxRate, PenalCharge, PenalRate, AcctStatus, CreditRating, RunDate)


SELECT        AccountNo, AccountName, TeamCode, AccountOfficerCode, CaptionName, BranchCode, ProductCode, Category, CurrencyType, Currency, ActualBal, AverageBal, Interest, EffIntRate, Pool, 
                         PoolRate, ContractRate, VolumeGL, InterestGL, EntryStatus, MaxRate, PenalCharge, PenalRate, AcctStatus, CreditRating, RunDate
FROM            mpr_balancesheet_managementtree;