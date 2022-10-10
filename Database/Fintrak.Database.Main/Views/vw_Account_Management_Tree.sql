Create View vw_mpr_account_management_tree
as

Select a.AccountNo, b.AccountName, sum(Rate) Total_Rate from mpr_managementtree a left outer join cor_cust_account b
on a.AccountNo = b.AccountNo
where  a.Active = 1 and a.Deleted = 0
group by a.AccountNo, b.AccountName