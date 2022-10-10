Create proc [dbo].[spp_mpr_revenue_mgt_tree_apply]
as
IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'mpr_revenue_managementtree') DROP TABLE mpr_revenue_managementtree

SELECT        RevenueId, TransId, transdate, Narrative, b.TeamCode, b.AccountOfficerCode, BranchCode, Indicator, Currency, GLCode, GLAccount, GLDescription, Caption, RelatedAccount, AccountTitle, CustCode, ProductCode, 
                         (Amount_LCY * b.Rate/100) Amount_LCY, Period, a.Year,'Management Tree ' + cast(b.Rate as varchar(20)) + '% share' EntryStatus, RunDate, a.CompanyCode, a.Active, a.Deleted, a.CreatedBy, a.CreatedOn, a.UpdatedBy, a.UpdatedOn
into mpr_revenue_managementtree FROM mpr_revenue a inner join mpr_managementtree b on a.Currency = b.AccountNo
and b.Active = 1 and b.Deleted = 0;


Delete mpr_revenue where RelatedAccount in (Select  b.AccountNo from mpr_managementtree b where b.Active = 1 and b.Deleted = 0);


Insert into mpr_revenue (TransId, transdate, Narrative, TeamCode, AccountOfficerCode, BranchCode, Indicator, Currency, GLCode, GLAccount, GLDescription, Caption, RelatedAccount, AccountTitle, CustCode, ProductCode, 
                         Amount_LCY, Period, Year, EntryStatus, RunDate, CompanyCode, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)

Select TransId, transdate, Narrative, TeamCode, AccountOfficerCode, BranchCode, Indicator, Currency, GLCode, GLAccount, GLDescription, Caption, RelatedAccount, AccountTitle, CustCode, ProductCode, 
                         Amount_LCY, Period, Year, EntryStatus, RunDate, CompanyCode, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn from mpr_revenue_managementtree



