Create proc spp_mpr_balancesheet_process_derivation
as

Declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');
Declare @Year int = (Select Year(a.RunDate) from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');
Declare @Period int = (Select Month(a.RunDate) from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');

--Drop temp table
IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'mpr_non_product_final') DROP TABLE mpr_non_product_final;

--Delete derived caption
Delete from mpr_balancesheet where EntryStatus = 'Derived Transactions' ;
--ProductCode in (Select c.NonProductCode from mpr_non_product_map c where c.Active = 1 and c.Deleted = 0);

--Get derived settings
SELECT       distinct  NonProductCode, c.CaptionCode, d.CaptionName, a.ProductCode DepositCode, b.CaptionName Deposits, e.Rate
into mpr_non_product_final
FROM            mpr_non_product_map AS a inner join mpr_bs_caption b on a.CaptionCode = b.CaptionCode inner join
mpr_product c on a.NonProductCode = c.ProductCode inner join mpr_bs_caption d on c.CaptionCode = d.CaptionCode 
inner join mpr_non_product_rate e on a.NonProductCode = e.ProductCode and e.Year = @Year and e.Period = @Period
 where a.Active = 1 and a.Deleted = 0 and b.Active = 1 and b.Deleted = 0 and c.Active = 1 and c.Deleted = 0
 and d.Active = 1 and d.Deleted = 0 and e.Active = 1 and e.Deleted = 0;


 --Push the Derived elements
Insert into mpr_balancesheet (AccountNo, AccountName, TeamCode, AccountOfficerCode, a.CaptionName, BranchCode, a.ProductCode, Category, CurrencyType, Currency, 
ActualBal, AverageBal, Interest, EffIntRate, Pool, 
                         PoolRate, ContractRate, VolumeGL, InterestGL, EntryStatus, MaxRate, PenalCharge, PenalRate, AcctStatus, CreditRating, 
						 RunDate)
SELECT        AccountNo, AccountName, TeamCode, AccountOfficerCode, c.CaptionName, BranchCode, b.NonProductCode ProductCode, c.Category, c.CurrencyType, Currency, 
ActualBal * b.Rate, AverageBal * b.Rate,0 Interest,0 EffIntRate,0 Pool, 
                        0 PoolRate,0 ContractRate,null VolumeGL,null InterestGL,'Derived Transactions' EntryStatus,0 MaxRate,0 PenalCharge,0 PenalRate, AcctStatus,'n/a' CreditRating, 
						 RunDate
FROM            mpr_balancesheet a inner join mpr_non_product_final b on a.ProductCode = b.NonProductCode
and a.CaptionName = b.CaptionName inner join dbo.vw_mpr_product c on b.NonProductCode = c.ProductCode and b.CaptionName = c.CaptionName where a.RunDate = @Rundate;







