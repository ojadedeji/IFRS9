Create proc [dbo].[spp_mpr_pl_income_report_adjustment_push]
as

/* Push manual Adjustments when final table is refreshed*/

Declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR')


Insert into mpr_pl_income_report(TeamCode, AccountOfficerCode, Narrative, BranchCode, GLCode, Caption, RelatedAccount, Amount, RunDate, CompanyCode,
Period, Year, GLAccount, AccountTitle, CustCode, ProductCode, EntryStatus, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)

SELECT        a.TeamCode, a.AccountOfficerCode, Narrative, a.BranchCode, GLCode, Caption, RelatedAccount, Amount, RunDate, a.CompanyCode,
Month(@Rundate) , Year(@Rundate), GLCode Glaccount, c.AccountName,c.CustNo, c.ProductCode, 'Manual Entry'  entrystatus, a.CreatedBy, a.CreatedOn, a.UpdatedBy, a.UpdatedOn
FROM            dbo.mpr_pl_income_report_adjustment a  left outer join cor_cust_account c on a.RelatedAccount = c.AccountNo where a.RunDate = @Rundate